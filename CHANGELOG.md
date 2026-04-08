# Changelog

All notable changes to Athena Web 2 will be documented in this file.

## [0.0.6] — 2026-04-08

### Added

- Added Firewill-style I-TGT 8-digit coordinate readout (`XXXXYYYY`) to the map cursor overlay while preserving legacy XY coordinate display
- Added keyboard and mouse capture flow for I-TGT targets (`T` hotkey or middle mouse click) with a persistent right-panel target list
- Added target management actions in the right panel: rename, per-target copy, per-target delete, and bulk Delete All
- Added map rendering for stored I-TGT targets using blue tactical triangle markers with persistent labels
- Added touch/tablet I-TGT capture workflow: tap map to set cursor and use in-map `SAVE TGT` action for storing targets without keyboard/middle mouse
- Added iPad Safari landscape top-offset handling (`safe-area` + `dvh`) to keep top UI controls visible under browser chrome/favorites bar

### Changed

- Increased on-map I-TGT triangle marker size for clearer visibility during target planning
- Enlarged and simplified the bottom-right coordinate overlay for easier in-flight readability
- Updated right-panel I-TGT guidance text to adapt automatically for touch input vs mouse/keyboard input
- Updated I-TGT default label sequence behavior so first saved target is `TGT_0`

### Fixed

- Fixed cross-map airport visual regression where yellow taxi centerline overlays appeared on cached maps outside Malden; runway/taxi surfaces now remain neutral gray across all cached worlds
- Fixed I-TGT label counter reset so using `Delete All` restarts new entries from `TGT_0`

### Removed

- Removed the bottom-right overlay copy-to-clipboard icon/button (copy remains available from stored target entries in the right panel)

## [0.0.5] — 2026-04-08

### Fixed

- Fixed Stratis static-layer misalignment after world swaps where shoreline, roads, structures, and location labels rendered shifted toward the bottom due to incorrect world-size fallback precedence
- Fixed Stratis world-size fallback in relay-frame world mapping (`8192`) to prevent scale drift before cache world metadata hydrates
- Fixed remaining missing vehicle icon cases by expanding classname fallback heuristics for modded/common families (helis, jets, APC/IFV, MBT, trucks/cars, boats)
- Fixed fallback vehicle icon behavior so unknown classes no longer render as question-mark symbols
- Fixed UAV/radar-class marker parity for key assets (Greyhawk, Sentinel, Stomper, Falcon, Pelican, AN/MPQ-105, Ship MRLS) by applying Athena Desktop NATO marker textures (`*_uav`, `*_installation`, `*_art`) instead of generic plane/static MG fallback
- Fixed quad-bike icon mapping so quadbike classes no longer render as two-wheel motorcycle icons
- Fixed Malden airport taxi-lane rendering by switching to hide-tile topology detection with centerline overlays while keeping nearby normal roads unchanged
- Fixed drone/turret consistency by applying Arma-style NATO marker textures for all drone and turret categories (with subtype-specific turret symbols)
- Added AI fallback badge behavior so generic AI-only vehicle classes (for example `*_UAV_AI`) render a visible `AI` tag when specific class detail is unavailable

### Changed

- Added collapsible left/right sidebar controls to improve map visibility and usability on iPad Mini/tablet resolutions
- Moved sidebar collapse/expand controls to the top edge of each map-side border for quicker tablet access
- Restored group marker visibility by enabling Groups layer by default at startup
- Hid non-functional Waypoints layer toggle from UI for this Web2 release line

### Tweaked

- Increased airport taxi-lane detection radius for runway-adjacent zero-width road segments so Malden taxi lanes render reliably
- Updated airport taxi-lane styling from subtle centerline to high-contrast paved strip rendering for better visibility on Malden
- Expanded airport taxi-lane classification fallback (runway proximity + segment-length constraints) and switched to bright high-contrast lane color to ensure lanes remain visible on Malden
- Tightened airport taxi-lane classifier (airport-bounds + strict runway-near segment criteria) and changed lane tint to neutral concrete to avoid recoloring normal roads around the airfield
- Switched Malden taxi-lane rendering to hide-tile topology detection (lane-like hide strips with centerline overlay), removing normal-road recoloring around the airport

