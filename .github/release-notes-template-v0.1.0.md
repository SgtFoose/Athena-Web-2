# Athena Web 2 v0.1.0

## Download (Easy Path)

1. Open this release and scroll to **Assets**.
2. Download **AthenaWeb-0.1.0.exe**.
3. Run **AthenaWeb-0.1.0.exe**.
4. Open the Local URL shown in the bridge console (usually http://localhost:3000).

If port 3000 is in use, Athena Web 2 automatically tries the next free port (3001, 3002, ...) and prints the correct Local URL.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click **More info**.
2. Click **Run anyway**.

If Run anyway is not shown:

1. Right-click **AthenaWeb-0.1.0.exe** and choose **Properties**.
2. In General, check **Unblock** (if available).
3. Click **Apply**, then **OK**.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.1.0.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.1.0 Highlights

### Airport / Runway / Taxiway Parity

- Runway and taxiway surfaces now render as concrete-gray across Altis, Stratis, Malden, and Tanoa
- `RoadType=3` (concrete) segments from cached Bus map exports are correctly preserved through the bridge import pipeline
- Airport expansion logic recovers sparse taxi connector strips that previously missed classification
- Taxi/runway coverage is approximately 95% on Tanoa; residual isolated connectors noted as known limitation and tracked for a future pass

### Map Rendering — Contour Lines and Coastlines

- Elevation contour lines: single Chaikin smoothing pass removes blocky 90° stairstepping while keeping lines tight
- Coastline (Z=0): two Chaikin passes produce natural shoreline curves
- Land fill / ocean fill edge bleed fixed across all maps: land polygon and coast polyline now share identical point sets with Leaflet zoom-simplification disabled (`smoothFactor: 0`)
- Malden-specific edge bleed fully resolved

### UI

- Trees row in MAP DATA panel is hidden when no tree data is available (removes misleading "0 points" display)

## Known Limitations

- Taxi/runway tile coverage approximately 95% on Tanoa; a small number of isolated low-density connector tiles may render as dirt paths; tracked for improvement in a future release
- Contour line and coastline quality are dependent on the static Athena Desktop cache being present in the bridge data path

## Notes
