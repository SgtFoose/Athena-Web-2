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
 * How the map component loads the two datasets it fetches itself (instead of
 * receiving them as props). Both Web2's bridge and AR's backend serve these
 * endpoints, but parameters and row order can differ per backend.
 */
export interface MapDataConfig {
  /** URL of the runtime tree list (`/api/game/trees` in both backends). */
  treesUrl: () => string
  /** URL of the raster land mask for a world at the given grid size. */
  landmaskUrl: (world: string, gridSize: number) => string
  /** Requested land-mask resolution in cells per side (Web2: 1536, AR used 1024). */
  landmaskGridSize: number
  /**
   * Row order of the decoded mask. `south-first`: row 0 is the southern edge
   * (worldY = 0), as Web2's bridge returns it. `north-first`: row 0 is the
   * northern edge, which is image order and needs no flip.
   */
  landmaskRowOrder: 'south-first' | 'north-first'
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
  /**
   * Offline world picker in the sidebar ("select a map" over the cached
   * Athena Desktop worlds). Web2 serves the picker via its bridge; apps without
   * it show "Waiting for a mission" until a mission is live.
   */
  worldPicker: boolean
}
