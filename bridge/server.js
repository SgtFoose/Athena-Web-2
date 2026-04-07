/**
 * server.js — Athena Web PoC bridge
 *
 * Bridges Athena Relay.exe (TCP 28800) → WebSocket → Browser
 * Also serves the web UI over HTTP on port 3000.
 *
 * Run: node server.js
 * Then open http://localhost:3000 on any device on the same network.
 *
 * Configuration (via env vars or edit the CONFIG block below):
 *   RELAY_HOST   — IP of the PC running Relay.exe  (default: 127.0.0.1)
 *   RELAY_PORT   — Port of Relay.exe               (default: 28800)
 *   WEB_PORT     — HTTP/WS port for the browser     (default: 3000)
 */

'use strict';

const http    = require('http');
const net     = require('net');
const fs      = require('fs');
const path    = require('path');
const crypto  = require('crypto');
const { WebSocketServer, WebSocket } = require('ws');

// ─── Configuration ────────────────────────────────────────────────────────────
const CONFIG = {
  relayHost : process.env.RELAY_HOST || '127.0.0.1',
  relayPort : parseInt(process.env.RELAY_PORT  || '28800', 10),
  webPort   : parseInt(process.env.WEB_PORT    || '3000',  10),
  relayClientType: process.env.RELAY_CLIENT_TYPE || 'General',
  mapExportStrategy: process.env.MAPEXPORT_STRATEGY || 'basic',
  relayFramePollType: process.env.RELAY_FRAME_POLL_TYPE || 'next',
  relayFramePollCommand: process.env.RELAY_FRAME_POLL_COMMAND || '',
  relayFramePollIntervalMs: Math.max(250, parseInt(process.env.RELAY_FRAME_POLL_INTERVAL_MS || '250', 10) || 250),
  worldSelectionMode: process.env.WORLD_SELECTION_MODE || 'fresh',
  worldCacheOverride: process.env.WORLD_CACHE_OVERRIDE || '',
  reconnectMs: 3000,   // ms between relay reconnect attempts
};

const RUNTIME = {
  worldSelectionMode: String(CONFIG.worldSelectionMode || 'fresh').toLowerCase() === 'stable' ? 'stable' : 'fresh',
  worldCacheOverride: String(CONFIG.worldCacheOverride || '').trim(),
};

const ATHENA_MAPS_DIR = process.env.ATHENA_MAPS_DIR
  || path.join(process.env.USERPROFILE || '', 'Documents', 'Athena', 'Maps');

const STATIC_ROOT_WWWROOT = path.join(__dirname, 'wwwroot');
const STATIC_ROOT_PUBLIC = path.join(__dirname, 'public');
const ACTIVE_STATIC_ROOT = fs.existsSync(path.join(STATIC_ROOT_WWWROOT, 'index.html'))
  ? STATIC_ROOT_WWWROOT
  : STATIC_ROOT_PUBLIC;
const IS_PACKAGED_UI = ACTIVE_STATIC_ROOT === STATIC_ROOT_WWWROOT;

const worldCacheMemo = new Map();
const contourCacheMemo = new Map();
const landMaskCacheMemo = new Map();
let latestWorldProbe = { at: 0, world: null };
let activeWebPort = CONFIG.webPort;

// GUID that identifies this bridge to the relay (keep stable between runs)
const BRIDGE_GUID = '5f3a9c12-4e7b-4d1a-b023-ae8f2c6d19f0';

// Protocol constants (discovered by binary analysis)
const SEP = '<ath_sep>';
const END = '<ath_sep>end';

// ─── State ────────────────────────────────────────────────────────────────────
let relaySocket    = null;
let relayStringBuf = '';         // string accumulator for <ath_sep> protocol
let relayConnected = false;
let relayFramePollTimer = null;
let gameState      = createEmptyState();    // last known game state for late-joiners
let mapImportState = createEmptyMapImportState();
let mapImportData  = createEmptyMapImportData();
let recentRelayCommands = [];
let recentOutboundRelayCommands = [];
let recentRawRelayMessages = [];
let frameDebugState = createEmptyFrameDebugState();
let lastLiveFrameAt = 0;

/** @type {Set<WebSocket>} */
const wsClients = new Set();

// ─── Helpers ──────────────────────────────────────────────────────────────────

function createEmptyState() {
  return {
    connected  : false,
    map        : null,
    missionGuid: null,
    missionName: null,
    profile    : null,
    date       : null,
    time       : null,
    units      : [],   // merged unit info + position
    markers    : [],
    groups     : [],
    vehicles   : [],
  };
}

function createEmptyMapImportState() {
  return {
    inProgress: false,
    startedAt: null,
    completedAt: null,
    stage: 'idle',
    counts: {
      maprow: 0,
      mapobjects: 0,
      maplocations: 0,
      maproads: 0,
      mapfoliage: 0,
    },
    world: null,
    lastCommand: null,
  };
}

function createEmptyMapImportData() {
  return {
    maprows: [],
    mapobjects: [],
    maplocations: [],
    maproads: [],
    mapfoliage: [],
    runtimeElevations: [],
    runtimeElevationDebug: {
      parsedMessages: 0,
      failedMessages: 0,
      samples: [],
    },
  };
}

function createEmptyFrameDebugState() {
  return {
    seenCount: 0,
    lastSeenAt: null,
    missionGuid: null,
    topLevelKeys: [],
    missionKeys: [],
    childKeys: [],
    guidHints: {},
    exportHints: [],
  };
}

function tryParseJson(text) {
  if (!text || !text.trim()) return null;
  try {
    return JSON.parse(text);
  } catch {
    return null;
  }
}

function parseNodeXY(nodeText) {
  if (typeof nodeText !== 'string') return null;
  const [x, y] = nodeText.split(',').map(v => Number(v));
  if (!Number.isFinite(x) || !Number.isFinite(y)) return null;
  return { x, y };
}

function parseFloatSafe(value, fallback = 0) {
  const n = Number(value);
  return Number.isFinite(n) ? n : fallback;
}

function pushDebugSample(debugObj, payload, reason) {
  if (!debugObj || !Array.isArray(debugObj.samples)) return;
  if (debugObj.samples.length >= 24) return;
  const text = String(payload || '').trim();
  debugObj.samples.push({
    reason,
    payload: text.length > 240 ? `${text.slice(0, 240)}...` : text,
  });
}

function parseRuntimeElevationPayload(payload, fallbackStep = 8) {
  const out = [];
  const parsed = tryParseJson(payload);

  if (parsed && typeof parsed === 'object') {
    if (Number.isFinite(parsed.x) && Number.isFinite(parsed.y) && Number.isFinite(parsed.z)) {
      out.push({ x: Number(parsed.x), y: Number(parsed.y), z: Number(parsed.z) });
      return out;
    }

    if (Array.isArray(parsed.points)) {
      for (const p of parsed.points) {
        if (p && Number.isFinite(p.x) && Number.isFinite(p.y) && Number.isFinite(p.z)) {
          out.push({ x: Number(p.x), y: Number(p.y), z: Number(p.z) });
        }
      }
      if (out.length > 0) return out;
    }

    if (Array.isArray(parsed.values)) {
      const y = Number(parsed.y ?? parsed.rowY ?? parsed.row ?? parsed.Y);
      const startX = Number(parsed.startX ?? parsed.x ?? parsed.X ?? 0);
      const step = Number(parsed.sampleSize ?? parsed.sample ?? parsed.step ?? parsed.cell ?? fallbackStep) || fallbackStep;
      if (Number.isFinite(y)) {
        for (let i = 0; i < parsed.values.length; i += 1) {
          const z = Number(parsed.values[i]);
          if (!Number.isFinite(z)) continue;
          out.push({ x: startX + i * step, y, z });
        }
      }
      if (out.length > 0) return out;
    }
  }

  if (Array.isArray(parsed)) {
    if (parsed.length === 3 && parsed.every(Number.isFinite)) {
      out.push({ x: Number(parsed[0]), y: Number(parsed[1]), z: Number(parsed[2]) });
      return out;
    }
    if (parsed.length >= 3 && parsed.length % 3 === 0 && parsed.every(Number.isFinite)) {
      for (let i = 0; i + 2 < parsed.length; i += 3) {
        out.push({ x: Number(parsed[i]), y: Number(parsed[i + 1]), z: Number(parsed[i + 2]) });
      }
      return out;
    }
  }

  const nums = String(payload || '')
    .split(/[,;\s]+/)
    .map((v) => Number(v))
    .filter((n) => Number.isFinite(n));

  if (nums.length === 3) {
    out.push({ x: nums[0], y: nums[1], z: nums[2] });
    return out;
  }

  if (nums.length >= 3 && nums.length % 3 === 0) {
    for (let i = 0; i + 2 < nums.length; i += 3) {
      out.push({ x: nums[i], y: nums[i + 1], z: nums[i + 2] });
    }
    return out;
  }

  return out;
}

