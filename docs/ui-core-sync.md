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