### Documentation

- Updated release metadata/docs for `v0.0.5` in README, worklog, and release notes template
- Finalized `v0.0.5` as release-ready after validation that airport-adjacent normal roads render with standard styling

## [0.0.4] — 2026-04-07

### Changed

- Groups layer is now disabled by default on startup
- Removed zoom-threshold auto-switching between groups and units/vehicles; layer visibility now follows explicit user toggles

### Fixed

- Fixed stale map overlay carry-over during live world swaps by clearing old geometry immediately and ignoring stale hydration responses
- Fixed cross-world static overlay contamination by gating rendered geometry to the currently active world only
- Fixed offline cached-world selection so map picker remains available when the game/relay is not connected
- Fixed bridge-connected editor-idle state where stale live map values blocked map switching by expiring live map state after frame inactivity
- Fixed disconnected/unknown-world startup behavior to show the Athena welcome/fallback screen until a world is selected or received
- Fixed false cache-warning banner cases where the active world was already available in local cache
- Fixed vehicle toggle behavior where zoom auto-switch logic could override user selection and hide vehicles unexpectedly
- Fixed mounted-entity visibility gap: when relay vehicle coordinates are zero, vehicle markers now fall back to occupant positions and mounted units remain visible if their vehicle has no valid position
- Fixed contour rendering parity drift by replacing angle-regularized contour simplification with Athena Desktop-style world-scaled trimming behavior
- Fixed vehicle icon fallback issue where all vehicles rendered as question-mark symbols by hardening relay class extraction and class/category mapping lookup (including numeric `class` payload edge cases)
- Fixed dirt/gravel track appearance where `hide` road tiles were rendered as concrete grey instead of brown path styling
- Fixed fragmented dirt path rendering where hide-tile centerline stubs appeared detached and north-facing by stitching path-like hide tiles into connected brown polylines
- Fixed runway/apron color regression by classifying square hide tiles as concrete surfaces while keeping elongated hide tiles as dirt-path styling
- Fixed hedge/bush strip orientation mismatch by removing forced +90 degree hedge rotation offset

### Added

- GitHub release distribution flow for end users: `v0.0.4` release now includes `AthenaWeb-0.0.4.exe` under **Assets**
- README first-time map export guide with screenshot for required Athena Desktop one-time per-map export setup
- In-app map cache warning banner link to first-time map export instructions in README

### Documentation

- README now includes explicit `v0.0.4` release link and tracked bridge dist binary note
- Release notes template added for consistent user-facing download/start/SmartScreen guidance

## [0.0.3] — 2026-04-06

### Changed

- Shoreline geometry rendering now simplifies sea-level contour stair-steps into cleaner diagonal segments (instead of rounded wobble) for closer parity with Athena Desktop coastlines
- Tree marker styling now renders with a dark outer ring and green center, with zoom-aware sizing so trees do not appear unnaturally tiny when zooming in

### Notes

- This release focuses on map rendering parity only (shoreline + tree symbols)
- Shot origin, ballistic tracking, and active laze telemetry remain intentionally deferred for BattleEye-safe Web2 compatibility

## [0.0.2] — 2026-04-05

### Added

- Map cache health banner — warning shown when Athena Desktop cache is missing, empty, or the active world is unavailable
- `useHealthCheck` hook — polls `/api/health` every 15 seconds for cache and bridge status
- World picker dropdown in sidebar — browse cached maps offline for pre-mission planning
- Auto-override to live game world — clears manual world selection as soon as live game world data arrives
- Follow Active Player auto-disable — manual drag/zoom interaction disables follow mode

### Changed

- Sidebar map source controls simplified for Web2 usage
- README and worklog updated to keep Web2 documentation isolated from Athena Remastered

### Fixed

- Shoreline land/ocean edge artifacts reduced by preferring vector Z=0 coastline fill and sealing edge gaps
- Raster fallback shoreline quality improved with higher-resolution masks and edge dilation for full shoreline coverage
- Post-export map rebuild now refreshes shoreline fill automatically when `mapImportDataReady` is received
- Packaged bridge UI (`bridge/wwwroot`) now synced from `ui/dist` during release flow so live bridge serves latest fixes