function inferSampleSizeFromCells(cells, fallback = 8) {
  if (!Array.isArray(cells) || cells.length < 2) return fallback;
  const xs = Array.from(new Set(cells.map((c) => Number(c.x)).filter(Number.isFinite))).sort((a, b) => a - b);
  const ys = Array.from(new Set(cells.map((c) => Number(c.y)).filter(Number.isFinite))).sort((a, b) => a - b);
  let minDelta = Infinity;

  for (let i = 1; i < xs.length; i += 1) {
    const d = xs[i] - xs[i - 1];
    if (d > 0 && d < minDelta) minDelta = d;
  }
  for (let i = 1; i < ys.length; i += 1) {
    const d = ys[i] - ys[i - 1];
    if (d > 0 && d < minDelta) minDelta = d;
  }

  return Number.isFinite(minDelta) ? minDelta : fallback;
}

function elapsedSeconds(isoDate) {
  if (!isoDate) return null;
  const t = Date.parse(isoDate);
  if (!Number.isFinite(t)) return null;
  return Math.max(0, Math.floor((Date.now() - t) / 1000));
}

function summarizePayload(payload) {
  if (payload == null) return '';
  const text = String(payload).replace(/\s+/g, ' ').trim();
  if (!text) return '';
  return text.length > 180 ? `${text.slice(0, 180)}...` : text;
}

function pushRelayCommand(cmd, payload) {
  const item = {
    at: new Date().toISOString(),
    cmd: String(cmd || '').toLowerCase().trim(),
    payload: summarizePayload(payload),
  };
  recentRelayCommands.push(item);
  if (recentRelayCommands.length > 120) {
    recentRelayCommands = recentRelayCommands.slice(recentRelayCommands.length - 120);
  }
}

function pushOutboundRelayCommand(cmd, payload) {
  const item = {
    at: new Date().toISOString(),
    cmd: String(cmd || '').toLowerCase().trim(),
    payload: summarizePayload(payload),
  };
  recentOutboundRelayCommands.push(item);
  if (recentOutboundRelayCommands.length > 60) {
    recentOutboundRelayCommands = recentOutboundRelayCommands.slice(recentOutboundRelayCommands.length - 60);
  }
}

function pushRawRelayMessage(cmd, raw) {
  const lc = String(cmd || '').toLowerCase().trim();
  if (!lc || lc === 'keepalive') return;
  const text = String(raw || '').replace(/\s+/g, ' ').trim();
  recentRawRelayMessages.push({
    at: new Date().toISOString(),
    cmd: lc,
    raw: text.length > 420 ? `${text.slice(0, 420)}...` : text,
  });
  if (recentRawRelayMessages.length > 120) {
    recentRawRelayMessages = recentRawRelayMessages.slice(recentRawRelayMessages.length - 120);
  }
}

function toSortedKeys(obj) {
  if (!obj || typeof obj !== 'object' || Array.isArray(obj)) return [];
  return Object.keys(obj).sort((a, b) => a.localeCompare(b));
}

function extractGuidHints(frame, child) {
  const out = {};
  const addIfGuidLike = (key, value) => {
    if (!value || typeof value !== 'string') return;
    if (/guid/i.test(key)) out[key] = value;
  };

  for (const [k, v] of Object.entries(frame || {})) addIfGuidLike(k, v);
  for (const [k, v] of Object.entries((frame && frame.Mission) || {})) addIfGuidLike(`Mission.${k}`, v);
  for (const [k, v] of Object.entries(child || {})) addIfGuidLike(`Child.${k}`, v);
  return out;
}

function extractExportHints(frame, child) {
  const out = [];
  const pushHint = (scope, key, value) => {
    const type = Array.isArray(value) ? 'array' : typeof value;
    const size = Array.isArray(value) ? value.length : undefined;
    out.push({ scope, key, type, size });
  };

  const scan = (scope, obj) => {
    if (!obj || typeof obj !== 'object' || Array.isArray(obj)) return;
    for (const [k, v] of Object.entries(obj)) {
      if (/(map|road|foliage|location|object|export|mesh)/i.test(k)) {
        pushHint(scope, k, v);
      }
    }
  };

  scan('frame', frame);
  scan('mission', frame && frame.Mission);
  scan('child', child);
  return out.slice(0, 40);
}

function updateFrameDebug(frame, child) {
  frameDebugState.seenCount += 1;
  frameDebugState.lastSeenAt = new Date().toISOString();
  frameDebugState.missionGuid = frame?.MissionGUID || frameDebugState.missionGuid;
  frameDebugState.topLevelKeys = toSortedKeys(frame);
  frameDebugState.missionKeys = toSortedKeys(frame?.Mission);
  frameDebugState.childKeys = toSortedKeys(child);
  frameDebugState.guidHints = extractGuidHints(frame, child);
  frameDebugState.exportHints = extractExportHints(frame, child);
}

function roadTypeToText(roadType) {
  switch (Number(roadType)) {
    case 0: return 'road';
    case 1: return 'hide';
    case 2: return 'track';
    case 3: return 'road';
    default: return '';
  }
}

function safeReadJsonFile(filePath) {
  try {
    if (!fs.existsSync(filePath)) return null;
    const txt = fs.readFileSync(filePath, 'utf8');
    if (!txt || !txt.trim()) return null;
    return JSON.parse(txt);
  } catch {
    return null;
  }
}

function worldCacheKey(worldName) {
  return String(worldName || '').trim().toLowerCase();
}

function resolveMostRecentWorld(mode = 'fresh') {
  const now = Date.now();
  if (now - latestWorldProbe.at < 5000) {
    return latestWorldProbe.world;
  }

  let bestWorld = null;
  let bestTick = 0;
  try {
    if (!fs.existsSync(ATHENA_MAPS_DIR)) {
      latestWorldProbe = { at: now, world: null };
      return null;
    }

    const dirs = fs.readdirSync(ATHENA_MAPS_DIR, { withFileTypes: true });
    for (const d of dirs) {
      if (!d.isDirectory()) continue;
      const worldDir = path.join(ATHENA_MAPS_DIR, d.name);
      const mapPath = path.join(worldDir, 'Map.txt');
      if (!fs.existsSync(mapPath)) continue;

      let lastTick = 0;
      const directFiles = ['Map.txt', 'Roads.txt', 'Forests.txt', 'Locations.txt', 'Objects.txt', 'Trees.txt'];
      for (const name of directFiles) {
        const p = path.join(worldDir, name);
        if (!fs.existsSync(p)) continue;
        const t = fs.statSync(p).mtimeMs;
        if (t > lastTick) lastTick = t;
      }

      if (mode !== 'stable') {
        const rowsDir = path.join(worldDir, 'Rows');
        const roadsDir = path.join(worldDir, 'Roads');
        if (fs.existsSync(rowsDir)) {
          const t = fs.statSync(rowsDir).mtimeMs;
          if (t > lastTick) lastTick = t;
        }
        if (fs.existsSync(roadsDir)) {
          const t = fs.statSync(roadsDir).mtimeMs;
          if (t > lastTick) lastTick = t;
        }
      }

      if (lastTick > bestTick) {
        bestTick = lastTick;
        bestWorld = d.name;
      }
    }
  } catch {
    // Keep best-effort behavior; fall back to current hints if probing fails.
  }

  latestWorldProbe = { at: now, world: bestWorld };
  return bestWorld;
}

function listMapCacheWorlds() {
  if (!fs.existsSync(ATHENA_MAPS_DIR)) {
    return [];
  }

  try {
    const dirs = fs.readdirSync(ATHENA_MAPS_DIR, { withFileTypes: true });
    const worlds = [];
    for (const d of dirs) {
      if (!d.isDirectory()) continue;
      const worldDir = path.join(ATHENA_MAPS_DIR, d.name);
      const hasMapTxt = fs.existsSync(path.join(worldDir, 'Map.txt'));
      if (!hasMapTxt) continue;
      worlds.push({ name: d.name, hasMapTxt: true });
    }
    worlds.sort((a, b) => a.name.localeCompare(b.name));
    return worlds;
  } catch {
    return [];
  }
}

