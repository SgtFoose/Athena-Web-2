# ClientOnly Worklog (Athena Web 2)

Purpose: track every step, fix, and next action while migrating from the legacy backend/DLL approach to a Bus-compatible client-only architecture.

## Scope Rules
- Use only Bus-approved original mod runtime (extension + relay behavior as shipped).
- Do not modify Bus DLLs/relay binaries.
- Build around local client-side bridge + browser UI.
- This folder is the new working surface for cleanup and future GitHub publication.

## Timeline

### 2026-11-22 — UI core sync (Web2 → AR): analysis + Phase 1 step 1
- PR #4 merged by the owner (`8c06673`); local `main` updated.
- **Owner request:** investigate the differences between AR (`C:\AthenaRemastered`) and Web2 and bring them into sync, keeping in mind that AR has more functionality because of its custom DLL.
- Analysis (two read-only sub-agents plus my own checks):
  - Frontends differ by +3435/−579 lines (`AthenaMap.tsx` ~2200, `useAthenaHub.ts` ~800). `types/game.ts` is still the same contract (Web2 adds only `RelayMarker`/`GameFrame.markers`).
  - Identical in both repos: build configs, tsconfigs, ESLint config, dependencies, and all 169 shared `public/` assets. Web2 adds 12 `icons/handdrawn`.
  - About 80% of the `AthenaMap.tsx` changes are pure rendering and portable to AR. Web2's props are a superset of AR's. Both backends serve the two endpoints the map fetches directly (`/api/game/trees`, `/api/staticmap/{world}/landmask`).
  - Web2-only: the bridge/WebSocket data layer, cache picker, health banner, relay marker layers (AO/Spawn/Firewill).
  - AR-only (custom DLL): lazes, projectiles, killed/fired events, the EVENTS panel. Web2 still carries that code inert. `EventFeed.tsx` is unused in Web2.
  - Data-specific tuning to verify against AR data: landmask Y-flip/dilation, `hide`-roads-as-dirt, airport thresholds.
  - Glyphs `−`, `×`, `·` were deleted (not repaired) during an earlier mojibake cleanup in Web2.
  - AR history has a reverted earlier port (`7941dbf` → `1e3be1a`).
