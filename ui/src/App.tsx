import { Component, useState, useRef, useCallback, useEffect, useMemo } from 'react'
import type { ErrorInfo, ReactNode } from 'react'
import { AthenaMap, type FirewillITgtTarget, type StoredITgtTarget }   from './components/AthenaMap'
import { Sidebar }     from './components/Sidebar'
import { MapCacheBanner } from './components/MapCacheBanner'
import { useAthenHub } from './hooks/useAthenaHub'
import { useStaticMap } from './hooks/useStaticMap'
import { useAthenaLibrary } from './hooks/useAthenaLibrary'
import { useHealthCheck } from './hooks/useHealthCheck'
import type { RelayMarker } from './types/game'
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
  armaMarkers:boolean
  firewillITgtMarkers:boolean
  aoMarkers:  boolean
  spawnMarkers:boolean
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

function firewillITgtMarkerLabel(marker: RelayMarker): string {
  const text = String(marker.text || '').trim()
  if (text) return text
  return String(marker.name || '').trim()
}

function isFirewillITgtRelayMarker(marker: RelayMarker): boolean {
  return firewillITgtMarkerLabel(marker).toUpperCase().startsWith('TGT_')
}

function isDeletedFirewillITgtMarker(marker: RelayMarker): boolean {
  const text = String(marker.text || '').toLowerCase()
  const name = String(marker.name || '').toLowerCase()
  if (text.includes('delete') || name.includes('delete')) return true
  return String(marker.color || '').toLowerCase().includes('red')
}

