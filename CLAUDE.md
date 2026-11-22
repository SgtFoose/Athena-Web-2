# CLAUDE.md — Athena Web 2

Canonical project context for Claude Code. Consolidated on 2026-11-22 from `.github/copilot-instructions.md`, `README.md`, `WORKLOG.md`, `CHANGELOG.md`, `.github/release-notes-template-v*.md` and `ui/icon-mapping.md`. If those files disagree with this one, this file wins for *process rules*; `WORKLOG.md`/`CHANGELOG.md` remain the source of truth for *history*.

---

## RULE #1 — Log every step in WORKLOG.md (non-negotiable)

Every step we take — code change, investigation, build, release, doc edit, config change, failed attempt, decision — gets logged in `WORKLOG.md` **in the same working batch**, before moving on or ending the turn.

- Add entries under `## Timeline`, **newest first** (top of the timeline).
- Heading format: `### YYYY-MM-DD — <version or area>: <short title>` (em dash, matching existing entries).
- Body: bullets covering **what** changed, **why**, **which files** (backtick paths), and **validation** (commands run + results, including failures).
- Multiple steps on the same day/topic: append bullets to that day's entry, or add a `#### YYYY-MM-DD follow-up — <title>` sub-entry.
- Investigations that didn't pan out are logged too (what was tried, what was learned, rolled back or not).
- Open problems / release blockers: record with **exact repro coordinates + world** when available, plus a `#### TODO (carry into next patch)` list if work remains.
- Keep the backlog sections at the bottom of `WORKLOG.md` (Known Limitations, Parity Backlog, Next Tasks, Parity Matrix) current when status changes.

---

## RULE #2 — Everything in the repo is in English (non-negotiable)

The project owner usually types in **Dutch**. Conversation may be in Dutch, but **everything written into the repository is English**: code, identifiers, comments, log/console messages, UI text, commit messages, PR descriptions, release notes, `WORKLOG.md`/`CHANGELOG.md`/`README.md` entries, and file names. Translate Dutch instructions into English before they land in any file.

## RULE #3 — Write for collaborators: comment the code well (non-negotiable)

The goal is to give outside collaborators the best possible chance to understand and work on this code. Every piece of code we write or touch must be properly commented:

- **File/module header:** short description of what the file is responsible for and how it fits the bridge → UI pipeline.
- **Functions/components/hooks:** a doc comment (JSDoc/TSDoc) with purpose, parameters, return value, and side effects (network calls, WebSocket sends, cache/memo mutation, Leaflet layer changes).
- **Non-obvious logic:** explain the *why*, not just the *what*: protocol quirks (e.g. `next` polling), Bus-exact constants (cite the Athena Desktop source, e.g. "Bus `GenerateForestGeometry`: fixed 16×16 cells"), coordinate transforms / Y-flips, race-condition guards, and workarounds for specific maps (name the world + coordinates).
- **Magic numbers:** named constants with a comment on origin and units (metres, px, ms).
- **Types/interfaces:** comment fields whose meaning, units or source (relay vs. disk cache) isn't obvious.
- When editing existing uncommented code, add comments to the parts we touched. Don't mass-rewrite unrelated files in the same change.
- Keep comments accurate: update or remove them when the code changes.

## RULE #4 — GitHub account separation (critical)

This repo belongs to the owner's **personal** GitHub account (**`SgtFoose`**), which is **not** the work GitHub account used for other projects on this machine. Never let work credentials or the work email end up in this repo.

- Commit identity is pinned in the **repo-local** git config (set 2026-11-22):
  `user.name = Sgt.Foose`, `user.email = 17387175+SgtFoose@users.noreply.github.com` (matches all historical commits).
