/**
 * server.js — Athena Web PoC bridge
 *
 * Bridges Athena Relay.exe (TCP 28800) → WebSocket → Browser
 * Also serves public/index.html over HTTP on port 3000.
 *
 * Run: node server.js
 * Then open http://localhost:3000 on any device on the same network.
 *
 * Configuration (via env vars or edit the CONFIG block below):
 *   RELAY_HOST   — IP of the PC running Relay.exe  (default: 127.0.0.1)
 *   RELAY_PORT   — Port of Relay.exe               (default: 28800)
 *   WEB_PORT     — HTTP/WS port for the browser     (default: 3000)
 */

'use strict';

const http    = require('http');
const net     = require('net');
const fs      = require('fs');
const path    = require('path');
const crypto  = require('crypto');
const { WebSocketServer, WebSocket } = require('ws');

// ─── Configuration ────────────────────────────────────────────────────────────
const CONFIG = {
  relayHost : process.env.RELAY_HOST || '127.0.0.1',
  relayPort : parseInt(process.env.RELAY_PORT  || '28800', 10),
  webPort   : parseInt(process.env.WEB_PORT    || '3000',  10),
  reconnectMs: 3000,   // ms between relay reconnect attempts
};

// GUID that identifies this bridge to the relay (keep stable between runs)
const BRIDGE_GUID = '5f3a9c12-4e7b-4d1a-b023-ae8f2c6d19f0';

// Protocol constants (discovered by binary analysis)
const SEP = '<ath_sep>';
const END = '<ath_sep>end';

// ─── State ────────────────────────────────────────────────────────────────────
let relaySocket    = null;
let relayStringBuf = '';         // string accumulator for <ath_sep> protocol
let relayConnected = false;
let gameState      = createEmptyState();    // last known game state for late-joiners
let mapImportState = createEmptyMapImportState();
let mapImportData  = createEmptyMapImportData();

/** @type {Set<WebSocket>} */
const wsClients = new Set();

// ─── Helpers ──────────────────────────────────────────────────────────────────

function createEmptyState() {
  return {
    connected  : false,
    map        : null,
    missionGuid: null,
    missionName: null,
    profile    : null,
    date       : null,
    time       : null,
    units      : [],   // merged unit info + position
    markers    : [],
    groups     : [],
    vehicles   : [],
  };
}

function createEmptyMapImportState() {
  return {
    inProgress: false,
    startedAt: null,
    completedAt: null,
    stage: 'idle',
    counts: {
      maprow: 0,
      mapobjects: 0,
      maplocations: 0,
      maproads: 0,
      mapfoliage: 0,
    },
    world: null,
    lastCommand: null,
  };
}

function createEmptyMapImportData() {
  return {
    maprows: [],
    mapobjects: [],
    maplocations: [],
    maproads: [],
    mapfoliage: [],
  };
}

function tryParseJson(text) {
  if (!text || !text.trim()) return null;
  try {
    return JSON.parse(text);
  } catch {
    return null;
  }
}

function broadcast(obj) {
  const msg = JSON.stringify(obj);
  for (const ws of wsClients) {
    if (ws.readyState === WebSocket.OPEN) {
      ws.send(msg);
    }
  }
}

// ─── Relay data parsing ───────────────────────────────────────────────────────
// Protocol (discovered): messages are delimited by "<ath_sep>end"
// Fields within a message are split by "<ath_sep>"
// Frame format: "frame<ath_sep>{json}<ath_sep>end"
// Registration:  "General<ath_sep>{guid}<ath_sep><ath_sep>end"

function processIncomingBytes(chunk) {
  relayStringBuf += chunk.toString('utf8');

  // Safety: discard if buffer grows too large
  if (relayStringBuf.length > 2 * 1024 * 1024) {
    console.warn('[relay] Buffer overflow — discarding');
    relayStringBuf = '';
    return;
  }

  // Extract all complete messages (terminated by <ath_sep>end)
  let idx;
  while ((idx = relayStringBuf.indexOf(END)) !== -1) {
    const raw = relayStringBuf.slice(0, idx);
    relayStringBuf = relayStringBuf.slice(idx + END.length);
    if (raw.length > 0) handleAthenaMessage(raw);
  }
}

