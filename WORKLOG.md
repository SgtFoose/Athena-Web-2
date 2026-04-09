# ClientOnly Worklog (Athena Web 2)

Purpose: track every step, fix, and next action while migrating from the legacy backend/DLL approach to a Bus-compatible client-only architecture.

## Scope Rules
- Use only Bus-approved original mod runtime (extension + relay behavior as shipped).
- Do not modify Bus DLLs/relay binaries.
- Build around local client-side bridge + browser UI.
- This folder is the new working surface for cleanup and future GitHub publication.

## Timeline

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
