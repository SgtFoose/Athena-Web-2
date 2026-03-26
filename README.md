# ATHENA WEB 2

> Tactical second-screen web companion for Arma 3 (Proof of Concept)

Version: v0.0.1-alpha (first public draft)

## SITREP

Athena Web 2 is currently a **Proof of Concept** at **v0.0.1-alpha** that connects to the local Athena relay and renders live mission data in a mobile-friendly web view.

This project will evolve into a full operational tool in the coming weeks.

## WHAT THIS DOES TODAY

- Connects to Athena Relay on TCP `28800`
- Parses Athena protocol messages using `<ath_sep>` and `<ath_sep>end`
- Bridges relay data to browser clients over WebSocket
- Renders live units on a tactical map canvas
- Supports phone/tablet viewing on local network

## QUICK START (TESTERS)

### 1. Prerequisites

- Arma 3 with **Athena - An Arma 2nd Screen Application** installed and running
- Node.js 18+ installed
- Local network access (for phone/tablet testing)

### 2. Configure Athena extension

Open:

`C:\Program Files (x86)\Steam\steamapps\common\Arma 3\!Workshop\@Athena - An Arma 2nd Screen Application\AthenaExtensionSettings.txt`

Recommended:

- `ath_launch_relay=true`
- `ath_launch_app=false` (so desktop app does not compete with this PoC)

### 3. Run the PoC

```powershell
cd poc
npm install
node server.js
```

### 4. Open on device

Use the network URL printed in terminal, for example:

`http://192.168.x.x:3000`

## CONTRIBUTING

Contributions are welcome while this is moving from prototype to full release.

### Priority areas

- Better map rendering and scaling logic
- Marker and vehicle visualization
- Mission/session persistence
- UI/UX hardening for mobile
- Error handling and reconnect resilience
- Packaging for non-technical users

### Contribution workflow

1. Fork this repository
2. Create a feature branch
3. Keep changes scoped and documented
4. Test with a live Athena relay session
5. Open a PR with:
   - What changed
   - How to test
   - Screenshots/gifs when UI changed

## PROJECT STRUCTURE

```text
poc/
  package.json
  server.js        # TCP relay -> WebSocket bridge
  probe.js         # Protocol probing utility
  public/
    index.html     # Mobile tactical map client
```

## ROADMAP (NEAR TERM)

- Stabilize protocol and frame parsing
- Improve tactical rendering fidelity
- Add client-side filters and layer toggles
- Add configuration UI
- Prepare packaged release path

## CREDITS

### Original Athena mod

- **Athena - An Arma 2nd Screen Application**
- Creator credit: **Bus**
- Workshop: https://steamcommunity.com/sharedfiles/filedetails/?id=1181881736&searchtext=athena

### Related work by this team

- https://steamcommunity.com/sharedfiles/filedetails/?id=3687225607

## DISCLAIMER

This repository is an independent web proof-of-concept bridge/client around Athena relay output and is not a replacement for the original Athena mod.

Respect all rights and credits of original authors.
