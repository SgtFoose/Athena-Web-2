import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { execSync } from 'node:child_process'

/*
 * Build label shown next to the version in the UI (see core/utils/version.ts).
 * This file is identical in Athena Web 2 (ui/) and Athena Remastered (Frontend/).
 *
 * - Test/dev builds (default): "test (<short git sha>)", plus "-dirty" when the
 *   working tree has uncommitted changes, so every test build is traceable.
 * - Release builds: set ATHENA_RELEASE=1 → empty label, the UI shows only the
 *   plain version (e.g. "v1.2.0").
 */

/** Short git commit of the source tree, "-dirty" if uncommitted changes exist; "nogit" outside a repo. */
function gitBuildId(): string {
  try {
    const run = (cmd: string) => execSync(cmd, { stdio: ['ignore', 'pipe', 'ignore'] }).toString().trim()
    const sha = run('git rev-parse --short HEAD')
    return run('git status --porcelain') ? `${sha}-dirty` : sha
  } catch {
    return 'nogit'
  }
}

const isRelease = process.env.ATHENA_RELEASE === '1'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    host: '0.0.0.0',   // listen on all interfaces so tablet can connect over LAN
    port: 5173,
  },
  define: {
    // Replaced at build time; declared in src/build-info.d.ts.
    __ATHENA_BUILD__: JSON.stringify(isRelease ? '' : `test (${gitBuildId()})`),
    __ATHENA_BUILD_DATE__: JSON.stringify(new Date().toISOString()),
  },
})
