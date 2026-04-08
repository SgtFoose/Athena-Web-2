# Athena Web 2 v0.0.6

## Download (Easy Path)

1. Open this release and scroll to Assets.
2. Download AthenaWeb-0.0.6.exe.
3. Run AthenaWeb-0.0.6.exe.
4. Open http://localhost:3000 in your browser.

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click More info.
2. Click Run anyway.

If Run anyway is not shown:

1. Right-click AthenaWeb-0.0.6.exe and choose Properties.
2. In General, check Unblock (if available).
3. Click Apply, then OK.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.0.6.exe
```

## What Is Included

- Built-in bridge server (HTTP + WebSocket)
- Built-in UI bundle
- Built-in map cache health checks

## v0.0.6 Highlights

- Added Firewill-style 8-digit I-TGT coordinate output (`XXXXYYYY`) in the map cursor overlay
- Added fast I-TGT capture actions (`T` hotkey, middle mouse click, and touch/tablet `SAVE TGT` flow)
- Added persistent right-panel target list with rename, copy, delete, and Delete All actions
- Updated I-TGT label behavior so labels start at `TGT_0` and reset to `TGT_0` after Delete All
- Added labeled blue map triangle markers for saved I-TGT targets
- Increased marker size slightly for improved in-map readability
- Removed bottom-right overlay copy icon button to simplify target workflow
- Fixed cross-map airport visual leak where yellow taxi overlays appeared on non-Malden cached maps
- Added iPad Safari landscape spacing mitigation so top UI is not covered by browser chrome/favorites bar

## Notes

- SmartScreen prompts can appear for unsigned/new binaries downloaded from the internet.
- Download only from this repository's official GitHub release page.