- Push credentials are pinned with repo-local `credential.username = SgtFoose`. Git Credential Manager on this machine also stores the owner's work account; never use it for this repo.
- Before every commit: run `git config user.email` and confirm it is the SgtFoose noreply address. Never use `--author` or `-c user.email=` overrides with another address.
- Before every push / release / GitHub API call: confirm with the user and make sure the authenticated account is `SgtFoose`. If a push fails with a permission error or a different account shows up, stop and ask; don't fix it by switching accounts on your own.
- Never write the work email, work account name, or other work details into any repo file, commit, or release note.

---

## Project overview

Athena Web 2 (Web2) is a **client-only**, browser-based second-screen tactical map for Arma 3. It connects to the original **Athena** mod relay by **Bus** and renders a military-cartography map with live unit tracking, viewable from any device on the LAN.

```
Arma 3 → Athena extension (SQF + Bus DLL) → Relay.exe (named pipe → TCP 28800) → Bridge (Node.js) → Browser (React + Vite + Leaflet)
```

- **Live data** (units, vehicles, groups, markers, kills, shots) — streamed from the relay.
- **Static geometry** (roads, structures, forests, trees, locations, elevation, contours) — read from the Athena Desktop cache on disk: `%USERPROFILE%\Documents\Athena\Maps\{world}\`. A map must be exported once via Athena Desktop (`Export` button) before Web2 can render its static layers.

Current version: **v0.1.1** (2026-11-22). Release line is `v0.x`.

Links: repo https://github.com/SgtFoose/Athena-Web-2 · Workshop (Web2) id `3707444008` · Bus's Athena mod Workshop id `1181881736` · Firewill I-TGT mod id `366425329` · Athena Remastered Workshop id `3687225607`.

---

## Product identity & repository boundaries (critical)

- **Athena Remastered** and **Athena Web 2** are separate products, separate repositories, separate release lines. They may share UI patterns but are not interchangeable.
- This repo is **Web2 only (`v0.x`)**. Athena Remastered (**AR**) is a separate product with its own repo, https://github.com/SgtFoose/AthenaRemastered, cloned locally at `C:\AthenaRemastered`. AR isn't relay-only, so it has capabilities Web2 must not assume.
- **One-way feature flow:** Web2 → AR is allowed (bug fixes, rendering parity). AR → Web2 is **not** (AR features usually rely on the DLL). Web2's frontend started as a copy of AR's `Frontend/` (2026-04-01) and has since diverged heavily, especially `AthenaMap.tsx`, `useAthenaHub.ts`, and `App.tsx`.
- Never commit Remastered-only artifacts here: `Backend/`, `Extension/`, `@AthenaRemastered/`, DLL/BattlEye release notes, `1.x` version entries.
- Web2 must remain compatible with Bus's original Athena mod + relay behavior. Do not modify Bus DLLs or relay binaries.

## Capability boundaries (critical)

- Data source is Bus's relay protocol (`Relay.exe`), **not** custom DLL extraction. Runtime stack is Node.js bridge + React UI. No custom Arma extension DLL features.
- If a feature depends on Remastered-only telemetry or DLL hooks: implement a Web2-compatible fallback from relay/cache data, **or** clearly mark it out-of-scope.
- Deliberately deferred (BattlEye risk / extra telemetry): shot-origin tracking, ballistic reconstruction, active laze telemetry.

## Working rules

- Before adding a feature, classify it as `Web2-only` or `Remastered-only`. Reject cross-repo edits by default.
- Keep docs scoped to `v0.x` language. No `v1.x` release notes, BattlEye steps, or DLL guidance. Short factual references to AR are allowed only where they explain the repo boundary (as in the section above).
- Preserve existing Web2 look-and-feel (see "Bus-exact rendering" below); don't import Remastered backend assumptions.
- **Safety check before commit:** changed files are only Web2 paths (`bridge/`, `ui/`, `@AthenaWeb2/`, Web2 docs/config); no Remastered folders, and no `1.x` release/version entries; new features achievable from relay/cache data.
- **Branch + PR workflow (agreed 2026-11-22):** `main` has a GitHub ruleset (`Protect Master`) that requires changes to go through a pull request (0 required approvals, since authors can't approve their own PRs) and blocks deletion and force-pushes. Never commit straight to `main`. Each task gets its own branch off up-to-date `main` with a prefix: `feat/`, `fix/`, `docs/`, `chore/`, `release/` (e.g. `fix/ao-camp-labels`). Open a PR to `main` with an English description: what changed, why, how to test, and screenshots for UI changes. The owner merges. Use Conventional Commit style messages (`feat:`, `fix:`, `docs:`, `chore:`, `build:`, `release:`), matching existing history.
- **Git/GitHub operations:** only commit, push, tag, or touch GitHub Releases/API when the user asks; confirm before each such remote operation. Auth comes from the user's session (`gh` CLI or `GITHUB_TOKEN` env var). Never hardcode, print, commit, or store tokens anywhere in the repo, prompts, release notes, or source.

---

## Repository layout

```
bridge/                  Node.js bridge (CommonJS)
  server.js              relay TCP client + REST API + WebSocket broadcast (~1900 lines)
  probe.js               protocol probing utility
  wwwroot/               packaged UI (mirror of ui/dist) served on port 3000
  dist/                  AthenaWeb.exe + AthenaWeb-<version>.exe (tracked, must match release)
  public/