function buildHealthStatus() {
  const activeWorld = detectActiveWorld();
  const cacheExists = fs.existsSync(ATHENA_MAPS_DIR);
  const worlds = listMapCacheWorlds();
  const activeWorldCached = !!activeWorld && worlds.some((w) => w.name.toLowerCase() === String(activeWorld).toLowerCase());
  const tips = [];

  if (!relayConnected) {
    tips.push('Relay is not connected. Ensure Relay.exe is running and reachable.');
  }
  if (!cacheExists) {
    tips.push('Athena map cache folder was not found. Export a map from Athena Desktop to create it.');
  } else if (worlds.length === 0) {
    tips.push('Map cache folder exists but has no exported worlds. Export at least one map in Athena Desktop.');
  }
  if (activeWorld && !activeWorldCached) {
    tips.push(`Active world "${activeWorld}" is not cached yet. Export this world in Athena Desktop.`);
  }

  return {
    statusCode: (!cacheExists || worlds.length === 0 || (activeWorld && !activeWorldCached)) ? 503 : 200,
    payload: {
      relay: {
        connected: relayConnected,
        host: CONFIG.relayHost,
        port: CONFIG.relayPort,
      },
      mapCache: {
        path: ATHENA_MAPS_DIR,
        exists: cacheExists,
        worldCount: worlds.length,
        worlds,
      },
      activeWorld: activeWorld || null,
      activeWorldCached: activeWorld ? activeWorldCached : true,
      tips,
    },
  };
}

function parseGridCoordToken(token) {
  const [sx, sy] = String(token || '').split(',');
  const x = Number(sx);
  const y = Number(sy);
  if (!Number.isFinite(x) || !Number.isFinite(y)) return null;
  return { x, y };
}

function listAvailableContourZ(worldDir) {
  const heightDir = path.join(worldDir, 'Height');
  if (!fs.existsSync(heightDir)) return [];
  const out = [];
  for (const entry of fs.readdirSync(heightDir, { withFileTypes: true })) {
    if (!entry.isFile()) continue;
    const m = /^Z(-?\d+)\.txt$/i.exec(entry.name);
    if (m) out.push(Number(m[1]));
  }
  return out.sort((a, b) => a - b);
}

function loadContourLine(worldName, z, worldCell) {
  const key = `${worldCacheKey(worldName)}:${z}`;
  if (contourCacheMemo.has(key)) return contourCacheMemo.get(key);

  const worldDir = path.join(ATHENA_MAPS_DIR, worldName);
  const contourPath = path.join(worldDir, 'Height', `Z${z}.txt`);
  const raw = safeReadJsonFile(contourPath);

  if (!raw || !Array.isArray(raw.PointGroups)) {
    const empty = { z: Number(z), major: Number(z) % 20 === 0, lines: [] };
    contourCacheMemo.set(key, empty);
    return empty;
  }

  // Need worldSize to flip canvas-Y to world-Y.
  const mapData = safeReadJsonFile(path.join(worldDir, 'Map.txt'));
  const worldSize = Number(mapData?.WorldSize || 10240);

  // Athena Desktop Height files store grid indices in canvas coordinates
  // (Y=0 is north). Flip Y to world coordinates: worldY = worldSize - canvasY.
  const lines = raw.PointGroups
    .map((ring) => {
      if (!Array.isArray(ring) || ring.length < 2) return null;
      const flat = [];
      for (const token of ring) {
        const p = parseGridCoordToken(token);
        if (!p) continue;
        flat.push(p.x * worldCell, worldSize - p.y * worldCell);
      }
      return flat.length >= 6 ? flat : null;
    })
    .filter(Boolean);

  const contour = {
    z: Number(raw.Z ?? z),
    major: Number(raw.Z ?? z) % 20 === 0,
    lines,
  };
  contourCacheMemo.set(key, contour);
  return contour;
}

function pointInPolygon(x, y, polygon) {
  let inside = false;
  for (let i = 0, j = polygon.length - 1; i < polygon.length; j = i++) {
    const xi = polygon[i][0];
    const yi = polygon[i][1];
    const xj = polygon[j][0];
    const yj = polygon[j][1];
    const intersects = ((yi > y) !== (yj > y))
      && (x < ((xj - xi) * (y - yi)) / ((yj - yi) || 1e-9) + xi);
    if (intersects) inside = !inside;
  }
  return inside;
}

function buildLandMaskFromContour(contour, worldSize, gridSize) {
  const width = gridSize;
  const height = gridSize;
  const cellWorld = worldSize / gridSize;
  const bytes = Buffer.alloc(width * height, 0);

  if (!contour || !Array.isArray(contour.lines) || contour.lines.length === 0) {
    return { width, height, mask: bytes };
  }

  const rings = contour.lines
    .map((flat) => {
      const ring = [];
      for (let i = 0; i + 1 < flat.length; i += 2) {
        ring.push([flat[i], flat[i + 1]]);
      }
      if (ring.length < 3) return null;
      // Simplify very dense contour rings to keep server-side rasterization fast.
      const step = Math.max(1, Math.ceil(ring.length / 512));
      const simple = step === 1 ? ring : ring.filter((_, idx) => idx % step === 0);
      return simple.length >= 3 ? simple : null;
    })
    .filter(Boolean);

  if (rings.length === 0) {
    return { width, height, mask: bytes };
  }

  const ringBounds = rings.map((ring) => {
    let minX = Infinity;
    let minY = Infinity;
    let maxX = -Infinity;
    let maxY = -Infinity;
    for (const [x, y] of ring) {
      if (x < minX) minX = x;
      if (y < minY) minY = y;
      if (x > maxX) maxX = x;
      if (y > maxY) maxY = y;
    }
    return { minX, minY, maxX, maxY };
  });

  for (let gy = 0; gy < height; gy++) {
    const worldY = (gy + 0.5) * cellWorld;
    for (let gx = 0; gx < width; gx++) {
      const worldX = (gx + 0.5) * cellWorld;
      let inside = false;
      for (let r = 0; r < rings.length; r += 1) {
        const b = ringBounds[r];
        if (worldX < b.minX || worldX > b.maxX || worldY < b.minY || worldY > b.maxY) {
          continue;
        }
        if (pointInPolygon(worldX, worldY, rings[r])) inside = !inside;
      }
      if (inside) bytes[gy * width + gx] = 1;
    }
  }

  return { width, height, mask: bytes };
}

function detectActiveWorld() {
  const override = String(RUNTIME.worldCacheOverride || '').trim();
  if (override) {
    const overrideDir = path.join(ATHENA_MAPS_DIR, override);
    if (fs.existsSync(overrideDir) && fs.existsSync(path.join(overrideDir, 'Map.txt'))) {
      return override;
    }
  }

  // Relay-reported world takes priority — use case-insensitive folder match
  // so "tanoa" matches "Tanoa" folder and returns the exact folder name.
  const hinted = (mapImportState.world || gameState.map || '').trim();
  if (hinted) {
    // Try exact match first (fast path)
    const hintedDir = path.join(ATHENA_MAPS_DIR, hinted);
    if (fs.existsSync(hintedDir) && fs.existsSync(path.join(hintedDir, 'Map.txt'))) {
      return hinted;
    }
    // Case-insensitive scan: relay may report "tanoa" but folder is "Tanoa"
    try {
      const dirs = fs.readdirSync(ATHENA_MAPS_DIR, { withFileTypes: true });
      const match = dirs.find(d => d.isDirectory() && d.name.toLowerCase() === hinted.toLowerCase());
      if (match) {
        const matchDir = path.join(ATHENA_MAPS_DIR, match.name);
        if (fs.existsSync(path.join(matchDir, 'Map.txt'))) {
          return match.name;
        }
      }
    } catch { /* ignore */ }
    // Even without a cache folder, trust the relay hint so we don't
    // fall through to a stale filesystem probe of a different world.
    return hinted;
  }

  const mode = String(RUNTIME.worldSelectionMode || 'fresh').toLowerCase();
  return resolveMostRecentWorld(mode) || hinted;
}

