# Changelog

All notable changes to Athena Web 2 will be documented in this file.

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
