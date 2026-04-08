# Athena Web 2 v0.0.5

## Download (Easy Path)

1. Open this release and scroll to Assets.
2. Download AthenaWeb-0.0.5.exe.
3. Run AthenaWeb-0.0.5.exe.
4. Open http://localhost:3000 in your browser.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click More info.
2. Click Run anyway.

If Run anyway is not shown:

1. Right-click AthenaWeb-0.0.5.exe and choose Properties.
2. In General, check Unblock (if available).
3. Click Apply, then OK.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.0.5.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.0.5 Highlights

- Fixed Stratis static-layer alignment drift after map swaps (shoreline, roads, structures, and labels)
- Fixed Stratis fallback world-size handling to avoid pre-hydration scaling issues
- Expanded vehicle classname fallback mapping so more placed vehicles resolve proper icons
- Unknown vehicle classes now fall back to a generic vehicle silhouette instead of question-mark icon
- Added Arma-style NATO marker parity for drones/turrets (including radar, artillery, and support turret cases)
- Fixed quadbike icon classification so quadbikes no longer render as motorcycle symbols
- Added AI fallback badge for generic AI-only vehicle classes when specific platform identity is unavailable
- Added collapsible left/right sidebars for iPad Mini and tablet map usability
- Fixed Malden airport taxi-lane rendering while preserving normal road styling around the airfield

## Notes

- SmartScreen prompts can appear for unsigned/new binaries downloaded from the internet.
- Download only from this repository's official GitHub release page.