ui/                      React + Vite + TypeScript + Leaflet
  src/App.tsx            root; active-world logic, layer defaults
  src/version.ts         APP_VERSION / APP_NAME
  src/apiBase.ts
  src/components/AthenaMap.tsx   all map rendering layers (~4900 lines)
  src/components/Sidebar.tsx     MAP/ORBAT tabs, layer toggles, locations, world picker, I-TGT targets
  src/components/EventFeed.tsx   kills/shots feed
  src/components/MapCacheBanner.tsx  cache health warning
  src/hooks/useAthenaHub.ts      bridge WebSocket + REST geometry hydration
  src/hooks/useStaticMap.ts      contours/landmask fetch
  src/hooks/useHealthCheck.ts, useAthenaLibrary.ts
  src/types/game.ts              TS interfaces for game data
  public/icons/vehicles/         Bus original vehicle icon PNGs
  icon-mapping.md                vehicle class → category → icon table (see below)
@AthenaWeb2/             Steam Workshop distribution stub (EXE + no-op stub PBO, publish scripts)
Athena Desktop dnSpy/    decompiled Athena Desktop reference (source of Bus-exact values and icon assets)
poc/                     original proof-of-concept bridge (reference only)
Images/                  README screenshots
.github/                 copilot-instructions.md (legacy), release-notes-template-vX.Y.Z.md
repo_dist_contents.json  tracked versioned EXE path/size/blob SHA
CHANGELOG.md / WORKLOG.md / README.md
```

## Commands

```powershell
# Dev: bridge (port 3000) + UI dev server (port 5173)
cd bridge; npm ci; node server.js
cd ui; npm ci; npm run dev               # http://localhost:5173 (network URL printed by Vite)

# Checks
node --check bridge/server.js
npm --prefix ui run build                 # tsc -b && vite build
npm --prefix ui run lint

# Packaged UI sync (required before validating on port 3000 / releasing)
npm --prefix ui run build
robocopy ui\dist bridge\wwwroot /MIR      # then confirm bridge/wwwroot/index.html references the new asset hashes

