/**
 * App-specific configuration for Athena Web 2.
 *
 * The shared UI core (`./core`, see docs/ui-core-sync.md) reads every
 * app-specific value from this module only. Athena Remastered has its own
 * appConfig.ts with the same exports and its own values.
 */
import type { AppCapabilities, MapDataConfig } from './core/types/ui'
import { API_BASE } from './apiBase'

export { API_BASE }
export { APP_NAME, APP_VERSION } from './version'

/**
 * Donate link shown in the sidebar header (the owner's public PayPal.Me page).
 *
 * Why PayPal.Me: before v0.1.1 this was a `paypal.com/donate/?business=...`
 * link inherited from Athena Remastered. That donate page only works for
 * PayPal accounts with donations enabled; for this account PayPal answered
 * "this organization can't accept donations at the moment". PayPal.Me works
 * with a personal account and needs no extra parameters.
 */
export const DONATE_URL = 'https://paypal.me/jeroenvdvelden'

/**
 * Web2 reads Bus's original relay, so it has mission markers but no
 * laze/projectile/kill telemetry (that needs AR's custom extension DLL).
 */
export const CAPABILITIES: AppCapabilities = {
  lazes: false,
  projectiles: false,
  killFeed: false,
  relayMarkers: true,
  // Web2 has shown waypoints without a sidebar toggle since v0.0.x; kept as-is.
  waypointToggle: false,
}

/**
 * Endpoints served by the Node bridge (`bridge/server.js`). The bridge builds
 * the land mask from the cached Z=0 coastline with row 0 = south (worldY 0).
 */
export const MAP_DATA: MapDataConfig = {
  treesUrl: () => `${API_BASE}/api/game/trees`,
  landmaskUrl: (world, gridSize) =>
    `${API_BASE}/api/staticmap/${encodeURIComponent(world)}/landmask?gridSize=${gridSize}`,
  landmaskGridSize: 1536,
  landmaskRowOrder: 'south-first',
}
