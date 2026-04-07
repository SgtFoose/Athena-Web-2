# Athena Web 2 v0.0.4

## Download (Easy Path)

1. Open this release and scroll to Assets.
2. Download AthenaWeb-0.0.4.exe.
3. Run AthenaWeb-0.0.4.exe.
4. Open http://localhost:3000 in your browser.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click More info.
2. Click Run anyway.

If Run anyway is not shown:

1. Right-click AthenaWeb-0.0.4.exe and choose Properties.
2. In General, check Unblock (if available).
3. Click Apply, then OK.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.0.4.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.0.4 Stability Highlights

- Fixed stale/cross-world overlay carry-over during map switching
- Fixed Arma Editor idle stale-map lock by expiring inactive live map state
- Restored vehicle and plane icon category resolution (question-mark fallback fix)
- Corrected dirt path vs runway hide-tile styling split
- Added disconnected/unknown-world fallback screen until map selection

## Notes

- SmartScreen prompts can appear for unsigned/new binaries downloaded from the internet.
- Download only from this repository's official GitHub release page.