# EXE build (from bridge/) — bundles Node 22 runtime, no Node/.NET needed by users
npm run build:exe                         # npx @yao-pkg/pkg . --targets node22-win-x64 --output dist/AthenaWeb.exe --compress GZip
```

Notes: port 3000 falls back to 3001, 3002… if busy (console `Local:` line shows the real URL). Close running `AthenaWeb*.exe` processes before rebuilding — a locked EXE silently stays stale.

### Bridge environment variables

| Variable | Default | Purpose |
|---|---|---|
| `WEB_PORT` | `3000` | HTTP/WS port (falls back to 3001, 3002… if busy). Note: the variable is `WEB_PORT`, not `PORT` (`server.js:29`) |
| `RELAY_HOST` / `RELAY_PORT` | `127.0.0.1` / `28800` | Relay endpoint |
| `ATHENA_MAPS_DIR` | `~/Documents/Athena/Maps` | Desktop map cache |
| `WORLD_SELECTION_MODE` | `fresh` | `fresh` (latest updated incl. `Rows`/`Roads` dirs) or `stable` (core files only) |
| `WORLD_CACHE_OVERRIDE` | empty | Force a world |
| `RELAY_CLIENT_TYPE` | `General` | Relay registration type (only `General` is accepted) |
| `RELAY_FRAME_POLL_TYPE` / `RELAY_FRAME_POLL_INTERVAL_MS` | `next` / `250` | Frame queue polling |
| `MAPEXPORT_STRATEGY` | `basic` | Export command variant harness (`guid-*` variants get `malformed`) |
| `UI_DEV_REDIRECT` | true | Dev-era redirect of `/` to :5173 |

---

## Relay protocol (verified, decompilation-backed)

- Framing: fields separated by `<ath_sep>`, message terminated by `<ath_sep>end`.
- Bridge registers as client type **`General`** (other types → `error: unrecognized type`).
- **Continuous frames require polling** with `next<ath_sep><ath_sep>end` (~250 ms). Passive receive only yields sparse/cached frames — this was the key "frame stream restored" breakthrough.
- Map export trigger: `command<ath_sep>mapexport<ath_sep>end` (relay replies `success`). Map import message types: `mapbegin`, `maprow`, `mapobjects`, `maplocations`, `maproads`, `mapfoliage`, `mapend`. In practice the relay does **not** deliver the chunk stream to the bridge; Web2 relies on Athena Desktop writing the disk cache. On `mapend`, all memos (`worldCacheMemo`, `contourCacheMemo`, `landMaskCacheMemo`) are cleared.
- Live frames carry the world name (`gameState.map`) and markers (`Children -> Markers`). Firewill I-TGT relay marker example: `name: MKR_0_NightHawk`, `text: TGT_0_NightHawk`, `type: mil_triangle`, `color: ColorBlue`.
- Relay log: `Relay.exe.log` next to `Relay.exe` in the Athena Workshop folder (watch for `Pipe is broken`).
- Extension settings: `AthenaExtensionSettings.txt` in the `@Athena - An Arma 2nd Screen Application` Workshop folder (`ath_launch_relay=true`, `ath_launch_app=false` optional).

## Bridge REST API (main endpoints)

- Geometry: `/api/game/worldinfo`, `roads`, `forests`, `locations`, `structures`, `elevations`, `trees`, `exportstatus`
- Static map: `/api/staticmap/{world}` (`availableZ` from `Height/Z*.txt`), `/api/staticmap/{world}/contours/{z}`, `/api/staticmap/{world}/landmask?gridSize=`
- Cache control: `GET/POST /api/game/cachemode` (`mode`, `world`/`worldOverride`)
- Health: `/api/health` (used by cache banner)
- Diagnostics: `/api/game/relaydebug`, `framedebug`, `elevationdebug`, `rawrelaydebug?limit=&clear=1`
- WebSocket: `state` broadcasts, `startMapExport` message, `mapImportDataReady` event.

## Map data / rendering knowledge

**Cache import (bridge):**
- Roads: non-`hide` roads emit one record per adjacent node pair; `hide` roads stay single area tiles (runway/apron/taxiway surfaces). Bus caches use only `RoadType=1` and `RoadType=3`; `RoadType=3` must map to Web2 concrete/main-road styling (v0.1.0 fix).
- Forests: fixed 16×16 cells (Bus `GenerateForestGeometry`); Y = `worldSize - (yIdx + 1) * 16`. Parses `CellsLight` + `CellsHeavy`.
- World size must come from hydrated cache metadata before relay fallbacks (Stratis = 8192).

**Bus-exact visual values (from dnSpy of Athena Desktop — keep these):**
- Side colors (alpha 0.706): WEST `rgba(78,118,204)`, EAST `rgba(204,78,78)`, GUER `rgba(0,128,0)`, CIV `rgba(178,0,255)`.
- Roads: Highway SandyBrown `#F4A460`, Concrete LightGray `#D3D3D3`, Asphalt DimGray `#696969`, Dirt Tan `#D2B48C`, Unknown Brown `#A52A2A`.
- Contours weight 0.75: Z=0 MediumBlue `#0000CD`, Z<0 DodgerBlue `#1E90FF`, Z>0 RosyBrown `#BC8F8F`.
- Ocean PowderBlue `#B0E0E6`; land Ivory `#FFFFF0` (Z=0 land polygon stroke `#FFFFF0` weight 1).
- Forest heavy `rgba(0,127,12,0.275)`, light `rgba(0,181,18,0.275)`. Trees radius 2.0, dark ring + green center.
- Aesthetic: flat, sharp, solid "military nav" — opaque strokes, black shadows/text outlines, `svg { shape-rendering: crispEdges }`, `canvas { image-rendering: pixelated }`.