function App() {
  const {
    connected,
    frame,
    recentFired,
    recentFiredImpacts,
    worldInfo,
    geometryWorld,
    roads,
    forests,
    locations,
    structures,
    elevations,
    serverSettings,
    exportStatus,
    shorelineRefreshToken,
    cacheMode,
    selectWorld,
    requestWorldExport,
  } = useAthenHub()

  const liveWorld = connected
    ? (frame?.world?.nameWorld ?? frame?.mission?.world ?? '')
    : ''
  const hasLiveTelemetry = connected && liveWorld.length > 0
  const units    = hasLiveTelemetry ? (frame?.units    ?? {}) : {}
  const vehicles = hasLiveTelemetry ? (frame?.vehicles ?? {}) : {}
  const groups   = hasLiveTelemetry ? (frame?.groups   ?? {}) : {}
  const relayMarkers = hasLiveTelemetry ? (frame?.markers ?? []) : []
  const lazes    = hasLiveTelemetry ? (frame?.lazes    ?? []) : []
  const firewillITgtTargets = useMemo<FirewillITgtTarget[]>(() => (
    relayMarkers
      .filter(isFirewillITgtRelayMarker)
      .map((marker, index) => ({
        id: `${marker.name || marker.text || 'firewill-itgt'}:${index}`,
        name: String(marker.name || '').trim(),
        label: firewillITgtMarkerLabel(marker),
        x: marker.posX,
        y: marker.posY,
        deletedInFirewill: isDeletedFirewillITgtMarker(marker),
      }))
  ), [relayMarkers])
  // User-selected world for pre-planning; auto-cleared when live game sends a world
  const [userSelectedWorld, setUserSelectedWorld] = useState('')
  const world     = liveWorld || userSelectedWorld || ''
  const hasActiveWorld = world.length > 0
  const hasMatchingGeometry = hasActiveWorld && geometryWorld.toLowerCase() === world.toLowerCase()
  const activeWorldInfo = worldInfo?.nameWorld === world ? worldInfo : null

  // When live telemetry starts, clear any UI-selected or bridge-stable world override.
  const prevLiveWorldRef = useRef('')
  useEffect(() => {
    if (!liveWorld) {
      prevLiveWorldRef.current = ''
      return
    }

    const liveWorldChanged = liveWorld !== prevLiveWorldRef.current
    const hasManualOrStableOverride = Boolean(userSelectedWorld || cacheMode.worldOverride || cacheMode.mode === 'stable')

    if (liveWorldChanged && hasManualOrStableOverride) {
      setUserSelectedWorld('')
      // Force bridge back to live-follow mode so world geometry matches live telemetry.
      void selectWorld('')
    }

    prevLiveWorldRef.current = liveWorld
  }, [liveWorld, userSelectedWorld, cacheMode.mode, cacheMode.worldOverride, selectWorld])

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

  // worldSize: prefer cached/static map metadata first to avoid relay fallback-size drift
  // (for example Stratis reported as 10240 before worldinfo hydration), then live fallback.
  const worldSize = hasActiveWorld
    ? (activeWorldInfo?.size ?? staticInfo?.worldSize ?? frame?.world?.size ?? 10240)
    : 10240

  const mapRoads = hasMatchingGeometry ? roads : []
  const mapForests = hasMatchingGeometry ? forests : null
  const mapLocations = hasMatchingGeometry ? locations : []
  const mapStructures = hasMatchingGeometry ? structures : []
  const mapElevations = hasMatchingGeometry ? elevations : null
  const mapContours = hasActiveWorld ? contours : []

  // Load Athena Desktop vehicle/location classification library
  const { vehicleMap, locationMap } = useAthenaLibrary()

  // Health check  detect missing map cache and show first-time instructions
  const { health, error: healthError, refetch: refetchHealth } = useHealthCheck(15_000)
  const [cacheBannerDismissed, setCacheBannerDismissed] = useState(false)

  useEffect(() => {
    if (!world) return
    void refetchHealth()
  }, [world, refetchHealth])

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
    armaMarkers: true,
    firewillITgtMarkers: true,
    aoMarkers:  true,
    spawnMarkers:false,
    groups:     true,
    waypoints:  true,
    lazes:      true,
    projectiles:true,
    vehicles:   false,
    units:      false,
  })
  const [renderMode, setRenderMode] = useState<RenderMode>('2d')
  const [leftSidebarCollapsed, setLeftSidebarCollapsed] = useState(false)
  const [rightSidebarCollapsed, setRightSidebarCollapsed] = useState(false)
  const [isTouchInput, setIsTouchInput] = useState(false)
  const [followActivePlayer, setFollowActivePlayer] = useState(false)
  const [storedITgtTargets, setStoredITgtTargets] = useState<StoredITgtTarget[]>([])
  const itgtNextIndexRef = useRef(0)
  const [mapSessionKey, setMapSessionKey] = useState(0)
  const previousWorldRef = useRef('')
  const previousConnectedRef = useRef(false)
  const hadLiveTelemetryRef = useRef(false)

  const toggleLayer = (key: keyof LayerVisibility) =>
    setLayers(prev => ({ ...prev, [key]: !prev[key] }))

  // Map focus callback  allows sidebar to pan the map to a world coordinate
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

  const handleStoreCursorITgt = useCallback((target: { x: number; y: number; code: string }) => {
    const idx = itgtNextIndexRef.current++
    const defaultLabel = `TGT_${idx}`
    const id = `${Date.now()}-${Math.random().toString(36).slice(2, 7)}`
    setStoredITgtTargets(prev => [
      {
        id,
        label: defaultLabel,
        x: target.x,
        y: target.y,
        code: target.code,
      },
      ...prev,
    ])
  }, [])

  const handleRenameITgt = useCallback((id: string, nextLabel: string) => {
    setStoredITgtTargets(prev => prev.map(t => t.id === id ? { ...t, label: nextLabel } : t))
  }, [])

  const handleDeleteITgt = useCallback((id: string) => {
    setStoredITgtTargets(prev => prev.filter(t => t.id !== id))
  }, [])

  const handleDeleteAllITgt = useCallback(() => {
    setStoredITgtTargets([])
    itgtNextIndexRef.current = 0
  }, [])

  const handleCopyITgt = useCallback(async (code: string) => {
    try {
      await navigator.clipboard.writeText(code)
    } catch {
      // no-op
    }
  }, [])

  useEffect(() => {
    if (typeof window === 'undefined') return
    const mq = window.matchMedia('(pointer: coarse)')

    const detectInput = () => {
      const hasTouch = (navigator.maxTouchPoints ?? 0) > 0 || 'ontouchstart' in window
      setIsTouchInput(Boolean(mq.matches && hasTouch))
    }

    detectInput()
    if (typeof mq.addEventListener === 'function') {
      mq.addEventListener('change', detectInput)
      return () => mq.removeEventListener('change', detectInput)
    }
    mq.addListener(detectInput)
    return () => mq.removeListener(detectInput)
  }, [])

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

  useEffect(() => {
    const justLostLiveTelemetry = !hasLiveTelemetry && hadLiveTelemetryRef.current
    if (justLostLiveTelemetry) {
      // Entering offline/splash mode should start from a clean tactical session.
      setStoredITgtTargets([])
      itgtNextIndexRef.current = 0
      setFollowActivePlayer(false)
      setMapSessionKey(prev => prev + 1)
    }
    hadLiveTelemetryRef.current = hasLiveTelemetry
  }, [hasLiveTelemetry])

  const relayEndpoint = health?.relay
    ? `${health.relay.host}:${health.relay.port}`
    : '127.0.0.1:28800'
  const relayOffline = !connected && Boolean(health?.relay) && !health?.relay?.connected
  const welcomeStatusText = connected && exportStatus.phase !== 'idle'
    ? 'Loading world data...'
    : connected
    ? 'Connected to server - waiting for game data...'
    : relayOffline
    ? `Bridge online, relay offline (${relayEndpoint})`
    : 'Connecting to server...'
  const relayStatusHint = relayOffline
    ? `Start Athena relay and verify ${relayEndpoint} is listening.`
    : (!connected && healthError)
    ? `Bridge is unreachable: ${healthError}`
    : ''

  return (
    <div className={`app-shell ${leftSidebarCollapsed ? 'left-collapsed' : ''} ${rightSidebarCollapsed ? 'right-collapsed' : ''}`}>
      <aside className={`sidebar ${leftSidebarCollapsed ? 'collapsed' : ''}`}>
        <div className="sidebar-header">
          <span className="logo-text">ATHENA REMASTERED</span>
          <span className="header-right">
            <span className="version-label">v{APP_VERSION}</span>
            <a
              className="donate-link"
              href="https://www.paypal.com/donate/?business=G76WK9YDWUSAE&item_name=Athena+Remastered+Dev&EUR&no_note=0"
              target="_blank"
              rel="noreferrer"
              title="Support development"
            >Donate</a>
          </span>
        </div>
        <Sidebar
          frame={frame}
          connected={connected}
          onRequestWorld={handleRequestWorld}
          roadCount={roads.length}
          treeCount={exportStatus.treeCount > 0 ? exportStatus.treeCount : exportStatus.forestCount}
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
        <button
          className={`sidebar-toggle left ${leftSidebarCollapsed ? 'collapsed' : ''}`}
          type="button"
          onClick={() => setLeftSidebarCollapsed(prev => !prev)}
          title={leftSidebarCollapsed ? 'Expand left sidebar' : 'Collapse left sidebar'}
        >
          {leftSidebarCollapsed ? '>' : '<'}
        </button>
        <button
          className={`sidebar-toggle right ${rightSidebarCollapsed ? 'collapsed' : ''}`}
          type="button"
          onClick={() => setRightSidebarCollapsed(prev => !prev)}
          title={rightSidebarCollapsed ? 'Expand right sidebar' : 'Collapse right sidebar'}
        >
          {rightSidebarCollapsed ? '<' : '>'}
        </button>
        {/* Welcome overlay shown when no world has been loaded yet */}
        {!hasActiveWorld && (
          <div className="welcome-overlay">
            <img
              className="welcome-bg"
              src="/athena-default-bg.png"
              alt="Athena Remastered"
            />
            <div className="welcome-banner">
              <div className="welcome-title">ATHENA REMASTERED</div>
              <div className="welcome-status">
                {welcomeStatusText}
              </div>
              {relayStatusHint && (
                <div style={{ fontSize: 12, color: '#ffd166', marginTop: 6 }}>
                  {relayStatusHint}
                </div>
              )}
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
                      <span style={{ color: g.done ? '#2ecc71' : '#888' }}>{g.count}{g.done ? ' OK' : '...'}</span>
                    </div>
                  ))}
                </div>
              ) : (
                <div className="welcome-instructions">
                  <p>1. Launch <strong>AthenaWeb.exe</strong> (or <code>node server.js</code> in the bridge folder)</p>
                  <p>2. Launch Arma 3 with the <strong>@Athena</strong> mod enabled</p>
                  <p>3. Open <strong>Athena Desktop</strong> and connect - the relay starts on port 28800</p>
                  <p>4. Start or join a mission - live data will appear automatically</p>
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
            relayMarkers={relayMarkers}
            lazes={lazes}
            firedEvents={recentFired}
            firedImpacts={recentFiredImpacts}
            worldSize={worldSize}
            world={world}
            roads={mapRoads}
            forests={mapForests}
            locations={mapLocations}
            structures={mapStructures}
            elevations={mapElevations}
            contours={mapContours}
            vehicleMap={vehicleMap}
            locationMap={locationMap}
            layers={layers}
            onLayersChange={setLayers}
            renderMode={renderMode}
            shorelineRefreshToken={shorelineRefreshToken}
            onRegisterFocus={(fn) => { mapFocusRef.current = fn }}
            onRegisterPan={(fn) => { mapPanRef.current = fn }}
            onUserInteraction={() => setFollowActivePlayer(false)}
            storedITgtTargets={storedITgtTargets}
            firewillITgtTargets={firewillITgtTargets}
            onStoreCursorITgt={handleStoreCursorITgt}
            isTouchInput={isTouchInput}
          />
        </MapErrorBoundary>
        {exportStatus.phase !== 'idle' && (
          <div className="export-status-overlay">
            <div className="export-status-title">
              {exportStatus.phase === 'cached' ? 'Loaded from cache'
               : exportStatus.phase === 'complete' ? 'Export complete'
               : 'Exporting world data...'}
            </div>
            <div className="export-status-row">
              <span className={exportStatus.roadsComplete ? 'done' : 'pending'}>
                Roads: {exportStatus.roadCount}{exportStatus.roadsComplete ? ' OK' : '...'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.treesComplete ? 'done' : 'pending'}>
                Trees: {exportStatus.treeCount}{exportStatus.treesComplete ? ' OK' : '...'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.forestsComplete ? 'done' : 'pending'}>
                Forests: {exportStatus.forestCount}{exportStatus.forestsComplete ? ' OK' : '...'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.locationsComplete ? 'done' : 'pending'}>
                Locations: {exportStatus.locationCount}{exportStatus.locationsComplete ? ' OK' : '...'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.structuresComplete ? 'done' : 'pending'}>
                Structures: {exportStatus.structureCount}{exportStatus.structuresComplete ? ' OK' : '...'}
              </span>
            </div>
            <div className="export-status-row">
              <span className={exportStatus.elevationsComplete ? 'done' : 'pending'}>
                Elevations: {exportStatus.elevationCount}{exportStatus.elevationsComplete ? ' OK' : '...'}
              </span>
            </div>
          </div>
        )}
      </main>
      <aside className={`event-panel ${rightSidebarCollapsed ? 'collapsed' : ''}`}>
        <div className="panel-header">I-TGT TARGETS</div>
        <div style={{ padding: '10px 8px', display: 'flex', flexDirection: 'column', gap: 8, fontSize: 12 }}>
          <div style={{ color: '#8FA7C8', lineHeight: 1.45 }}>
            {isTouchInput
              ? <>Touch mode: tap map to set cursor, then use <strong>SAVE TGT</strong> on the map overlay.</>
              : <>Store current mouse map position: <strong>T</strong> or <strong>middle mouse button</strong>.</>}
          </div>

          {storedITgtTargets.length > 0 && (
            <button
              type="button"
              onClick={handleDeleteAllITgt}
              style={{
                alignSelf: 'flex-start',
                border: '1px solid #6E3B49',
                borderRadius: 4,
                background: '#2B1319',
                color: '#FFB9C7',
                cursor: 'pointer',
                fontSize: 11,
                fontWeight: 700,
                padding: '4px 8px',
              }}
            >
              Delete All
            </button>
          )}

          {storedITgtTargets.length === 0 ? (
            <div style={{ color: '#7A8796', fontSize: 12 }}>No stored I-TGT coordinates yet.</div>
          ) : (
            <div style={{ display: 'flex', flexDirection: 'column', gap: 6, maxHeight: 'calc(100vh - 180px)', overflowY: 'auto', paddingRight: 2 }}>
              {storedITgtTargets.map(target => (
                <div
                  key={target.id}
                  style={{
                    border: '1px solid #2C3C53',
                    borderRadius: 6,
                    background: 'rgba(11, 17, 30, 0.75)',
                    padding: 6,
                    display: 'flex',
                    flexDirection: 'column',
                    gap: 6,
                  }}
                >
                  <div style={{ display: 'flex', alignItems: 'center', gap: 6 }}>
                    <input
                      value={target.label}
                      onChange={(e) => handleRenameITgt(target.id, e.target.value)}
                      style={{
                        flex: 1,
                        minWidth: 0,
                        background: '#101827',
                        border: '1px solid #364963',
                        color: '#DDE9FF',
                        borderRadius: 4,
                        fontSize: 12,
                        padding: '4px 6px',
                      }}
                    />
                    <button
                      type="button"
                      title="Copy I-TGT code"
                      onClick={() => void handleCopyITgt(target.code)}
                      style={{
                        width: 24,
                        height: 24,
                        border: '1px solid #4D6382',
                        borderRadius: 4,
                        background: '#142239',
                        color: '#DDE9FF',
                        cursor: 'pointer',
                      }}
                    >
                      ⧉
                    </button>
                    <button
                      type="button"
                      title="Delete target"
                      onClick={() => handleDeleteITgt(target.id)}
                      style={{
                        width: 24,
                        height: 24,
                        border: '1px solid #6E3B49',
                        borderRadius: 4,
                        background: '#2B1319',
                        color: '#FFB9C7',
                        cursor: 'pointer',
                      }}
                    >
                      ✕
                    </button>
                  </div>
                  <div style={{ color: '#FFCD7E', fontFamily: 'Consolas, "Lucida Console", monospace', fontSize: 14, fontWeight: 700 }}>
                    {target.code}
                  </div>
                </div>
              ))}
            </div>
          )}
        </div>
      </aside>
    </div>
  )
}

export default App
