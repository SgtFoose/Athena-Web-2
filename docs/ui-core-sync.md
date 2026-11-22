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

## Capabilities

AR has data that Web2 never gets: active lazes, projectile tracking, and killed/fired events. Shared core code for those features is switched on or off through `CAPABILITIES` in `appConfig.ts`, not by whether data happens to arrive.

## Phases

1. **Web2 restructure.** Split `ui/src` into `core/` and `adapter/`, add `appConfig.ts` and capabilities, and extract the portable parts of `App.tsx` into the core. Also repair glyphs lost in an earlier encoding cleanup (`−`, `×`, `·`). This phase changes no functionality.
2. **AR adoption** (on a branch in the AR repo):
   - copy the core into AR
   - write an AR adapter that maps the SignalR hook onto the same shape (lazes, projectiles and kill feed on; relay markers off)
   - check the data-specific tuning against AR data: landmask orientation, `hide`-road and airport thresholds
   - test live in Arma without BattlEye
3. **Sync tooling.** A script copies `ui/src/core/` from Web2 to AR and writes a checksum manifest, so drift can be detected. The sync rules are documented in both repos' `CLAUDE.md`.

## Status

- 2026-11-22: analysis done, approach approved by the owner. Phase 1 started on branch `refactor/ui-core-split`.