**Current smoothing (v0.1.0):** contours = RDP (world-scaled ~5 m, Desktop `TrimPoints` tolerance) + one Chaikin pass; coastline Z=0 = two Chaikin passes. Land polygon and coast polyline must use **identical** point sets with `smoothFactor: 0` (prevents Leaflet per-zoom divergence / edge bleed).

**Airport surfaces:** square 20×20 `hide` tiles = gray airport surface; only explicit long/thin `hide` strips become brown dirt paths (stitched into polylines). Airport expansion second pass: `ringCount >= 2`, `cardinalCount >= 2`.

**World switching:** stale async geometry responses are ignored via request sequencing; geometry is cleared on world change; static layers render only when hydrated world == active world; live world expires after inactivity; offline cached-world picker works while disconnected.

**UI behavior to preserve:** Groups layer default OFF; Spawn Markers default OFF; separate toggles for Arma / AO / Spawn markers; manual layer toggles are not overridden by zoom; follow-player auto-disables on manual pan/zoom; I-TGT: 8-digit `XXXXYYYY` code, `T` or middle-click (or `SAVE TGT` on touch) stores target, labels start at `TGT_0` and reset on Delete All, stored targets cleared on online→offline transition; Firewill `TGT_*` relay markers route to the I-TGT layer.

