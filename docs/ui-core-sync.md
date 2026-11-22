# Shared UI core: Athena Web 2 → Athena Remastered

This document describes how the browser UI is shared between **Athena Web 2** (this repo, `v0.x`) and **Athena Remastered** (AR, https://github.com/SgtFoose/AthenaRemastered, `v1.x`). The two apps stay separate products with separate repos, releases and Workshop items. Only the UI *core* is shared.

## Why

AR came first. Web2 started from a copy of AR's `Frontend/` (2026-04-01) and has since received many rendering and UI fixes, which AR missed. Examples are Bus-exact colours, contour and coastline smoothing, airport surfaces, tree rings, vehicle and NATO icons, collapsible sidebars, I-TGT, world-switch guards and the custom-map world-size fix.

A comparison on 2026-11-22 found the following:

- `types/game.ts` is still the same data contract. Web2 only adds `RelayMarker` and `GameFrame.markers`.
- Build config, tsconfigs, ESLint config, dependencies and all shared `public/` assets are byte-identical.
- About 80% of the `AthenaMap.tsx` differences are pure rendering, which doesn't depend on the data source.
- The real difference is the **data layer**:
  - Web2 uses a Node bridge, a WebSocket and Athena Desktop's disk cache.
  - AR uses an ASP.NET Core backend, SignalR and a custom extension DLL.

## Rule: one-way sync

**Web2 → AR only.** The shared core lives in this repo and is copied *to* AR. Nothing flows from AR into Web2, because AR features usually depend on the custom DLL, which Web2 must not assume.

## Layout

```
ui/src/
  core/            shared with AR — must not import from adapter/ or App.tsx
    components/    AthenaMap, Sidebar, EventFeed
    hooks/         useStaticMap, useAthenaLibrary
    types/         game.ts (data contract), ui.ts (LayerVisibility, RenderMode)
  adapter/         Web2-only data layer: useAthenaHub (bridge WebSocket/REST),
                   useHealthCheck, MapCacheBanner
  appConfig.ts     per-app values the core reads: API base, name, version,
                   donate link, capabilities
  App.tsx          app shell (per app for now)
```

Each app provides its own `appConfig.ts` with the **same exports**. The core imports app-specific values only from there.

### `appConfig.ts` contract

| Export | Web2 value | AR value (planned) |
|---|---|---|
| `API_BASE` | bridge, `:3000` in dev | AR server, `:5000` |
| `APP_NAME`, `APP_VERSION` | `Athena Web 2`, `0.x` | `Athena Remastered`, `1.x` |
| `DONATE_URL` | `https://paypal.me/jeroenvdvelden` | same PayPal.Me link (AR's old `donate/?business=` link fails for this account) |
| `CAPABILITIES` | see below | see below |
| `MAP_DATA.treesUrl` | `/api/game/trees` | `/api/game/trees` (same endpoint) |
| `MAP_DATA.landmaskUrl` | `/api/staticmap/{world}/landmask?gridSize=` | same endpoint |
| `MAP_DATA.landmaskGridSize` | `1536` | `1024` (AR's previous value, to be verified) |
| `MAP_DATA.landmaskRowOrder` | `south-first` (bridge: row 0 = worldY 0) | **`north-first`**: AR's `StaticMapController.cs:65` documents "row 0 = north (image top)". Without this, AR's land fill would render upside down. |

Build label (both apps):
- `APP_BUILD` / `APP_BUILD_DATE` come from `vite.config.ts` `define`. That file is identical in both apps, and `src/build-info.d.ts` declares the constants.
- Releases set `ATHENA_RELEASE=1`, which gives an empty label. Test builds get `test (<sha>[-dirty])`.
- The core shows `APP_VERSION_LABEL` (`core/utils/version.ts`) in the sidebar header and footer.

### Core components and hooks

- **Map and sidebar:** `AthenaMap`, `Sidebar`
- **App shell:** `AppShell` (collapsible sidebars), `SidebarHeader`, `ITgtTargetsPanel`, `WelcomeOverlay` (texts and steps supplied by the app), `ExportStatusOverlay`, `MapErrorBoundary`
- **Hooks:** `useStaticMap`, `useAthenaLibrary`, `useITgtTargets`, `useFollowActivePlayer`, `useTouchInput`
- **Utils:** `utils/firewillITgt.ts`

`App.tsx` stays per app. It composes the core with the app's own data hook and its texts.

## AR-only features live in the AR repo (owner decision 2026-11-22)

Active lazes, projectile tracking/prediction, the Fire Control panel and the killed/fired event feed (`EventFeed`) need AR's extension DLL. Web2 can neither use nor test them, and they need live AR testing to fix bugs. For example, during the owner's MLRS test the AR map showed no projectile track even though "Recent Shots" received the rockets.

So this code is **not** part of the shared core. The core offers extension points instead:

| Extension point | Where | What AR plugs in |
|---|---|---|
| `AthenaMap` prop `mapExtensions` | rendered inside the Leaflet `MapContainer` (children can use `useMap()`) | laze layer, projectile prediction layer |
| `AthenaMap` prop `overlayExtensions` | rendered over the map area, outside Leaflet | Fire Control panel |
| `AthenaMap` prop `pulseUnitIds` | `Set` of unit ids | units that fired recently (computed by AR from its fired events); drives the existing firing pulse on unit/vehicle/group markers |
| Reserved panes `athena-laze` (z 716) and `athena-projectile` (z 717) | created by the core | AR's layers use these panes. Their visibility follows `layers.lazes` / `layers.projectiles` when the matching capability is on. |
| App-level slot | each app's own `App.tsx` / `AppShell` `rightPanel` | AR renders `EventFeed` itself |

### Projectile tracking bugs observed live (AR v1.1.6, Malden, 2026-11-22)

| Weapon | Observed | Likely cause (from AR code; to verify in Phase 2) |
|---|---|---|
| M5 Sandstorm MLRS, `rockets_230mm_GAT` / `R_230mm_HE`, fired at grid 066047 (DIST 3014 m, ETA 33 s) | Shots appear in "Recent Shots", but there is no track on the map and no Fire Control entry. | The weapon *is* recognised (`rocket` profile, AR `AthenaMap.tsx:393`). Probably the track is dropped because there is no target data and the allowed track angle is narrow (`maxTrackAngleDeg: 22`). |
| M4 Scorcher, `mortar_155mm…` / `Sh_155mm…` | Fire Control shows Lock FREE, Src **HDG**, ETA 5 s, target 5180,11729; a dashed line runs straight north from the vehicle. | No target data for indirect fire (`targetSource` missing), so it falls back to the **vehicle (hull) heading** instead of the gun/turret azimuth and the artillery computer's target. The ETA and target are computed along that wrong line. |

Additional code smell: the missile profile matches `text.includes('at')` (AR `AthenaMap.tsx:405`), which also hits "gat", "battery" and "artillery". It only works today because the rocket and artillery checks run first.

Planned AR fix direction: send the projectile's initial velocity vector (azimuth + elevation) from the `Fired` event through AR's SQF/DLL, compute the ballistic impact point from it, and keep `fired_impact` as the correction. Replace the substring weapon matching with explicit, ordered rules.

### Design: AR projectile tracking v2 (approved by the owner 2026-11-22)

**Current approach (built ~7 months ago):**
- the direction comes from the turret azimuth of the first round
- distance and time come from per-weapon curves fitted to two test shots (`ETA ≈ ballA × d^ballB`)
- the impact point and ETA are corrected only after the first impact

That is a guess plus a correction. Arma itself knows the exact trajectory, and AR's DLL lets us read it out.

**New approach**, all AR-side (SQF + DLL + backend + the AR UI extension):

1. **Exact launch data.**
   - Hook the `Fired` event, or better the `ProjectileCreated` mission event, which also covers other players' projectiles in multiplayer.
   - Read `velocity _projectile` and the launch position.
   - That gives the real launch azimuth **and** elevation, including the angle the artillery computer chose. No turret estimate and no first-round calibration.
2. **Physical trajectory prediction.**
   - Simulate Arma's projectile model in small time steps: gravity plus drag (`airFriction` from `CfgAmmo` × v·|v|).
   - Stop where the path meets the terrain, using the elevation data already in the map cache.
   - This gives the impact point and ETA on the **first** shot, for any range, with no per-weapon `ballA/ballB` table.
3. **Live in-flight tracking.**
   - One shared low-rate loop (about 4 Hz) samples the position and velocity of all active projectiles and sends them through the DLL.
   - The UI draws the **real** trail and re-runs the prediction from the latest sample, so the impact estimate converges during flight.
   - This is required for MLRS rockets: their motor burns in the first seconds, so a pure ballistic prediction from the launch velocity is wrong there.
4. **Exact impact.**
   - The projectile `Explode` event handler (Arma 3 2.10+) reports the exact impact position.
   - This replaces the estimate-from-first-impact correction and makes `fired_impact` exact.
5. **Weapon classification:** explicit, ordered rules on `CfgAmmo` simulation type and class (`shotShell`, `shotRocket`, `shotMissile`, …) instead of substring matching on names.

| | Current | v2 |
|---|---|---|
| Direction | turret azimuth (estimate) | real launch vector |
| First shot | inaccurate until first impact | accurate immediately |
| ETA | curve fitted to two test shots | physics simulation + live updates |
| MLRS rockets | no track | live tracking (covers the motor phase) |
| Impact | estimated | exact (`Explode`) |
| Other players' fire (MP) | unclear | `ProjectileCreated` |

**To verify live before building on it:**
- `ProjectileCreated` and `Explode` are available in the owner's Arma version
- `airFriction`-based trajectories match real 155 mm and 230 mm impacts
- FPS cost of tracking a full MLRS salvo (12 rockets)

**Performance guardrails:**
- one shared loop rather than one script per projectile
- a cap on the number of tracked projectiles
- data is sent only while projectiles are in flight

The removed Web2 copy of that code is still in git history: commit `966b6bc` (`ui/src/core/components/AthenaMap.tsx`, and `EventFeed.tsx`). AR's current frontend also has its own copy. In Phase 2, AR builds its extension from those sources and fixes the projectile tracking bug there, with live testing.

## Capabilities

`CAPABILITIES` in `appConfig.ts` tells the core what the app's data source provides. Since the AR-only code moved out, the flags only control UI the core still owns: sidebar toggles and pane visibility.

| Flag | Gates (in the core) | Web2 | AR (planned) |
|---|---|---|---|
| `lazes` | "Active Lazes" toggle + `athena-laze` pane visibility | off | on |
| `projectiles` | "Projectile Tracking" toggle + `athena-projectile` pane visibility | off | on |
| `killFeed` | nothing in the core; documents that the app has a kill/shot feed (AR renders `EventFeed` itself) | off | on |
| `relayMarkers` | Arma / Firewill I-TGT / AO / Spawn marker toggles | on | off, until AR streams mission markers |
| `waypointToggle` | "Waypoints" toggle (the layer itself renders whenever waypoint data arrives) | off | on |

## Phases

1. **Web2 restructure.** Split `ui/src` into `core/` and `adapter/`, add `appConfig.ts` and capabilities, and extract the portable parts of `App.tsx` into the core. Also repair glyphs lost in an earlier encoding cleanup (`−`, `×`, `·`). This phase changes no functionality.
2. **AR adoption** (on a branch in the AR repo):
   - copy the core into AR
   - write an AR adapter that maps the SignalR hook onto the same shape (lazes, projectiles and kill feed on; relay markers off)
   - check the data-specific tuning against AR data: landmask orientation, `hide`-road and airport thresholds
   - test live in Arma without BattlEye
3. **Sync tooling.** A script copies `ui/src/core/` from Web2 to AR and writes a checksum manifest, so drift can be detected. The sync rules are documented in both repos' `CLAUDE.md`.

## AR repository and release alignment (owner request 2026-11-22)

AR's GitHub repo should follow the same structure and release process as Web2. Observed state of `SgtFoose/AthenaRemastered`:

- 0 releases and 0 tags (Web2 publishes a GitHub Release with the EXE for every `v0.x`)
- the source sits in a nested `AthenaRemastered/` folder, and there is a duplicate `@AthenaRemastered/` mod folder at the repo root
- a tracked `publish/wwwroot` build output, even though `publish/` is in `.gitignore`
- no `WORKLOG.md` and no `CLAUDE.md`; the About text still reads "a repository for testing…"
- unknown whether `main` has a PR ruleset like Web2's `Protect Master`

Planned alignment, on AR branches with the owner's approval per step:

1. **Process files:** `CLAUDE.md` (rules #1–#4 plus AR context: DLL, BattlEye, server), `WORKLOG.md`, and a release-notes template with the same end-user structure as Web2 (Quick Start, first-time setup, troubleshooting, SmartScreen, What's New, Known Limitations), adapted to AR's server and the non-BattlEye requirement.
2. **Branch protection:** the same ruleset as Web2 (PR required, 0 approvals, no deletion or force-push).
3. **Repo layout:** decide with the owner whether to flatten the nested `AthenaRemastered/` folder, which of the two `@AthenaRemastered/` folders is canonical, and whether to stop tracking `publish/wwwroot`. Layout changes are risky for build scripts and Workshop paths, so this is done as one reviewed PR.
4. **Releases:** tag the current state (`v1.1.6`) and publish GitHub Releases with the server package as an asset, the same way as Web2's `AthenaWeb-<version>.exe`.
5. **README / About:** the same section structure as Web2's README (version, downloads, Quick Start, Workshop, credits) and an up-to-date About description.

## AR-side fixes planned for Phase 2

Found during the owner's live AR test on 2026-11-22. These are AR repo changes, made on a branch in the AR repo:

| Problem | Cause | Planned fix |
|---|---|---|
| AR's map shows Malden (the last-used world) as soon as the backend starts, even while Arma is still in the main menu. Users think the game is running or stuck. | `GameStateService` calls `LoadCachedWorld()` in its constructor. | Drop the startup auto-load. Show the shared core's welcome screen until a real mission arrives; offline maps stay available through the world picker. |
| The AR script runs in Arma's main menu and triggers a world export of the menu's background world. | `init.sqf` is a `postInit` function, with no main-menu guard. | Add a main-menu guard at the top of `init.sqf` (exact check verified live). |
| A full world export runs on every mission start, even for a cached world. Live on Malden: "Rendering map for: Malden — This may take a few minutes…" while the backend had already loaded Malden from cache, and the game dropped to ~16 FPS during the export. | `PutMission()` always enqueues `world`. | Only export when the world isn't cached yet. |
| Huge square forest blocks (hundreds of metres, dark grey-green) that don't follow the real forest and spill over the coastline; a stair-stepped coastline; opaque forest. Seen live in AR v1.1.6 (Workshop) on Malden. | AR has the pre-fix rendering. In Web2, the forest grid fix (fixed 16×16 cells + `worldSize - (yIdx + 1) * 16` Y-flip) lives in the **bridge** (`bridge/server.js`), not in the UI. | Smoothing and Bus-exact colours come with the shared core. The forest cell size and Y-flip must also be fixed in AR's **C# backend**, wherever it builds forest cells. |
| Sidebars can't be collapsed (awkward on tablets). | AR has the pre-Web2 app shell. | Comes with the shared core's app shell (Phase 1 step 2). |
| No I-TGT. | AR predates the I-TGT work. | **Stored I-TGT targets** (T / middle click / SAVE TGT, target panel) are UI-only and come with the shared core. **Firewill I-TGT markers** need mission markers from the game: Web2 gets them from Bus's relay, but AR's backend has no markers (`GameFrame` has no `Markers`). Optional AR feature: collect `allMapMarkers` in AR's SQF/DLL and stream them as `GameFrame.markers`; then enable `CAPABILITIES.relayMarkers` in AR. |

## Sync tooling (Phase 3)

`scripts/sync-core-to-ar.ps1` mirrors `ui/src/core/` into AR's `Frontend/src/core/`:

```powershell
.\scripts\sync-core-to-ar.ps1 -WhatIf          # show what would change
.\scripts\sync-core-to-ar.ps1                  # sync (AR path defaults to C:\AthenaRemastered)
.\scripts\sync-core-to-ar.ps1 -ArRepo D:\AR    # other AR checkout
```

- It refuses uncommitted Web2 core changes, so the manifest always points at a real Web2 commit (override: `-AllowDirty`).
- It writes `Frontend/src/core/core-manifest.json` in AR (source commit, UTC timestamp, SHA-256 per file).
- **Drift check:** files edited, added or deleted by hand in AR's core since the last sync abort the run and are listed (override: `-Force`).
- It deletes AR core files that no longer exist in Web2.
- It never commits. Review the change and commit it in AR on a branch.

## Status

- 2026-11-22: analysis done, approach approved by the owner. Phase 1 started on branch `refactor/ui-core-split`.
- 2026-11-22: Phase 1 done on that branch:
  - step 1: core/adapter split
  - step 2: app shell into the core
  - step 3: capabilities
  - step 4: glyph repair
  - step 5: `MAP_DATA`
  - step 6: AR-only laze/projectile/Fire Control/EventFeed code removed from the core, replaced by extension slots
  - Verified with build, lint and before/after screenshots in the fake-relay simulation. Next: Phase 1 PR, then Phase 2 in the AR repo.
