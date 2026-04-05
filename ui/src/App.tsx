import { Component, useState, useRef, useCallback, useEffect, useMemo } from 'react'
import type { ErrorInfo, ReactNode } from 'react'
import { AthenaMap }   from './components/AthenaMap'
import { Sidebar }     from './components/Sidebar'
import { MapCacheBanner } from './components/MapCacheBanner'
import { useAthenHub } from './hooks/useAthenaHub'
import { useStaticMap } from './hooks/useStaticMap'
import { useAthenaLibrary } from './hooks/useAthenaLibrary'
import { useHealthCheck } from './hooks/useHealthCheck'
import { APP_VERSION } from './version'
import './App.css'

export type RenderMode = '2d' | 'heatmap1' | 'heatmap2'

export interface LayerVisibility {
  contours:   boolean
  forest:     boolean
  trees:      boolean
  roads:      boolean
  structures: boolean
  locations:  boolean
  groups:     boolean
  waypoints:  boolean
  lazes:      boolean
  projectiles:boolean
  vehicles:   boolean
  units:      boolean
}

interface MapErrorBoundaryProps {
  children: ReactNode
}

interface MapErrorBoundaryState {
  error: Error | null
}

class MapErrorBoundary extends Component<MapErrorBoundaryProps, MapErrorBoundaryState> {
  state: MapErrorBoundaryState = { error: null }

  static getDerivedStateFromError(error: Error): MapErrorBoundaryState {
    return { error }
  }

  componentDidCatch(error: Error, info: ErrorInfo) {
    console.error('AthenaMap render error', error, info)
  }

  render() {
    if (this.state.error) {
      return (
        <div style={{
          width: '100%',
          height: '100%',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          background: '#12181d',
          color: '#f0f3f6',
          padding: 24,
          textAlign: 'center',
          fontFamily: 'Segoe UI, system-ui, sans-serif',
        }}>
          <div>
            <div style={{ fontSize: 18, fontWeight: 700, marginBottom: 8 }}>Map render failed</div>
            <div style={{ fontSize: 13, opacity: 0.88 }}>{this.state.error.message}</div>
          </div>
        </div>
      )
    }
    return this.props.children
  }
}