**Vehicle icons:** `ui/icon-mapping.md` (header says "Athena Remastered" but it's the shared Bus icon table: 896 classes, 79 PNGs, category → icon, used by `categoryToIconFile()`). Unknown classes fall back to a generic vehicle silhouette (not `?`); drones/turrets use NATO marker textures; generic `*_UAV_AI` classes get an `AI` badge.

---

## Release process (v0.x)

A release is not complete until every item below is done in the same batch:

1. **Version sync:** `ui/src/version.ts` (`APP_VERSION`), `README.md` (version line, tracked EXE line, Quick Start EXE names, highlights), `CHANGELOG.md` top entry (Keep a Changelog style: Added/Changed/Fixed/Known Limitations), `WORKLOG.md` timeline entry.
2. **Packaged UI sync:** build UI → `robocopy ui\dist bridge\wwwroot /MIR` → verify `index.html` asset hashes.
3. **Dist parity:** `bridge/dist/AthenaWeb-<version>.exe` and `bridge/dist/AthenaWeb.exe` are the same build; remove the previous-version tracked EXE; update `repo_dist_contents.json` (path/size/blob SHA).
4. **Release notes:** create/update `.github/release-notes-template-vX.Y.Z.md` with the same version + filename. Written for **end users of the EXE**. **No Node.js/dev-mode instructions** in release notes; those stay in the README. Copy the structure of `release-notes-template-v0.1.1.md`:
   - **Quick Start (Windows EXE):** what you need (Arma 3, Bus Athena mod Workshop `1181881736`, Athena Desktop Workshop `668999292` for the one-time export, optional phone/tablet); start steps (download from Assets, run, keep the console open, allow the firewall on private networks, `Local:` vs `Network:` address, start Arma)
   - **First time on a new map:** Athena Desktop Export, then restart the EXE
   - **Troubleshooting** table (page not loading, port fallback, relay not connected / `ath_launch_relay=true`, phone can't connect, empty/squeezed map, Workshop: don't load `@AthenaWeb2`)
   - **SmartScreen workaround** (`More info` → `Run anyway`, `Properties` → `Unblock`, `Unblock-File -Path .\AthenaWeb-<version>.exe`)
   - **What's New** and **Known Limitations**
   - Keep README Quick Start wording consistent.
5. **GitHub Release** (only with user go-ahead): tag `vX.Y.Z`, attach `AthenaWeb-<version>.exe` under Assets; after any asset replacement, compare local SHA256 with the release asset digest.
6. **Workshop** (optional, user-driven): copy EXE into `@AthenaWeb2/`, write `@AthenaWeb2/workshop-change-note-<version>.txt`, run `.\Update-Workshop.ps1 -WorkshopId 3707444008` (needs Arma 3 Tools). `@AthenaWeb2` must never be loaded as a gameplay mod.

Documentation sync is always-on: any change to behavior, packaging, setup, or release flow updates `README.md` / `CHANGELOG.md` / `WORKLOG.md` in the same batch.

---

## Open items (as of v0.1.1 — verify against WORKLOG before acting)

- AO camp objective number labels (`1..5`) not consistently visible on camp icons.
- Tanoa taxi/runway coverage ~95%; some isolated connector tiles render as dirt.
- Vegetation/hedge orientation mismatch at `X:6874.88 Y:7381.98` (Tanoa cache) — long-standing.
- Relay map-export chunk delivery to bridge still unconfirmed (Desktop cache is the working path).
- Cleanup/hardening found 2026-11-22 (tracked in WORKLOG backlog "Cleanup / Hardening"):
  - bridge world-name validation (security, low severity; keep details out of public docs until fixed)
  - malformed URL encoding can crash the bridge
  - unused `@microsoft/signalr` dependency
  - inherited laze/projectile code to review
  - `npm audit` advisories
- Security findings for unfixed issues: describe them only at a high level in public files (WORKLOG, CHANGELOG, issues). Put the exact details in the fix PR.
- Parity backlog & next tasks: see bottom of `WORKLOG.md` (connection diagnostics panel, ORBAT player/AI styling, anchors + Locate, tracking lines with bearing/distance, session save/load, ink layers, record/playback).

## Environment notes

- Windows. Older WORKLOG entries reference paths on a previous machine (`C:\Users\jvdv2\...`); use `%USERPROFILE%` equivalents on the current machine. Repo now lives at `C:\Athena-Web-2`.
- Arma Workshop root: `C:\Program Files (x86)\Steam\steamapps\common\Arma 3\!Workshop\`.
- Toolchain (installed 2026-11-22 via winget): Node.js 22 LTS (`OpenJS.NodeJS.22`, npm 10), GitHub CLI `gh` (`GitHub.cli`). Node 22 matches the pkg EXE target; README's "Node.js 18+" is the minimum for dev mode.
- Shells started before those installs don't have them on PATH. Restart VS Code, or in PowerShell run: `$env:Path = [Environment]::GetEnvironmentVariable('Path','Machine') + ';' + [Environment]::GetEnvironmentVariable('Path','User')`.
- Claude Code's auto mode blocks `git push` when a credential login prompt is involved. The owner does any first-time login (`git push`, `gh auth login`) in their own terminal.