function applyCacheModeUpdate(modeValue, worldValue, hasWorldValue) {
  let changed = false;

  if (typeof modeValue === 'string') {
    const nextMode = modeValue.toLowerCase() === 'stable' ? 'stable' : 'fresh';
    if (nextMode !== RUNTIME.worldSelectionMode) {
      RUNTIME.worldSelectionMode = nextMode;
      changed = true;
    }
  }

  if (hasWorldValue) {
    const nextWorld = String(worldValue || '').trim();
    if (nextWorld !== RUNTIME.worldCacheOverride) {
      RUNTIME.worldCacheOverride = nextWorld;
      changed = true;
    }
  }

  if (changed) {
    worldCacheMemo.clear();
    contourCacheMemo.clear();
    landMaskCacheMemo.clear();
    latestWorldProbe = { at: 0, world: null };
  }

  return changed;
}

function loadWorldCache(worldName) {
  if (!worldName) return null;
  const memoKey = worldCacheKey(worldName);
  if (worldCacheMemo.has(memoKey)) return worldCacheMemo.get(memoKey);

  const worldDir = path.join(ATHENA_MAPS_DIR, worldName);
  if (!fs.existsSync(worldDir)) return null;

  const map = safeReadJsonFile(path.join(worldDir, 'Map.txt'));
  const roadsRaw = safeReadJsonFile(path.join(worldDir, 'Roads.txt'));
  const forestsRaw = safeReadJsonFile(path.join(worldDir, 'Forests.txt'));
  const locationsRaw = safeReadJsonFile(path.join(worldDir, 'Locations.txt'));
  const objectsRaw = safeReadJsonFile(path.join(worldDir, 'Objects.txt'));
  const treesRaw = safeReadJsonFile(path.join(worldDir, 'Trees.txt'));

  const worldSize = Number(map?.WorldSize || 10240);
  const worldInfo = map ? {
    nameDisplay: map.DisplayName || map.WorldName || worldName,
    nameWorld: map.WorldName || worldName,
    author: '',
    size: worldSize,
    forestMin: 0,
    offsetX: Number(map.OffsetX || 0),
    offsetY: Number(map.OffsetY || worldSize),
    centerX: Number(map.CenterX || worldSize / 2),
    centerY: Number(map.CenterY || worldSize / 2),
  } : null;

  // Athena Desktop cache stores road nodes in canvas coordinates:
  //   CanvasX = worldX (unchanged), CanvasY = worldSize - worldY
  // Flip Y back to world coordinates for the UI.
  const roads = Array.isArray(roadsRaw?.Roads)
    ? roadsRaw.Roads.flatMap((r, idx) => {
      const nodes = Array.isArray(r.Nodes)
        ? r.Nodes.map(parseNodeXY).filter(Boolean)
            .map(n => ({ x: n.x, y: worldSize - n.y }))
        : [];
      const roadType = roadTypeToText(r.RoadType);
      const width = parseFloatSafe(r.Width, 0);
      const dir = parseFloatSafe(r.Dir, 0);

      // Airport surface tiles are exported as "hide" roads and should remain
      // area rectangles centered on the object position.
      if (roadType === 'hide') {
        const center = nodes.length > 0 ? nodes[0] : { x: 0, y: 0 };
        const len = parseFloatSafe(r.Length, width || 20);
        return [{
          id: `road-${idx}`,
          type: roadType,
          foot: false,
          bridge: false,
          posX: center.x,
          posY: center.y,
          beg1X: center.x,
          beg1Y: center.y,
          end2X: center.x,
          end2Y: center.y,
          width,
          length: len,
          dir,
        }];
      }

      // Build one rendered road segment per adjacent node pair.
      if (nodes.length >= 2) {
        return nodes.slice(0, -1).map((n0, sIdx) => {
          const n1 = nodes[sIdx + 1];
          const dx = n1.x - n0.x;
          const dy = n1.y - n0.y;
          const segLength = Math.hypot(dx, dy);
          const segDir = (Math.atan2(dx, dy) * 180) / Math.PI;
          return {
            id: `road-${idx}-${sIdx}`,
            type: roadType,
            foot: false,
            bridge: false,
            posX: (n0.x + n1.x) / 2,
            posY: (n0.y + n1.y) / 2,
            beg1X: n0.x,
            beg1Y: n0.y,
            end2X: n1.x,
            end2Y: n1.y,
            width,
            length: segLength,
            dir: Number.isFinite(segDir) ? segDir : dir,
          };
        });
      }

      const only = nodes.length > 0 ? nodes[0] : { x: 0, y: 0 };
      return [{
        id: `road-${idx}`,
        type: roadType,
        foot: false,
        bridge: false,
        posX: only.x,
        posY: only.y,
        beg1X: only.x,
        beg1Y: only.y,
        end2X: only.x,
        end2Y: only.y,
        width,
        length: parseFloatSafe(r.Length, 0),
        dir,
      }];
    })
      .filter((r) => Number.isFinite(r.beg1X) && Number.isFinite(r.beg1Y) && Number.isFinite(r.end2X) && Number.isFinite(r.end2Y))
    : [];

  const locations = Array.isArray(locationsRaw)
    ? locationsRaw.map((l) => ({
      type: String(l.Type || ''),
      name: String(l.Text || ''),
      posX: parseFloatSafe(l.PosX, 0),
      posY: parseFloatSafe(l.PosY, 0),
      dir: 0,
      sizeX: parseFloatSafe(l.Width, 0),
      sizeY: parseFloatSafe(l.Length, 0),
    }))
    : [];

  const structures = Array.isArray(objectsRaw)
    ? objectsRaw.map((o, idx) => ({
      id: String(o.ObjectID || `obj-${idx}`),
      type: 'object',
      model: String(o.Model || '').trim(),
      posX: parseFloatSafe(o.PosX, 0),
      posY: parseFloatSafe(o.PosY, 0),
      dir: parseFloatSafe(o.Dir, 0),
      width: parseFloatSafe(o.Width, 0),
      length: parseFloatSafe(o.Length, 0),
      height: 0,
    }))
    : [];

  const trees = Array.isArray(treesRaw)
    ? treesRaw
      .map((t) => {
        const x = parseFloatSafe(t.CanvasX, Number.NaN);
        const canvasY = parseFloatSafe(t.CanvasY, Number.NaN);
        if (!Number.isFinite(x) || !Number.isFinite(canvasY)) return null;
        return { x, y: worldSize - canvasY };
      })
      .filter(Boolean)
    : [];

  const forests = (() => {
    if (!forestsRaw) return null;
    const lightBlocks = Array.isArray(forestsRaw.CellsLight) ? forestsRaw.CellsLight : [];
    const heavyBlocks = Array.isArray(forestsRaw.CellsHeavy) ? forestsRaw.CellsHeavy : [];
    if (lightBlocks.length === 0 && heavyBlocks.length === 0) return null;

    const lightPoints = [];
    const heavyPoints = [];

    for (const b of lightBlocks) {
      for (const token of (b.Cells || [])) {
        const [sx, sy] = String(token).split(',');
        const x = Number(sx);
        const y = Number(sy);
        if (Number.isFinite(x) && Number.isFinite(y)) lightPoints.push([x, y]);
      }
    }
    for (const b of heavyBlocks) {
      for (const token of (b.Cells || [])) {
        const [sx, sy] = String(token).split(',');
        const x = Number(sx);
        const y = Number(sy);
        if (Number.isFinite(x) && Number.isFinite(y)) heavyPoints.push([x, y]);
      }
    }

    // Bus exact: GenerateForestGeometry uses fixed 16×16 pixel cells
    // (ForestPoint.X * 16.0, ForestPoint.Y * 16.0, 16.0, 16.0)
    // Canvas = WorldSize × WorldSize, so grid = WorldSize / 16.
    const sampleSize = 16;

    const cellMap = new Map();
    // Athena Desktop forest cell indices use canvas Y (0 = north/top).
    // Bus draws at (yIdx * 16) from the top. To convert to world Y (0 = south):
    // worldY = worldSize - (yIdx + 1) * 16  (bottom edge of the 16px cell)
    for (const [xIdx, yIdx] of lightPoints) {
      const key = `${xIdx}:${yIdx}`;
      cellMap.set(key, {
        x: xIdx * sampleSize,
        y: worldSize - (yIdx + 1) * sampleSize,
        level: 2,
      });
    }
    for (const [xIdx, yIdx] of heavyPoints) {
      const key = `${xIdx}:${yIdx}`;
      cellMap.set(key, {
        x: xIdx * sampleSize,
        y: worldSize - (yIdx + 1) * sampleSize,
        level: 3,
      });
    }

    return {
      sampleSize,
      cells: Array.from(cellMap.values()),
    };
  })();

  const availableZ = listAvailableContourZ(worldDir);
  const staticInfo = map ? {
    worldName: worldInfo.nameWorld,
    worldSize: worldInfo.size,
    cellSize: Number(map.WorldCell || 8),
    maxZ: Number(map.MaxZ || 0),
    minZ: Number(map.MinZ || 0),
    availableZ,
    hasTrees: trees.length > 0,
  } : null;

  const result = {
    worldInfo,
    roads,
    forests,
    locations,
    structures,
    trees,
    elevations: null,
    staticInfo,
  };

  worldCacheMemo.set(memoKey, result);
  return result;
}

