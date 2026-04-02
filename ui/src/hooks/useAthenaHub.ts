import { useEffect, useMemo, useRef, useState, useCallback } from 'react';
import type {
  GameFrame,
  FiredEvent,
  FiredImpactEvent,
  KilledEvent,
  WorldInfo,
  Road,
  ForestsData,
  MapLocation,
  MapStructure,
  ElevationsData,
  ServerSettings,
  ExportStatus,
  Group,
  Unit,
  Vehicle,
  Mission,
  GameTime,
} from '../types/game';
import { API_BASE } from '../apiBase';

const BRIDGE_WS_URL = import.meta.env.VITE_RELAY_BRIDGE_WS || `ws://${window.location.hostname}:3000`;

interface RelayState {
  connected?: boolean;
  map?: string;
  missionName?: string;
  profile?: string;
  date?: string;
  time?: string;
  units?: Array<Record<string, unknown>>;
  groups?: Array<Record<string, unknown>>;
  vehicles?: Array<Record<string, unknown>>;
}

interface CacheModeState {
  mode: 'fresh' | 'stable';
  worldOverride: string;
  activeWorld: string;
}

function asNumber(value: unknown, fallback = 0): number {
  const n = Number(value);
  return Number.isFinite(n) ? n : fallback;
}

function asString(value: unknown, fallback = ''): string {
  return typeof value === 'string' ? value : fallback;
}

function mapWorldSize(worldName: string): number {
  const key = worldName.trim().toLowerCase();
  switch (key) {
    case 'altis': return 30720;
    case 'tanoa': return 15360;
    case 'malden': return 12800;
    case 'enoch': return 12800;
    default: return 10240;
  }
}

function parseGameTime(dateText?: string, timeText?: string): GameTime | null {
  if (!timeText) return null;
  const now = new Date();
  const hm = timeText.match(/^(\d{1,2}):(\d{1,2})/);
  if (!hm) {
    return {
      year: now.getFullYear(),
      month: now.getMonth() + 1,
      day: now.getDate(),
      hour: 0,
      minute: 0,
    };
  }
  const maybeDate = dateText ? new Date(dateText) : now;
  const y = Number.isFinite(maybeDate.getTime()) ? maybeDate.getFullYear() : now.getFullYear();
  const m = Number.isFinite(maybeDate.getTime()) ? maybeDate.getMonth() + 1 : now.getMonth() + 1;
  const d = Number.isFinite(maybeDate.getTime()) ? maybeDate.getDate() : now.getDate();
  return {
    year: y,
    month: m,
    day: d,
    hour: Number(hm[1]),
    minute: Number(hm[2]),
  };
}

function mapRelayStateToFrame(state: RelayState): { frame: GameFrame; worldInfo: WorldInfo | null } {
  const mapName = asString(state.map);
  const worldSize = mapWorldSize(mapName);

  const worldInfo: WorldInfo | null = mapName
    ? {
      nameDisplay: mapName,
      nameWorld: mapName,
      author: '',
      size: worldSize,
      forestMin: 0,
      offsetX: 0,
      offsetY: worldSize,
      centerX: worldSize / 2,
      centerY: worldSize / 2,
    }
    : null;

  const mission: Mission | null = mapName
    ? {
      name: asString(state.missionName, 'Mission'),
      author: '',
      world: mapName,
      description: '',
      isMulti: true,
      player: asString(state.profile),
      steamId: '',
    }
    : null;

  const groups: Record<string, Group> = {};
  for (const g of state.groups || []) {
    const id = asString(g.groupnetid || g.groupNetId || g.id);
    if (!id) continue;
    groups[id] = {
      id,
      leaderId: asString(g.leadernetid || g.leaderNetID),
      name: asString(g.name, id),
      wpX: asNumber(g.wpx),
      wpY: asNumber(g.wpy),
      wpType: '',
    };
  }

  const vehicles: Record<string, Vehicle> = {};
  for (const v of state.vehicles || []) {
    const id = asString(v.netid || v.netId || v.id);
    if (!id) continue;
    vehicles[id] = {
      id,
      class: asString(v.class || v.type),
      crew: [],
      posX: asNumber(v.posx || v.posX),
      posY: asNumber(v.posy || v.posY),
      posZ: asNumber(v.posz || v.posZ),
      dir: asNumber(v.dir || v.direction),
      speed: asNumber(v.speed),
    };
  }

  const units: Record<string, Unit> = {};
  for (const u of state.units || []) {
    const id = asString(u.netid || u.netId || u.id);
    if (!id) continue;
    units[id] = {
      id,
      groupId: asString(u.groupnetid || u.groupNetId),
      leaderId: asString(u.leadernetid || u.leaderNetID),
      vehicleId: asString(u.vehiclenetid || u.vehicleNetID),
      playerName: asString(u.player),
      sessionId: asString(u.sessionid || u.sessionId),
      steamId: asString(u.steamid || u.steamId),
      name: asString(u.name || u.displayName, id),
      faction: asString(u.faction),
      side: asString(u.side, 'west').toLowerCase(),
      team: '',
      type: asString(u.type),
      rank: asString(u.rank),
      hasMediKit: String(u.hasMedikit || '').toLowerCase() === 'true',
      weaponPrimary: asString(u.weapon1),
      weaponSecondary: asString(u.weapon2),
      weaponHandgun: '',
      posX: asNumber(u.posx || u.posX),
      posY: asNumber(u.posy || u.posY),
      posZ: asNumber(u.posz || u.posZ),
      dir: asNumber(u.dir || u.direction),
      speed: asNumber(u.speed),
      isActivePlayer: false,
    };
  }

  return {
    worldInfo,
    frame: {
      mission,
      world: worldInfo,
      time: parseGameTime(asString(state.date), asString(state.time)),
      groups,
      units,
      vehicles,
      lazes: [],
      fired: [],
      killed: [],
    },
  };
}

