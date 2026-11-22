/**
 * App-specific configuration for Athena Web 2.
 *
 * The shared UI core (`./core`, see docs/ui-core-sync.md) reads every
 * app-specific value from this module only. Athena Remastered has its own
 * appConfig.ts with the same exports and its own values.
 */
import type { AppCapabilities } from './core/types/ui'

export { API_BASE } from './apiBase'
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
