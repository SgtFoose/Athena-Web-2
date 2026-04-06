# Athena Web 2 — Copilot Context

## Project Overview
Athena Web 2 is the client-only web companion for Arma 3 Athena relay data.
Architecture: **Bridge** (Node.js relay protocol adapter) -> **UI** (React + Vite + Leaflet).

## Product Identity (Must Keep Clear)
- `Athena Remastered` and `Athena Web 2` are separate products, separate repositories, and separate release lines.
- They may share similar UI patterns, but they are not interchangeable implementations.
- Web2 must remain compatible with Bus's original Athena mod + relay behavior.

## Repository Boundaries (Important)
- This repository is for Web2 work only (`v0.x`).
- Athena Remastered (`v1.x`, DLL + ASP.NET backend) belongs in:
  - `C:\Users\jvdv2\Documents\Athena Remastered`
- Do not commit Remastered-only artifacts here:
  - `Backend/`, `Extension/`, `@AthenaRemastered/`
  - DLL/BattlEye release notes
  - version entries like `1.x`

## Capability Boundaries (Critical)
- Web2 is **client-only** and must stay within original Athena limitations:
  - Data source is Bus's relay protocol (`Relay.exe` path), not custom DLL extraction.
  - Runtime stack is Node.js bridge + React UI.
  - No custom Arma extension DLL features.
- Remastered may implement extra functionality via custom DLL/backend; Web2 must not assume those capabilities exist.
- If a requested feature depends on Remastered-only telemetry or DLL hooks, either:
  - implement a Web2-compatible fallback using relay/cache data, or
  - clearly mark it as out-of-scope for Web2.

## Copilot Behavior Rules
- Before adding features, classify request as `Web2-only` or `Remastered-only`.
- Reject cross-repo edits by default.
- Never add `v1.x` release notes, BattlEye steps, or DLL guidance to this repo.
- Keep docs in this repo scoped to `v0.x` language only.
- Preserve existing Web2 UI look-and-feel, but do not import Remastered-specific backend assumptions.
- Keep unresolved release blockers in `WORKLOG.md` with exact repro coordinates when available.
- Current unresolved map-render issue to track: vegetation/hedge orientation mismatch at `X:6874.88 Y:7381.98` (Tanoa cache).

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
- `ui/src/version.ts` (`APP_VERSION`)

## Packaged UI Sync Rule
- When shipping or validating Web2 from bridge port `3000`, ensure packaged UI is synced:
  - build UI: `npm --prefix ui run build`
  - mirror output: `robocopy ui\dist bridge\wwwroot /MIR`
- Confirm `bridge/wwwroot/index.html` points to current asset hashes before release/push.

## Safety Check Before Commit
- Confirm changed files are only Web2 paths (`bridge/`, `ui/`, Web2 docs/config).
- Confirm no Remastered-specific folders or `1.x` version text were introduced.
- Confirm any new feature is achievable using relay/cache data without custom DLL dependencies.