function broadcast(obj) {
  const msg = JSON.stringify(obj);
  for (const ws of wsClients) {
    if (ws.readyState === WebSocket.OPEN) {
      ws.send(msg);
    }
  }
}

// ─── Relay data parsing ───────────────────────────────────────────────────────
// Protocol (discovered): messages are delimited by "<ath_sep>end"
// Fields within a message are split by "<ath_sep>"
// Frame format: "frame<ath_sep>{json}<ath_sep>end"
// Registration:  "General<ath_sep>{guid}<ath_sep><ath_sep>end"

function processIncomingBytes(chunk) {
  relayStringBuf += chunk.toString('utf8');

  // Safety: discard if buffer grows too large
  if (relayStringBuf.length > 2 * 1024 * 1024) {
    console.warn('[relay] Buffer overflow — discarding');
    relayStringBuf = '';
    return;
  }

  // Extract all complete messages (terminated by <ath_sep>end)
  let idx;
  while ((idx = relayStringBuf.indexOf(END)) !== -1) {
    const raw = relayStringBuf.slice(0, idx);
    relayStringBuf = relayStringBuf.slice(idx + END.length);
    if (raw.length > 0) handleAthenaMessage(raw);
  }
}

function handleAthenaMessage(raw) {
  const parts = raw.split(SEP);
  const cmd   = parts[0].toLowerCase().trim();
  const payload = parts[1] || '';
  pushRawRelayMessage(cmd, raw);
  pushRelayCommand(cmd, payload);

  if (cmd === 'frame') {
    // parts[1] is the JSON payload
    const json = payload;
    if (!json) return;
    try {
      const frame = JSON.parse(json);
      handleFrame(frame);
    } catch (e) {
      console.warn('[relay] JSON parse failed:', e.message, json.slice(0, 100));
    }
  } else if (cmd === 'keepalive') {
    // nothing — just keeps the connection alive
  } else if (cmd === 'mapbegin' || cmd === 'maprow' || cmd === 'mapobjects' || cmd === 'maplocations' || cmd === 'maproads' || cmd === 'mapfoliage' || cmd === 'mapend') {
    handleMapImportMessage(cmd, payload);
  } else if (cmd === 'elevation') {
    // Arma extension style: elevation<sep>index<sep>x<sep>y<sep>z
    const nums = parts.slice(1).map((v) => Number(v)).filter((n) => Number.isFinite(n));
    let point = null;
    if (nums.length >= 4) {
      point = { x: nums[1], y: nums[2], z: nums[3] };
    } else if (nums.length === 3) {
      point = { x: nums[0], y: nums[1], z: nums[2] };
    }
    if (point) {
      if (!mapImportState.inProgress) {
        mapImportState.inProgress = true;
        mapImportState.startedAt = mapImportState.startedAt || new Date().toISOString();
        mapImportState.stage = 'receiving';
      }
      mapImportState.counts.maprow += 1;
      mapImportData.runtimeElevations.push(point);
      mapImportData.runtimeElevationDebug.parsedMessages += 1;
    } else {
      mapImportData.runtimeElevationDebug.failedMessages += 1;
      pushDebugSample(mapImportData.runtimeElevationDebug, parts.slice(1).join(SEP), 'elevation-command-unparsed');
    }
    mapImportState.lastCommand = cmd;
  } else if (cmd === 'elevationscomplete') {
    mapImportState.stage = 'complete';
    mapImportState.completedAt = new Date().toISOString();
    mapImportState.inProgress = false;
    mapImportState.lastCommand = cmd;
  } else {
    console.log('[relay] Unknown command:', cmd, summarizePayload(payload));
  }
}

function handleMapImportMessage(cmd, payload) {
  if (cmd === 'mapbegin') {
    mapImportState = createEmptyMapImportState();
    mapImportData = createEmptyMapImportData();
    mapImportState.inProgress = true;
    mapImportState.startedAt = new Date().toISOString();
    mapImportState.stage = 'receiving';
    mapImportState.world = payload || null;
  } else if (cmd === 'mapend') {
    mapImportState.inProgress = false;
    mapImportState.completedAt = new Date().toISOString();
    mapImportState.stage = 'complete';
    // Invalidate disk-cache memo so REST endpoints re-read fresh files
    // written by Athena Desktop during this export.
    worldCacheMemo.clear();
    contourCacheMemo.clear();
    landMaskCacheMemo.clear();
    broadcast({
      type: 'mapImportDataReady',
      data: {
        world: mapImportState.world,
        counts: {
          rows: mapImportData.maprows.length,
          roads: mapImportData.maproads.length,
          objects: mapImportData.mapobjects.length,
          locations: mapImportData.maplocations.length,
          foliage: mapImportData.mapfoliage.length,
        },
        parse: {
          roadSegments: mapImportData.maproads
            .reduce((sum, item) => sum + (((item && item.Segments) || []).length), 0),
          objectCount: mapImportData.mapobjects
            .reduce((sum, item) => sum + (((item && item.Objects) || []).length), 0),
          locationCount: mapImportData.maplocations
            .reduce((sum, item) => sum + (((item && item.Locations) || []).length), 0),
          treeCount: mapImportData.mapfoliage
            .reduce((sum, item) => sum + (((item && item.Trees) || []).length), 0),
          bushCount: mapImportData.mapfoliage
            .reduce((sum, item) => sum + (((item && item.Bushes) || []).length), 0),
          elevationSamples: mapImportData.runtimeElevations.length,
          elevationParsedMessages: mapImportData.runtimeElevationDebug.parsedMessages,
          elevationFailedMessages: mapImportData.runtimeElevationDebug.failedMessages,
        },
      },
    });
  } else {
    if (!mapImportState.inProgress) {
      mapImportState.inProgress = true;
      mapImportState.startedAt = mapImportState.startedAt || new Date().toISOString();
      mapImportState.stage = 'receiving';
    }
    if (Object.prototype.hasOwnProperty.call(mapImportState.counts, cmd)) {
      mapImportState.counts[cmd] += 1;
    }

    if (payload) {
      if (cmd === 'maprow') {
        mapImportData.maprows.push(payload);
        const points = parseRuntimeElevationPayload(payload, 8);
        if (points.length > 0) {
          mapImportData.runtimeElevations.push(...points);
          mapImportData.runtimeElevationDebug.parsedMessages += 1;
        } else {
          mapImportData.runtimeElevationDebug.failedMessages += 1;
          pushDebugSample(mapImportData.runtimeElevationDebug, payload, 'maprow-unparsed');
        }
      } else if (cmd === 'mapobjects') {
        mapImportData.mapobjects.push(tryParseJson(payload) || { _raw: payload });
      } else if (cmd === 'maplocations') {
        mapImportData.maplocations.push(tryParseJson(payload) || { _raw: payload });
      } else if (cmd === 'maproads') {
        mapImportData.maproads.push(tryParseJson(payload) || { _raw: payload });
      } else if (cmd === 'mapfoliage') {
        mapImportData.mapfoliage.push(tryParseJson(payload) || { _raw: payload });
      }
    }
  }

  mapImportState.lastCommand = cmd;
  broadcast({
    type: 'mapImportStatus',
    data: {
      command: cmd,
      world: mapImportState.world,
      inProgress: mapImportState.inProgress,
      stage: mapImportState.stage,
      counts: mapImportState.counts,
      startedAt: mapImportState.startedAt,
      completedAt: mapImportState.completedAt,
      hasPayload: Boolean(payload),
      payloadLength: payload ? payload.length : 0,
    },
  });
}

function sendRelayCommand(command, data = '') {
  if (!relaySocket || !relayConnected) return false;
  pushOutboundRelayCommand(command, data);
  const frame = command + SEP + data + SEP + END;
  relaySocket.write(frame);
  return true;
}

