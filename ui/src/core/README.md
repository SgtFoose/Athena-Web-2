# Shared UI core

This folder is the **shared UI core** of Athena Web 2 and Athena Remastered (AR). It's owned by Athena Web 2 (https://github.com/SgtFoose/Athena-Web-2) and copied **one-way** into AR's `Frontend/src/core/` by `scripts/sync-core-to-ar.ps1`.

- **Change core files only in Athena Web 2.** In AR, the sync script detects edits through `core-manifest.json` and refuses to overwrite them.
- Core files read app-specific values only from the app's own `appConfig.ts`: API base, name, version, donate link, `CAPABILITIES`, `MAP_DATA`.
- No app-specific strings or URLs belong here.
- AR-only features (lazes, projectile tracking, Fire Control, kill feed) are **not** part of the core. AR plugs them in through `AthenaMap`'s `mapExtensions` / `overlayExtensions` / `pulseUnitIds` slots.

Full plan, appConfig contract and capabilities: `docs/ui-core-sync.md` in Athena Web 2.
