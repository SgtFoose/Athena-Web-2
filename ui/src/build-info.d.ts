/**
 * Build-time constants injected by vite.config.ts (`define`).
 * Identical in Athena Web 2 and Athena Remastered.
 */

/** "" for release builds (ATHENA_RELEASE=1), otherwise e.g. "test (a1b2c3d)". */
declare const __ATHENA_BUILD__: string

/** ISO-8601 UTC timestamp of the build. */
declare const __ATHENA_BUILD_DATE__: string
