/**
 * Version label shown in the UI (shared core, see docs/ui-core-sync.md).
 *
 * Release builds show the plain version ("v1.2.0"). Test builds append the
 * build label from vite.config.ts ("v1.2.0-test (a1b2c3d)"), so the owner can
 * always tell which build is running, even when the version number hasn't
 * been bumped yet.
 */
import { APP_VERSION, APP_BUILD, APP_BUILD_DATE } from '../../appConfig'

/** e.g. "v1.2.0" (release) or "v1.2.0-test (a1b2c3d)" (test build). Shown in the sidebar footer. */
export const APP_VERSION_LABEL = APP_BUILD ? `v${APP_VERSION}-${APP_BUILD}` : `v${APP_VERSION}`

/**
 * Compact form for the narrow sidebar header: "v1.2.0-test" on test builds
 * (the commit is in the tooltip and the footer), "v1.2.0" on releases.
 */
export const APP_VERSION_SHORT_LABEL = APP_BUILD ? `v${APP_VERSION}-test` : `v${APP_VERSION}`

/** Tooltip text with the build timestamp, e.g. "Built 2026-11-22 14:05 UTC". */
export const APP_BUILD_TOOLTIP = `Built ${APP_BUILD_DATE.replace('T', ' ').slice(0, 16)} UTC`