function sendRelayCommandToSelf(command, data = '') {
  if (!relaySocket || !relayConnected) return false;
  pushOutboundRelayCommand(`${command}@self`, data);
  const frame = command + SEP + BRIDGE_GUID + SEP + data + SEP + END;
  relaySocket.write(frame);
  return true;
}

function buildMapExportPayloads() {
  const strategy = String(CONFIG.mapExportStrategy || 'basic').toLowerCase();
  if (strategy === 'guid-csv') {
    return [`mapexport,${BRIDGE_GUID}`, 'mapexport'];
  }
  if (strategy === 'guid-pipe') {
    return [`mapexport|${BRIDGE_GUID}`, 'mapexport'];
  }
  if (strategy === 'guid-colon') {
    return [`mapexport:${BRIDGE_GUID}`, 'mapexport'];
  }
  if (strategy === 'guid-space') {
    return [`mapexport ${BRIDGE_GUID}`, 'mapexport'];
  }
  if (strategy === 'all-guid') {
    return [
      'mapexport',
      `mapexport,${BRIDGE_GUID}`,
      `mapexport|${BRIDGE_GUID}`,
      `mapexport:${BRIDGE_GUID}`,
      `mapexport ${BRIDGE_GUID}`,
    ];
  }
  return ['mapexport'];
}

function sendMapExportCommand() {
  const payloads = buildMapExportPayloads();
  let sentAny = false;
  for (const payload of payloads) {
    const ok = sendRelayCommand('command', payload);
    sentAny = sentAny || ok;
  }
  return sentAny;
}

function stopRelayFramePoll() {
  if (relayFramePollTimer) {
    clearInterval(relayFramePollTimer);
    relayFramePollTimer = null;
  }
}

function startRelayFramePoll() {
  stopRelayFramePoll();
  const pollType = String(CONFIG.relayFramePollType || 'command').trim() || 'command';
  const pollCommand = String(CONFIG.relayFramePollCommand || '').trim();
  if (pollType === 'command' && !pollCommand) return;
  if (!pollType) return;
  relayFramePollTimer = setInterval(() => {
    if (!relayConnected || !relaySocket) return;
    sendRelayCommand(pollType, pollCommand);
  }, CONFIG.relayFramePollIntervalMs);
  console.log(`[relay] Frame poll enabled: type=${pollType} data=${pollCommand || '<empty>'} every ${CONFIG.relayFramePollIntervalMs}ms`);
}

/**
 * Handle a parsed v2 frame from the relay.
 * Schema (v2):
 *   frame.Format       = 'v2'
 *   frame.MissionGUID  = string
 *   frame.Mission      = { name, map, profile, author, ismultiplayer, steamid }
 *   frame.Children[]   = {
 *     FrameGUID, Date, Time,
 *     Groups[]   : { leaderNetID, groupNetId, name, id, side, wpx, wpy }
 *     Markers[]  : { ... }
 *     Units[]    : { name, netid, side, type, faction, rank, player, groupnetid, ... }
 *     Vehicles[] : { ... }
 *     UnitUpdates[]: { netid, posx, posy, posz, dir, speed }
 *   }
 */
function handleFrame(frame) {
  lastLiveFrameAt = Date.now();
  // Mission-level metadata
  if (frame.MissionGUID) gameState.missionGuid = frame.MissionGUID;
  if (frame.Mission) {
    gameState.map         = String(frame.Mission.map || '').trim();
    gameState.missionName = frame.Mission.name        || gameState.missionName;
    gameState.profile     = frame.Mission.profile     || gameState.profile;
  }

  const child = Array.isArray(frame.Children) ? frame.Children[0] : null;
  updateFrameDebug(frame, child);
  if (!child) return;

  gameState.date   = child.Date || gameState.date;
  gameState.time   = child.Time || gameState.time;
  gameState.groups  = child.Groups   || [];
  gameState.markers = child.Markers  || [];
  gameState.vehicles = child.Vehicles || [];

  // Build a unit map keyed by netid, merging static info + position update
  const unitMap = new Map();

  for (const u of (child.Units || [])) {
    unitMap.set(u.netid, { ...u });
  }
  for (const upd of (child.UnitUpdates || [])) {
    const u = unitMap.get(upd.netid);
    if (u) {
      u.posx  = parseFloat(upd.posx)  || 0;
      u.posy  = parseFloat(upd.posy)  || 0;
      u.posz  = parseFloat(upd.posz)  || 0;
      u.dir   = parseFloat(upd.dir)   || 0;
      u.speed = parseFloat(upd.speed) || 0;
    }
  }

  gameState.units = [...unitMap.values()];

  broadcast({ type: 'state', data: buildClientState() });
}

function buildClientState() {
  const liveFrameFresh = lastLiveFrameAt > 0 && (Date.now() - lastLiveFrameAt) <= 8000;
  return {
    connected  : relayConnected,
    map        : liveFrameFresh ? (gameState.map || '') : '',
    missionGuid: gameState.missionGuid,
    missionName: gameState.missionName,
    profile    : gameState.profile,
    date       : gameState.date,
    time       : gameState.time,
    units      : gameState.units,
    markers    : gameState.markers,
    groups     : gameState.groups,
    vehicles   : gameState.vehicles,
  };
}

// ─── Relay TCP connection ─────────────────────────────────────────────────────
function connectToRelay() {
  if (relaySocket) return;

  console.log(`[relay] Connecting to ${CONFIG.relayHost}:${CONFIG.relayPort} …`);
  relaySocket = new net.Socket();

  relaySocket.connect(CONFIG.relayPort, CONFIG.relayHost, () => {
    console.log('[relay] Connected!');
    relayConnected = true;
    broadcast({ type: 'relayConnected' });

    // Register as a relay client using the <ath_sep> protocol.
    // Format: {CommandType}<ath_sep>{ClientGUID}<ath_sep>{ContentData}<ath_sep>end
    const handshake = CONFIG.relayClientType + SEP + BRIDGE_GUID + SEP + SEP + END;
    relaySocket.write(handshake);
    console.log(`[relay] Sent registration handshake (${CONFIG.relayClientType})`);
    startRelayFramePoll();
  });

  relaySocket.on('data', (chunk) => {
    processIncomingBytes(chunk);
  });

  relaySocket.on('error', (err) => {
    console.error(`[relay] Error: ${err.message}`);
  });

  relaySocket.on('close', () => {
    console.log('[relay] Disconnected. Reconnecting in', CONFIG.reconnectMs, 'ms …');
    stopRelayFramePoll();
    relaySocket    = null;
    relayConnected = false;
    relayStringBuf = '';
    lastLiveFrameAt = 0;
    gameState      = createEmptyState();
    mapImportState = createEmptyMapImportState();
    mapImportData = createEmptyMapImportData();
    broadcast({ type: 'relayDisconnected' });
    setTimeout(connectToRelay, CONFIG.reconnectMs);
  });
}

// ─── HTTP server (serves public/) ────────────────────────────────────────────
const MIME = {
  '.html': 'text/html; charset=utf-8',
  '.js'  : 'application/javascript',
  '.css' : 'text/css',
  '.svg' : 'image/svg+xml',
  '.png' : 'image/png',
  '.ico' : 'image/x-icon',
  '.json': 'application/json',
};

