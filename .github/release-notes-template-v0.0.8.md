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

- Firewill marker relay interoperability: bridge now consumes live relay marker payloads and renders Firewill I-TGT labels on-map
- Local + Firewill I-TGT label parity: both marker paths share the same tactical triangle and label rendering behavior
- Marker readability polish: larger label text with Firewill-style blue color treatment and improved contrast handling on bright map surfaces
- Session hygiene hardening: tactical marker state cleanup remains consistent across live disconnects and world/map context changes
- Packaged runtime sync: rebuilt EXE with latest embedded UI assets for v0.0.8 release parity

![Athena Web 2 v0.0.8 Arma in-game markers](../Images/Athena%20Web%202%20v0.0.8%20Arma%20in-game%20markers.png)
![Athena Web 2 v0.0.8 Arma in-game and I-TGT markers](../Images/Athena%20Web%202%20v0.0.8%20Arma%20in-game%20and%20I-TGT%20markers.png)

## Notes

- SmartScreen prompts can appear for unsigned/new binaries downloaded from the internet.
- Download only from this repository's official GitHub release page.