function handleAthenaMessage(raw) {
  const parts = raw.split(SEP);
  const cmd   = parts[0].toLowerCase().trim();

  if (cmd === 'frame') {
    // parts[1] is the JSON payload
    const json = parts[1];
    if (!json) return;
    try {
      const frame = JSON.parse(json);
      handleFrame(frame);
    } catch (e) {
      console.warn('[relay] JSON parse failed:', e.message, json.slice(0, 100));
    }
  } else if (cmd === 'keepalive') {
    // nothing — just keeps the connection alive
  } else if (cmd === 'mapbegin' || cmd === 'maprow' || cmd === 'mapobjects' || cmd === 'maplocations' || cmd === 'maproads' || cmd === 'mapfoliage' || cmd === 'mapend') {
    handleMapImportMessage(cmd, parts[1] || '');
  } else {
    console.log('[relay] Unknown command:', cmd, parts.slice(1, 2));
  }
}

function handleMapImportMessage(cmd, payload) {
  if (cmd === 'mapbegin') {
    mapImportState = createEmptyMapImportState();
    mapImportData = createEmptyMapImportData();
    mapImportState.inProgress = true;
    mapImportState.startedAt = new Date().toISOString();
    mapImportState.stage = 'receiving';
    mapImportState.world = payload || null;
  } else if (cmd === 'mapend') {
    mapImportState.inProgress = false;
    mapImportState.completedAt = new Date().toISOString();
    mapImportState.stage = 'complete';
    broadcast({
      type: 'mapImportDataReady',
      data: {
        world: mapImportState.world,
        counts: {
          rows: mapImportData.maprows.length,
          roads: mapImportData.maproads.length,
          objects: mapImportData.mapobjects.length,
          locations: mapImportData.maplocations.length,
          foliage: mapImportData.mapfoliage.length,
        },
        parse: {
          roadSegments: mapImportData.maproads
            .reduce((sum, item) => sum + (((item && item.Segments) || []).length), 0),
          objectCount: mapImportData.mapobjects
            .reduce((sum, item) => sum + (((item && item.Objects) || []).length), 0),
          locationCount: mapImportData.maplocations
            .reduce((sum, item) => sum + (((item && item.Locations) || []).length), 0),
          treeCount: mapImportData.mapfoliage
            .reduce((sum, item) => sum + (((item && item.Trees) || []).length), 0),
          bushCount: mapImportData.mapfoliage
            .reduce((sum, item) => sum + (((item && item.Bushes) || []).length), 0),
        },
      },
    });
  } else {
    if (!mapImportState.inProgress) {
      mapImportState.inProgress = true;
      mapImportState.startedAt = mapImportState.startedAt || new Date().toISOString();
      mapImportState.stage = 'receiving';
    }
    if (Object.prototype.hasOwnProperty.call(mapImportState.counts, cmd)) {
      mapImportState.counts[cmd] += 1;
    }

    if (payload) {
      if (cmd === 'maprow') {
        mapImportData.maprows.push(payload);
      } else if (cmd === 'mapobjects') {
        mapImportData.mapobjects.push(tryParseJson(payload) || { _raw: payload });
      } else if (cmd === 'maplocations') {
        mapImportData.maplocations.push(tryParseJson(payload) || { _raw: payload });
      } else if (cmd === 'maproads') {
        mapImportData.maproads.push(tryParseJson(payload) || { _raw: payload });
      } else if (cmd === 'mapfoliage') {
        mapImportData.mapfoliage.push(tryParseJson(payload) || { _raw: payload });
      }
    }
  }

  mapImportState.lastCommand = cmd;
  broadcast({
    type: 'mapImportStatus',
    data: {
      command: cmd,
      world: mapImportState.world,
      inProgress: mapImportState.inProgress,
      stage: mapImportState.stage,
      counts: mapImportState.counts,
      startedAt: mapImportState.startedAt,
      completedAt: mapImportState.completedAt,
      hasPayload: Boolean(payload),
      payloadLength: payload ? payload.length : 0,
    },
  });
}

function sendRelayCommand(command, data = '') {
  if (!relaySocket || !relayConnected) return false;
  const frame = command + SEP + data + SEP + END;
  relaySocket.write(frame);
  return true;
}

