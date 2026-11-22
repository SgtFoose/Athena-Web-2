# Athena Web 2 v0.1.1

## Download (Easy Path)

1. Open this release and scroll to **Assets**.
2. Download **AthenaWeb-0.1.1.exe**.
3. Run **AthenaWeb-0.1.1.exe**.
4. Open the Local URL shown in the bridge console (usually http://localhost:3000).

If port 3000 is in use, Athena Web 2 automatically tries the next free port (3001, 3002, ...) and prints the correct Local URL.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click **More info**.
2. Click **Run anyway**.

If Run anyway is not shown:

1. Right-click **AthenaWeb-0.1.1.exe** and choose **Properties**.
2. In General, check **Unblock** (if available).
3. Click **Apply**, then **OK**.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.1.1.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.1.1 Highlights

### Custom / Community / CDLC Map Fix

- Live sessions on maps outside the built-in vanilla list (for example **Kunduz**, **VR**, CDLC and community terrains) no longer squeeze all map content into the bottom-left corner
- The map now uses the real world size from the Athena Desktop cache (`Map.txt` → `WorldSize`)
- Relay and cache world names are matched case-insensitively (`kunduz` vs `Kunduz`)
- Verified live on VR (`WorldSize` 8192): map edge and player position match Athena Desktop

### UI

- Donate link repaired: now opens PayPal.Me (https://paypal.me/jeroenvdvelden)
- Sidebar header and welcome screen now read **ATHENA WEB 2** (previously showed leftover "ATHENA REMASTERED" branding); browser tab title is "Athena Web 2"

### Docs

- The bridge port variable is `WEB_PORT` (previously documented as `PORT`)

## Important: First-Time Map Export

Each new map still needs a one-time export with the original Athena Desktop (`Export` button) before Web2 can use its real size, roads, structures and other static layers. See the README section **First-Time Map Export**.

## Known Limitations

- Without an Athena Desktop cache for the current map, maps outside the built-in vanilla list fall back to a 10240 m extent
- Taxi/runway tile coverage approximately 95% on Tanoa (unchanged from v0.1.0)

## Notes
