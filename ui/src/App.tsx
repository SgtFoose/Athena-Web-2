/**
 * Athena Web 2 app root.
 *
 * Composes the shared UI core (`./core`, copied to Athena Remastered; see
 * docs/ui-core-sync.md) with Web2's own data layer (`./adapter`: Node bridge
 * over WebSocket/REST, Athena Desktop map cache, cache health banner).
 *
 * Web2-specific logic stays in this file:
 * - which world is active (live relay world, or a cached world picked offline)
 * - cache-mode handling on the bridge
 * - the welcome texts (AthenaWeb.exe, relay port 28800)
 * - default layer visibility
 */
import { useState, useRef, useCallback, useEffect, useMemo } from 'react'
// Shared UI core (copied to Athena Remastered; see docs/ui-core-sync.md)
import { AthenaMap, type FirewillITgtTarget } from './core/components/AthenaMap'
import { Sidebar } from './core/components/Sidebar'
import { AppShell } from './core/components/AppShell'
import { SidebarHeader } from './core/components/SidebarHeader'
import { ITgtTargetsPanel } from './core/components/ITgtTargetsPanel'
import { WelcomeOverlay } from './core/components/WelcomeOverlay'
import { ExportStatusOverlay } from './core/components/ExportStatusOverlay'
import { MapErrorBoundary } from './core/components/MapErrorBoundary'
import { useStaticMap } from './core/hooks/useStaticMap'
import { useAthenaLibrary } from './core/hooks/useAthenaLibrary'
import { useITgtTargets } from './core/hooks/useITgtTargets'
import { useFollowActivePlayer } from './core/hooks/useFollowActivePlayer'
import { useTouchInput } from './core/hooks/useTouchInput'
import { toFirewillITgtTargets } from './core/utils/firewillITgt'
import type { LayerVisibility, RenderMode } from './core/types/ui'
// Web2-only data layer (bridge WebSocket/REST, cache health)
import { MapCacheBanner } from './adapter/MapCacheBanner'
import { useAthenHub, isSameWorld } from './adapter/useAthenaHub'
import { useHealthCheck } from './adapter/useHealthCheck'
import './core/styles/app.css'

/** Fallback world size in metres before any world metadata is known. */
const DEFAULT_WORLD_SIZE_M = 10240

/** How often the bridge health endpoint is polled (cache banner, relay status). */
const HEALTH_POLL_INTERVAL_MS = 15_000

function App() {
  const {
    connected,
    frame,
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
  const firewillITgtTargets = useMemo<FirewillITgtTarget[]>(
    () => toFirewillITgtTargets(relayMarkers),
    [relayMarkers],
  )
  // User-selected world for pre-planning; auto-cleared when live game sends a world
  const [userSelectedWorld, setUserSelectedWorld] = useState('')
  const world     = liveWorld || userSelectedWorld || ''
  const hasActiveWorld = world.length > 0
  const hasMatchingGeometry = hasActiveWorld && geometryWorld.toLowerCase() === world.toLowerCase()
  // Case-insensitive: the relay may report "kunduz" while the cache's Map.txt says "Kunduz".
  const activeWorldInfo = isSameWorld(worldInfo?.nameWorld, world) ? worldInfo : null

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
    await selectWorld(worldName || '')
  }, [selectWorld])

  // Load pre-computed Athena Desktop cache (contour lines + metadata) for the active world
  const { staticInfo, contours } = useStaticMap(world || null)

  // worldSize: prefer cached/static map metadata first to avoid relay fallback-size drift
  // (for example Stratis reported as 10240 before worldinfo hydration), then live fallback.
  const worldSize = hasActiveWorld
    ? (activeWorldInfo?.size ?? staticInfo?.worldSize ?? frame?.world?.size ?? DEFAULT_WORLD_SIZE_M)
    : DEFAULT_WORLD_SIZE_M

  const mapRoads = hasMatchingGeometry ? roads : []
  const mapForests = hasMatchingGeometry ? forests : null
  const mapLocations = hasMatchingGeometry ? locations : []
  const mapStructures = hasMatchingGeometry ? structures : []
  const mapElevations = hasMatchingGeometry ? elevations : null
  const mapContours = hasActiveWorld ? contours : []

  // Load Athena Desktop vehicle/location classification library
  const { vehicleMap, locationMap } = useAthenaLibrary()

  // Health check: detect missing map cache and show first-time instructions
  const { health, error: healthError, refetch: refetchHealth } = useHealthCheck(HEALTH_POLL_INTERVAL_MS)
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
  const isTouchInput = useTouchInput()
  const itgt = useITgtTargets()
  const {
    followActivePlayer,
    setFollowActivePlayer,
    activePlayerAnchor,
    toggleFollowActivePlayer,
    mapFocusRef,
    mapPanRef,
  } = useFollowActivePlayer(frame, units, vehicles)
  // Bumped to remount AthenaMap (fresh Leaflet state) on world/session changes.
  const [mapSessionKey, setMapSessionKey] = useState(0)
  const previousWorldRef = useRef('')
  const previousConnectedRef = useRef(false)
  const hadLiveTelemetryRef = useRef(false)

  const toggleLayer = (key: keyof LayerVisibility) =>
    setLayers(prev => ({ ...prev, [key]: !prev[key] }))

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

  const { reset: resetITgtTargets } = itgt
  useEffect(() => {
    const justLostLiveTelemetry = !hasLiveTelemetry && hadLiveTelemetryRef.current
    if (justLostLiveTelemetry) {
      // Entering offline/splash mode should start from a clean tactical session.
      resetITgtTargets()
      setFollowActivePlayer(false)
      setMapSessionKey(prev => prev + 1)
    }
    hadLiveTelemetryRef.current = hasLiveTelemetry
  }, [hasLiveTelemetry, resetITgtTargets, setFollowActivePlayer])

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
    <AppShell
      sidebar={(
        <>
          <SidebarHeader />
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
            onToggleFollowActivePlayer={toggleFollowActivePlayer}
            renderMode={renderMode}
            onChangeRenderMode={setRenderMode}
            serverSettings={serverSettings}
            locations={locations}
            groups={groups}
            units={units}
            onFocusPosition={(posX, posY) => mapFocusRef.current(posX, posY)}
          />
        </>
      )}
      rightPanel={(
        <ITgtTargetsPanel
          targets={itgt.targets}
          isTouchInput={isTouchInput}
          onRename={itgt.rename}
          onDelete={itgt.remove}
          onDeleteAll={itgt.reset}
          onCopy={itgt.copy}
        />
      )}
    >
      {/* Welcome overlay shown when no world has been loaded yet */}
      {!hasActiveWorld && (
        <WelcomeOverlay
          statusText={welcomeStatusText}
          hint={relayStatusHint}
          exportProgress={connected && exportStatus.phase !== 'idle' ? exportStatus : null}
          instructions={(
            <>
              <p>1. Launch <strong>AthenaWeb.exe</strong> (or <code>node server.js</code> in the bridge folder)</p>
              <p>2. Launch Arma 3 with the <strong>@Athena</strong> mod enabled</p>
              <p>3. Open <strong>Athena Desktop</strong> and connect - the relay starts on port 28800</p>
              <p>4. Start or join a mission - live data will appear automatically</p>
            </>
          )}
        />
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
          storedITgtTargets={itgt.targets}
          firewillITgtTargets={firewillITgtTargets}
          onStoreCursorITgt={itgt.store}
          isTouchInput={isTouchInput}
        />
      </MapErrorBoundary>
      <ExportStatusOverlay status={exportStatus} />
    </AppShell>
  )
}

export default App