export function useAthenHub() {
  const wsRef = useRef<WebSocket | null>(null);
  const [connected, setConnected] = useState(false);
  const [frame, setFrame] = useState<GameFrame | null>(null);
  const [worldInfo, setWorldInfo] = useState<WorldInfo | null>(null);
  const [roads, setRoads] = useState<Road[]>([]);
  const [forests, setForests] = useState<ForestsData | null>(null);
  const [locations, setLocations] = useState<MapLocation[]>([]);
  const [structures, setStructures] = useState<MapStructure[]>([]);
  const [elevations, setElevations] = useState<ElevationsData | null>(null);
  const [recentKills] = useState<KilledEvent[]>([]);
  const [recentFired] = useState<FiredEvent[]>([]);
  const [recentFiredImpacts] = useState<FiredImpactEvent[]>([]);
  const [serverSettings] = useState<ServerSettings>({ showEast: true, showGuer: true, showCiv: true });
  const [exportStatus, setExportStatus] = useState<ExportStatus>({
    phase: 'idle',
    roadCount: 0,
    roadsComplete: false,
    treeCount: 0,
    treesComplete: false,
    forestCount: 0,
    forestsComplete: false,
    locationCount: 0,
    locationsComplete: false,
    structureCount: 0,
    structuresComplete: false,
    elevationCount: 0,
    elevationsComplete: false,
  });
  const [cacheMode, setCacheMode] = useState<CacheModeState>({
    mode: 'fresh',
    worldOverride: '',
    activeWorld: '',
  });

  const fetchCacheMode = useCallback(async () => {
    try {
      const res = await fetch(`${API_BASE}/api/game/cachemode`);
      if (!res.ok) return;
      const json = await res.json() as {
        mode?: string;
        worldOverride?: string | null;
        activeWorld?: string | null;
      };
      setCacheMode({
        mode: String(json.mode || 'fresh').toLowerCase() === 'stable' ? 'stable' : 'fresh',
        worldOverride: String(json.worldOverride || ''),
        activeWorld: String(json.activeWorld || ''),
      });
    } catch {
      // Non-fatal: geometry/live feed can keep operating.
    }
  }, []);

  const hydrateGeometry = useCallback(async () => {
    const fetchJsonSafe = async <T,>(url: string, fallback: T): Promise<T> => {
      try {
        const res = await fetch(url);
        if (!res.ok) return fallback;
        return await res.json() as T;
      } catch {
        return fallback;
      }
    };

    try {
      const [wi, r, f, l, s, e, es] = await Promise.all([
        fetchJsonSafe<WorldInfo | null>(`${API_BASE}/api/game/worldinfo`, null),
        fetchJsonSafe<Road[]>(`${API_BASE}/api/game/roads`, []),
        fetchJsonSafe<ForestsData | null>(`${API_BASE}/api/game/forests`, null),
        fetchJsonSafe<MapLocation[]>(`${API_BASE}/api/game/locations`, []),
        fetchJsonSafe<MapStructure[]>(`${API_BASE}/api/game/structures`, []),
        fetchJsonSafe<ElevationsData | null>(`${API_BASE}/api/game/elevations`, null),
        fetchJsonSafe<ExportStatus | null>(`${API_BASE}/api/game/exportstatus`, null),
      ]);

      if (wi) setWorldInfo(wi);
      setRoads(Array.isArray(r) ? r : []);
      setForests(f);
      setLocations(Array.isArray(l) ? l : []);
      setStructures(Array.isArray(s) ? s : []);
      setElevations(e);
      if (es) setExportStatus(es);
      await fetchCacheMode();
    } catch {
      // Bridge endpoint failures are non-fatal; live unit feed can still operate.
    }
  }, [fetchCacheMode]);

  const applyCacheMode = useCallback(async (mode: 'fresh' | 'stable', worldOverride: string) => {
    try {
      const payload = {
        mode,
        worldOverride: (worldOverride || '').trim(),
      };
      const res = await fetch(`${API_BASE}/api/game/cachemode`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(payload),
      });
      if (!res.ok) return false;
      await hydrateGeometry();
      return true;
    } catch {
      return false;
    }
  }, [hydrateGeometry]);

  const refreshMapCache = useCallback(async () => {
    await hydrateGeometry();
  }, [hydrateGeometry]);

  useEffect(() => {
    let cancelled = false;
    let reconnectTimer: number | null = null;
    let lastHydratedWorld = '';
    let hasHydrated = false;

    const connect = () => {
      if (cancelled) return;
      const ws = new WebSocket(BRIDGE_WS_URL);
      wsRef.current = ws;

      ws.onopen = () => {
        if (!cancelled) setConnected(true);
      };

      ws.onmessage = (ev) => {
        if (cancelled) return;
        let msg: { type?: string; data?: unknown };
        try {
          msg = JSON.parse(String(ev.data));
        } catch {
          return;
        }

        if (msg.type === 'state') {
          const relayState = (msg.data || {}) as RelayState;
          const mapped = mapRelayStateToFrame(relayState);
          setFrame(mapped.frame);
          // Only overwrite worldInfo when relay actually provides a world name;
          // otherwise preserve the REST-hydrated cache value.
          if (mapped.worldInfo?.nameWorld) {
            setWorldInfo(mapped.worldInfo);
          }
          setConnected(Boolean(relayState.connected));
          // Hydrate once on first state, and again when the relay world changes.
          const relayWorld = mapped.worldInfo?.nameWorld || '';
          if (!hasHydrated || (relayWorld && relayWorld !== lastHydratedWorld)) {
            hasHydrated = true;
            if (relayWorld) lastHydratedWorld = relayWorld;
            hydrateGeometry();
          }
          return;
        }

        if (msg.type === 'relayConnected') {
          setConnected(true);
          return;
        }

        if (msg.type === 'relayDisconnected') {
          setConnected(false);
          return;
        }

        if (msg.type === 'mapImportStatus') {
          const data = (msg.data || {}) as {
            inProgress?: boolean;
            stage?: string;
            counts?: { maproads?: number; mapfoliage?: number; maplocations?: number; mapobjects?: number; maprow?: number };
            command?: string;
          };
          const counts = data.counts || {};
          const complete = data.stage === 'complete' || data.command === 'mapend';
          setExportStatus({
            phase: data.stage || (data.inProgress ? 'exporting' : 'idle'),
            roadCount: counts.maproads || 0,
            roadsComplete: complete,
            treeCount: counts.mapfoliage || 0,
            treesComplete: complete,
            forestCount: counts.mapfoliage || 0,
            forestsComplete: complete,
            locationCount: counts.maplocations || 0,
            locationsComplete: complete,
            structureCount: counts.mapobjects || 0,
            structuresComplete: complete,
            elevationCount: counts.maprow || 0,
            elevationsComplete: complete,
          });
          return;
        }

        if (msg.type === 'mapImportDataReady') {
          hydrateGeometry();
        }
      };

      ws.onclose = () => {
        if (cancelled) return;
        setConnected(false);
        reconnectTimer = window.setTimeout(connect, 2000);
      };

      ws.onerror = () => {
        // onclose handles reconnect logic.
      };
    };

    connect();

    return () => {
      cancelled = true;
      if (reconnectTimer !== null) window.clearTimeout(reconnectTimer);
      wsRef.current?.close();
      wsRef.current = null;
    };
  }, []);

  useEffect(() => {
    hydrateGeometry();
  }, [hydrateGeometry]);

  const requestWorldExport = useCallback((command: string) => {
    if (command !== 'world') return;
    setExportStatus(prev => ({ ...prev, phase: 'requested' }));
    const ws = wsRef.current;
    if (!ws || ws.readyState !== WebSocket.OPEN) return;
    ws.send(JSON.stringify({ type: 'startMapExport' }));
  }, []);

  return useMemo(() => ({
    connected,
    frame,
    recentKills,
    recentFired,
    recentFiredImpacts,
    worldInfo,
    roads,
    forests,
    locations,
    structures,
    elevations,
    serverSettings,
    exportStatus,
    cacheMode,
    applyCacheMode,
    refreshMapCache,
    requestWorldExport,
  }), [
    connected,
    frame,
    recentKills,
    recentFired,
    recentFiredImpacts,
    worldInfo,
    roads,
    forests,
    locations,
    structures,
    elevations,
    serverSettings,
    exportStatus,
    cacheMode,
    applyCacheMode,
    refreshMapCache,
    requestWorldExport,
  ]);
}
