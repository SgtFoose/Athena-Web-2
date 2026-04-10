# Athena Web 2 v0.0.8

## Download (Easy Path)

1. Open this release and scroll to Assets.
2. Download AthenaWeb-0.0.8.exe.
3. Run AthenaWeb-0.0.8.exe.
4. Open http://localhost:3000 in your browser.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click More info.
2. Click Run anyway.

If Run anyway is not shown:

1. Right-click AthenaWeb-0.0.8.exe and choose Properties.
2. In General, check Unblock (if available).
3. Click Apply, then OK.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.0.8.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.0.8 Highlights

- Firewill live marker passthrough validated bridge relay frames now confirmed to carry Firewill I-TGT marker payloads end-to-end (text label and XY coordinates)
- Users can now see Arma map markers in Athena Web 2.
- I-TGT marker naming parity confirmed validated live Firewill marker payload shape using in-session marker TGT_0_NightHawk (name: MKR_0_NightHawk, type: mil_triangle, color: ColorBlue)
- I-TGT label readability polish Firewill and locally placed marker labels now use a larger Firewill-style blue text treatment for better in-map visibility
- v0.0.8 local test packaging rebuilt bridge/UI bundle and produced local AthenaWeb-0.0.8.exe test artifact

![Athena Web 2 v0.0.8 Arma in-game markers](../Images/Athena%20Web%202%20v0.0.8%20Arma%20in-game%20markers.png)
![Athena Web 2 v0.0.8 Arma in-game and I-TGT markers](../Images/Athena%20Web%202%20v0.0.8%20Arma%20in-game%20and%20I-TGT%20markers.png)

## Notes

- SmartScreen prompts can appear for unsigned/new binaries downloaded from the internet.
- Download only from this repository's official GitHub release page.