function App() {
  const {
    connected,
    frame,
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
    selectWorld,
    requestWorldExport,
  } = useAthenHub()

  const units    = frame?.units    ?? {}
  const vehicles = frame?.vehicles ?? {}
  const groups   = frame?.groups   ?? {}
  const lazes    = frame?.lazes    ?? []
  const liveWorld = frame?.world?.nameWorld ?? frame?.mission?.world ?? ''
  // User-selected world for pre-planning; auto-cleared when live game sends a world
  const [userSelectedWorld, setUserSelectedWorld] = useState('')
  const world     = liveWorld || userSelectedWorld || worldInfo?.nameWorld || ''

  // When the game starts sending a live world, clear any user selection so the game takes over
  const prevLiveWorldRef = useRef('')
  useEffect(() => {
    if (liveWorld && liveWorld !== prevLiveWorldRef.current && userSelectedWorld) {
      setUserSelectedWorld('')
      // Switch bridge back to live-follow mode
      selectWorld('')
    }
    prevLiveWorldRef.current = liveWorld
  }, [liveWorld, userSelectedWorld, selectWorld])

  // Handle user picking a cached world from the dropdown
  const handleSelectWorld = useCallback(async (worldName: string) => {
    setUserSelectedWorld(worldName)
    if (worldName) {
      await selectWorld(worldName)
    } else {
      await selectWorld('')
    }
  }, [selectWorld])

  // Load pre-computed Athena Desktop cache (contour lines + metadata) for the active world
  const { staticInfo, contours } = useStaticMap(world || null)

  // worldSize: prefer live SignalR data, fall back to static Athena Desktop cache, then default
  const worldSize = frame?.world?.size ?? worldInfo?.size ?? staticInfo?.worldSize ?? 10240

  // Load Athena Desktop vehicle/location classification library
  const { vehicleMap, locationMap } = useAthenaLibrary()

  // Health check ΓÇö detect missing map cache and show first-time instructions
  const { health, error: healthError } = useHealthCheck(15_000)
  const [cacheBannerDismissed, setCacheBannerDismissed] = useState(false)

  // List of cached world names from the health endpoint (for world picker dropdown)
  const cachedWorlds = useMemo(
    () => (health?.mapCache?.worlds ?? []).filter(w => w.hasMapTxt).map(w => w.name),
    [health],
  )

  const [layers, setLayers] = useState<LayerVisibility>({
    contours:   true,
    forest:     true,
    trees:      true,
    roads:      true,
    structures: true,
    locations:  true,
    groups:     true,
    waypoints:  true,
    lazes:      true,
    projectiles:true,
    vehicles:   false,
    units:      false,
  })
  const [renderMode, setRenderMode] = useState<RenderMode>('2d')
  const [followActivePlayer, setFollowActivePlayer] = useState(false)
  const [mapSessionKey, setMapSessionKey] = useState(0)
  const previousWorldRef = useRef('')
  const previousConnectedRef = useRef(false)

  const toggleLayer = (key: keyof LayerVisibility) =>
    setLayers(prev => ({ ...prev, [key]: !prev[key] }))

  // Map focus callback ΓÇö allows sidebar to pan the map to a world coordinate
  const mapFocusRef = useRef<(posX: number, posY: number) => void>(() => {})
  const mapPanRef = useRef<(posX: number, posY: number) => void>(() => {})
  const lastFollowPosRef = useRef<{ x: number; y: number } | null>(null)
  const lastFollowTargetIdRef = useRef<string | null>(null)
  const previousMissionPlayerRef = useRef('')

  const activePlayerAnchor = useMemo(() => {
    const missionPlayer = frame?.mission?.player?.trim().toLowerCase() ?? ''
    const missionSteamId = frame?.mission?.steamId?.trim() ?? ''
    const unitList = Object.values(units)
    const isSameMissionSteam = (steamId: string) => missionSteamId !== '' && steamId.trim() === missionSteamId

    // Prefer explicit active marker from the game export.
    const primary = unitList.find(u => Boolean(u.isActivePlayer))
      // Then try direct unit-name match (important for switchable SP units).
      ?? (missionPlayer ? unitList.find(u => u.name?.trim().toLowerCase() === missionPlayer) : undefined)
      // Then try player profile name match.
      ?? (missionPlayer ? unitList.find(u => u.playerName?.trim().toLowerCase() === missionPlayer) : undefined)

    // Fallback only to mission SteamID-linked unit; avoid generic fallback that can lock onto the wrong unit.
    const fallback = primary
      ?? unitList.find(u => isSameMissionSteam(u.steamId))

    if (!fallback) return null

    const veh = fallback.vehicleId ? vehicles[fallback.vehicleId] : undefined
    return {
      id: fallback.id,
      name: fallback.playerName?.trim() || fallback.name || 'Active Player',
      x: veh?.posX ?? fallback.posX,
      y: veh?.posY ?? fallback.posY,
    }
  }, [frame?.mission?.player, frame?.mission?.steamId, units, vehicles])

  useEffect(() => {
    if (!followActivePlayer || !activePlayerAnchor) return
    const last = lastFollowPosRef.current
    const dx = (last?.x ?? Number.NaN) - activePlayerAnchor.x
    const dy = (last?.y ?? Number.NaN) - activePlayerAnchor.y
    const moved = !Number.isFinite(dx) || !Number.isFinite(dy) || Math.hypot(dx, dy) >= 2.5
    const targetChanged = lastFollowTargetIdRef.current !== activePlayerAnchor.id
    if (!moved && !targetChanged) return
    lastFollowPosRef.current = { x: activePlayerAnchor.x, y: activePlayerAnchor.y }
    lastFollowTargetIdRef.current = activePlayerAnchor.id
    mapPanRef.current(activePlayerAnchor.x, activePlayerAnchor.y)
  }, [followActivePlayer, activePlayerAnchor])

  useEffect(() => {
    if (!followActivePlayer) {
      lastFollowPosRef.current = null
      lastFollowTargetIdRef.current = null
    }
  }, [followActivePlayer])

  useEffect(() => {
    const missionPlayer = frame?.mission?.player?.trim() ?? ''
    const previous = previousMissionPlayerRef.current
    if (previous && missionPlayer && previous !== missionPlayer && followActivePlayer) {
      // Switching controlled units can briefly produce stale identities; require manual re-enable.
      setFollowActivePlayer(false)
      lastFollowPosRef.current = null
      lastFollowTargetIdRef.current = null
    }
    previousMissionPlayerRef.current = missionPlayer
  }, [frame?.mission?.player, followActivePlayer])

  const handleToggleFollowActivePlayer = useCallback(() => {
    setFollowActivePlayer(prev => {
      const next = !prev
      if (next && activePlayerAnchor) {
        // When follow is enabled, jump to the active unit at maximum map zoom.
        mapFocusRef.current(activePlayerAnchor.x, activePlayerAnchor.y)
        lastFollowPosRef.current = { x: activePlayerAnchor.x, y: activePlayerAnchor.y }
        lastFollowTargetIdRef.current = activePlayerAnchor.id
      }
      return next
    })
  }, [activePlayerAnchor])

  const handleRequestWorld = useCallback(() => {
    setMapSessionKey(prev => prev + 1)
    requestWorldExport('world')
  }, [requestWorldExport])

  useEffect(() => {
    const worldKey = world || ''
    const justConnected = connected && !previousConnectedRef.current
    const worldChanged = worldKey !== previousWorldRef.current

    if ((justConnected && worldKey) || (worldKey && worldChanged)) {
      setMapSessionKey(prev => prev + 1)
    }

    previousConnectedRef.current = connected
    previousWorldRef.current = worldKey
  }, [connected, world])

  return (
    <div className="app-shell">
      <aside className="sidebar">
        <div className="sidebar-header">
          <span className="logo-text">Γ¼í ATHENA REMASTERED</span>
          <span className="header-right">
            <span className="version-label">v{APP_VERSION}</span>
            <a
              className="donate-link"
              href="https://www.paypal.com/donate/?business=G76WK9YDWUSAE&item_name=Athena+Remastered+Dev&EUR&no_note=0"
              target="_blank"
              rel="noreferrer"
              title="Support development"
            >ΓÖÑ Donate</a>
          </span>
        </div>
        <Sidebar
          frame={frame}
          connected={connected}
          onRequestWorld={handleRequestWorld}
          roadCount={roads.length}
          treeCount={exportStatus.treeCount}
          forestCellCount={forests?.cells.length ?? 0}
          locationCount={locations.length}
          structureCount={structures.length}
          elevationCellCount={elevations?.cells.length ?? 0}
          cachedWorlds={cachedWorlds}
          selectedWorld={userSelectedWorld}
          liveWorld={liveWorld}
          onSelectWorld={handleSelectWorld}
          layers={layers}
          onToggleLayer={toggleLayer}
          followActivePlayer={followActivePlayer}
          activePlayerName={activePlayerAnchor?.name ?? null}
          onToggleFollowActivePlayer={handleToggleFollowActivePlayer}
          renderMode={renderMode}
          onChangeRenderMode={setRenderMode}
          serverSettings={serverSettings}
          locations={locations}
          groups={groups}
          units={units}
          onFocusPosition={(posX, posY) => mapFocusRef.current(posX, posY)}
        />
      </aside>
      <main className="map-area">
        {/* Welcome overlay ΓÇö shown when no world has been loaded yet */}
        {!worldInfo && (
          <div className="welcome-overlay">
            <img
              className="welcome-bg"
              src="/athena-default-bg.png"
              alt="Athena Remastered"
            />
            <div className="welcome-banner">
              <div className="welcome-title">Γ¼í ATHENA REMASTERED</div>
              <div className="welcome-status">
                {connected && exportStatus.phase !== 'idle'
                  ? 'Loading world dataΓÇª'
                  : connected
                  ? 'Connected to server ΓÇö waiting for game dataΓÇª'
                  : 'Connecting to serverΓÇª'}
              </div>
              {connected && exportStatus.phase !== 'idle' ? (
                <div className="welcome-export-progress">
                  {[
                    { label: 'Roads',      count: exportStatus.roadCount,      done: exportStatus.roadsComplete },
                    { label: 'Trees',      count: exportStatus.treeCount,      done: exportStatus.treesComplete },
                    { label: 'Forests',    count: exportStatus.forestCount,    done: exportStatus.forestsComplete },
                    { label: 'Locations',  count: exportStatus.locationCount,  done: exportStatus.locationsComplete },
                    { label: 'Structures', count: exportStatus.structureCount, done: exportStatus.structuresComplete },
                    { label: 'Elevations', count: exportStatus.elevationCount, done: exportStatus.elevationsComplete },
                  ].map(g => (
                    <div key={g.label} className="welcome-export-row">
                      <span style={{ color: g.done ? '#2ecc71' : '#f0a500' }}>{g.label}</span>
                      <span style={{ color: g.done ? '#2ecc71' : '#888' }}>{g.count}{g.done ? ' Γ£ô' : 'ΓÇª'}</span>
                    </div>
                  ))}
                </div>
              ) : (
                <div className="welcome-instructions">
                  <p>1. Launch <strong>AthenaWeb.exe</strong> (or <code>node server.js</code> in the bridge folder)</p>
                  <p>2. Launch Arma 3 with the <strong>@Athena</strong> mod enabled</p>
                  <p>3. Open <strong>Athena Desktop</strong> and connect ΓÇö the relay starts on port 28800</p>
                  <p>4. Start or join a mission ΓÇö live data will appear automatically</p>
                </div>
              )}
            </div>
          </div>
        )}
        {!cacheBannerDismissed && (
          <MapCacheBanner
            health={health}
            healthError={healthError}
            activeWorld={world}
            onDismiss={() => setCacheBannerDismissed(true)}
          />
        )}
        <MapErrorBoundary>
          <AthenaMap
            key={`${world || 'noworld'}:${worldSize}:${mapSessionKey}`}
            units={units}
            vehicles={vehicles}
            groups={groups}
            lazes={lazes}
            firedEvents={recentFired}
            firedImpacts={recentFiredImpacts}
            worldSize={worldSize}
            world={world}
            roads={roads}
            forests={forests}
            locations={locations}
            structures={structures}
            elevations={elevations}
            contours={contours}
            vehicleMap={vehicleMap}
            locationMap={locationMap}
            layers={layers}
            onLayersChange={setLayers}
            renderMode={renderMode}
            onRegisterFocus={(fn) => { mapFocusRef.current = fn }}
            onRegisterPan={(fn) => { mapPanRef.current = fn }}
            onUserInteraction={() => setFollowActivePlayer(false)}
          />
        </MapErrorBoundary>
        {exportStatus.phase !== 'idle' && (
          <div className="export-status-overlay">
            <div className="export-status-title">
              {exportStatus.phase === 'cached' ? 'ΓùÅ Loaded from cache'
               : exportStatus.phase === 'complete' ? 'ΓùÅ Export complete'
               : 'ΓùÅ Exporting world dataΓÇª'}
            </div>
            <div className="export-status-row">
              <span className={exportStatus.roadsComplete ? 'done' : 'pending'}>
                Roads: {exportStatus.roadCount}{exportStatus.roadsComplete ? ' Γ£ô' : 'ΓÇª'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.treesComplete ? 'done' : 'pending'}>
                Trees: {exportStatus.treeCount}{exportStatus.treesComplete ? ' Γ£ô' : 'ΓÇª'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.forestsComplete ? 'done' : 'pending'}>
                Forests: {exportStatus.forestCount}{exportStatus.forestsComplete ? ' Γ£ô' : 'ΓÇª'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.locationsComplete ? 'done' : 'pending'}>
                Locations: {exportStatus.locationCount}{exportStatus.locationsComplete ? ' Γ£ô' : 'ΓÇª'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.structuresComplete ? 'done' : 'pending'}>
                Structures: {exportStatus.structureCount}{exportStatus.structuresComplete ? ' Γ£ô' : 'ΓÇª'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.elevationsComplete ? 'done' : 'pending'}>
                Elevations: {exportStatus.elevationCount}{exportStatus.elevationsComplete ? ' Γ£ô' : 'ΓÇª'}
              </span>
            </div>
          </div>
        )}
      </main>
      <aside className="event-panel">
        <div className="panel-header">EVENTS</div>
        <div style={{ padding: '12px 8px', color: '#666', fontSize: 12, textAlign: 'center' }}>
          <p style={{ margin: '0 0 8px', color: '#888' }}>Event tracking (kills &amp; shots) requires the Athena Remastered extension DLL and is not available in ClientOnly mode.</p>
        </div>
      </aside>
    </div>
  )
}

export default App
