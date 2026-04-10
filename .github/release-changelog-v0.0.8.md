## [0.0.8] â€” 2026-04-10

### Added

- Added Firewill I-TGT relay interoperability validation to the v0.0.8 local test cycle, confirming live marker payload passthrough from relay frames into bridge state (`Markers[]` with `name`, `text`, `type`, `color`, and XY position data)

### Validated

- Validated live Firewill marker label capture in-session using marker text `TGT_0_NightHawk`
- Validated corresponding relay marker object fields from live state feed: `name=MKR_0_NightHawk`, `type=mil_triangle`, `color=ColorBlue`, `posx=746.009`, `posy=12267.2`

### Changed

- Updated release/version references across README and app versioning for `v0.0.8`
- Updated I-TGT marker label readability and styling for both local and Firewill marker paths (larger text, Firewill-style blue labels, boxed background removed)