const httpServer = http.createServer((req, res) => {
  // CORS — allow the Vite dev server (and any other origin) to call bridge APIs.
  res.setHeader('Access-Control-Allow-Origin', '*');
  res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS');
  res.setHeader('Access-Control-Allow-Headers', 'Content-Type');
  if (req.method === 'OPTIONS') {
    res.writeHead(204);
    res.end();
    return;
  }

  const requestUrl = new URL(req.url || '/', `http://${req.headers.host || 'localhost'}`);
  const pathname = decodeURIComponent(requestUrl.pathname);

  if (pathname === '/api/health') {
    const health = buildHealthStatus();
    res.writeHead(health.statusCode, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(health.payload));
    return;
  }

  if (pathname === '/api/game/cachemode') {
    const respond = (changed) => {
      const activeWorld = detectActiveWorld();
      res.writeHead(200, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify({
        changed,
        mode: RUNTIME.worldSelectionMode,
        worldOverride: RUNTIME.worldCacheOverride || null,
        activeWorld: activeWorld || null,
      }));
    };

    if ((req.method || 'GET').toUpperCase() === 'POST') {
      let body = '';
      req.on('data', (chunk) => {
        body += chunk.toString('utf8');
        if (body.length > 64 * 1024) {
          body = body.slice(0, 64 * 1024);
        }
      });
      req.on('end', () => {
        let parsed = {};
        if (body.trim()) {
          try {
            parsed = JSON.parse(body);
          } catch {
            res.writeHead(400, { 'Content-Type': 'application/json' });
            res.end(JSON.stringify({ error: 'Invalid JSON body' }));
            return;
          }
        }
        const hasWorld = Object.prototype.hasOwnProperty.call(parsed, 'world') || Object.prototype.hasOwnProperty.call(parsed, 'worldOverride');
        const worldValue = Object.prototype.hasOwnProperty.call(parsed, 'worldOverride') ? parsed.worldOverride : parsed.world;
        const changed = applyCacheModeUpdate(parsed.mode, worldValue, hasWorld);
        respond(changed);
      });
      return;
    }

    const modeParam = requestUrl.searchParams.get('mode');
    const hasWorldParam = requestUrl.searchParams.has('world');
    const worldParam = requestUrl.searchParams.get('world');
    const changed = applyCacheModeUpdate(modeParam, worldParam, hasWorldParam);
    respond(changed);
    return;
  }

  if (pathname === '/api/game/worldinfo') {
    const cache = loadWorldCache(detectActiveWorld());
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(cache?.worldInfo || null));
    return;
  }

  if (pathname === '/api/game/roads') {
    const cache = loadWorldCache(detectActiveWorld());
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(cache?.roads || []));
    return;
  }

  if (pathname === '/api/game/forests') {
    const cache = loadWorldCache(detectActiveWorld());
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(cache?.forests || null));
    return;
  }

  if (pathname === '/api/game/locations') {
    const cache = loadWorldCache(detectActiveWorld());
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(cache?.locations || []));
    return;
  }

  if (pathname === '/api/game/structures') {
    const cache = loadWorldCache(detectActiveWorld());
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(cache?.structures || []));
    return;
  }

  if (pathname === '/api/game/elevations') {
    const cache = loadWorldCache(detectActiveWorld());
    const worldName = cache?.worldInfo?.nameWorld;
    const worldSize = Number(cache?.worldInfo?.size || 0);
    const worldCell = Number(cache?.staticInfo?.cellSize || 8);
    if (!worldName || !worldSize) {
      res.writeHead(200, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify(null));
      return;
    }

    if (Array.isArray(mapImportData.runtimeElevations) && mapImportData.runtimeElevations.length > 0) {
      const dedup = new Map();
      for (const c of mapImportData.runtimeElevations) {
        if (!c || !Number.isFinite(c.x) || !Number.isFinite(c.y) || !Number.isFinite(c.z)) continue;
        dedup.set(`${c.x}:${c.y}`, { x: Number(c.x), y: Number(c.y), z: Number(c.z) });
      }
      const cells = Array.from(dedup.values());
      res.writeHead(200, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify({
        sampleSize: inferSampleSizeFromCells(cells, worldCell),
        worldSize,
        cells,
      }));
      return;
    }

    // Coarse elevation approximation from major contour points (20m bands).
    const zList = (cache?.staticInfo?.availableZ || []).filter((z) => z % 20 === 0);
    const cellMap = new Map();
    for (const z of zList) {
      const contour = loadContourLine(worldName, z, worldCell);
      for (const flat of contour.lines || []) {
        for (let i = 0; i + 1 < flat.length; i += 2) {
          const x = flat[i];
          const y = flat[i + 1];
          const kx = Math.round(x / worldCell);
          const ky = Math.round(y / worldCell);
          const key = `${kx}:${ky}`;
          const prev = cellMap.get(key);
          if (!prev || z > prev.z) {
            cellMap.set(key, { x: kx * worldCell, y: ky * worldCell, z });
          }
        }
      }
    }

    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({
      sampleSize: worldCell,
      worldSize,
      cells: Array.from(cellMap.values()),
    }));
    return;
  }

  if (pathname === '/api/game/elevationdebug') {
    const cache = loadWorldCache(detectActiveWorld());
    const runtimeCells = Array.isArray(mapImportData.runtimeElevations) ? mapImportData.runtimeElevations.length : 0;
    const rowsSeen = Array.isArray(mapImportData.maprows) ? mapImportData.maprows.length : 0;
    const dbg = mapImportData.runtimeElevationDebug || { parsedMessages: 0, failedMessages: 0, samples: [] };
    const total = dbg.parsedMessages + dbg.failedMessages;
    const successRate = total > 0 ? Number((dbg.parsedMessages / total).toFixed(4)) : null;

    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({
      world: cache?.worldInfo?.nameWorld || mapImportState.world || null,
      inProgress: mapImportState.inProgress,
      maprowMessages: rowsSeen,
      runtimeElevationCells: runtimeCells,
      parsedMessages: dbg.parsedMessages,
      failedMessages: dbg.failedMessages,
      successRate,
      samples: dbg.samples || [],
    }));
    return;
  }

  if (pathname === '/api/game/statedebug') {
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({
      map: gameState.map,
      missionName: gameState.missionName,
      profile: gameState.profile,
      unitCount: (gameState.units || []).length,
      detectActiveWorld: detectActiveWorld(),
    }));
    return;
  }

  if (pathname === '/api/game/relaydebug') {
    const recent = recentRelayCommands.slice(-40);
    const outbound = recentOutboundRelayCommands.slice(-20);
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({
      connected: relayConnected,
      relayClientType: CONFIG.relayClientType,
      exportStage: mapImportState.stage,
      exportLastCommand: mapImportState.lastCommand,
      recent,
      outbound,
    }));
    return;
  }

  if (pathname === '/api/game/rawrelaydebug') {
    const shouldClear = requestUrl.searchParams.get('clear') === '1';
    const limitReq = Number(requestUrl.searchParams.get('limit') || '10');
    const limit = Number.isFinite(limitReq) && limitReq > 0 ? Math.min(200, Math.floor(limitReq)) : 10;
    const recent = recentRawRelayMessages.slice(-limit);
    if (shouldClear) {
      recentRawRelayMessages = [];
    }
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({
      relayClientType: CONFIG.relayClientType,
      count: recent.length,
      recent,
      cleared: shouldClear,
    }));
    return;
  }

  if (pathname === '/api/game/framedebug') {
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(frameDebugState));
    return;
  }

  if (pathname === '/api/game/trees') {
    const cache = loadWorldCache(detectActiveWorld());
    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify(cache?.trees || []));
    return;
  }

  if (pathname === '/api/game/exportstatus') {
    const complete = mapImportState.stage === 'complete' || mapImportState.lastCommand === 'mapend';
    const activeSeconds = elapsedSeconds(mapImportState.startedAt);
    const totalIncoming =
      (mapImportState.counts.maprow || 0)
      + (mapImportState.counts.maproads || 0)
      + (mapImportState.counts.mapobjects || 0)
      + (mapImportState.counts.maplocations || 0)
      + (mapImportState.counts.mapfoliage || 0);
    const stalled =
      mapImportState.inProgress
      && mapImportState.stage === 'requested'
      && totalIncoming === 0
      && Number.isFinite(activeSeconds)
      && activeSeconds >= 8;

    res.writeHead(200, { 'Content-Type': 'application/json' });
    res.end(JSON.stringify({
      phase: mapImportState.stage || 'idle',
      roadCount: mapImportState.counts.maproads || 0,
      roadsComplete: complete,
      treeCount: mapImportState.counts.mapfoliage || 0,
      treesComplete: complete,
      forestCount: mapImportState.counts.mapfoliage || 0,
      forestsComplete: complete,
      locationCount: mapImportState.counts.maplocations || 0,
      locationsComplete: complete,
      structureCount: mapImportState.counts.mapobjects || 0,
      structuresComplete: complete,
      elevationCount: mapImportState.counts.maprow || 0,
      elevationsComplete: complete,
      activeSeconds,
      stalled,
      lastCommand: mapImportState.lastCommand || null,
    }));
    return;
  }

  if (pathname.startsWith('/api/staticmap/')) {
    const parts = pathname.split('/').filter(Boolean);
    // /api/staticmap/{world}
    if (parts.length === 3) {
      const worldName = parts[2];
      const cache = loadWorldCache(worldName);
      if (!cache?.staticInfo) {
        res.writeHead(404, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify({ error: 'World not found' }));
        return;
      }
      res.writeHead(200, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify(cache.staticInfo));
      return;
    }

    // /api/staticmap/{world}/contours/{z}
    if (parts.length === 5 && parts[3] === 'contours') {
      const worldName = parts[2];
      const cache = loadWorldCache(worldName);
      if (!cache?.staticInfo) {
        res.writeHead(404, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify({ error: 'World not found' }));
        return;
      }
      const z = Number(parts[4] || 0);
      const contour = loadContourLine(worldName, z, Number(cache.staticInfo.cellSize || 8));
      res.writeHead(200, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify(contour));
      return;
    }

    // /api/staticmap/{world}/landmask?gridSize=...
    if (parts.length === 4 && parts[3] === 'landmask') {
      const worldName = parts[2];
      const cache = loadWorldCache(worldName);
      const worldSize = Number(cache?.worldInfo?.size || 10240);
      const worldCell = Number(cache?.staticInfo?.cellSize || 8);
      const requestedGridSize = Math.max(32, Math.min(2048, Number(requestUrl.searchParams.get('gridSize') || 256)));
      const maskKey = `${worldCacheKey(worldName)}:${requestedGridSize}`;
      if (landMaskCacheMemo.has(maskKey)) {
        const cached = landMaskCacheMemo.get(maskKey);
        res.writeHead(200, { 'Content-Type': 'application/json' });
        res.end(JSON.stringify(cached));
        return;
      }

      const z0 = loadContourLine(worldName, 0, worldCell);
      let width = requestedGridSize;
      let height = requestedGridSize;
      let mask = Buffer.alloc(width * height, 1);

      if (z0?.lines?.length) {
        let effectiveGrid = requestedGridSize;
        let estimatedOps = z0.lines.length * effectiveGrid * effectiveGrid;
        // Lower raster resolution for very complex coastlines to keep endpoint responsive,
        // while still returning a real coastline mask instead of an all-land fallback.
        while (estimatedOps >= 8_000_000 && effectiveGrid > 256) {
          effectiveGrid = Math.max(256, Math.floor(effectiveGrid / 2));
          estimatedOps = z0.lines.length * effectiveGrid * effectiveGrid;
        }

        const generated = buildLandMaskFromContour(z0, worldSize, effectiveGrid);
        width = generated.width;
        height = generated.height;
        mask = generated.mask;
      }

      const payload = {
        width,
        height,
        worldSize,
        mask: mask.toString('base64'),
      };
      landMaskCacheMemo.set(maskKey, payload);

      res.writeHead(200, { 'Content-Type': 'application/json' });
      res.end(JSON.stringify(payload));
      return;
    }
  }

  // In development mode (when packaged UI is unavailable), opening :3000 by habit
  // should land on the React dev server (:5173).
  if (!IS_PACKAGED_UI
    && (pathname === '/' || pathname === '/index.html')
    && String(process.env.UI_DEV_REDIRECT || 'true').toLowerCase() !== 'false') {
    const hostHeader = String(req.headers.host || 'localhost');
    const hostName = hostHeader.includes(':') ? hostHeader.split(':')[0] : hostHeader;
    const target = `http://${hostName}:5173/`;
    res.writeHead(302, { Location: target });
    res.end();
    return;
  }

  // Security: prevent path traversal
  const safePath = path.normalize(pathname).replace(/^(\.\.\/)+/, '');
  let filePath = path.join(ACTIVE_STATIC_ROOT, safePath === '/' ? 'index.html' : safePath);

  fs.stat(filePath, (err, stat) => {
    if (err || !stat.isFile()) {
      // Fallback to index.html for single-page behaviour
      filePath = path.join(ACTIVE_STATIC_ROOT, 'index.html');
    }
    fs.readFile(filePath, (readErr, data) => {
      if (readErr) {
        res.writeHead(404);
        res.end('Not found');
        return;
      }
      const ext  = path.extname(filePath);
      const mime = MIME[ext] || 'application/octet-stream';
      res.writeHead(200, { 'Content-Type': mime });
      res.end(data);
    });
  });
});