/**
 * Handle a parsed v2 frame from the relay.
 * Schema (v2):
 *   frame.Format       = 'v2'
 *   frame.MissionGUID  = string
 *   frame.Mission      = { name, map, profile, author, ismultiplayer, steamid }
 *   frame.Children[]   = {
 *     FrameGUID, Date, Time,
 *     Groups[]   : { leaderNetID, groupNetId, name, id, side, wpx, wpy }
 *     Markers[]  : { ... }
 *     Units[]    : { name, netid, side, type, faction, rank, player, groupnetid, ... }
 *     Vehicles[] : { ... }
 *     UnitUpdates[]: { netid, posx, posy, posz, dir, speed }
 *   }
 */
function handleFrame(frame) {
  // Mission-level metadata
  if (frame.MissionGUID) gameState.missionGuid = frame.MissionGUID;
  if (frame.Mission) {
    gameState.map         = frame.Mission.map         || gameState.map;
    gameState.missionName = frame.Mission.name        || gameState.missionName;
    gameState.profile     = frame.Mission.profile     || gameState.profile;
  }

  const child = Array.isArray(frame.Children) ? frame.Children[0] : null;
  if (!child) return;

  gameState.date   = child.Date || gameState.date;
  gameState.time   = child.Time || gameState.time;
  gameState.groups  = child.Groups   || [];
  gameState.markers = child.Markers  || [];
  gameState.vehicles = child.Vehicles || [];

  // Build a unit map keyed by netid, merging static info + position update
  const unitMap = new Map();

  for (const u of (child.Units || [])) {
    unitMap.set(u.netid, { ...u });
  }
  for (const upd of (child.UnitUpdates || [])) {
    const u = unitMap.get(upd.netid);
    if (u) {
      u.posx  = parseFloat(upd.posx)  || 0;
      u.posy  = parseFloat(upd.posy)  || 0;
      u.posz  = parseFloat(upd.posz)  || 0;
      u.dir   = parseFloat(upd.dir)   || 0;
      u.speed = parseFloat(upd.speed) || 0;
    }
  }

  gameState.units = [...unitMap.values()];

  broadcast({ type: 'state', data: buildClientState() });
}

function buildClientState() {
  return {
    connected  : relayConnected,
    map        : gameState.map,
    missionGuid: gameState.missionGuid,
    missionName: gameState.missionName,
    profile    : gameState.profile,
    date       : gameState.date,
    time       : gameState.time,
    units      : gameState.units,
    markers    : gameState.markers,
    groups     : gameState.groups,
    vehicles   : gameState.vehicles,
  };
}

// ─── Relay TCP connection ─────────────────────────────────────────────────────
function connectToRelay() {
  if (relaySocket) return;

  console.log(`[relay] Connecting to ${CONFIG.relayHost}:${CONFIG.relayPort} …`);
  relaySocket = new net.Socket();

  relaySocket.connect(CONFIG.relayPort, CONFIG.relayHost, () => {
    console.log('[relay] Connected!');
    relayConnected = true;
    broadcast({ type: 'relayConnected' });

    // Register as a General client using the <ath_sep> protocol
    // Format: {CommandType}<ath_sep>{ClientGUID}<ath_sep>{ContentData}<ath_sep>end
    const handshake = 'General' + SEP + BRIDGE_GUID + SEP + SEP + END;
    relaySocket.write(handshake);
    console.log('[relay] Sent registration handshake');
  });

  relaySocket.on('data', (chunk) => {
    processIncomingBytes(chunk);
  });

  relaySocket.on('error', (err) => {
    console.error(`[relay] Error: ${err.message}`);
  });

  relaySocket.on('close', () => {
    console.log('[relay] Disconnected. Reconnecting in', CONFIG.reconnectMs, 'ms …');
    relaySocket    = null;
    relayConnected = false;
    relayStringBuf = '';
    gameState      = createEmptyState();
    mapImportState = createEmptyMapImportState();
    mapImportData = createEmptyMapImportData();
    broadcast({ type: 'relayDisconnected' });
    setTimeout(connectToRelay, CONFIG.reconnectMs);
  });
}

// ─── HTTP server (serves public/) ────────────────────────────────────────────
const MIME = {
  '.html': 'text/html; charset=utf-8',
  '.js'  : 'application/javascript',
  '.css' : 'text/css',
  '.png' : 'image/png',
  '.ico' : 'image/x-icon',
  '.json': 'application/json',
};

