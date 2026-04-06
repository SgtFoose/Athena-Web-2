# ATHENA WEB 2

> Tactical second-screen web companion for Arma 3 — powered by the original Athena relay

**Version: v0.0.3**

![Athena Web 2 v0.0.2 — Tanoa](Images/Athena%20Web%202%20v0.0.1.png)

## Latest Video

[![Athena Web 2 v0.0.1](https://img.youtube.com/vi/4-AioVt9iUQ/maxresdefault.jpg)](https://youtu.be/4-AioVt9iUQ)

## SITREP

Athena Web 2 is a browser-based second-screen tactical map for Arma 3. It connects to the original **Athena** mod relay (by Bus) and renders a full military cartography map with live unit tracking — straight from your browser, on any device on your local network.

This is v0.0.3 — the current active Web2 release line. It sharpens map rendering parity with Athena Desktop by smoothing shoreline geometry and improving individual tree symbol styling.

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

## QUICK START

### Option A: Standalone EXE (recommended)

Use this when you want a one-file launcher with no dev setup.

1. Download `AthenaWeb-0.0.3.exe` from this repository's release artifacts
2. Run `AthenaWeb-0.0.3.exe`
3. Open `http://localhost:3000`
4. Start Arma 3 with the original Athena mod running (relay path)

What the EXE includes:

- Built-in bridge server (HTTP + WebSocket)
- Built-in precompiled web UI (`wwwroot`)
- Built-in map cache health checks (`/api/health`) used by the in-app cache warning banner

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
- Workshop: https://steamcommunity.com/sharedfiles/filedetails/?id=1181881736

### Athena Remastered (related work)

- Workshop: https://steamcommunity.com/sharedfiles/filedetails/?id=3687225607

## LICENSE

This repository is an independent web client built around the Athena relay protocol. It does not modify or redistribute any original Athena mod binaries.

Respect all rights and credits of original authors.