// ─── WebSocket server ─────────────────────────────────────────────────────────
const wss = new WebSocketServer({ server: httpServer });

wss.on('error', (err) => {
  if (err && err.code === 'EADDRINUSE') {
    // HTTP listener retry logic handles this case.
    return;
  }
  console.error('[ws] Server error:', err?.message || err);
});

wss.on('connection', (ws, req) => {
  const ip = req.socket.remoteAddress;
  console.log(`[ws] Client connected: ${ip}  (total: ${wsClients.size + 1})`);
  wsClients.add(ws);

  // Send current state immediately so late joiners catch up
  ws.send(JSON.stringify({ type: 'state', data: buildClientState() }));
  ws.send(JSON.stringify({ type: relayConnected ? 'relayConnected' : 'relayDisconnected' }));
  ws.send(JSON.stringify({ type: 'mapImportStatus', data: {
    command: mapImportState.lastCommand,
    world: mapImportState.world,
    inProgress: mapImportState.inProgress,
    stage: mapImportState.stage,
    counts: mapImportState.counts,
    startedAt: mapImportState.startedAt,
    completedAt: mapImportState.completedAt,
    hasPayload: false,
    payloadLength: 0,
  }}));

  ws.on('message', (raw) => {
    let msg;
    try {
      msg = JSON.parse(raw.toString('utf8'));
    } catch {
      ws.send(JSON.stringify({ type: 'bridgeError', data: { message: 'Malformed client message' } }));
      return;
    }

    if (msg?.type === 'startMapExport') {
      const ok = sendMapExportCommand();
      if (!ok) {
        ws.send(JSON.stringify({ type: 'bridgeError', data: { message: 'Relay not connected' } }));
        return;
      }

      mapImportState = createEmptyMapImportState();
      mapImportData = createEmptyMapImportData();
      mapImportState.inProgress = true;
      mapImportState.startedAt = new Date().toISOString();
      mapImportState.stage = 'requested';
      mapImportState.lastCommand = 'mapexport-requested';

      broadcast({
        type: 'mapImportStatus',
        data: {
          command: mapImportState.lastCommand,
          world: null,
          inProgress: true,
          stage: 'requested',
          counts: mapImportState.counts,
          startedAt: mapImportState.startedAt,
          completedAt: null,
          hasPayload: false,
          payloadLength: 0,
        },
      });
      return;
    }

    ws.send(JSON.stringify({ type: 'bridgeError', data: { message: 'Unknown client action' } }));
  });

  ws.on('close', () => {
    wsClients.delete(ws);
    console.log(`[ws] Client disconnected: ${ip}  (total: ${wsClients.size})`);
  });

  ws.on('error', (err) => {
    console.error('[ws] Client error:', err.message);
    wsClients.delete(ws);
  });
});

// ─── Boot ─────────────────────────────────────────────────────────────────────
function startHttpServer(preferredPort, maxRetries = 8) {
  const tryListen = (port, retriesLeft) => {
    const onError = (err) => {
      if (err && err.code === 'EADDRINUSE' && retriesLeft > 0) {
        console.warn(`[web] Port ${port} is in use. Trying ${port + 1} ...`);
        tryListen(port + 1, retriesLeft - 1);
        return;
      }
      console.error(`[web] Failed to bind HTTP server on port ${port}:`, err?.message || err);
      process.exitCode = 1;
    };

    httpServer.once('error', onError);
    httpServer.listen(port, '0.0.0.0', () => {
      httpServer.removeListener('error', onError);
      activeWebPort = port;

      const ifaces = require('os').networkInterfaces();
      const addrs  = Object.values(ifaces).flat()
        .filter(i => i.family === 'IPv4' && !i.internal)
        .map(i => `http://${i.address}:${activeWebPort}`);

      console.log('\n╔════════════════════════════════════════╗');
      console.log('║   Athena Web PoC — Bridge Server       ║');
      console.log('╚════════════════════════════════════════╝');
      console.log(`Local:   http://localhost:${activeWebPort}`);
      addrs.forEach(a => console.log(`Network: ${a}  ← open on your phone`));
      console.log(`\nRelay:   ${CONFIG.relayHost}:${CONFIG.relayPort}`);
      console.log('─────────────────────────────────────────\n');
    });
  };

  tryListen(preferredPort, maxRetries);
}

startHttpServer(CONFIG.webPort);

connectToRelay();