const httpServer = http.createServer((req, res) => {
  // Security: prevent path traversal
  const safePath = path.normalize(req.url).replace(/^(\.\.\/)+/, '');
  let filePath = path.join(__dirname, 'public', safePath === '/' ? 'index.html' : safePath);

  fs.stat(filePath, (err, stat) => {
    if (err || !stat.isFile()) {
      // Fallback to index.html for single-page behaviour
      filePath = path.join(__dirname, 'public', 'index.html');
    }
    fs.readFile(filePath, (readErr, data) => {
      if (readErr) {
        res.writeHead(404);
        res.end('Not found');
        return;
      }
      const ext  = path.extname(filePath);
      const mime = MIME[ext] || 'application/octet-stream';
      res.writeHead(200, { 'Content-Type': mime });
      res.end(data);
    });
  });
});

// ─── WebSocket server ─────────────────────────────────────────────────────────
const wss = new WebSocketServer({ server: httpServer });

wss.on('connection', (ws, req) => {
  const ip = req.socket.remoteAddress;
  console.log(`[ws] Client connected: ${ip}  (total: ${wsClients.size + 1})`);
  wsClients.add(ws);

  // Send current state immediately so late joiners catch up
  ws.send(JSON.stringify({ type: 'state', data: buildClientState() }));
  ws.send(JSON.stringify({ type: relayConnected ? 'relayConnected' : 'relayDisconnected' }));
  ws.send(JSON.stringify({ type: 'mapImportStatus', data: {
    command: mapImportState.lastCommand,
    world: mapImportState.world,
    inProgress: mapImportState.inProgress,
    stage: mapImportState.stage,
    counts: mapImportState.counts,
    startedAt: mapImportState.startedAt,
    completedAt: mapImportState.completedAt,
    hasPayload: false,
    payloadLength: 0,
  }}));

  ws.on('message', (raw) => {
    let msg;
    try {
      msg = JSON.parse(raw.toString('utf8'));
    } catch {
      ws.send(JSON.stringify({ type: 'bridgeError', data: { message: 'Malformed client message' } }));
      return;
    }

    if (msg?.type === 'startMapExport') {
      const ok = sendRelayCommand('command', 'mapexport');
      if (!ok) {
        ws.send(JSON.stringify({ type: 'bridgeError', data: { message: 'Relay not connected' } }));
        return;
      }

      mapImportState = createEmptyMapImportState();
      mapImportData = createEmptyMapImportData();
      mapImportState.inProgress = true;
      mapImportState.startedAt = new Date().toISOString();
      mapImportState.stage = 'requested';
      mapImportState.lastCommand = 'mapexport-requested';

      broadcast({
        type: 'mapImportStatus',
        data: {
          command: mapImportState.lastCommand,
          world: null,
          inProgress: true,
          stage: 'requested',
          counts: mapImportState.counts,
          startedAt: mapImportState.startedAt,
          completedAt: null,
          hasPayload: false,
          payloadLength: 0,
        },
      });
      return;
    }

    ws.send(JSON.stringify({ type: 'bridgeError', data: { message: 'Unknown client action' } }));
  });

  ws.on('close', () => {
    wsClients.delete(ws);
    console.log(`[ws] Client disconnected: ${ip}  (total: ${wsClients.size})`);
  });

  ws.on('error', (err) => {
    console.error('[ws] Client error:', err.message);
    wsClients.delete(ws);
  });
});

// ─── Boot ─────────────────────────────────────────────────────────────────────
httpServer.listen(CONFIG.webPort, '0.0.0.0', () => {
  const ifaces = require('os').networkInterfaces();
  const addrs  = Object.values(ifaces).flat()
    .filter(i => i.family === 'IPv4' && !i.internal)
    .map(i => `http://${i.address}:${CONFIG.webPort}`);

  console.log('\n╔════════════════════════════════════════╗');
  console.log('║   Athena Web PoC — Bridge Server       ║');
  console.log('╚════════════════════════════════════════╝');
  console.log(`Local:   http://localhost:${CONFIG.webPort}`);
  addrs.forEach(a => console.log(`Network: ${a}  ← open on your phone`));
  console.log(`\nRelay:   ${CONFIG.relayHost}:${CONFIG.relayPort}`);
  console.log('─────────────────────────────────────────\n');
});

connectToRelay();
