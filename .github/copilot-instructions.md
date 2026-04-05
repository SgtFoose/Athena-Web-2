# Athena Web 2 — Copilot Context

## Project Overview
Athena Web 2 is the client-only web companion for Arma 3 Athena relay data.
Architecture: **Bridge** (Node.js relay protocol adapter) -> **UI** (React + Vite + Leaflet).

## Repository Boundaries (Important)
- This repository is for Web2 work only (`v0.x`).
- Athena Remastered (`v1.x`, DLL + ASP.NET backend) belongs in:
  - `C:\Users\jvdv2\Documents\Athena Remastered`
- Do not commit Remastered-only artifacts here:
  - `Backend/`, `Extension/`, `@AthenaRemastered/`
  - DLL/BattlEye release notes
  - version entries like `1.x`

## Structure
- `bridge/`: relay TCP client, REST API, WebSocket broadcast
- `ui/`: frontend map application
- `CHANGELOG.md`: Web2 release history (`0.0.x`)
- `WORKLOG.md`: chronological engineering log for Web2

## Release Rule
When shipping Web2 updates, update at minimum:
- `README.md` version
- `CHANGELOG.md` top entry
- relevant `WORKLOG.md` timeline entry