- **Owner decisions:**
  - approach "shared UI core + one-way sync (Web2 → AR)"
  - AR gets the same process as Web2 (CLAUDE.md with rules #1–#4, WORKLOG, branch + PR)
- Plan documented in the new `docs/ui-core-sync.md`:
  - Phase 1: Web2 core/adapter split
  - Phase 2: AR adoption with its own SignalR adapter + capabilities
  - Phase 3: sync script + checksum manifest
- **Phase 1 step 1** (branch `refactor/ui-core-split`, no functional changes):
  - `git mv` into `ui/src/core/` (AthenaMap, Sidebar, EventFeed, useStaticMap, useAthenaLibrary, types/game) and `ui/src/adapter/` (useAthenaHub, useHealthCheck, MapCacheBanner)
  - new `core/types/ui.ts` with `RenderMode` and `LayerVisibility` (moved out of `App.tsx`, which core components used to import), plus a new `AppCapabilities` type
  - new `ui/src/appConfig.ts`: the single source of app-specific values for the core (`API_BASE`, `APP_NAME`, `APP_VERSION`, `DONATE_URL` moved from `App.tsx`, and `CAPABILITIES`: lazes/projectiles/killFeed off, relayMarkers on)
  - relative imports updated
  - Validation: `npm --prefix ui run build` passed. No core file imports `adapter/` or `App`; core reads app values only via `appConfig`.
- **AR live test by the owner** (AR mod without BattlEye): the game seems to freeze right after "Athena Remastered: Connected. Requesting map data…".
  - Code reading: AR's backend auto-enqueues a full `world` export on every mission start (`Backend/Services/GameStateService.cs:102-103`). The Arma side then runs `ATH_fnc_World` (roads/structures/trees/forests/elevations exported in-game). This is likely the stall on larger maps.
  - Asked the owner for the map, whether it recovers after a few minutes, the backend console and the RPT `Athena Queue:` lines.
  - Candidate AR fix for Phase 2: skip the export when the world is already cached.
  - Owner clarified: the freeze happens in the **main menu**, with no mission loaded.
    - Root cause: AR's `init.sqf` is registered as `postInit` (`@AthenaRemastered/addons/athena/config.cpp:19-21`). It therefore also runs in the main-menu background scene, which is a real world (often Altis), and it has no main-menu guard.
    - So it connects, `getMission.sqf` reports the menu scene, and the backend auto-exports the whole menu world. Arma stalls in the menu.
  - Workaround given to the owner: start Arma with AR but without the AR backend (init waits in the handshake loop and does nothing heavy), load a small mission (VR/Stratis), then start the backend.
  - AR Phase 2 fixes to plan:
    - (1) main-menu guard at the top of AR `init.sqf` (exact check to be tested live)
    - (2) skip the auto world export when the world is already cached
  - Follow-up from the owner: the game was **not** frozen. On starting the AR backend (v1.1.6, port 5000), the AR UI immediately showed **Malden** (the last-used world) while Arma was in the main menu, which looks like a stuck game.
    - Cause: `Backend/Services/GameStateService.cs:21-22` calls `LoadCachedWorld()` in the constructor ("Auto-load last-used world from disk cache so the map renders without the game", `:845`). The console confirms `Loaded cached world 'Malden'` before `Now listening`.
    - AR also reads the Athena Desktop cache (`StaticAthenaCacheService`), like Web2.
    - Planned AR fix (Phase 2): drop the startup auto-load and show the shared core's welcome screen until a mission arrives (Web2 behaviour since v0.0.4). Added to `docs/ui-core-sync.md` ("AR-side fixes planned for Phase 2").
  - Owner then loaded a mission on Malden. AR showed "Rendering map for: Malden — This may take a few minutes…" and "Rendering map geometry for 'Malden'", and the game ran at ~16 FPS during the export, even though the backend had already loaded Malden from cache at startup. This confirms AR fix (2): skip the auto export for cached worlds.
  - Owner's AR v1.1.6 (Steam Workshop) live view on Malden: huge square forest blocks not following the real forest, a stair-stepped coastline, sidebars that can't collapse, and no I-TGT.
    - Noted in `docs/ui-core-sync.md`: the forest grid fix lives in Web2's bridge, so AR needs it in its C# backend too.
    - Stored I-TGT is UI-only and comes with the core. Firewill I-TGT markers need AR to stream mission markers (optional AR feature via `allMapMarkers`).
  - Owner request: the **GitHub structure and releases** of AR must be aligned with Web2 too. AR has 0 releases and 0 tags, a nested `AthenaRemastered/` source folder, a duplicate root `@AthenaRemastered/`, tracked `publish/wwwroot`, no WORKLOG/CLAUDE.md, and an outdated About text. Added the section "AR repository and release alignment" to `docs/ui-core-sync.md` (process files, ruleset, layout decision, releases with tags, README/About), to be done on AR branches with approval per step.
- **Phase 1 step 2: app shell into the core** (no functional changes):
  - New core components:
    - `AppShell.tsx` (3-column layout + collapsible sidebars)
    - `SidebarHeader.tsx` (name/version/donate from `appConfig`)
    - `ITgtTargetsPanel.tsx`
    - `WelcomeOverlay.tsx` (status/hint/instructions supplied by the app)
    - `ExportStatusOverlay.tsx`
    - `MapErrorBoundary.tsx`
  - New core hooks: `useITgtTargets.ts`, `useFollowActivePlayer.ts` (re-pan threshold as the named constant `FOLLOW_REPAN_MIN_DISTANCE_M = 2.5`), `useTouchInput.ts`.
  - New core util: `utils/firewillITgt.ts` (`toFirewillITgtTargets`).
  - `App.tsx` is now the Web2 composition: bridge hook, world selection/cache mode, health banner, Web2 welcome texts, layer defaults. Named constants: `DEFAULT_WORLD_SIZE_M`, `HEALTH_POLL_INTERVAL_MS`. JSX structure and CSS classes are unchanged.
  - Validation:
    - `npm --prefix ui run build` passed
    - ESLint: no new findings (App's existing 7 → 4, one existing `set-state-in-effect` moved with the code into `useFollowActivePlayer`; the AthenaMap findings already existed)
    - Simulation (fake relay + fake 5120 m Kunduz cache), headless Chrome:
      - live view identical to the pre-refactor screenshot (layout, centred island, sidebar, I-TGT panel, both collapse buttons); only the branding is the new v0.1.1 one
      - with the relay stopped, the welcome screen shows "Bridge online, relay offline (127.0.0.1:28801)", the hint and the Web2 steps
  - Committed `75296b6`.
- **Phase 1 step 3: capabilities** (`b6a97af`):
  - `CAPABILITIES` now gates the laze/projectile panes and the Fire Control panel in `AthenaMap`, plus the sidebar toggles: marker toggles need `relayMarkers`; Waypoints / Active Lazes / Projectile Tracking need `waypointToggle` / `lazes` / `projectiles`, in AR's original order.
  - New flag `waypointToggle`: Web2 keeps rendering waypoints without a toggle, as before.
  - Web2 UI is unchanged (same toggle list; those layers never had data in Web2).
- **Phase 1 step 4: glyph repair** (`4487532`):
  - restored characters deleted in the earlier mojibake cleanup: zoom slider `−` and `{scale}×`, unit tooltip `side · rank`, Fire Control `shooter · weapon`, ORBAT group `position · count`
  - AR has these as mojibake (`Â·`), so they were restored by intent rather than copied
  - kept the readable ASCII replacements (`--`, `Time:`)
  - verified no `Â`/`â€` remain in `ui/src`; headless screenshot of the slider shows `+`, `−`, `0.5×`
- **Phase 1 step 5: `MAP_DATA`:**
  - new `MapDataConfig` type plus `MAP_DATA` in `appConfig.ts` (`treesUrl`, `landmaskUrl`, `landmaskGridSize` 1536, `landmaskRowOrder` `south-first`)
  - `AthenaMap` no longer builds URLs or hard-codes the flip
  - Found in AR: `Backend/Controllers/StaticMapController.cs:65` returns the land mask with **row 0 = north**, the opposite of Web2's bridge. AR's appConfig must use `north-first`, or its land fill would render upside down. Documented in `docs/ui-core-sync.md` (appConfig contract table).
  - Validation: build passed; simulation screenshot identical to step 2 (island centred), slider glyphs visible.
  - Committed `966b6bc`.
- **AR live test, projectile tracking** (owner, AR v1.1.6, Malden): M5 Sandstorm MLRS fired 230 mm rockets at grid 066047 (artillery computer: DIST 3014, MIN 2846, MAX 3829, ETA 33 s). AR's "Recent Shots" received both rockets (`rockets_230mm_GAT · R_230mm_HE`), but the AR map showed no projectile track or Fire Control panel. Owner: "there are also bugs in Projectile tracking, a function Web 2 can't have." Exact expected vs. actual behaviour to be clarified and fixed in AR Phase 2 with live testing.
  - Follow-up from the owner: the **M4 Scorcher does trigger** Fire Control: Lock FREE, Src HDG, ETA 5 s, target 5180,11729, with a dashed track straight north from the vehicle.
    - Read-only analysis of AR `Frontend/src/components/AthenaMap.tsx`: with no target data for indirect fire, AR falls back to the vehicle (hull) heading, not the gun azimuth or the artillery computer target. The MLRS is recognised (`rocket` profile, :393) but no track is drawn; suspected missing target plus narrow `maxTrackAngleDeg` (unverified). Code smell: missile match on `includes('at')` (:405).
    - Fix direction (AR Phase 2): send the projectile's initial velocity from `Fired` via SQF/DLL and compute the ballistic impact. Documented in `docs/ui-core-sync.md` ("Projectile tracking bugs observed live").
- **Owner decision:** AR-only features (lazes, projectile tracking, Fire Control, kill/shot feed) move out of the shared core into the AR repo. The core offers extension slots instead.
- **Phase 1 step 6: AR-only code out of the core:**
  - `ui/src/core/components/AthenaMap.tsx` (4906 → 3984 lines). A one-off node script (scratchpad, not committed) removed blocks only via unique start/end markers and aborts without writing on any mismatch; the first run aborted on a CRLF-sensitive marker and wrote nothing. Removed:
    - projectile/laze types and helpers (`normDeg` … `buildPredictedPath`; kept `clamp` and `toITgt8`, which other code uses)
    - `ProjectileDebugEntry`
    - the laze rendering and projectile prediction effects in `LayerManager`
    - the projectile refs and the 1 s `clockMs` timer
    - the Fire Control panel
  - Firing pulse kept as a generic feature: `recentShooterIds` now comes from the new optional prop `pulseUnitIds` (stable empty default `NO_PULSE_UNITS`).
  - New `AthenaMap` extension slots: `mapExtensions` (inside `MapContainer`) and `overlayExtensions` (over the map). Panes `athena-laze` / `athena-projectile` stay reserved for AR.
  - Removed props `lazes` / `firedEvents` / `firedImpacts`; `App.tsx` no longer passes them. Deleted the unused `core/components/EventFeed.tsx`.
  - The removed Web2 copy remains in git history at `966b6bc` for AR Phase 2. Documented in `docs/ui-core-sync.md` ("AR-only features live in the AR repo"), with the capabilities table updated. `CLAUDE.md` core rules updated.
  - Validation:
    - build passed
    - ESLint: same 11 findings as before, no new ones
    - simulation screenshot identical to step 5
    - One run logged a Leaflet console error `Cannot read properties of undefined (reading '_leaflet_pos')`, on the first cold page load right after starting the Vite dev server. Not reproduced in 8 further runs of the new code (0/8). Baseline without step 6 (stashed): 0/4. Most likely Vite's first-load dependency optimisation reload, which the production build doesn't do. Logged as unconfirmed; watch for it.
- Docs: `docs/ui-core-sync.md` now has the appConfig contract, the core component list, the capabilities table and status.
- Owner approved the push. Checked identity first (noreply email, `gh` user SgtFoose). Pushed `refactor/ui-core-split` (9 commits) and opened **PR #5**: https://github.com/SgtFoose/Athena-Web-2/pull/5 ("refactor(ui): shared UI core for Web2 and Athena Remastered (Phase 1)"). No release needed: no functional change for Web2 users. `CLAUDE.md` repository layout updated, and new "Shared UI core rules" added to the working rules.
  - Found: the welcome background `ui/public/athena-default-bg.png` contains the text "ATHENA REMASTERED – LOADING BATTLEFIELD INTEL…" (inherited from AR). Backlog: make the welcome image per-app via `appConfig`.
  - The AR screenshot (Malden) shows the pre-fix rendering: forest blocks spilling over the coast, a stair-stepped coastline, opaque dark forest. That confirms the value of porting the shared core.

### 2026-11-22 — v0.1.1 Published + end-user start guide in release notes
- PR #3 (`release/v0.1.1`) merged by the owner as `7a697ed`. Local `main` updated, release branch deleted.
- Published the GitHub Release (owner approved): `gh release create v0.1.1 bridge/dist/AthenaWeb-0.1.1.exe --target main --title "Athena Web 2 v0.1.1" --notes-file .github/release-notes-template-v0.1.1.md --latest`.
  - Validation: `gh release list` → v0.1.1 **Latest**, not a draft or pre-release. `releases/latest` → v0.1.1. Tag `v0.1.1` → `7a697ed`.
  - **Asset parity:** release asset `AthenaWeb-0.1.1.exe` digest `sha256:513722d8f49859628fc7b1378622ef5c76df06a7b7d97deb71a0cc4ac2eb6ec4` = local `bridge/dist/AthenaWeb-0.1.1.exe`. Size 62,044,389.
  - The owner briefly saw v0.1.0 as latest; that was before the create call finished.
- Drafted a Steam reply for the Kunduz reporter (owner posts it).
- Owner request: add clear start instructions for EXE users to the release notes, without the Node.js dev guide.
  - Rewrote `.github/release-notes-template-v0.1.1.md` for end users: Quick Start (what you need, start steps, `Local:` vs `Network:` address, firewall, keep the console open), first time on a new map (Athena Desktop Export → restart), troubleshooting table, SmartScreen, What's New, Known Limitations. Dropped the dev-only `WEB_PORT` line.
  - Facts checked in code first: the Athena Desktop Workshop link comes from `MapCacheBanner.tsx:98` (id `668999292`); the console output `Local:` / `Network: … ← open on your phone` comes from `server.js:1889-1890`.
  - Updated the live release body with `gh release edit v0.1.1 --notes-file …`. Verified: it has Quick Start, first-time map and troubleshooting sections, no `npm`/`node server` mentions, and the asset is still attached.
  - `CLAUDE.md` release step 4 now requires this end-user structure, with no Node.js instructions, for future releases.
  - The first combined commit+push+PR command was blocked by Claude Code's auto-mode check, because the push wasn't explicitly requested yet; nothing ran. Committed locally (`c4c605b`), asked the owner, then pushed `docs/release-start-instructions` and opened PR #4: https://github.com/SgtFoose/Athena-Web-2/pull/4.

### 2026-11-22 — v0.1.1 Release: custom map world-size fix
- PR #2 (`fix/unknown-world-size`) merged by the owner as `365addc`. Local `main` updated, merged branch deleted.
- Stopped the local simulation (fake relay, bridge, Vite dev server); ports 3000, 3001, 5173 and 28801 confirmed free.
- At the owner's request, copied the test build to `D:\AthenaWeb-0.1.1-test\` (`AthenaWeb-0.1.1-test-worldsize.exe`, SHA256 `883D3C8D…` matches the original, plus `TEST-INSTRUCTIONS.txt`).
- **Live validation by the owner** (home PC, test EXE, iPad client, Arma 3 + Bus `@Athena`, Domination mission on **VR**):
  - VR wasn't cached at first ("No cached data for VR"), so the fix had no real size to use (expected; fallback 10240).
  - Owner ran the Athena Desktop **Export**; Arma reported "All world data supplied to relay". The cache `Documents\Athena\Maps\VR\` was created (`Map`, `Objects`, `Forests`, `Locations`, `Trees`, `Height/`; no roads on VR). `Map.txt` `WorldSize` = **8192**.
  - After restarting the EXE: player NightHawk at X 482 / Y 3326.68, matching Athena Desktop grid `G:004033`.
  - **Map-extent check:** dragged to the map's left edge at zoom 0.1 and tapped the right edge → **X 7976.96**, i.e. about 8192 (real VR size), not 10240 (old guess). **Fix confirmed live.**
- Release `v0.1.1` prepared on `release/v0.1.1`:
  - Version sync:
    - `ui/src/version.ts` `APP_VERSION` → `0.1.1` (rewritten without BOM after PowerShell 5 `Set-Content -Encoding utf8` added one)
    - `ui/package.json` + `ui/package-lock.json` → `0.1.1` via `npm version --no-git-tag-version` (was `0.0.2`; backlog item closed)
    - `README.md`: version, tracked EXE path, new v0.1.1 highlights, Quick Start / SmartScreen / maintainer / first-time export EXE names, workshop change-note path
    - `CHANGELOG.md` `[0.1.1]` entry
    - `CLAUDE.md` current version and open items
  - Release notes: `.github/release-notes-template-v0.1.1.md` (download path, SmartScreen + `Unblock-File`, highlights, first-time export reminder, known limitations).
  - Workshop: `@AthenaWeb2/workshop-change-note-0.1.1.txt`; `Update-Workshop.ps1` default change note → 0.1.1. Publishing to the Workshop remains the owner's call.
  - **Added to v0.1.1 at the owner's request:** the owner reported the PayPal Donate link was broken.
    - The link (inherited from AR; AR has the identical link) contained a bare `&EUR` parameter instead of `currency_code=EUR`, and `item_name=Athena+Remastered+Dev`.
    - Owner chose to keep the same PayPal account ID and fix the parameters, and to include the "ATHENA REMASTERED" branding fix in this release.
    - Changes, committed separately (final commit `dfb40fa`):
      - `ui/src/App.tsx`: `DONATE_URL` constant (with comment); first attempt used `item_name=Athena+Web+2&currency_code=EUR`, final value is the PayPal.Me link (see below)
      - `ui/src/App.tsx`: sidebar header, welcome title and image alt now use the existing `APP_NAME` constant
      - `ui/index.html`: page title `frontend` → `Athena Web 2`
    - Owner tested the corrected `donate/?business=` link: PayPal answered "Deze organisatie kan op dit moment geen donaties accepteren" ("this organization can't accept donations at the moment"). The URL is now well-formed, but that donate page needs a PayPal account with donations enabled.
    - Owner chose PayPal.Me and provided their PayPal.Me settings URL. `DONATE_URL` → public link `https://paypal.me/jeroenvdvelden` (locale/country query parameters dropped). Folded into the fix commit (amended, unpushed) → `dfb40fa`.
    - Process note: the first release commit (`31825ee`, unpushed) already held a 62 MB EXE. It was undone locally (`git reset --soft`) before rebuilding, so only the final EXE enters history.
    - Process note: Windows PowerShell 5 mangles double quotes in native-command arguments (a here-string passed to `git commit -m` was split into pathspecs). Commit messages containing quotes now go through `git commit -F <file>`.
  - Build (final):
    - `npm --prefix ui run build` → bundle `index-CQGC_xb4.js`
    - `robocopy ui\dist bridge\wwwroot /MIR`
    - `npm run build:exe` → `bridge/dist/AthenaWeb.exe`
  - Dist parity: `bridge/dist/AthenaWeb-0.1.1.exe`, `bridge/dist/AthenaWeb.exe`, `@AthenaWeb2/AthenaWeb-0.1.1.exe` and `@AthenaWeb2/AthenaWeb.exe` are identical (SHA256 `513722d8f4985962…`, 62,044,389 bytes). Removed the tracked `AthenaWeb-0.1.0.exe` from both folders. Updated `repo_dist_contents.json` (path, blob SHA `5d47cec8…`, SHA256, size).
  - Smoke test of `AthenaWeb-0.1.1.exe`:
    - page title `Athena Web 2`, bundle `assets/index-CQGC_xb4.js`
    - bundle contains `https://paypal.me/jeroenvdvelden`, "Athena Web 2" and `0.1.1`; the old PayPal ID and "ATHENA REMASTERED" are gone
    - `/api/health` returns 503 by design when no map cache exists (this machine has none; `server.js:493`)
- Owner noted `paypal.com/paypalme/jeroenvdvelden` also works. Kept the official short form `paypal.me/jeroenvdvelden`, which redirects there; no rebuild needed.
- Owner approved the push. Checked identity first (noreply email, `gh` user SgtFoose). Pushed `release/v0.1.1` (`dfb40fa`, `c74dbb1`) and opened PR #3: https://github.com/SgtFoose/Athena-Web-2/pull/3.
  - GitHub warned `GH001: Large files detected` for the ~59 MB EXEs: over the recommended 50 MB, under the hard 100 MB limit. Same as earlier releases. Possible future improvement: Git LFS, or stop tracking EXEs in the repo and rely on Release assets.
- Next: owner merges PR #3. Then, with the owner's go-ahead: tag `v0.1.1` and a GitHub Release with `AthenaWeb-0.1.1.exe`, verifying the asset SHA256 against local. Optional Workshop update.

### 2026-11-22 — v0.1.x Investigation: custom maps render squeezed into the bottom-left corner (Workshop report)
- PR #1 (`chore/claude-setup-toolchain`) was merged by the owner as `c197289`. Local `main` updated, merged branch deleted. Created `fix/unknown-world-size` from `main` for this investigation.
- **User report (Steam Workshop comments):** on the custom map **Kunduz**, live in-game, the player was shown and the MAP DATA panel listed all cached objects. But "everything on the map was condensed into the bottom-left corner of the map", so there was no usable map. The same user says AR and the original Athena Desktop work on that map.
- **Owner check:** v0.1.0 EXE on a fresh install with vanilla **Malden** works fine.
- **Root cause found (code reading, `ui/src`):**
  - `useAthenaHub.ts` `mapWorldSize()` (line 78) hard-codes world sizes for five vanilla maps only (Altis 30720, Tanoa 15360, Malden/Enoch 12800, Stratis 8192). **Every other world falls back to 10240.**
  - On **every** relay `state` message (several per second), `useAthenaHub.ts:459-461` calls `setWorldInfo(mapped.worldInfo)` with that relay-derived guess. That overwrites the correct `worldInfo` hydrated from the Athena Desktop cache (`/api/game/worldinfo` → `Map.txt` `WorldSize`, `useAthenaHub.ts:363-365`).
  - `App.tsx:181-182` picks `activeWorldInfo?.size` **first**, before `staticInfo?.worldSize` (cache). So in live sessions the map extent is always the hard-coded guess.
  - The geometry itself (roads, structures, contours) is converted by the bridge with the real `Map.txt` `WorldSize`, so it's placed at true world metres. Result: on any non-listed map smaller than 10240 m, all content sits in the bottom-left part of a 10240 m map (Leaflet `CRS.Simple` origin is bottom-left). That matches the report. On larger maps the top/right would be cut off.
  - It isn't visible on Malden/Altis/Stratis/Tanoa/Livonia because the hard-coded guess happens to be right there. It's also not visible offline (no relay `state` overwriting the cache value).
  - Likely also affected: VR and CDLC/community maps (any world name not in the table).
- Could not reproduce locally: this machine has no Arma install or Athena map cache (`Documents\Athena\Maps` missing).
- Owner approved the fix. Changes:
  - `ui/src/hooks/useAthenaHub.ts`:
    - documented `mapWorldSize()` as last resort only, and added the named constant `FALLBACK_WORLD_SIZE_M`
    - new `isSameWorld()` (case-insensitive, exported)
    - new `resolveLiveWorldInfo()`: keeps the cache-hydrated `WorldInfo` when it's for the same world as the relay frame
    - new `cacheWorldInfoRef`, set or cleared on every `/api/game/worldinfo` hydration
    - the relay `state` handler now calls `setWorldInfo(resolveLiveWorldInfo(...))` instead of overwriting with the guessed size
  - `ui/src/App.tsx`: `activeWorldInfo` now uses `isSameWorld()`. Before, the relay's "kunduz" never matched the cache's "Kunduz".
- Validation:
  - `npm --prefix ui run build` passed.
  - ESLint on the two changed files: 8 problems before and 8 after, identical rule hits. All are existing `react-hooks/*` findings in untouched code; none are new.
  - **Local simulation without Arma** (scratchpad, not committed):
    - fake relay `sim/fake-relay.js` on TCP 28801, answering `next` polls with a v2 frame for world `kunduz` and one player at 2560,2560
    - fake cache `sim/maps/Kunduz/Map.txt` (`WorldName "Kunduz"`, `WorldSize 5120`, a test value) plus a square Z0 island
    - bridge run with `RELAY_PORT=28801`, `ATHENA_MAPS_DIR=<sim>/maps`
    - bridge output confirmed the bug conditions: `/api/game/worldinfo` → `Kunduz`, size 5120; `/api/staticmap/kunduz` → worldSize 5120; WebSocket `state` → `map=kunduz`, unit at 2560,2560. The old code would have used `mapWorldSize('kunduz')` = 10240.
    - UI dev server started on :5173 against this setup for a visual check by the owner.
    - First visual check showed the whole map as land with no player marker. Both causes were in the test setup, not the app:
      - the fixture's `Height/Z0.txt` used `{x,y}` objects, but Athena stores contour points as `"x,y"` string tokens (`parseGridCoordToken`). So there was no coastline and the UI fell back to all-land.
      - the Units layer is OFF by default (`App.tsx:227`).
    - Fixed the fixture and restarted the bridge (contour memo). `/contours/0` → 1 line (512…4608 m); landmask 41616/65536 land cells (63.5%, expected 64% for a 4096 m island in a 5120 m world).
    - **Before/after visual proof** with headless Chrome (`--headless=new --window-size=1600,1000 --virtual-time-budget=10000 --screenshot`), same simulation, same default view (zoom 0.5, centred on the map centre):
      - **before** (fix temporarily stashed; Vite hot-reloaded the old code): island in the **bottom-left** of the view, the rest is water. The UI used a 10240 m extent, so the 5120 m world landed in the lower-left quarter. This reproduces the Workshop report.
      - **after** (fix restored): island fills the view, centred, with symmetric water corners. The UI used the cache size, 5120 m.
    - The owner also checked the fixed build in their own browser: the player "Tester" renders at the map centre (with the Units layer on).
- Owner confirmed in their own browser at zoom 0.1: the island is centred in the map, with the player in the middle. Also noticed (not caused by this fix, added to the backlog):
  - a small gap in the coastline stroke at the ring start/end (closed-ring smoothing doesn't close the ring)
  - "Exporting world data…" stays visible during the simulation
- Committed `99129f8`, pushed `fix/unknown-world-size`, and opened PR #2: https://github.com/SgtFoose/Athena-Web-2/pull/2. Identity checked first (noreply email, `gh` user SgtFoose).
- **Test EXE for live validation** (not a release, nothing committed):
  - `npm --prefix ui run build` → `robocopy ui\dist bridge\wwwroot /MIR` → `npx @yao-pkg/pkg . --targets node22-win-x64 --compress GZip --output <scratchpad>\AthenaWeb-0.1.1-test-worldsize.exe` (62 MB)
  - then `git restore bridge/wwwroot` + `git clean -fd bridge/wwwroot`, so tracked `wwwroot` and `bridge/dist` are unchanged
  - smoke test: the EXE served the fixed bundle `index-j0ipix27.js` and connected to the fake relay
- **Doc bug found during the smoke test:** the EXE ignored `PORT=3005` and auto-selected 3001. The bridge reads **`WEB_PORT`** (`server.js:13,29`), but `README.md` and `CLAUDE.md` listed `PORT`. Fixed the env-var tables in both.
- Remaining: owner runs the test EXE live in Arma on a world that isn't in the size table (e.g. VR). Then the v0.1.1 release steps (version sync, wwwroot/dist sync, CHANGELOG, release notes, GitHub Release).

### 2026-11-22 — Tooling: Move from GitHub Copilot to Claude Code + CLAUDE.md consolidation
- Cloned `https://github.com/SgtFoose/Athena-Web-2` into the new workspace `C:\Athena-Web-2` (835 files, HEAD `c459a3f` "release: finalize v0.1.0 map rendering and dist/workshop sync").
- Surveyed every markdown file in the repo (excluding `node_modules`):
  - `.github/copilot-instructions.md` (Copilot rules), `README.md`, `CHANGELOG.md`, `WORKLOG.md`
  - `.github/release-notes-template-v0.0.4` … `v0.1.0.md`, `.github/release-changelog-v0.0.8.md`
  - `ui/icon-mapping.md` (vehicle class → icon table)
- Created `CLAUDE.md` at repo root as the canonical Claude Code context, merging:
  - all Copilot rules (product identity, repo/capability boundaries, safety check, release/dist/doc/packaged-UI sync rules, GitHub release distribution, token handling), reworded for Claude
  - architecture, repo layout, commands, and bridge env vars from `README.md` + `bridge/package.json` + `ui/package.json`
  - distilled technical knowledge from `WORKLOG.md`: relay protocol (`<ath_sep>` framing, `General` registration, `next` polling, `mapexport`), bridge REST/diagnostic endpoints, cache import rules, Bus-exact rendering values, v0.1.0 smoothing/airport rules, UI behaviors to preserve
  - release-notes template requirements and open items/backlog pointers
- New top-priority rule in `CLAUDE.md`: every step must be logged in `WORKLOG.md` (newest-first timeline, what/why/files/validation, failed attempts included).
- Updated the stale "current status" (Copilot file said v0.0.8) to v0.1.0 in `CLAUDE.md`; noted that older WORKLOG paths (`C:\Users\jvdv2\...`) are from the previous machine.
- Left `.github/copilot-instructions.md` and the other source `.md` files untouched (history/reference); `CLAUDE.md` takes precedence for process rules.
- Validation: docs-only change; no code, build, or dist artifacts touched. Nothing committed yet.

#### 2026-11-22 follow-up — Language, commenting, and GitHub account rules
- Added three new non-negotiable rules to `CLAUDE.md`:
  - **Rule #2:** everything written into the repo is English (code, comments, UI text, logs, commits, docs), even though the owner usually gives instructions in Dutch.
  - **Rule #3:** all written or touched code must be well commented for outside collaborators (file headers, JSDoc/TSDoc, explain why for protocol quirks, Bus-exact constants, coordinate transforms, map-specific workarounds; named constants for magic numbers).
  - **Rule #4:** account separation. This repo belongs to the personal `SgtFoose` GitHub account, which is not the owner's work account.
- Found that no git identity was configured on this machine (global and local `user.name`/`user.email` were empty).
- Checked commit history: every past commit is authored as `Sgt.Foose <17387175+SgtFoose@users.noreply.github.com>`.
- Set the **repo-local** git config (only affects this repo):
  - `user.name = Sgt.Foose`
  - `user.email = 17387175+SgtFoose@users.noreply.github.com`
  - `credential.username = SgtFoose` (Git Credential Manager had only a different account stored, so pushes are now pinned to SgtFoose)
- Noted that the `gh` CLI is not installed on this machine.
- Validation: `git config --local --list` shows the three values above.

#### 2026-11-22 follow-up — First commit from the new workspace
- Committed `CLAUDE.md` + `WORKLOG.md` as `04c71da` ("docs: add CLAUDE.md and log move from Copilot to Claude Code"). Checked the author before committing: `Sgt.Foose <17387175+SgtFoose@users.noreply.github.com>` (noreply address, so no personal email is exposed).
- `git push origin main` from Claude's shell failed: `Cannot prompt because user interactivity has been disabled`. Git Credential Manager needs an interactive login the first time.
- Next step: the owner runs the first push from their own VS Code terminal and signs in as `SgtFoose`. Later pushes reuse the stored credential.
- A second push attempt from Claude's shell (with interactive login enabled) was blocked by Claude Code's auto-mode permission check, which doesn't allow pushes that involve a credential login.
- The owner pushed from their own terminal: `c459a3f..04c71da  main -> main`. Checked with `git fetch`: `origin/main` = `04c71da`, author `Sgt.Foose <17387175+SgtFoose@users.noreply.github.com>`, so no personal email is exposed on GitHub.
- GitHub credentials for `SgtFoose` are now stored in Git Credential Manager on this machine.
- The push output showed `Bypassed rule violations for refs/heads/main: Changes must be made through a pull request.` `main` has a ruleset that requires PRs; the owner's admin rights bypassed it for this push.

#### 2026-11-22 follow-up — Dev toolchain check on the new machine
- The owner tried `npm --prefix ui run build` and got `npm` not recognized.
- Checked the machine: `node`, `npm`, and `gh` are not installed (no `%ProgramFiles%\nodejs`, no nvm). `winget` v1.29.380 is available.
- Node is needed for `npm --prefix ui run build`, the bridge (`node server.js`), and `npm run build:exe` (pkg target `node22-win-x64`). Waited for the owner's OK before installing.
- The owner approved. Installed via winget: `OpenJS.NodeJS.22` → Node v22.23.5 / npm 10.9.9, and `GitHub.cli` → gh 2.105.0. Node 22 matches the pkg EXE target.
- Owner decision: from now on, work on branches and open PRs to `main`, following the GitHub ruleset. Added this workflow to `CLAUDE.md`, along with toolchain/PATH notes.
- Created branch `chore/claude-setup-toolchain`.
- Validation:
  - `npm --prefix ui ci` succeeded. npm audit reports advisories; not addressed yet.
  - `npm --prefix ui run build` passed (`tsc -b && vite build`, 84 modules, JS bundle 476.57 kB / 144.82 kB gzip). The pre-v0.1.0 `ui/src/App.tsx` nullability error is resolved, so I removed that open item from `CLAUDE.md`.
  - `node --check bridge/server.js` passed.
  - The build left no tracked files changed (`ui/dist` is ignored, and `bridge/wwwroot` wasn't touched).
- Small finding: `ui/package.json` still reports `"version": "0.0.2"` (`frontend@0.0.2`), while `APP_VERSION` is `0.1.0`. Not changed yet; worth syncing at the next release.
- Committed `1ee3a0c` on `chore/claude-setup-toolchain` (not pushed yet; waiting for the owner to run `gh auth login` as SgtFoose).

#### 2026-11-22 follow-up — Workspace layout question: Web2 + Athena Remastered (AR)
- The owner asked whether to work from two folders on `C:\` or one combined folder for Web2 and Athena Remastered (now called **AR**).
- Context from the owner:
  - AR is older and is missing bug fixes that Web2 has.
  - AR has capabilities Web2 can't use (it isn't relay-only), so feature flow is one-way: Web2 → AR is fine, AR → Web2 is not.
- Spawned a sub-agent to clone `https://github.com/SgtFoose/AthenaRemastered` into `C:\AthenaRemastered` (read-only survey, no commits) and compare its structure and shared code with Web2.
- Sub-agent results (key items re-checked by hand):
  - AR cloned (631 files, last commit `4d916a9`, 2026-04-05). Repo-local identity set to the SgtFoose noreply address, same as all AR history.
  - AR's frontend (`Frontend/`) uses React, Vite, Leaflet and a SignalR client. Its backend and extension are outside Web2's scope.
  - Frontend comparison: 14 files in common, 5 byte-identical. `AthenaMap.tsx` is +1942/-295 in Web2, and `useAthenaHub.ts` is essentially a rewrite. The data layers have split (AR uses a SignalR hub, Web2 uses a raw WebSocket plus REST hydration). Build configs, all of AR's `public/` assets, and `icon-mapping.md` are identical.
  - Releases/Workshop are fully separate (Web2 has 7 GitHub releases plus Workshop item 3707444008; AR has Workshop item 3687225607). Neither repo has CI workflows.
- AR leftovers found in Web2 (verified):
  - `ui/src/App.tsx` lines 429, 434, 492, 495 still show "ATHENA REMASTERED" branding and an AR PayPal donate link.
  - `ui/package.json` still depends on `@microsoft/signalr` and reports version `0.0.2`.
  - `AthenaMap.tsx` still contains laze/projectile code inherited from AR (needs review against Web2's capability boundary).
  - Not fixed yet; to be planned as a separate cleanup branch.
- Updated `CLAUDE.md`: the AR repo URL and local path `C:\AthenaRemastered` replace the stale old-machine path, and the one-way Web2 → AR feature-flow rule is now written down.

#### 2026-11-22 follow-up — Codebase architecture overview (read-only)
- The owner asked for an architecture overview. Checked the code-level wiring: bridge HTTP routes and WebSocket handler (`bridge/server.js` ~1378–1845), UI data entry points (`ui/src/apiBase.ts`, `hooks/useAthenaHub.ts`, `useStaticMap.ts`, `useHealthCheck.ts`, and direct fetches in `AthenaMap.tsx`).
- WebSocket contract confirmed:
  - bridge → UI: `state`, `relayConnected`, `relayDisconnected`, `mapImportStatus`, `mapImportDataReady`, `bridgeError`
  - UI → bridge: only `startMapExport`
- No code changes.

#### 2026-11-22 follow-up — Question: where are uploaded file types validated? (read-only audit)
- Result: there are **no file uploads** in Web2. No multipart parsing, no upload route, no `<input type="file">`, no `FileReader` in `bridge/` or `ui/src`. So there's nothing that validates file types.
- The only request body the bridge reads is the JSON body of `POST /api/game/cachemode` (`bridge/server.js:1397-1421`): capped at 64 KB, invalid JSON → 400, `mode` coerced to `fresh|stable`.
- **Security finding (low severity, not fixed yet):** user-supplied world names in the bridge's map-cache routes aren't validated strictly enough before they're used as disk paths. Exact details are deliberately kept out of this public log until the fix ships. They'll be described in the fix PR (`fix/validate-world-names`).
- Planned fix: one helper that accepts only names of folders that actually exist in `ATHENA_MAPS_DIR`, used by every route that takes a world name, and make cache-mode changes POST-only.
- Static file serving (`server.js:1750-1752`) checked: it stays inside `wwwroot`.

#### 2026-11-22 follow-up — Claude Code session health check (`/debug`)
- The owner ran `/debug` without naming a specific issue. No session debug log or daemon log exists. User `settings.json` has only notification flags, and there's no project `.claude/settings*.json`.
- Problems seen during this session, and their status:
  1. The Bash tool crashed once at session start (`bash.exe: *** fatal error - add_item ... errno 1`). Resolved: it works again later in the session (`echo` test passed). No action needed.
  2. `git push` was blocked by Claude Code's auto-mode classifier (credential-prompt risk). Open: there's an optional project-local allow rule (`.claude/settings.local.json`, not committed) for `git push` / `gh pr create`, pending the owner's decision.
  3. Git Credential Manager couldn't show its first-time login prompt. Resolved: the owner pushed once, and the SgtFoose credential is stored.
  4. `node`/`npm`/`gh` were missing from shells opened before the winget install. Fix: restart VS Code.
  5. `gh auth status` → "not logged into any GitHub hosts". Resolved: the owner ran `gh auth login` (device flow; terminal opened before install needed a PATH reload). `gh auth status` → logged in as `SgtFoose` (keyring, HTTPS, scopes `repo`, `workflow`, `gist`, `read:org`).
- Plan saved and approved; no settings were changed.

#### 2026-11-22 follow-up — `/code-review` + `/simplify` on the docs branch
- The owner ran `/code-review` (9 findings) and `/simplify` on `chore/claude-setup-toolchain`. The diff is docs only (`CLAUDE.md`, `WORKLOG.md`), so I did the simplify pass inline instead of spawning four review agents.
- Fixed:
  - Removed exploit-level detail about the unfixed bridge security issue from this public log. A neutral description stays; details go in the fix PR.
  - Removed the owner's work account name from `CLAUDE.md` Rule #4. It's still in public history (commit `04c71da` on `main`). **Owner decision: leave history as is** (only an account name, no email or secrets; a force-push would break the PR ruleset on `main`).
  - Settled the contradiction between the "no v1.x/BattlEye/DLL text" rule and the AR boundary notes. `CLAUDE.md` now allows short factual AR references only where they explain the boundary. AR version, stack, and BattlEye details are trimmed from these entries.
  - One install command: `CLAUDE.md` Commands now uses `npm ci`. The duplicate in the toolchain notes is gone, and the Node note explains README's "18+" (dev minimum) vs Node 22 (EXE build target).
  - Moved the newly found open problems into `CLAUDE.md` Open items and the backlog at the bottom of this file.
  - Corrected the stale Bash-tool status above.
- Note: VS Code's C# Dev Kit logs `Failed to load project 'Athena Desktop dnSpy/Athena Desktop/Athena Desktop.csproj'` (old-style .NET Framework project). It's harmless: the folder is decompiled reference material, never built, and Web2 has no C# code. Suggested to the owner: disable C# Dev Kit for this workspace only (no repo change). Possible follow-up: mark `Athena Desktop dnSpy/` as `linguist-vendored` in `.gitattributes`, so GitHub stops reporting the repo as 62% C#.
- Skipped (outside this docs diff, needs a code change): an unhandled `URIError` path in the bridge's request handling, which can crash the process. Logged as an open item for the hardening branch.

#### 2026-11-22 follow-up — First PR from the new workspace
- Before pushing, checked `git config user.email` → SgtFoose noreply, and `gh api user` → `SgtFoose`.
- The owner approved the push. `git push -u origin chore/claude-setup-toolchain` succeeded from Claude's shell; no login prompt was needed now that `gh` stores the credential.
- Opened PR #1 → `main`: https://github.com/SgtFoose/Athena-Web-2/pull/1 ("chore: Claude Code setup: CLAUDE.md rules, toolchain, workflow and audit notes"). The owner reviews and merges.
- PR #1 was blocked: "at least 1 approving review is required". GitHub doesn't let authors approve their own PRs, so on this solo repo every PR would need a bypass.
  - Checked the rules (read-only). There's no classic branch protection. Ruleset `Protect Master` (id 27794164, active) has `deletion`, `non_fast_forward`, and `pull_request` with `required_approving_review_count: 1`; the admin role can bypass.
  - Owner decision: set required approvals to 0. Applied via `gh api -X PUT repos/SgtFoose/Athena-Web-2/rulesets/27794164`, sending all three rules so only the approval count changed.
  - Validation: re-read the ruleset → `deletion`, `non_fast_forward`, `pull_request approvals=0`, still `active`. `gh pr view 1` → `mergeStateStatus: CLEAN`.
- Side note: the local working copy had been switched to `main` and the local PR branch removed (outside Claude). I recreated the branch from `origin/chore/claude-setup-toolchain` (`06a75d1`); nothing was lost.

### 2026-05-04 — v0.1.0 Release: Map Rendering Pass (Contours, Land Fill, Taxi)

- Applied contour line smoothing: single Chaikin pass (after RDP) on elevation contours removes blocky stairstepping.
- Applied coastline smoothing: two Chaikin passes on Z=0 coast rings for natural shoreline curves.
- Fixed land fill / ocean fill edge bleed by aligning land polygon and coast polyline to identical Chaikin-smoothed point sets with `smoothFactor: 0` (disables Leaflet per-zoom re-simplification that caused polygon/polyline divergence, including Malden-specific bleed).
- Loosened airport expansion second-pass threshold (`ringCount >= 2`, `cardinalCount >= 2`) to recover Tanoa sparse taxi connectors.
- Hidden Trees row in MAP DATA panel when `treeCount === 0` (no data, avoids misleading "0 points" display).
- Built `AthenaWeb-0.1.0.exe` via `@yao-pkg/pkg`, updated `bridge/dist`, updated `repo_dist_contents.json`.
- Created `.github/release-notes-template-v0.1.0.md`.
- Tagged `v0.1.0`, pushed to `main`, published GitHub Release with EXE asset.

### 2026-05-04 — v0.1.0 Runway/Taxiway Parity Prep Pass
- Investigated original Bus map cache exports directly from `C:\Users\jvdv2\Documents\Athena\Maps` to verify how airport surfaces are encoded across multiple worlds before changing Web2 rendering.
- Verified multi-map cache pattern:
  - Altis, Stratis, and Malden rely heavily on square `hide` roadway tiles for airport/apron/taxiway surfaces
  - Tanoa mixes square `hide` tiles/models but still follows the same broad airport-surface encoding pattern
  - original Bus caches for these worlds expose only `RoadType=1` and `RoadType=3` in `Roads.txt`, making bridge-side road-type preservation critical for parity
- Applied bridge import fix in `bridge/server.js`:
  - remapped `RoadType=3` to Web2 `main road`/concrete styling instead of collapsing it into generic `road`
  - preserves gray concrete segment styling for airport-adjacent lines before UI rendering begins
- Applied UI render fix in `ui/src/components/AthenaMap.tsx`:
  - tightened `hide` tile classification so only explicit long/thin strips resolve to dirt-path rendering
  - square 20x20 `hide` tiles now remain gray airport/taxiway surface geometry even when clusters are sparse or narrow
- Validation:
  - `node --check bridge/server.js` passed after the bridge road-type fix
  - no editor errors reported for `ui/src/components/AthenaMap.tsx`
  - full `npm --prefix ui run build` is still blocked by an existing nullability error in `ui/src/App.tsx` (`health` possibly `null`), outside the runway/taxiway changes
- Release-prep docs updated for the pending `v0.1.0` publish path:
  - `CHANGELOG.md`
  - `WORKLOG.md`
  - `AthenaWeb2/workshop-change-note-0.1.0.txt`

### 2026-04-15 — v0.0.9 Workshop Publish + Distribution Messaging Pass
- Applied bridge crash-hardening pass in `bridge/server.js`:
  - wrapped relay write/send operations in guarded try/catch paths
  - added safe WebSocket send helper with stale-client cleanup
  - added listen-attempt guard to avoid stale startup callback races when port fallback occurs
- Published Athena Web 2 Workshop item and linked published ID `3707444008` in `@AthenaWeb2/meta.cpp`.
- Built/validated Arma 3 Publisher compatibility path for first publish:
  - added no-op stub addon source `@AthenaWeb2/addons_src/athenaweb2_stub/config.cpp`
  - added stub build helper `@AthenaWeb2/Build-StubPbo.ps1`
  - generated required publisher-validation PBO `@AthenaWeb2/addons/athenaweb2_stub.pbo`
- Added Workshop maintenance/update helpers:
  - `@AthenaWeb2/Update-Workshop.ps1`
  - `@AthenaWeb2/workshop-change-note-0.0.9.txt`
  - `@AthenaWeb2/workshop-description.txt`
  - `@AthenaWeb2/WORKSHOP-FIRST-PUBLISH-CHECKLIST.txt`
- Updated Workshop preview asset for publish flow (`@AthenaWeb2/workshop-preview-v0.0.9.png`) from current v0.0.9 imagery.
- Updated README for user-facing distribution clarity:
  - added Athena Web 2 Workshop link and dedicated QUICK START flow with "Option A: Steam Workshop" and "Option B: Standalone EXE"
  - explicitly documented that users should not load `@AthenaWeb2` as server/client gameplay mod and should run `AthenaWeb.exe` only
  - documented Steam/Arma alias behavior where `!Workshop` may show `@AthenaWeb2` or `@@AthenaWeb2`; canonical launch path remains `steamapps/workshop/content/107410/3707444008/`
  - updated Latest Video to `https://youtu.be/BwCr86wAnSw` and moved prior latest to Older Videos

### 2026-04-12 — v0.0.9 Active AO + Marker Routing Pass
- Implemented live AO visibility routing from relay marker stream with separate marker ownership buckets:
  - Arma Markers (general/player map markers)
  - AO Markers (active AO area/objective families)
  - Spawn Markers (spawn/support infrastructure families; default OFF)
- Restored Firewill I-TGT relay marker visibility after the v0.0.9 routing pass dropped the client-side `TGT_*` -> I-TGT layer mapping.
- Added and validated side-mission and AO-selection marker visibility in live Domination sessions.
- Added and validated radiotower objective marker presence in AO marker flow.
- Restored bottom cursor coordinate HUD with visible/collapsible X/Y + I-TGT panel.
- Increased AO marker icon scale for better readability in active objective zones.
- Added v0.0.9 screenshot assets for release/readme coverage:
  - `Images/Athena Web 2 v0.0.9 Active AO visible.png`
  - `Images/Athena Web 2 v0.0.9 Active AO.png`

#### TODO (carry into next patch)
- AO camp objective number labels (`1..5`) still not consistently visible on top of camp icons in live sessions; keep open as a targeted rendering follow-up.

### 2026-04-10 — v0.0.8 Local Firewill I-TGT Marker Interop Validation
- Investigated live Firewill I-TGT marker passthrough while in-game with relay active.
- Verified relay/bridge frame ingestion included marker channel (`Children -> Markers`) in live v2 frames.
- Confirmed live Firewill I-TGT marker payload present in bridge WebSocket `state` feed:
  - `name: MKR_0_NightHawk`
  - `text: TGT_0_NightHawk`
  - `type: mil_triangle`
  - `color: ColorBlue`
  - `posx: 746.009`, `posy: 12267.2`
- Confirmed current gap for UI parity remains client-side marker consumption/rendering of relay marker payloads.
- Prepared v0.0.8 local test packaging update and version/doc sync.

#### 2026-04-10 follow-up — v0.0.8 Marker Label Readability Final Pass
- Implemented final I-TGT label style tuning for both local placed targets and Firewill relay targets in `ui/src/components/AthenaMap.tsx`.
- Removed dark boxed label background and aligned label color to Firewill-style readable blue while keeping enlarged label size.
- Rebuilt UI bundle, mirrored `ui/dist` to `bridge/wwwroot`, and rebuilt packaged executable with latest embedded UI.
- Validated runtime asset swap on port 3000 to confirm live EXE serves the updated marker-label rendering path.

### 2026-04-08 — v0.0.7 To-Do Queue
- Target label index behavior for stored I-TGT entries:
  - start default labels at `TGT_0` (instead of starting from 1)
  - when `Delete All` is used, reset counter so next stored target is `TGT_0` again
- Offline static map parity for coastline/contour rendering on cache-backed worlds (for example Stratis):
  - improve shoreline and terrain contour line quality/smoothing in offline mode to match Tanoa-level visual finish
  - verify no jagged/rough contour or coastline artifacts after switching into offline maps
- Online -> offline session cleanup behavior:
  - when Athena Splash appears after disconnect/offline transition, clear all stored I-TGT markers/entries from the active map session
  - clear live dynamic markers (vehicles, units, groups) so switching between offline maps does not show stale online entities

#### 2026-04-09 follow-up — v0.0.7 Offline Smoothing + Live->Offline Cleanup
- Implemented offline contour/coastline smoothing tuning in `ui/src/components/AthenaMap.tsx`:
  - reduced contour simplification tolerance for smaller worlds to preserve curved line detail
  - added dedicated tighter coastline simplification tolerance and applied it to both land-fill ring generation and Z=0 coastline rendering
- Implemented live-to-offline cleanup hardening in `ui/src/App.tsx`:
  - dynamic live marker datasets (units/vehicles/groups/lazes) are now gated behind active live telemetry availability
  - when live telemetry drops (online -> offline), clear stored I-TGT targets and reset next target label index
  - reset follow-active-player state and bump map session key on live telemetry loss to prevent stale session carry-over
- Updated release/version docs for `v0.0.7` in:
  - `ui/src/version.ts`
  - `README.md`
  - `CHANGELOG.md`

#### 2026-04-09 follow-up — v0.0.7 Offline Tanoa Dirt Path Classification Pass
- Investigated offline Tanoa report where dirt-walk hide tiles still showed mixed rendering (part brown path, part gray runway-like blocks).
- Updated `ui/src/components/AthenaMap.tsx` hide-surface classification:
  - added hide-tile cluster analysis (size + bounding-box density) to classify dense airport/apron regions vs sparse path networks
  - constrained taxi-lane styling to airport-classified clusters only
  - routed off-airfield sparse hide tiles to stitched dirt-path polyline rendering
- Rebuilt UI and repackaged local executable for validation; synced `bridge/wwwroot` to the new build output.

#### 2026-04-08 follow-up — Global Taxi Overlay Cleanup + Tablet I-TGT Capture
- Fixed cross-map runway/taxi visual regression where yellow taxi centerline overlays leaked into cached worlds beyond Malden (for example Tanoa).
  - removed the global yellow taxi-lane overlay pass from hide-tile rendering
  - preserved neutral gray runway/taxi surface rendering for all cached maps
- Added touch/tablet-compatible I-TGT capture workflow:
  - detect coarse touch input at runtime and switch guidance text to touch mode
  - allow cursor coordinate updates from map tap/click (not only mouse move)
  - added in-map `SAVE TGT` action on touch input so target capture does not require keyboard `T` or middle mouse
- Added iPad Safari landscape layout hardening:
  - use dynamic viewport height (`dvh`) + safe-area top offset in shell layout
  - add coarse-pointer landscape top offset so Safari favorites/browser chrome does not overlap top controls
- Updated I-TGT target label sequencing behavior:
  - first saved target label starts at `TGT_0`
  - `Delete All` now resets label index so next saved target returns to `TGT_0`
- Rebuilt and repackaged `AthenaWeb-0.0.6.exe` after validating UI build with the new tablet capture path.

### 2026-04-08 — v0.0.6 I-TGT Target Workflow + UI Polish
- Added Firewill-compatible 8-digit I-TGT cursor coordinate output (`XXXXYYYY`) while keeping legacy XY readout visible.
- Added map capture workflow for I-TGT targets:
  - keyboard capture with `T` (guarded to avoid firing while typing in input/textarea/contenteditable)
  - middle mouse capture on map
- Added persistent I-TGT target management in right sidebar:
  - default target labels (`TGT_n`)
  - per-target rename
  - per-target copy code
  - per-target delete
  - bulk Delete All action
- Added stored I-TGT map annotations:
  - dark blue triangle markers with labels
  - pane ordering tuned to keep markers readable while remaining below city/dynamic marker layers
- Applied final usability polish pass:
  - slightly increased stored target marker size for readability
  - removed bottom-right I-TGT copy icon button per UX request (copy remains in right-panel target entries)
- Updated release/version tracking for `v0.0.6` in:
  - `ui/src/version.ts`
  - `CHANGELOG.md`
  - `README.md`
  - `.github/release-notes-template-v0.0.6.md`

### 2026-04-08 — v0.0.5 Vehicle Icon Coverage + Stratis Alignment Fix
- Investigated report after high-density vehicle placement where some vehicle markers still fell back to question-mark icon rendering.
- Updated vehicle classification fallback in `ui/src/components/AthenaMap.tsx`:
  - expanded classname heuristics for additional common/modded families (helis, jets, APC/IFV, MBT, trucks/cars, boats)
  - changed unknown icon fallback from `iconvehicle` (question-mark visual) to `iconcar` generic silhouette
  - broadened truck/motorbike subtype checks for category-to-icon mapping
- Investigated Stratis world swap misalignment where shoreline/roads/objects/location labels appeared shifted downward relative to land.
- Fixed world-size source precedence in `ui/src/App.tsx`:
  - prefer hydrated cache/static world metadata (`activeWorldInfo/staticInfo`) before relay fallback size
  - prevents stale fallback scale from distorting static-layer positioning
- Added explicit Stratis world-size fallback (`8192`) in `ui/src/hooks/useAthenaHub.ts` to reduce pre-hydration scale drift.
- Synced release metadata/docs for `v0.0.5`:
  - `ui/src/version.ts`
  - `README.md`
  - `CHANGELOG.md`
  - `.github/release-notes-template-v0.0.5.md`

#### 2026-04-08 follow-up — ArmaTools UAV/Radar Marker Overrides
- Applied requested marker improvements for specific UAV/radar-class vehicles that still rendered generic icons:
  - `B_UAV_02_dynamicLoadout_F` (Greyhawk)
  - `B_UAV_05_F` (UCAV Sentinel)
  - `B_UGV_01_F` (UGV Stomper)
  - `B_T_UAV_03_dynamicLoadout_F` (MQ-12 Falcon)
  - `B_UAV_06_F`, `B_UAV_06_medical_F` (AL-6 Pelican)
  - `B_Radar_System_01_F` (AN/MPQ-105)
  - `B_Ship_MRLS_01_F` (VLS/Ship MRLS)
- Updated `ui/src/components/AthenaMap.tsx` icon pipeline:
  - side-aware NATO marker mask overrides now use Athena Desktop marker textures (`*_uav`, `*_installation`, `*_art`) for these classes/categories
  - Turret MRLS subclasses now resolve to artillery-style marker/icon path instead of default static MG fallback

#### 2026-04-08 follow-up — Universal Drone/Turret Arma Markers + iPad UX
- Expanded marker policy to use Arma-style NATO marker textures for all Drone and Turret categories (not only per-class exceptions):
  - drones/UAV/UGV -> `*_uav`
  - turret radar -> `*_installation`
  - turret mortar -> `*_mortar`
  - turret naval/SAM/AAA -> `*_art`
  - turret generic support weapons -> `*_support`
- Added generic AI fallback badge in marker renderer for classes like `*_UAV_AI` when specific platform identity is unavailable.
- Fixed quadbike icon regression where `quadbike` was being matched by generic `bike` logic and rendered as motorcycle.
- Investigated Malden airport taxi-lane gap using live cache dimensions:
  - confirmed `hide` tiles are 20x20 runway/apron squares
  - identified nearby `RoadType=3` zero-width segments used for taxi-lane centerlines
  - added runway-adjacent taxi-lane detection and high-contrast taxi-lane rendering so lanes are visible over airport surfaces
- Added collapsible left and right sidebar controls in app shell for tablet (including iPad Mini) so users can maximize map viewport as needed.

#### 2026-04-08 follow-up — Release Polish Adjustments
- Moved collapse/expand buttons from map-middle placement to top-edge placement on each sidebar boundary for faster access.
- Restored Groups layer default to ON after user report that group markers were not visible in normal startup flow.
- Removed Waypoints toggle from sidebar layer controls for this release because the feature is not functioning in Web2 parity mode.
- Retuned Malden taxi-lane detection by increasing runway-adjacent matching distance for zero-width road centerlines.
- Finalized Malden taxi-lane fix by broadening classifier to include midpoint/either-end runway adjacency and rendering taxi lanes as paved high-contrast strips instead of subtle centerlines.
- Added aggressive fallback pass for Malden airport lanes:
  - expanded runway-proximity search radius for zero-width road segments
  - constrained candidate segment lengths to reduce false positives
  - switched lane styling to bright high-contrast yellow for unambiguous visibility during validation
- Follow-up refinement after user validation:
  - tightened taxi-lane classifier to strict runway-near segments within airport bounds
  - reverted lane tint from yellow to neutral concrete to prevent nearby normal roads from being recolored
- Reworked taxi-lane approach after further validation:
  - removed road-segment recoloring override (it affected normal roads near airports)
  - detect taxi-lane candidates from hide-tile topology (sparse/linear hide neighborhoods)
  - render taxi tiles with subtle concrete lane contrast + centerline overlay inside airport surfaces
- Final user validation pass:
  - confirmed airport-adjacent normal roads are back to standard styling (no unwanted recolor)
  - accepted this state as final for `v0.0.5` release packaging and publication

### 2026-04-07 - v0.0.4 Final World-Swap Guard + Release Sync
- Added bridge live-frame freshness timeout behavior so live map state expires when relay is connected but telemetry is stale (for example Arma Editor idle):
  - `bridge/server.js` now tracks `lastLiveFrameAt`
  - `/api/game/state` map value is blanked after inactivity window to avoid stale live-world lock-in
- Added strict world/geometry render guard in UI so static layers render only when hydrated geometry world matches active world:
  - prevents cross-map contamination during rapid world handoff (for example Tanoa geometry persisting over Malden)
- Added no-world fallback behavior in app shell:
  - Athena welcome/static screen shows when there is no active world (disconnected or unknown)
  - world source uses live world or explicit offline selection only
- Refined hide-road styling split:
  - long/thin hide tiles remain brown dirt paths
  - square hide tiles (runway/apron surfaces) remain concrete-gray
- Hardened plane icon resolution for noisy relay payloads:
  - improved class token extraction and fuzzy lookup
  - expanded plane heuristics for common classname patterns
- Synced release documentation for this final v0.0.4 stabilization pass:
  - `README.md` highlights updated
  - `CHANGELOG.md` fixed-item list updated
  - release package context prepared for commit/push

### 2026-04-07 — v0.0.4 Dirt Path Continuity Finalization
- Confirmed hedge/bush orientation is now correct in user validation.
- Investigated remaining dirt-path issue where brown segments rendered as detached stubs (often north-facing) due to unreliable per-tile heading in many `hide` road records.
- Reworked hide-road rendering in `ui/src/components/AthenaMap.tsx`:
  - classify path-like hide tiles separately from large runway/apron hide surfaces
  - derive path nodes from hide tile centers
  - stitch nearby nodes into connected polylines
  - merge nearby polyline endpoints to reduce fragmentation and produce longer continuous path traces
- Tuned continuity parameters to improve chain completion in sparse tile regions:
  - widened path-like hide threshold (`<= 40m`)
  - increased link radius and nearest-neighbor fanout
  - applied endpoint merge pass for fragmented chains
- Rebuilt UI, mirrored to `bridge/wwwroot`, and produced hotfix artifacts for validation:
  - `AthenaWeb-0.0.4-hotfix3.exe`
  - `AthenaWeb-0.0.4-hotfix4.exe`

### 2026-04-07 — v0.0.4 Vehicle Icon Mapping + EXE Embed Refresh
- Investigated report where all vehicles rendered with question-mark fallback icon despite Bus icon assets being present.
- Verified Bus original icon set under `Athena Desktop dnSpy/Athena Desktop/assets/map/vehicleicons` and Web2 packaged icon set under `ui/public/icons/vehicles` / `bridge/wwwroot/icons/vehicles` (79 icons).
- Fixed Web2 vehicle icon resolution pipeline:
  - normalized `vehicleClasses.json` map keys (original + lowercase)
  - expanded relay vehicle class extraction to accept multiple key variants (`class`, `classname`, `vehicleClass`, `type`, `displayName`, etc.)
  - added stronger class-name fallback heuristics to resolve known Arma vehicle families into Bus categories
- Rebuilt UI and mirrored `ui/dist` to `bridge/wwwroot`.
- Rebuilt standalone EXE with overwrite target `bridge/dist/AthenaWeb-0.0.4.exe` after releasing file lock from running process.
- Updated GitHub release `v0.0.4` asset by replacing `AthenaWeb-0.0.4.exe` with rebuilt binary containing the mapping fix.

#### 2026-04-07 follow-up — root cause for `?` vehicle icons
- Confirmed live relay vehicle payload schema used numeric category code in `class` (for example `"3"`) while actual config classname was present in `type`.
- Updated vehicle class extraction to prefer `type`/`classname` keys before `class`, and to ignore numeric-only values.
- Added broader fallback matching for common modded vehicle classnames (for example Leopard MBT family) when library lookup misses.
- Rebuilt and re-uploaded `AthenaWeb-0.0.4.exe` release asset after this root-cause fix.

#### 2026-04-07 follow-up — dirt track color + hedge angle
- Investigated live cache around reported coordinate `X:6736.40 Y:7537.21` and confirmed nearby dirt-path records were exported as road type `hide`.
- Updated `ui/src/components/AthenaMap.tsx` road styling so `hide` paths render as brown gravel instead of concrete grey.
- Updated hedge orientation policy to remove forced +90 degree rotation offset and use cache heading directly.
- Rebuilt EXE and refreshed release asset after these visual parity fixes.

### 2026-04-07 — v0.0.4 Contour Processing Parity (Desktop Trace)
- Investigated Athena Desktop import progress stage `Processing world mesh and tracing contours` and confirmed it runs real contour generation after `mapend` (not a cosmetic status string).
- Traced Desktop contour pipeline in decompiled source:
  - `ProcessElevations()` -> `FillElevationCells()` -> `PopulateElevationPoints()`
  - contour tracing via `MarchingSquare.DoMarch(...)`
  - post-process trim via `MapHelper.TrimPoints(...)` (`SimplifyUtility` tolerance `5.0`)
- Updated Web2 contour/coastline rendering in `ui/src/components/AthenaMap.tsx`:
  - removed aggressive angle-straightening from contour/coastline lines
  - introduced world-scaled simplification epsilon equivalent to Desktop trim tolerance (5m in world space)
  - applied the same contour simplification tolerance to Z=0 coastline land-fill ring generation
- Result: contour lines now retain more natural Desktop-like geometry while still reducing noisy points.

### 2026-04-07 — GitHub Release Distribution + First-Time Map Guidance
- Published GitHub release `v0.0.4` and attached `AthenaWeb-0.0.4.exe` under release **Assets** for direct user download.
- Updated release notes with explicit download/start flow and Windows 11 SmartScreen unblock guidance (`More info` -> `Run anyway`, plus `Unblock-File`).
- Updated `README.md` quick-start and release visibility:
  - added direct `v0.0.4` release link
  - added explicit tracked bridge dist binary note (`bridge/dist/AthenaWeb-0.0.4.exe`)
  - added first-time map export section with screenshot `Images/Athena Web 2 First Map Usage.png`
- Updated in-app cache warning banner help links (`ui/src/components/MapCacheBanner.tsx`):
  - retained Athena Desktop download link
  - added direct link to README first-time map export instructions
- Updated tracked dist artifact on `main` from `AthenaWeb-0.0.3.exe` to `AthenaWeb-0.0.4.exe` so repository tree matches current release line.

### 2026-04-06 — Session Handoff Notes (Next Round)
- Current release target is `v0.0.4`.
- Latest packaged binaries in `bridge/dist`:
  - `AthenaWeb.exe` (rebuilt at latest timestamp)
  - `AthenaWeb-0.0.4-hotfix1.exe` (same payload as latest `AthenaWeb.exe`)
  - `AthenaWeb-0.0.4.exe` may be stale if file lock prevented overwrite during this session.
- UI bundle was rebuilt and mirrored to `bridge/wwwroot` after all fixes.
- Vehicle icon asset pack was re-synced from Bus original desktop assets:
  - source: `Athena Desktop dnSpy/Athena Desktop/assets/map/vehicleicons`
  - destination: `ui/public/icons/vehicles`

#### Next Round Quick Resume Checklist
1. Ensure all old `AthenaWeb*.exe` processes are closed.
2. Launch `bridge/dist/AthenaWeb-0.0.4-hotfix1.exe`.
3. Hard-refresh browser (`Ctrl+F5`) before validation.
4. Validate:
   - vehicles render with proper icon (A-10 no longer shows blue `?`)
   - groups are OFF by default on startup
   - manual layer toggles for groups/vehicles/units persist across zoom
5. If A-10 still shows `?`, add temporary debug label logging vehicle class + resolved category in map layer and patch class-to-category mapping.

### 2026-04-06 — v0.0.4 World/Layer Reliability Pass
- Investigated report where world switched from cached Stratis to live Tanoa but old-map roads remained visible.
- Updated UI hydration safety in `ui/src/hooks/useAthenaHub.ts`:
  - added request sequencing so stale async geometry responses are ignored
  - clear prior geometry immediately on detected relay world change before new world cache hydration completes
- Updated active-world behavior in `ui/src/App.tsx`:
  - treat live world as valid only while bridge is connected
  - keep cached world picker usable while disconnected
  - clear stable/manual world override when live world arrives
  - force health recheck on world change for faster cache-banner correctness
- Updated cache banner matching in `ui/src/components/MapCacheBanner.tsx`:
  - use case-insensitive cached-world list verification to avoid false "No cached data" warnings
- Updated layer defaults and toggle behavior:
  - set Groups layer default to OFF in `ui/src/App.tsx`
  - removed zoom-threshold auto-toggle for groups/vehicles/units in `ui/src/components/AthenaMap.tsx` so manual toggles are respected
- Added vehicle position fallback fix in `ui/src/components/AthenaMap.tsx`:
  - when relay vehicle `posX/posY` are zero, vehicle marker anchor now falls back to first mounted occupant with a valid position
  - mounted units are hidden only if their parent vehicle has a valid position, preventing mounted entities from disappearing entirely
- Rebuilt and mirrored packaged UI (`ui/dist` -> `bridge/wwwroot`) after the vehicle fallback patch.
- Rebuilt packaged executable after confirming runtime was still launching stale `bridge/dist/AthenaWeb-0.0.3.exe`:
  - produced `bridge/dist/AthenaWeb-0.0.4.exe` from updated `wwwroot` assets
  - release validation should use `AthenaWeb-0.0.4.exe` (or `AthenaWeb.exe` rebuilt at same timestamp)
- Synced Bus original vehicle icon set from `Athena Desktop dnSpy/Athena Desktop/assets/map/vehicleicons` into `ui/public/icons/vehicles`, rebuilt UI, and mirrored into `bridge/wwwroot`.
- Rebuilt packaged EXE again after icon sync and produced `bridge/dist/AthenaWeb-0.0.4-hotfix1.exe` (previous `AthenaWeb-0.0.4.exe` was file-locked by a running process).
- Release metadata set to `v0.0.4`:
  - `ui/src/version.ts`
  - `README.md`
  - `CHANGELOG.md`

### 2026-04-06 — Release Blocker Carried Forward (Tracked)
- Runway alignment regression is resolved and accepted.
- Remaining known blocker is still open:
  - vegetation/hedge orientation mismatch at `X:6874.88 Y:7381.98` (Tanoa cache)
- Action for release:
  - ship `v0.0.3` with blocker documented
  - keep this coordinate pinned in backlog until model-specific orientation mapping is corrected.

### 2026-04-06 — v0.0.3 Shoreline + Tree Symbol Parity Pass
- Focused this release on two desktop-parity rendering gaps:
  - shoreline looked too blocky/stair-stepped compared to original Athena Desktop
  - individual trees lacked the dark outer ring seen in desktop rendering
- Updated `ui/src/components/AthenaMap.tsx`:
  - replaced curve-style smoothing with line simplification on Z=0 shoreline paths to preserve cleaner diagonal coastline segments
  - applied same shoreline simplification to Z=0 land-fill polygon rings for stroke/fill alignment
  - updated tree canvas draw path to render dark outer ring + green center fill
  - made tree ring radius zoom-aware from world-metre scale so tree symbols retain expected visual weight when zooming in
- Updated release metadata for Web2 `v0.0.3`:
  - `ui/src/version.ts`
  - `README.md`
  - `CHANGELOG.md`
- Release intent:
  - prepare `v0.0.3` for visual review and parity sign-off before final publish/push.

### 2026-04-05 — v0.0.2 Shoreline Fill + Release Packaging
- Fixed shoreline rendering artifacts where land/ocean showed block overlap at coast borders.
- Updated land fill priority in UI to prefer vector Z=0 coastline polygons before raster fallback.
- Hardened raster fallback by increasing mask resolution and applying edge dilation to close shoreline seams.
- Added export-complete shoreline refresh trigger so map fill rebuilds automatically after world export.
- Verified bridge now serves latest UI bundle by syncing `ui/dist` -> `bridge/wwwroot` and restarting bridge.

### 2026-04-05 — Repository Separation Cleanup (v0.0.2)
- Corrected cross-repo mix-up: Web2-specific changes were removed from `AthenaRemastered` and restored into `Athena Web 2`.
- Moved v0.0.2 UI updates into Web2 repo scope:
  - map cache health banner
  - health polling hook
  - simplified sidebar/world picker flow
  - follow-player auto-disable behavior
- Updated Web2 manuals (`README.md`, `CHANGELOG.md`, `WORKLOG.md`) to reflect v0.0.2 ownership in this repository.
- Added explicit repository-boundary Copilot instructions to reduce future cross-repo commits.

### 2026-04-01 — Pivot Validation
- Confirmed BattleEye risk with custom extension path.
- Validated PoC relay bridge approach in `Athena Web 2/poc`:
  - Relay TCP 28800 connection works.
  - Browser receives live unit feed through bridge WebSocket.

### 2026-04-01 — START Investigation (decompilation-backed)
- Investigated `Athena Desktop.exe` and `Relay.exe` behavior.
- Verified protocol framing:
  - separator: `<ath_sep>`
  - terminator: `<ath_sep>end`
- Verified Desktop START action sends:
  - `command<ath_sep>mapexport`
- Verified map import message types consumed by Desktop:
  - `mapbegin`, `maprow`, `mapobjects`, `maplocations`, `maproads`, `mapfoliage`, `mapend`

### 2026-04-01 — PoC Bridge Enhancements
- Added browser START action support in bridge (`startMapExport`).
- Bridge now sends exact relay command to trigger map export.
- Added map import status events to browser:
  - stage, world, per-type counters
- Added map import completion summary payload:
  - road/object/location/foliage counts and parsed totals
- Fixed browser runtime bug (`world-name` wrong element id) causing blank behavior on updates.

### 2026-04-01 — New Clean Workspace
- Created new clean folder for future-focused work:
  - `AthenaRemastered/ClientOnly/`
- Copied remastered frontend into:
  - `AthenaRemastered/ClientOnly/ui`
- Copied proven bridge into:
  - `AthenaRemastered/ClientOnly/bridge`

### 2026-04-01 — ClientOnly UI Wiring (in progress)
- Replaced `ClientOnly/ui` hook data source:
  - old: SignalR + ASP.NET backend
  - new: Relay bridge WebSocket
- Implemented mapping from bridge state -> UI `GameFrame` shape.
- Wired `requestWorldExport('world')` -> bridge `startMapExport` message.
- Changed dev API base to bridge port `:3000`.

### 2026-04-01 — Validation + Tracking Setup
- Updated `.github/copilot-instructions.md` with ClientOnly pivot rules and mandatory worklog usage.
- Added this `WORKLOG.md` as the single running technical record for migration work.
- Installed dependencies and built `ClientOnly/ui` successfully (`npm run build` passed).
- Result: new isolated path is now buildable and ready for geometry decoding work.

### 2026-04-01 — Bridge Compatibility Endpoints + Geometry Hydration
- Added compatibility API endpoints to `ClientOnly/bridge/server.js`:
  - `/api/game/worldinfo`
  - `/api/game/roads`
  - `/api/game/forests`
  - `/api/game/locations`
  - `/api/game/structures`
  - `/api/game/elevations`
  - `/api/game/trees`
  - `/api/game/exportstatus`
  - `/api/staticmap/{world}`
  - `/api/staticmap/{world}/contours/{z}` (empty placeholder)
  - `/api/staticmap/{world}/landmask` (full-land placeholder mask)
- Bridge now reads local Athena map cache from `%USERPROFILE%/Documents/Athena/Maps/{world}` (or `ATHENA_MAPS_DIR` override).
- Added transformations from Athena cache shapes into remastered UI geometry types (roads/locations/structures/trees/worldinfo).
- Updated `ClientOnly/ui/src/hooks/useAthenaHub.ts` to hydrate geometry from bridge endpoints on connect/state/map-import completion.

### 2026-04-01 — Fixes During Integration
- Fixed TypeScript hook dependency error in `useAthenaHub.ts` (`hydrateGeometry` self-reference issue).
- Resolved port confusion by stopping old PoC server instance so `ClientOnly/bridge` owns port 3000.

### 2026-04-01 — Runtime Validation
- `ClientOnly/ui` build passed after integration changes.
- `ClientOnly/bridge` syntax check passed.
- Endpoint smoke test passed and returned JSON payloads:
  - world info
  - roads
  - locations
  - structures
  - trees
  - static map metadata

### 2026-04-01 — Geometry Fidelity Pass (roads + forests)
- Improved road conversion in `ClientOnly/bridge/server.js`:
  - uses first/last cached road nodes to produce real line segments (`beg1` -> `end2`)
  - infers fallback length from segment distance when cache length is missing/zero
  - keeps width/type mapping for existing map styling
- Implemented forest cell conversion from cache index tokens (`"x,y"`) to Athena `ForestsData` cells:
  - parses both `CellsLight` and `CellsHeavy`
  - computes sample size from detected index range vs. world size
  - maps light/heavy to different density levels for rendering
- Re-ran bridge syntax validation (`node --check server.js`) and confirmed clean parse.

### 2026-04-01 — Elevation + Land/Ocean + Runway Data Pass
- Replaced static-map placeholders in `ClientOnly/bridge/server.js`:
  - `/api/staticmap/{world}` now returns real `availableZ` from `Height/Z*.txt` files.
  - `/api/staticmap/{world}/contours/{z}` now parses `PointGroups` into world-metre contour polylines.
  - `/api/staticmap/{world}/landmask` now builds raster masks from coastline contour rings with caching and fast fallback.
- Implemented `/api/game/elevations` bridge response:
  - emits coarse elevation cells derived from major contour levels (20m bands).
  - payload now includes `sampleSize`, `worldSize`, and populated `cells`.
- Added bridge-side in-memory caches for world metadata, contour lines, and land masks to reduce repeated heavy parsing.
- Verified endpoint responses after bridge restart:
  - `Altis`: `availableZ=58`, `contours/0 lines=151`, valid landmask payload returned.
  - Active world endpoint now returns non-null elevation payload with populated cells.

### 2026-04-01 — Runtime Elevation Decode Pass (maprow + elevation)
- Added robust runtime elevation parsing in `ClientOnly/bridge/server.js`:
  - decodes `maprow` payloads into `{x,y,z}` cells using multiple formats (JSON object/array/csv triples).
  - added relay command support for `elevation` and `elevationscomplete` message variants.
  - stores decoded runtime terrain samples in `mapImportData.runtimeElevations`.
- Updated `/api/game/elevations` behavior:
  - now prefers decoded runtime samples when available.
  - falls back to contour-derived coarse elevations only when runtime samples are absent.
  - infers runtime sample size from decoded point spacing.
- Validation:
  - bridge syntax check passed.
  - active world endpoint currently returns dense elevation payload (`cells=372311`, `sampleSize=4` on Malden).

### 2026-04-01 — Road Fidelity Pass (multi-segment reconstruction)
- Upgraded road conversion in `ClientOnly/bridge/server.js`:
  - non-`hide` roads now emit one road record per adjacent node pair (`road-{idx}-{segment}`) for line fidelity.
  - per-segment length and direction are now inferred from node geometry.
  - center point (`posX`,`posY`) is now midpoint of each segment for consistent map usage.
- Preserved runway/airport surfaces behavior:
  - `hide` roads remain single area tiles (rectangle rendering path unchanged).
- Validation after bridge restart:
  - `/api/game/roads` now returns segmented geometry (`roads=6438`, `hide=415`, `nonHide=6023`).
  - runway tile count remains stable while non-hide road detail increased significantly.

### 2026-04-01 — Runtime Elevation Parser Diagnostics
- Added parser telemetry to `ClientOnly/bridge/server.js` runtime map import state:
  - counts parsed vs failed runtime elevation messages.
  - stores sample unparsed payloads (capped) for fast format debugging.
- Added new debug endpoint:
  - `/api/game/elevationdebug`
  - returns active world, in-progress state, maprow count, runtime cell count, parse success/failure counts, success rate, and payload samples.
- Included elevation parse metrics in `mapImportDataReady` broadcast summary.
- Validation:
  - bridge syntax check passed.
  - endpoint smoke test returned expected structured payload on active world.

### 2026-04-01 — Relay Command Trace for Stalled Export
- Added bridge-side relay command ring buffer in `ClientOnly/bridge/server.js`:
  - records recent command names with timestamp and payload preview.
  - keeps the latest 120 relay commands for short-window troubleshooting.
- Added new endpoint:
  - `/api/game/relaydebug`
  - returns relay connection state, export stage/last command, and recent relay command tail.
- Verified behavior after restart:
  - bridge remains relay-connected and continuously receives `frame` traffic.
  - stalled export reproduces as `phase=requested`, `stalled=true`, `lastCommand=mapexport-requested` when no map chunks arrive.
  - during idle monitoring, relay trace shows only `frame` commands (no `mapbegin`/`maprow` yet captured in this window).

### 2026-04-01 — GUID-Routed Export Request Attempt
- Found protocol clue in legacy Athena notes: relay supports client GUID mapping for map export routing.
- Updated `ClientOnly/bridge/server.js` export trigger behavior:
  - sends GUID-targeted command frame: `command<ath_sep>{bridgeGuid}<ath_sep>mapexport<ath_sep>end`
  - keeps legacy fallback frame: `command<ath_sep>mapexport<ath_sep>end`
- Added outbound trace label `command@self` to verify GUID-routed request is emitted on each export click.
- Next validation step: confirm whether this produces inbound `mapbegin/maprow/...` chunks instead of `requested/stalled`.

### 2026-04-01 — Export Routing Revalidation + Frame Introspection
- Live capture during user exports showed:
  - outbound: `command<ath_sep>mapexport`
  - inbound: `success`
  - no inbound `mapbegin/maprow/mapobjects/maproads/mapfoliage/mapend` commands observed.
- GUID-routed variant produced `malformed` from relay in current setup, so export trigger was reverted to legacy accepted form only.
- Added frame diagnostics endpoint in bridge:
  - `/api/game/framedebug`
  - reports frame schema keys, GUID hints, and map/export-related field hints to detect whether export payload is embedded in `frame` JSON.

### 2026-04-01 — Post-Export Delivery Gap (Reproduced)
- User-side game messages confirm full export pipeline progression:
  - adding foliage/structures/roads to buffer
  - all world data supplied to relay for delivery
- Immediate bridge diagnostics after export click:
  - `/api/game/exportstatus`: `phase=requested`, `stalled=true`, all geometry counters remain `0`
  - `/api/game/relaydebug`: outbound `command -> mapexport`, inbound `success` only
  - `/api/game/elevationdebug`: no parsed/failed maprow payloads
  - `/api/game/framedebug`: still no frame payload observations in this run window
- Conclusion for this stage:
  - relay acknowledges request but does not deliver map chunk stream to the current bridge consumer path.

### 2026-04-01 — Deep Relay Diagnostics Added
- Added configurable relay registration command type in bridge config:
  - `RELAY_CLIENT_TYPE` (default `General`)
  - handshake now logs the active registration type at connect time.
- Added raw relay dump endpoint for non-keepalive traffic:
  - `/api/game/rawrelaydebug?limit=10&clear=1`
  - captures raw message snippets with command tag and timestamp for unknown/new command forms.
- Updated `/api/game/relaydebug` payload to include current `relayClientType`.

### 2026-04-01 — Raw Relay Capture (General Registration)
- Post-export capture with clean raw buffer (`General` registration) returned:
  - outbound: `command -> mapexport`
  - inbound raw non-keepalive messages: exactly one entry, `success`
- No map chunk command forms observed (`mapbegin/maprow/mapobjects/maproads/mapfoliage/mapend` all absent).
- Export status remains `requested/stalled` with zero geometry counters in bridge state.

### 2026-04-01 — Desktop-Open A/B Check
- Ran export test with Athena Desktop open while bridge stayed on `General` registration.
- Capture outcome remained unchanged:
  - outbound: `command -> mapexport`
  - inbound raw non-keepalive: only `success`
  - no map chunk command stream delivered to bridge.
- Conclusion: merely running Desktop does not change relay delivery behavior to this bridge client in current setup.

### 2026-04-01 — MapExport Strategy Harness
- Added configurable export payload strategy to bridge:
  - env var: `MAPEXPORT_STRATEGY`
  - supported values: `basic` (default), `guid-csv`, `guid-pipe`, `guid-colon`, `guid-space`, `all-guid`
- Export trigger now sends one or more `command` payload variants according to strategy.
- Purpose: quickly test relay routing expectations without further code edits between runs.

### 2026-04-01 — Live Cache World Fallback (Documents/Athena/Maps)
- Added active-world fallback logic in `ClientOnly/bridge/server.js`:
  - if relay-hinted world is missing/unusable, bridge now selects the most recently updated world folder under `%USERPROFILE%/Documents/Athena/Maps`.
  - probe considers core map files and live chunk directories (`Rows`, `Roads`) with short TTL caching for performance.
- Validation after restart:
  - `/api/game/worldinfo` now resolves to live generated cache world (`Stratis`).
  - `/api/game/roads` returns populated road geometry for resolved world.

### 2026-04-01 — World Cache Selection Controls
- Added environment controls to choose between live and stable map cache selection:
  - `WORLD_SELECTION_MODE=fresh|stable`
    - `fresh` (default): prefers most recently updated world, including live chunk dirs (`Rows`, `Roads`).
    - `stable`: ignores live chunk dir mtimes and prefers worlds based on core cache files only.
  - `WORLD_CACHE_OVERRIDE=<WorldName>` to force a specific world folder (if present).
- This allows testing the "desktop loads stored map offline" behavior without deleting Stratis cache data.

### 2026-04-01 — Runtime Cache Mode API (No Restart Needed)
- Added API endpoint in bridge for live cache selection control:
  - `GET /api/game/cachemode` returns current mode/override and resolved active world.
  - `GET /api/game/cachemode?mode=fresh|stable&world=<WorldName>` updates settings via query.
  - `POST /api/game/cachemode` with JSON `{ mode, worldOverride }` updates settings.
- Cache mode changes now clear in-memory world/contour/landmask memoization to apply immediately.
- Validation:
  - default read returned `fresh` + `Stratis`.
  - switching to `stable+Altis` changed active world to `Altis` instantly.
  - switching back to `fresh` with cleared override returned to `Stratis`.

### 2026-04-01 — UI Map Source Controls
- Added map source controls in `ClientOnly/ui/src/components/Sidebar.tsx` (COMMON tab):
  - active cache world display
  - mode selector (`fresh` / `stable`)
  - world override input
  - `Apply` and `Refresh` actions
- Wired UI to bridge runtime cache API in `ClientOnly/ui/src/hooks/useAthenaHub.ts`:
  - loads current cache mode via `/api/game/cachemode`
  - applies updates via `POST /api/game/cachemode`

### 2026-04-01 — Empty-Ocean Fix (Desktop Cache Land Base)
- Investigated empty blue map in browser despite active world controls.
- Root cause identified in land base pipeline:
  - UI attempted contour polygon fill before raster mask fallback; contour branch could short-circuit without visible land fill.
  - Bridge landmask endpoint returned all-land fallback at high requested grid sizes when contour operation estimate exceeded threshold.
- Updated `ClientOnly/ui/src/components/AthenaMap.tsx`:
  - land build now prioritizes `/api/staticmap/{world}/landmask` first.
  - reduced requested landmask resolution from 1024 to 512 for faster/safer generation.
  - validates non-empty mask data and falls back to contour/elevation paths if needed.
- Updated `ClientOnly/bridge/server.js` landmask endpoint:
  - keeps requested grid for cache keying but auto-reduces effective raster grid (down to 64 min) when coastline complexity is high.
  - now returns a real generated coastline mask instead of defaulting to all-land for complex worlds.
- Validation after bridge restart:
  - `GET /api/staticmap/Stratis/landmask?gridSize=512` returned `width=256,height=256` with mixed land/ocean ratio (`18807/65536`, ~0.287).
  - `ClientOnly/ui` build passed (`npm run build`).
- Next concrete task:
  - Add optional direct `Rows`-based terrain texture overlay path (when row cache files exist) to match original desktop visual style beyond land/ocean silhouette.

### 2026-04-01 — Blue-Map Runtime Safety Fallback
- Added a last-resort land fill fallback in `ClientOnly/ui/src/components/AthenaMap.tsx`.
- New behavior:
  - if landmask fetch/render fails,
  - and contour fallback is unavailable,
  - and runtime elevations are missing/invalid,
  - map now renders a readable neutral land base rectangle instead of staying full ocean-blue.
- This keeps operator usability (units/roads context) while cache terrain sources recover.

### 2026-04-01 — Geometry Hydration Fault-Tolerance
- Diagnosed case where units were live but roads/structures/locations were missing in UI.
- Updated `ClientOnly/ui/src/hooks/useAthenaHub.ts` geometry hydration:
  - replaced brittle endpoint parsing with per-endpoint `fetchJsonSafe` handling.
  - each dataset now hydrates independently (`worldinfo`, `roads`, `forests`, `locations`, `structures`, `elevations`, `exportstatus`).
  - a single endpoint failure no longer prevents all geometry layers from appearing.
- Validation:
  - bridge endpoints are reachable and populated (`roads`, `structures`, `forests`, `locations`, `elevations`).
  - UI build passed after hook update.

### 2026-04-01 — Static Unit/No Group Labels Diagnosis
- Symptom reported: only land fallback visible; unit marker appears static; no group labels.
- Verified bridge diagnostics:
  - `/api/game/framedebug` `seenCount` stalled (and later zero after clean restart).
  - relay command tail showed no live `frame` stream, only occasional `success` acknowledgements.
- Conclusion:
  - movement/heading/group labels depend on continuous relay `frame` telemetry.
  - when relay stops sending `frame`, UI can only show stale/initial marker snapshots.
- Attempted bridge-side heartbeat to provoke frame updates, then rolled it back after it did not restore streaming.
- Current bridge state returned to clean mode (`General` registration, no synthetic heartbeat spam).
- Next concrete task:
  - validate Arma->Relay live frame production path (mission script/export loop and extension feed) until `/api/game/framedebug.seenCount` increments continuously.

### 2026-04-01 — Relay Endpoint/Type Verification + Pipe Recovery
- Verified active relay process and path:
  - `C:\Program Files (x86)\Steam\steamapps\common\Arma 3\!Workshop\@Athena - An Arma 2nd Screen Application\Relay.exe`
  - listening on port `28800`.
- Confirmed accepted relay client type for bridge remains `General`.
  - `Athena` and other guessed types return `error: unrecognized type`.
- Probed likely frame-poll command payloads (`frame`, `state`, `update`, `subscribe`, etc.): all returned `success` without enabling continuous frame stream.
- Inspected active `Relay.exe.log` and found repeated historical pipe instability (`SendPipeMessage :: Error: Pipe is broken`).
- Performed clean relay restart.
- Post-restart bridge observations:
  - frame count increased from 1 to 2 once,
  - but still not continuous (stalls at fixed `lastSeenAt`).
- Interpretation:
  - bridge/web path is healthy and connected,
  - remaining blocker is upstream continuous frame production from Arma extension -> Relay pipe for this run.

### 2026-04-01 — Dev URL Routing Fix (Splash Screen Confusion)
- Confirmed URL split:
  - `http://localhost:3000` = legacy bridge PoC page (`Athena Web PoC`)
  - `http://localhost:5173` = active React UI
- Added bridge root redirect in `ClientOnly/bridge/server.js`:
  - requests to `/` on port `3000` now redirect to `http://{host}:5173/` by default.
  - can be disabled with `UI_DEV_REDIRECT=false` if needed.
- Purpose:
  - prevent accidental landing on splash page while testing ClientOnly UI.
  - refreshes geometry immediately after apply/refresh
- Threaded cache-mode state/actions through `ClientOnly/ui/src/App.tsx` into the sidebar.
- Validation:
  - `npm run build` passed after UI integration changes.

### 2026-04-01 — Mission Init Instrumentation (Live Frame Stall)
- Revalidated ACS path is not required for original Relay/Desktop flow.
- Restarted bridge and relay stack; bridge remains relay-connected on `General` registration.
- Verified active mission launch path from latest RPT:
  - `...\missions\Editor%20Athena%20Test.Malden\`
- Added mission-side `init.sqf` in that folder and upgraded it with explicit `diag_log` probes:
  - `Athena Mission Init: starting`
  - `Athena Mission Init: player ready (...)`
  - `Athena Mission Init: launched init/loop scripts`
- Startup now triggers both preferred and fallback script paths:
  - `execVM "\athena\init.sqf"`
  - `execVM "\athena\loopPut.sqf"`
  - `execVM "\athena\loopGet.sqf"`
- Current status:
  - bridge API healthy, relay connected.
  - `framedebug.seenCount` still stalled at `1` until mission is restarted so new init instrumentation executes.

### 2026-04-01 — Eden Mission Path Sweep + Script Path Correction
- Applied `init.sqf` startup bootstrap to all local Eden mission folders under:
  - `...\NightHawk\missions\*Athena*`
  - `...\NightHawk\missions\~Editor*`
  - `...\NightHawk\missions\~tempMissionSP*`
- Added diagnostics + startup commands in each mission init:
  - `diag_log "Athena Mission Init: ..."`
  - `execVM "\athena\init.sqf"`
  - `execVM "\athena\loopPut.sqf"`
  - `execVM "\athena\loopGet.sqf"`
- Fixed path escaping bug introduced during batch injection:
  - normalized `"\\athena\\..."` to `"\athena\..."` in mission init files.

### 2026-04-01 — Relay Queue Pull Breakthrough (Frame Stream Restored)
- Root cause identified for stalled unit updates:
  - relay appears to require queue polling via `next` command for continuous delivery.
  - prior bridge behavior relied on passive receive (or `command` polling) and only saw sparse/cached frames.
- Updated bridge protocol polling in `ClientOnly/bridge/server.js`:
  - added configurable poll command type (`relayFramePollType`).
  - validated `next<ath_sep><ath_sep>end` polling restores continuous frames.
- Set new defaults:
  - `RELAY_FRAME_POLL_TYPE=next`
  - `RELAY_FRAME_POLL_INTERVAL_MS=250`
  - command payload remains empty.
  - guard added so empty `command` polls are not sent.
- Validation:
  - `/api/game/framedebug` now increments continuously (seen count rising each sample).
  - `/api/game/relaydebug` shows outbound `next` and inbound steady `frame` traffic.

### 2026-04-01 — End-of-Day Mission Init Cleanup
- Removed temporary mission-wide debug bootstrap injections created during triage.
- Current mission init footprint is now minimal and scoped to the active test mission only:
  - `Editor%20Athena%20Test.Malden/init.sqf`
  - content: `[] execVM "\athena\init.sqf";`
- Verification:
  - only one `init.sqf` remains under `...\NightHawk\missions`.
  - no remaining files contain `Athena Mission Init:` debug markers.

### 2026-04-02 — World-Change Auto-Detection Audit + Cache Fix
- Audited full world-change pipeline for fresh map scenario:
  1. Bridge receives new `map` name from relay frames → `gameState.map` updates immediately
  2. `buildClientState()` returns new world via `gameState.map || detectActiveWorld()`
  3. UI detects world change (`relayWorld !== lastHydratedWorld`) → triggers `hydrateGeometry()`
  4. REST endpoints use `detectActiveWorld()` → returns new world name → reads Athena Desktop disk cache
  5. `AthenaMap.tsx` resets `staticCacheRef` on world change → clears all rendered layers
- **Bug found and fixed**: `worldCacheMemo` was never invalidated when `mapend` arrived after export.
  - Result: if REST endpoints were called before Athena Desktop finished writing cache files, the stale/empty result was memoized and never refreshed.
  - Fix: added `worldCacheMemo.clear()`, `contourCacheMemo.clear()`, `landMaskCacheMemo.clear()` inside `handleMapImportMessage()` on `mapend`.
  - After fix, `mapImportDataReady` → UI `hydrateGeometry()` → REST endpoints re-read fresh disk cache.
- Confirmed safe behavior for already-cached worlds:
  - `loadWorldCache()` returns `null` (not memoized) when world dir doesn't exist.
  - Switching between cached worlds (Altis/Malden/Stratis/etc.) creates separate memo entries per world — no cross-contamination.
- Confirmed UI "Export World Data" button exists in Sidebar → sends `startMapExport` WS message → bridge sends `command<ath_sep>mapexport` to relay.
- Expected flow for fresh map test:
  1. Start Arma on new map → relay frames arrive with new `map` name
  2. Bridge updates `gameState.map` → UI detects world change → layers clear + re-hydrate from cache (if exists)
  3. User clicks "Export World Data" → Athena Desktop exports + writes cache files
  4. Relay delivers `mapbegin`→`mapend` → bridge clears all memos → broadcasts `mapImportDataReady`
  5. UI re-hydrates from REST → reads fresh disk cache → all layers render

### 2026-04-02 — Flat Military Cartography Style Overhaul
- Goal: match Bus's original Athena Desktop visual style — clean, flat, solid, sharp. No gradients, no fading, no soft edges. "16-bit precision military nav" aesthetic.
- **Rendering priority swap**: Z=0 contour vector polygon fill is now primary land path (smooth coastlines), raster mask demoted to fallback.
- **All opacities hardened to 1.0** (was 0.35–0.92 on various layers):
  - Road fills: 0.85 → 1.0
  - Road borders: 0.7 → 1.0
  - Airport surface strokes: 0.35 → 1.0
  - Contour lines: 0.58/0.7/0.76 → 1.0 (all three tiers)
  - Walls/fences: 0.88 → 1.0
  - Structure strokes: 0.92 → 1.0
  - Structure fills: 0.55 → 1.0
  - Waypoint lines: 0.85 → 1.0
  - Projectile trails: 0.9 → 1.0
  - Topo overlay: 0.75 → 1.0
  - Land fallback rectangle: 0.9 → 1.0
- **Forest cells fully opaque** (was semi-transparent alpha blending):
  - Dense (level 3): alpha 155 → 255 (solid dark green)
  - Medium (level 2): alpha 110 → 255 (new distinct mid-green: 148,182,140)
  - Sparse (level 1): alpha 78 → 255 (solid light green: 188,210,180)
- **Trees**: fill alpha 0.9 → 1.0 (solid forest green)
- **All line caps/joins changed to sharp**:
  - Coastline Z=0: round → butt/miter
  - Inland contours: round → butt/miter
  - Removed Chaikin corner-cutting smoothing on coastline (was softening grid-derived contour edges)
  - Coastline now single solid stroke (was dual outer+inner with semi-transparency)
- **Drop shadows solidified**:
  - Unit icons: rgba(0,0,0,0.5) → #000
  - Group icons: rgba(0,0,0,0.55) → #000
  - Vehicle SVG strokes: rgba(0,0,0,0.75) → #000
- **Text outlines solidified**:
  - Location labels: rgba(0,0,0,0.85) → #000
  - Unit marker labels: rgba(0,0,0,0.92) → #000
- **Global CSS added**:
  - `svg { shape-rendering: crispEdges; }` — flat SVG rendering, no anti-aliased softness
  - `canvas { image-rendering: pixelated; }` — global crisp canvas rendering
- **Dead code removed**: `smoothRing()` Chaikin function (no longer used after coastline change)
- Files changed: `AthenaMap.tsx`, `App.css`

### 2026-04-02 — Bus-Exact Rendering Values (dnSpy Decompilation)
- Decompiled Athena Desktop.exe with dnSpy and read all rendering code (MapHelper.cs, Common.cs, Marker.cs, Unit.cs, Vehicle.cs, XAML templates).
- Extracted every color, weight, alpha, and size value from Bus's original WPF renderer.
- Applied Bus-exact values to `ClientOnly/ui/src/components/AthenaMap.tsx`:
  - **Side colors**: `sideColor()` now returns Bus's ARGB(180,...) values as rgba (0.706 alpha):
    - WEST `rgba(78,118,204,0.706)`, EAST `rgba(204,78,78,0.706)`, GUER `rgba(0,128,0,0.706)`, CIV `rgba(178,0,255,0.706)`
  - **Road colors**: `roadStyle()` uses Bus named colors:
    - Highway=SandyBrown `#F4A460`, Concrete=LightGray `#D3D3D3`, Asphalt=DimGray `#696969`, Dirt=Tan `#D2B48C`, Unknown=Brown `#A52A2A`
  - **Contour lines**: `contourStyle()` uniform weight 0.75 (was variable 0.35–1.5):
    - Z=0 MediumBlue `#0000CD`, Z<0 DodgerBlue `#1E90FF`, Z>0 RosyBrown `#BC8F8F`
  - **Tree radius**: Fixed 2.0 (was zoom-scaled 1.5–3px), matching Bus's `EllipseGeometry(2.0, 2.0)`
  - **Ocean background**: `#B0E0E6` PowderBlue (already correct)
  - **Land fill**: `#FFFFF0` Ivory in all 5 render paths (was `#FEFFEF`)
  - **Coastline**: `#0000CD` MediumBlue, weight 0.75 (was `#7789C0`, weight 1.5)
  - **Forest cells**: Semi-transparent 27.5% alpha matching Bus's `ARGB(70,...)`:
    - Heavy `rgba(0,127,12,0.275)`, Light `rgba(0,181,18,0.275)` (was fully opaque 3-tier)
  - **Marching-squares contours**: Uniform 0.75 weight, exact named colors, no alpha variation
- Removed unused `is120`/`is60`/`is30` variables from marching-squares loop (were left over from old variable-weight logic).
- All changes verified with zero compile errors.

### 2026-04-02 — Forest Cell Size + Y-Flip Fix (Bus-Exact)
- Investigated Bus's `GenerateForestGeometry` via dnSpy decompilation:
  - Bus uses fixed 16×16 pixel cells: `new Rect(ForestPoint.X * 16.0, ForestPoint.Y * 16.0, 16.0, 16.0)`
- **Bridge fix**: Hardcoded `sampleSize = 16` in `bridge/server.js`:
  - Was: `Math.max(4, Math.round(worldSize / (maxIndex + 1)))` — gave 17 for Stratis, 21 for Tanoa (wrong grid sizes)
  - Now: fixed 16, matching Bus exactly
- **Y-flip off-by-one fix**: Changed Y formula from `worldSize - yIdx * sampleSize` to `worldSize - (yIdx + 1) * sampleSize`
  - Without this, cells at yIdx=0 mapped to worldY=worldSize instead of worldSize-16
- Result: forest overlay now aligns correctly with coastline, no longer leaks into ocean.

### 2026-04-02 — Land Polygon Bus-Exact Border
- Added `stroke: true, color: '#FFFFF0', weight: 1` to Z=0 land polygon, matching Bus's `GeneratePen(Thickness=1.0)`.

### 2026-04-02 — Land Refresh Race Condition Fix
- Diagnosed land fill disappearing on browser refresh:
  - contours start empty → fallback sets `landCacheRef.ready=true` → Z=0 contours arrive → guard blocks upgrade
- Fix 1: Added `source` field to `landCacheRef` (`'z0'` vs `'fallback'`) — fallbacks can be upgraded when Z=0 data arrives.
- Fix 2: Removed upfront `clearLayers()` — all three render paths now clear atomically just before adding replacement content, preventing blue flash during async fetches.
- Fix 3: Last-resort fallback skips if better content already showing.

### 2026-04-02 — v0.0.1 Release
- Published to GitHub: https://github.com/SgtFoose/Athena-Web-2
- Full bridge + React UI with Bus-exact rendering fidelity.
- All map layers functional: coastline, roads, forests, structures, contours, locations, elevation, live units.
- Video: https://youtu.be/4-AioVt9iUQ

## Current Known Limitations
- Runtime maprow parser is heuristic-first; needs live-capture verification against multiple mission/maprow payload variants.
- Road segment stitching is per-object local; optional future step is endpoint dedup/merge for long polylines to reduce draw calls.
- Relay map export delivery not yet confirmed working through bridge (relay acknowledges but does not deliver chunks).

## Web2 Parity Backlog (Bus Feature/Changelog Driven)

Goal: close the gap between Web2 and original Athena Desktop behavior while staying compatible with Bus's original relay/mod path.

Scope decision (2026-04-06): defer shot origin tracking, ballistic reconstruction, and active laze telemetry. These depend on extra runtime telemetry paths and are intentionally out of scope for Web2 parity work to avoid BattleEye-risky extension changes.

### Low Effort (S)
- [ ] Add optional connection diagnostics panel in UI (relay connected, frame age, active world, export phase/counters).
- [ ] Add ORBAT player/AI differentiation styling parity (italicize player-controlled entries).
- [ ] Add icon shadow parity toggle for unit/group markers (Desktop-like readability option).
- [ ] Add side-by-side world naming in selector and status areas (`nameDisplay` + internal world name where available).
- [ ] Add feature flag and UI label for multi-instance usage guidance (confirm same relay can feed multiple browser clients).

### Medium Effort (M)
- [ ] Add custom anchors (create/edit/delete) from coordinate input and map click.
- [ ] Add quick "Locate" workflow parity for locations and anchors (focus map + optional highlight pulse).
- [ ] Add tracking lines from active player to selected unit/group/anchor with live bearing and distance readout.
- [ ] Add session save/load for operator overlays (anchors, local drawings, selected layer/profile state).
- [ ] Add reconnect-safe map drawing layer (personal ink) persisted locally per world.
- [ ] Add manual relay pipe-name guidance and bridge UX support for alternate pipe deployments (documentation + runtime status visibility).

### High Effort (L)
- [ ] Add shared overlays over bridge WebSocket (anchors + ink sync across connected Web2 clients on same relay).
- [ ] Add session recording/playback for Web2 timeline review (record frame snapshots + UI playback controls).
- [ ] Add robust map/session playback mode isolation so playback cannot conflict with live relay mode.
- [ ] Add full export/import package parity for mission planning artifacts (portable JSON bundle).

### ACS/Community Features (Lower Priority)
- [ ] Design ACS-like collaboration mode for Web2 (outside-Arma planning sessions) with room model.
- [ ] Add room access controls (passworded rooms), explicit disconnect flow, and connection status notifications.
- [ ] Add presence/session list improvements with user display names and active world context.
- [ ] Add optional internet-safe deployment profile for remote collaboration (auth, rate limits, persistence boundaries).

### Cleanup / Hardening (found 2026-11-22)
- [ ] Bridge: validate world names in map-cache routes; make cache-mode changes POST-only (`fix/validate-world-names`; details withheld until fixed).
- [ ] Bridge: catch malformed URL encoding in the request handler so a bad request can't crash the process.
- [x] UI: remove leftover AR branding ("ATHENA REMASTERED") and fix the donate link in `ui/src/App.tsx` (done in v0.1.1).
- [ ] UI: drop the unused `@microsoft/signalr` dependency.
- [x] UI: sync `ui/package.json` version with `APP_VERSION` (done in v0.1.1).
- [ ] UI: review laze/projectile code inherited from AR against Web2's capability boundary.
- [ ] Review `npm audit` advisories in `ui/`.
- [ ] Repo: tracked ~59 MB EXEs trigger GitHub's large-file warning on every release. Consider Git LFS, or keeping EXEs only as Release assets.
- [ ] UI: welcome background `athena-default-bg.png` shows "ATHENA REMASTERED" branding; make the welcome image per-app (`appConfig`) and add a Web2 image.
- [ ] UI: closed-ring coastline smoothing leaves a small gap in the stroke at the ring start/end (seen in the 2026-11-22 simulation).

## Next Tasks (Immediate)
1. Connection diagnostics panel (S)
2. ORBAT player/AI styling parity (S)
3. Anchors + Locate workflow (M)
4. Tracking lines + bearing/distance (M)
5. Session save/load for overlays (M)

## Archived Source Notes (Bohemia Forum Thread)

Thread: Athena - An ARMA 2nd Screen Application

Live URL has intermittent availability, but content is recoverable via Wayback snapshots.

Verified archive captures:
- https://web.archive.org/web/20240503100332/https://forums.bohemia.net/forums/topic/171846-athena-an-arma-2nd-screen-application/
- https://web.archive.org/web/20240227191949/https://forums.bohemia.net/forums/topic/171846-athena-an-arma-2nd-screen-application/
- https://web.archive.org/web/20190719161758/https://forums.bohemia.net/forums/topic/171846-athena-an-arma-2nd-screen-application/
- https://web.archive.org/web/*/https://forums.bohemia.net/forums/topic/171846-athena-an-arma-2nd-screen-application/

Key feature statements extracted from archived thread (used for Web2 parity planning):
- Multiple Athena app instances supported from same game feed.
- Automatic map import for vanilla and custom worlds.
- Shaded height-map style views.
- Private and shared ink/map drawing workflows.
- ORBAT updates based on role/equipment changes (example: MG/AT role changes).
- Location list with locate action.
- Custom anchor creation from coordinates/grid references.
- Tracking lines from own unit to unit/group/anchor with bearing and distance.
- Session record and playback capability.
- ACS collaboration scenarios (mission planning/training outside live gameplay).

Web2 compatibility interpretation notes:
- Core map, ORBAT, locations, anchors, tracking lines, and local record/playback are feasible in Web2 scope.
- ACS collaboration remains lower-priority and should be phased after core desktop-parity tasks.
- Shot origin, ballistic tracking, and active laze telemetry remain deferred to avoid BattleEye-risky extension changes.

## Web2 Parity Matrix (Against Bus Thread Features)

Status key: Done | Partial | Planned | Deferred

1. Multi-instance second-screen usage
- Status: Done
- Notes: Web2 bridge supports multiple browser clients against one relay feed.

2. Automatic map import support (vanilla and custom)
- Status: Partial
- Notes: Export trigger and cache hydration exist; runtime-only fallback for maps without completed desktop cache remains a gap.

3. Shaded height-map views
- Status: Done
- Notes: Ground and Pilot map modes are implemented.

4. Private map drawing (ink)
- Status: Planned
- Notes: Local reconnect-safe drawing layer and persistence are in backlog.

5. Shared map drawing (ink sync)
- Status: Planned
- Notes: WebSocket-shared overlays planned; not implemented yet.

6. ORBAT real-time role/equipment representation
- Status: Partial
- Notes: ORBAT and weapon fields are present; role-label parity updates still planned.

7. Location list with Locate action
- Status: Done
- Notes: Locations tab supports focus/locate behavior.

8. Custom anchors from coordinates/grid
- Status: Planned
- Notes: Anchor create/edit/delete flow is queued in medium-effort backlog.

9. Tracking lines + bearing + distance (unit/group/anchor)
- Status: Planned
- Notes: Follow-active-player exists; explicit line/bearing/distance UI not yet implemented.

10. Session save/load (planning artifacts)
- Status: Planned
- Notes: Overlay/session save-load is queued in medium/high backlog.

11. Session recording and playback
- Status: Planned
- Notes: Recording/playback mode and live-mode isolation are queued in high backlog.

12. ACS collaboration for planning/training outside Arma
- Status: Planned
- Notes: Explicitly lower priority than core desktop parity.

13. Shooting origin, ballistic reconstruction, active laze telemetry
- Status: Deferred
- Notes: Deferred intentionally to preserve original-mod compatibility and avoid BattleEye-risky extension changes.

## Quick Run Commands
```powershell
# Terminal 1: bridge
cd bridge
npm install
node server.js

# Terminal 2: UI
cd ui
npm install
npm run dev
```

## Verification Checklist
- UI loads (not blank)
- Relay connected state turns green
- Live units render and update
- Map layers render (coastline, roads, forests, structures, contours, locations)
- Layer toggles work
- World auto-detection resolves correct map
