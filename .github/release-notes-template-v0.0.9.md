# Athena Web 2 v0.0.9

## Download (Easy Path)

1. Open this release and scroll to Assets.
2. Download AthenaWeb-0.0.9.exe.
3. Run AthenaWeb-0.0.9.exe.
4. Open the Local URL shown in the bridge console (usually http://localhost:3000).

If port 3000 is in use, Athena Web 2 automatically tries the next free port (3001, 3002, ...) and prints the correct Local URL.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click More info.
2. Click Run anyway.

If Run anyway is not shown:

1. Right-click AthenaWeb-0.0.9.exe and choose Properties.
2. In General, check Unblock (if available).
3. Click Apply, then OK.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.0.9.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.0.9 Highlights

- Active AO visible in-map with AO-specific marker ownership/toggle behavior
- Marker routing controls added and validated:
  - Arma Markers
  - AO Markers
  - Spawn Markers (default OFF)
- Side-mission marker/icon visibility in AO flow
- AO completion/selection flag visibility when AO cycle changes
- Camp capture status reflected on-map (for example captured objective turning blue)
- Radiotower objective marker present and visible in AO flow
- AO icon sizing increased for readability
- Bottom X/Y + I-TGT panel restored and now collapsible

## Known Issue

- AO camp objective number labels (`1..5`) may still be inconsistent in some live sessions and remain tracked for follow-up.

![Athena Web 2 v0.0.9 Active AO visible](../Images/Athena%20Web%202%20v0.0.9%20Active%20AO%20visible.png)
![Athena Web 2 v0.0.9 Active AO](../Images/Athena%20Web%202%20v0.0.9%20Active%20AO.png)

## Notes

- SmartScreen prompts can appear for unsigned/new binaries downloaded from the internet.
- Download only from this repository's official GitHub release page.
