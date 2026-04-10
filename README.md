# ATHENA WEB 2

> Tactical second-screen web companion for Arma 3 — powered by the original Athena relay

**Version: v0.0.8**

**Tracked bridge build in repo:** `bridge/dist/AthenaWeb-0.0.8.exe`

**Latest release download:** https://github.com/SgtFoose/Athena-Web-2/releases/latest

![Athena Web 2 v0.0.6 — I-TGT Workflow](Images/Athena%20Web%202%20v0.0.6.png)

## Latest Video

[![Athena Web 2 v0.0.8](Images/Athena%20Web%202%20v0.0.8%20Firewill%20I-TGT%20Interaction.png)](https://youtu.be/9CKiBNtlWgQ)

### Older Videos

- [Athena Web 2 v0.0.6 I-TGT Tablet Demo](https://youtu.be/7SljQN0B0Qk)
- [Athena Web 2 Early Demo](https://youtu.be/4-AioVt9iUQ)

## SITREP

Athena Web 2 is a browser-based second-screen tactical map for Arma 3. It connects to the original **Athena** mod relay (by Bus) and renders a full military cartography map with live unit tracking — straight from your browser, on any device on your local network.

This is v0.0.8 — the current active Web2 release line. It focuses on Firewill I-TGT interoperability validation (relay marker passthrough) while preserving Web2 relay compatibility.

### v0.0.8 highlights

- **Firewill live marker passthrough validated** bridge relay frames now confirmed to carry Firewill I-TGT marker payloads end-to-end (`text` label and XY coordinates)
- **Arma map marker visibility** users can now see Arma in-game map markers in Athena Web 2
- **I-TGT marker naming parity confirmed** validated live Firewill marker payload shape using in-session marker `TGT_0_NightHawk` (`name: MKR_0_NightHawk`, `type: mil_triangle`, `color: ColorBlue`)
- **I-TGT label readability polish** Firewill and locally placed marker labels now use a larger Firewill-style blue text treatment for better in-map visibility
- **v0.0.8 local test packaging** rebuilt bridge/UI bundle and produced local `AthenaWeb-0.0.8.exe` test artifact

![Athena Web 2 v0.0.8 Arma in-game markers](Images/Athena%20Web%202%20v0.0.8%20Arma%20in-game%20markers.png)

### v0.0.7 highlights

- **Offline shoreline/contour parity pass** reduced over-simplification on smaller worlds (for example Stratis) so coastline and contour geometry stays smoother in offline cache mode
- **Online -> offline cleanup hardening** when live telemetry drops (Athena splash/offline state), stored I-TGT targets are cleared and stale live entity markers no longer persist into offline map switching

- **Firewill I-TGT code output** map cursor now shows 8-digit I-TGT coordinates (`XXXXYYYY`) while keeping legacy `X/Y` readout visible
- **Fast target capture flow** store current map cursor as a target using `T` or middle mouse click
- **Touch/tablet I-TGT capture flow** tap map to set cursor then use in-map `SAVE TGT` action (no keyboard/middle mouse required)
- **Right-panel target management** saved targets can be renamed, copied, deleted individually, or cleared with Delete All
- **Predictable target labels** stored labels start at `TGT_0`, and `Delete All` resets the next saved label back to `TGT_0`
- **Persistent map target markers** saved I-TGT entries render as labeled blue tactical triangles on the map
- **Readability polish** stored target triangles are slightly larger for faster in-map scanning
- **Bottom-right overlay simplification** removed overlay copy icon button; copy action remains available per target in the right sidebar
- **Cross-map airport overlay fix** removed unintended yellow taxi-strip overlays leaking to non-Malden cached maps while preserving gray runway/taxi surfaces
- **iPad Safari landscape spacing fix** added top offset and dynamic viewport handling so browser chrome/favorites no longer overlap top UI controls

### v0.0.5 highlights

- **Stratis alignment fix** shoreline, roads, structures, and location labels now keep correct overlay alignment after world swaps by using hydrated world-size metadata before relay fallbacks
- **Stratis fallback size fix** relay fallback now uses the correct Stratis world size (`8192`) to avoid pre-hydration scale drift
- **Vehicle icon coverage pass** expanded classname heuristics for a wider set of vehicles (including modded/common families) to reduce unknown icon fallback cases
- **No more question-mark fallback default** unknown vehicle classes now render a generic vehicle silhouette instead of the question-mark icon
- **Arma-style drone/turret markers** all drone and turret categories now use Athena Desktop NATO marker textures (with subtype-specific radar/mortar/art/support mapping)
- **AI vehicle fallback badge** generic AI-only relay classes (for example `*_UAV_AI`) now display an `AI` badge when specific platform identity is unavailable
- **Quadbike marker correction** quad bikes now render as 4-wheel vehicle icons instead of 2-wheel motorcycle symbols
- **Malden airport surface parity fix** taxi-lane paths now render without recoloring nearby normal roads around the airfield
- **iPad-friendly fullscreen workflow** both sidebars now have collapse/expand edge buttons so map view can be maximized on tablet resolutions

### v0.0.4 highlights

- **World swap cache safety** clears stale geometry on world changes and prevents old async responses from repainting the wrong map overlays
- **Strict world/geometry matching** only renders static layers when hydrated geometry world equals active world, preventing Tanoa-on-Malden style cross-overlay contamination
- **Stale live-world timeout** automatically expires bridge live-world map state after inactivity so Arma Editor idle no longer locks map source selection
- **No-world fallback screen** shows Athena static welcome image while disconnected/unknown until a live or offline map is selected
- **Offline cached map selection** works while disconnected so pre-mission planning can switch between any cached worlds
- **Vehicle toggle reliability** no longer gets overridden by zoom-threshold auto-switch behavior
- **Vehicle icon mapping restored** relay class/category resolution now maps to Bus icon assets so vehicles no longer render as fallback question-mark symbols, including payloads where numeric `class` fields previously masked real classname values
- **Dirt track parity** dirt/gravel path tiles now render in brown gravel tones instead of runway/concrete grey
- **Dirt path continuity pass** path-like hide tiles are now stitched into connected brown polylines so paths read as continuous lines instead of detached north-facing stubs
- **Runway/path hide split** airport/apron hide tiles remain concrete-gray while long/thin hide strips render as dirt paths
- **Hedge orientation correction** hedge strips now use cache heading directly (no forced +90 rotation)
- **Groups default OFF** to reduce initial map clutter and match requested startup behavior
- **Cache warning accuracy** avoids false "No cached data" banners when the active world is already present in local cache
- **Contour parity tuning** now follows Athena Desktop-style contour point trimming (world-scaled tolerance) instead of aggressive angle straightening, producing cleaner natural contour lines

### v0.0.3 highlights

- **Smoother shoreline geometry** reduces blocky horizontal/vertical coastline stair-steps and restores cleaner angled shorelines
- **Ringed tree symbols** render with a dark outer ring and green center for closer desktop visual parity
- **Map cache health banner** warns when Athena Desktop map cache is missing, empty, or doesn't include the active world
- **World picker** lets you browse cached worlds offline for mission planning
- **Live-world auto override** clears manual world selection when live Arma data arrives
- **Follow Active Player auto-disable** turns off follow mode when you manually pan or zoom

### Known issue (v0.0.3)

- **Vegetation orientation mismatch remains under investigation** for a hedge/bush object around `X:6874.88 Y:7381.98` on Tanoa cache data.

### v0.0.2 highlights

- **Map cache health banner** warns when Athena Desktop map cache is missing, empty, or doesn't include the active world
- **World picker** lets you browse cached worlds offline for mission planning
- **Live-world auto override** clears manual world selection when live Arma data arrives
- **Follow Active Player auto-disable** turns off follow mode when you manually pan or zoom
- **Shoreline fill improvements** reduce land/ocean edge block artifacts and seal coastline gaps
- **Post-export shoreline refresh** automatically rebuilds land fill after world export completion
- **Packaged UI deployment sync** ensures bridge `wwwroot` serves the latest built UI bundle

## FEATURES

- **Live unit tracking** — groups, vehicles, units with real-time position and heading updates
- **Full map rendering** — coastlines, roads, forests, structures, contour lines, locations, elevation data
- **Bus-exact visual fidelity** — colors, weights, and styles faithfully match Bus's original Athena Desktop renderer
- **Layer controls** — toggle contours, forests, trees, roads, structures, locations, groups, vehicles, units, waypoints, projectile tracking
- **Map style modes** — 2D, Ground, Pilot views
- **ORBAT panel** — unit/vehicle listing with side filtering
- **Events feed** — kills and shots with timestamps
- **World auto-detection** — automatically loads the correct map from Athena Desktop cache
- **Multi-map support** — works with any cached Arma 3 map (Altis, Stratis, Malden, Tanoa, Enoch, etc.)
- **Export World Data** — trigger map data export from the browser
- **No custom DLL required** — uses Bus's original unmodified Athena mod and relay
- **Phone/tablet support** — responsive layout for mobile viewing on local network

## I-TGT TARGET WORKFLOW (v0.0.8)

Use this flow to capture and manage Firewill-compatible I-TGT target codes:

- Firewill mod Workshop: [Firewill I-TGT Mod](https://steamcommunity.com/sharedfiles/filedetails/?id=366425329)

![Athena Web 2 v0.0.8 Firewill I-TGT workflow](Images/Athena%20Web%202%20v0.0.8%20Firewill%20I-TGT%20Interaction.png)

![Athena Web 2 v0.0.6 I-TGT usage](Images/Athena%20Web%202%20v0.0.6%20I-TGT.png)

1. Move the cursor over the map and read the bottom-right `I-TGT: XXXXXXXX` value.
2. Press `T` or click middle mouse button to store that target.
3. On touch/tablet devices, tap map to set cursor and press `SAVE TGT` in the bottom-right overlay.

![Athena Web 2 I-TGT Tablet](Images/Athena%20Web%202%20I-TGT%20Tablet.PNG)

4. Open the right `I-TGT TARGETS` panel entry to rename/copy/delete as needed.
5. Use `Delete All` to clear the target list for a new run (next new label restarts at `TGT_0`).
6. Use the saved code in Firewill I-TGT strike workflow.

## QUICK START

### Option A: Standalone EXE (recommended)

Use this when you want a one-file launcher with no dev setup.

1. Open the latest release page: https://github.com/SgtFoose/Athena-Web-2/releases/latest
2. Under **Assets**, download `AthenaWeb-0.0.8.exe`
3. Run `AthenaWeb-0.0.8.exe`
4. Open `http://localhost:3000`
5. Start Arma 3 with the original Athena mod running (relay path)

If the browser page does not load, allow a few seconds for the EXE to start the bridge, then refresh once.

What the EXE includes:

- Built-in bridge server (HTTP + WebSocket)
- Built-in precompiled web UI (`wwwroot`)
- Built-in map cache health checks (`/api/health`) used by the in-app cache warning banner

#### Windows 11 SmartScreen workaround ("Windows protected your PC")

If Windows blocks the EXE on first launch:

1. On the SmartScreen dialog, click **More info**
2. Click **Run anyway**

If **Run anyway** is missing or still blocked:

1. Right-click `AthenaWeb-0.0.8.exe` -> **Properties**
2. In the **General** tab, check **Unblock** (if shown)
3. Click **Apply** then **OK**
4. Launch the EXE again

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.0.8.exe
```

Notes:

- SmartScreen prompts are expected for unsigned/new binaries downloaded from the internet.
- Always download the EXE from this repository's official GitHub Release page.

#### Release maintainer checklist (GitHub)

When publishing a new Web2 release, attach the EXE in the GitHub release so users can download it directly:

1. Build the executable in `bridge/dist`
2. Create a GitHub release tag (example: `v0.0.8`)
3. In the release editor, upload the EXE under **Assets** (example: `AthenaWeb-0.0.8.exe`)
4. In release notes, include the Quick Start URL from this README and the SmartScreen workaround above
5. Copy and adapt `.github/release-notes-template-v0.0.8.md` into the GitHub release description

This keeps non-technical users on a one-download install path.

### First-Time Map Export (required)

When opening a map for the first time in Athena Web 2, you must use the original Athena Desktop export flow once for that map so static geometry is cached.

![Athena Desktop first map usage export flow](Images/Athena%20Web%202%20First%20Map%20Usage.png)

Per new map, do this once:

1. Start Arma 3 with the original `@Athena - An Arma 2nd Screen Application` mod.
2. Join a server running the target map.
3. Open Athena Desktop and connect.
4. Click `Export` in Athena Desktop.
5. Restart `AthenaWeb-0.0.8.exe` and reopen `http://localhost:3000`.

After export, Athena Web 2 can render roads, structures, trees, and other static layers for that map from local cache.

### Option B: Development mode (Node.js)

Use this if you are developing or debugging the source code.

### Prerequisites

- Arma 3 with **[Athena - An Arma 2nd Screen Application](https://steamcommunity.com/sharedfiles/filedetails/?id=1181881736)** mod installed and running
- **Athena Desktop** must have exported the map at least once (creates cache in `Documents/Athena/Maps/`)
- Node.js 18+ installed

### 1. Configure Athena extension

Open:

```
C:\Program Files (x86)\Steam\steamapps\common\Arma 3\!Workshop\@Athena - An Arma 2nd Screen Application\AthenaExtensionSettings.txt
```

Recommended settings:

- `ath_launch_relay=true`
- `ath_launch_app=false` (optional — Desktop app can run alongside)

### 2. Start the bridge

```powershell
cd bridge
npm install
node server.js
```

### 3. Start the UI

```powershell
cd ui
npm install
npm run dev
```

### 4. Open in browser

- **Local**: http://localhost:5173
- **Network** (phone/tablet): Use the network URL printed by Vite, e.g. `http://192.168.x.x:5173`

The bridge runs on port `3000` (REST API + WebSocket). The UI dev server runs on port `5173`.

## EXE DEPENDENCY VERIFICATION

`AthenaWeb.exe` is built via:

```json
"build:exe": "npx @yao-pkg/pkg . --targets node22-win-x64 --output dist/AthenaWeb.exe --compress GZip"
```

Verification summary:

- `AthenaWeb.exe` bundles the Node.js runtime (target `node22-win-x64`) into the executable.
- No separate Node.js installation is required to run the EXE.
- No .NET runtime is required for `AthenaWeb.exe`.
- External requirements still apply: Arma 3 + original Athena mod/relay and Athena Desktop map cache data.

## PROJECT STRUCTURE

```
bridge/
  server.js          # TCP relay → REST API + WebSocket bridge
  probe.js           # Protocol probing utility
  package.json

ui/
  src/
    components/
      AthenaMap.tsx   # Leaflet map with all rendering layers
      Sidebar.tsx     # MAP/ORBAT tabs, layer toggles, location list
      EventFeed.tsx   # Kill/shot event feed panel
    hooks/
      useAthenaHub.ts # Bridge WebSocket + REST data hydration
      useStaticMap.ts # Static map contour/landmask fetching
    types/
      game.ts         # TypeScript interfaces for all game data
    App.tsx            # Root component
  index.html
  package.json
  vite.config.ts

poc/                   # Original proof-of-concept (preserved for reference)

Images/                # Screenshots
WORKLOG.md             # Detailed development log
CHANGELOG.md           # Release history
```

## HOW IT WORKS

```
Arma 3 → Athena Extension → Relay (TCP 28800) → Bridge (Node.js) → Browser (React + Leaflet)
```

1. **Athena mod** (SQF scripts + DLL) collects game state from Arma 3
2. **Relay** (`Relay.exe`) receives data over named pipe, serves it on TCP port 28800
3. **Bridge** (`bridge/server.js`) connects to relay, parses the `<ath_sep>` protocol, and serves REST endpoints + WebSocket events
4. **UI** (`ui/`) connects to bridge, fetches map geometry from Athena Desktop cache (via bridge), and renders everything with Leaflet

### Map data sources

- **Live data**: Units, vehicles, groups, kills, shots — streamed from relay in real time
- **Static geometry**: Roads, structures, forests, trees, locations, elevation, contours — read from Athena Desktop cache on disk (`Documents/Athena/Maps/{world}/`)

## ENVIRONMENT VARIABLES (bridge)

| Variable | Default | Description |
|----------|---------|-------------|
| `PORT` | `3000` | Bridge HTTP/WS port |
| `RELAY_HOST` | `127.0.0.1` | Athena relay host |
| `RELAY_PORT` | `28800` | Athena relay TCP port |
| `ATHENA_MAPS_DIR` | `~/Documents/Athena/Maps` | Path to Athena Desktop map cache |
| `WORLD_SELECTION_MODE` | `fresh` | `fresh` (latest updated) or `stable` (core files only) |
| `WORLD_CACHE_OVERRIDE` | _(empty)_ | Force a specific world name |

## CONTRIBUTING

Contributions are welcome. This project is actively developed.

### Priority areas

- Mobile UI improvements
- Additional map layer types
- Performance optimization for large maps
- Packaging for non-technical users
- Error handling and reconnect resilience

### Contribution workflow

1. Fork this repository
2. Create a feature branch
3. Keep changes scoped and documented
4. Test with a live Athena relay session
5. Open a PR with what changed, how to test, and screenshots when UI changed

## CREDITS

### Original Athena mod

- **Athena - An Arma 2nd Screen Application**
- Creator: **Bus**
- Workshop: [Athena - An Arma 2nd Screen Application](https://steamcommunity.com/sharedfiles/filedetails/?id=1181881736)

### Athena Remastered (related work)

- Workshop: [Athena Remastered](https://steamcommunity.com/sharedfiles/filedetails/?id=3687225607)

## LICENSE

This repository is an independent web client built around the Athena relay protocol. It does not modify or redistribute any original Athena mod binaries.

Respect all rights and credits of original authors.
