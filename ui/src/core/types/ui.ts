/**
 * UI-level types of the shared core (see docs/ui-core-sync.md).
 *
 * These describe UI state only (layer toggles, render mode, app capabilities),
 * not game data; the game data contract lives in ./game.ts. They used to be
 * defined in App.tsx, which made the core components depend on the app shell.
 */

/** Map background style: flat 2D map, or one of the two elevation heatmaps. */
export type RenderMode = '2d' | 'heatmap1' | 'heatmap2'

/**
 * Visibility of each map layer, toggled from the sidebar.
 *
 * The four marker flags (`armaMarkers`, `firewillITgtMarkers`, `aoMarkers`,
 * `spawnMarkers`) only have data in apps whose data source streams mission
 * markers (Web2 via the relay). `waypoints`, `lazes` and `projectiles` only
 * have data in apps with that telemetry (AR via its extension DLL).
 * Apps without the data simply keep those flags `false`.
 */
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

/**
 * Which optional data/features the app's data source can provide.
 *
 * Shared core code for these features is switched on or off by these flags,
 * so it doesn't depend on whether data happens to arrive. Each app declares
 * its own values in `appConfig.ts`.
 */
export interface AppCapabilities {
  /** Active laser designations (AR: extension DLL; Web2: not available). */
  lazes: boolean
  /** Projectile tracking and fired/impact events (AR only). */
  projectiles: boolean
  /** Killed/fired event feed (AR only). */
  killFeed: boolean
  /** Mission markers streamed by the data source (Web2: relay `Markers`). */
  relayMarkers: boolean
  /**
   * Show the "Waypoints" layer toggle in the sidebar. The waypoint layer itself
   * renders whenever group waypoint data (`wpx`/`wpy`) arrives; this flag only
   * controls whether the user can switch it off.
   */
  waypointToggle: boolean
}
