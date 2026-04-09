# Athena Web 2 v0.0.7

## Download (Easy Path)

1. Open this release and scroll to Assets.
2. Download AthenaWeb-0.0.7.exe.
3. Run AthenaWeb-0.0.7.exe.
4. Open http://localhost:3000 in your browser.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click More info.
2. Click Run anyway.

If Run anyway is not shown:

1. Right-click AthenaWeb-0.0.7.exe and choose Properties.
2. In General, check Unblock (if available).
3. Click Apply, then OK.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.0.7.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.0.7 Highlights

- Improved offline contour/coastline rendering smoothness for smaller worlds (for example Stratis) by reducing over-aggressive simplification
- Cleared stale online entities when switching to offline mode so vehicles, units, and groups no longer persist across offline world changes
- Cleared stored I-TGT targets on online -> offline transition to prevent stale tactical markers after Athena splash/disconnect
- Fixed offline Tanoa dirt-walk path rendering so sparse off-airfield `hide` tiles no longer appear as gray runway blocks
- Updated README I-TGT section with Firewill mod Workshop reference
- Updated README latest video block to the v0.0.6 video with local tablet thumbnail

## Notes

- SmartScreen prompts can appear for unsigned/new binaries downloaded from the internet.
- Download only from this repository's official GitHub release page.