## [0.0.1] — 2026-04-02

First functional release. Full second-screen tactical map rendered in the browser using Bus's original Athena relay.

### Added

- **Bridge server** (`bridge/server.js`) — Node.js TCP relay-to-REST/WebSocket bridge
  - Connects to Athena Relay on TCP 28800
  - Parses `<ath_sep>` / `<ath_sep>end` protocol framing
  - Polls relay with `next` command for continuous frame delivery
  - Reads Athena Desktop map cache from disk (`Documents/Athena/Maps/`)
  - Serves REST API for world info, roads, forests, structures, trees, locations, elevations, contours, land masks
  - WebSocket broadcast of live game state to browser clients
  - World auto-detection from relay frames or most recently updated cache folder
  - Runtime cache mode API (switch worlds without restart)
  - Map export trigger (`command<ath_sep>mapexport`) with cache invalidation on `mapend`
  - In-memory caching for contour, landmask, and world metadata
  - Debug/diagnostic endpoints for relay trace, frame inspection, elevation parsing

- **React UI** (`ui/`) — Leaflet-based tactical map with full layer stack
  - Coastline rendering from Z=0 contour polygons (vector) with raster mask and elevation fallbacks
  - Road rendering with multi-segment reconstruction, runway/airport surface tiles
  - Forest overlay with Bus-exact 16×16 cell grid and semi-transparent density fill
  - Individual tree rendering from Athena Desktop cache
  - Structure footprints with rotation
  - Location labels (cities, villages, peaks, airports, etc.)
  - Contour lines (marching-squares) with Bus-exact colors and weights
  - Elevation topo color ramp and greyscale overlays
  - Live unit markers with side-colored icons, heading indicators, and labels
  - Group markers with member counts and side coloring
  - Vehicle markers with type-specific SVG icons
  - Waypoint lines and markers
  - Projectile tracking trails
  - Active laser markers
  - Kill and shot event feed panel
  - ORBAT panel with unit/vehicle listing and side filtering
  - Layer toggle controls (contours, forests, trees, roads, structures, locations, groups, waypoints, active lasers, projectile tracking, vehicles, units)
  - Map style modes (2D, Ground, Pilot)
  - Map source controls (world selection, cache mode)
  - Export World Data button
  - Follow active player tracking
  - Map data statistics display
  - Responsive layout for phone/tablet

- **Bus-exact rendering fidelity** — all colors, weights, and styles decompiled from Athena Desktop.exe (dnSpy) and faithfully reproduced:
  - Side colors: WEST blue, EAST red, GUER green, CIV purple (ARGB 180 alpha)
  - Road colors: Highway=SandyBrown, Concrete=LightGray, Asphalt=DimGray, Dirt=Tan
  - Contour colors: Z=0 MediumBlue, Z<0 DodgerBlue, Z>0 RosyBrown (all weight 0.75)
  - Ocean=PowderBlue, Land=Ivory
  - Forest: Heavy=`rgba(0,127,12,0.275)`, Light=`rgba(0,181,18,0.275)`
  - Tree radius: fixed 2.0 (matching Bus's `EllipseGeometry`)
  - Coastline: MediumBlue with Ivory border (weight 0.75 + 1.0)

### Fixed

- Forest cell size locked to 16×16 (matching Bus's `GenerateForestGeometry`) — previously computed from maxIndex giving wrong grid sizes per map
- Forest Y-flip off-by-one: `worldSize - (yIdx+1)*16` instead of `worldSize - yIdx*16`
- Land fill race condition on browser refresh — atomic clear-before-add pattern prevents blue flash
- Land cache source tracking allows Z=0 vector upgrade over raster/elevation fallback
- Geometry hydration fault tolerance — each endpoint hydrates independently, single failure doesn't block all layers

## [0.0.1-alpha] — 2026-03-26

Initial proof of concept.

### Added

- Basic TCP relay → WebSocket bridge (`poc/server.js`)
- Protocol probing utility (`poc/probe.js`)
- Canvas-based tactical map client (`poc/public/index.html`)
- Live unit rendering on mobile-friendly canvas
