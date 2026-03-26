/**
 * probe.js — Athena Relay protocol discovery tool
 *
 * Connects to Relay.exe on TCP 28800 and dumps everything it sends/receives
 * so we can reverse-engineer the wire format before building the full client.
 *
 * Run: node probe.js
 * Optional: node probe.js <host> <port>   (defaults: 127.0.0.1 28800)
 */

const net = require('net');
const crypto = require('crypto');

const HOST = process.argv[2] || '127.0.0.1';
const PORT = parseInt(process.argv[3] || '28800', 10);

// A deterministic probe GUID so we can identify this client in logs
const CLIENT_GUID = crypto.randomUUID ? crypto.randomUUID() : 'aaaaaaaa-bbbb-cccc-dddd-eeeeeeeeeeee';

let rawChunks = [];
let messageCount = 0;

function hexDump(buf) {
  const hex = buf.toString('hex').match(/.{1,32}/g) || [];
  const asc = buf.toString('ascii').replace(/[^\x20-\x7e]/g, '.');
  return hex.map((h, i) => {
    const offset = (i * 16).toString(16).padStart(4, '0');
    const bytes = h.match(/.{1,2}/g).join(' ').padEnd(47, ' ');
    const chars = asc.slice(i * 16, i * 16 + 16);
    return `${offset}  ${bytes}  ${chars}`;
  }).join('\n');
}

function tryParseJSON(buf) {
  const str = buf.toString('utf8').trim();
  // Try full buffer
  try { return JSON.parse(str); } catch {}
  // Try each line
  for (const line of str.split('\n')) {
    try { return JSON.parse(line.trim()); } catch {}
  }
  return null;
}

function printChunk(label, data) {
  console.log(`\n─── ${label} (${data.length} bytes) ───`);
  console.log(hexDump(data));
  const j = tryParseJSON(data);
  if (j) {
    console.log('  → JSON parse OK:', JSON.stringify(j, null, 2).slice(0, 500));
  } else {
    const str = data.toString('utf8');
    const printable = str.replace(/[^\x09\x0a\x0d\x20-\x7e]/g, '?');
    if (printable.length > 0) console.log('  → ASCII:', printable.slice(0, 300));
  }
}

const client = new net.Socket();
client.setTimeout(10000);

console.log(`Connecting to Athena Relay at ${HOST}:${PORT} …`);
console.log(`Probe GUID: ${CLIENT_GUID}\n`);

client.connect(PORT, HOST, () => {
  console.log('✓ Connected to relay\n');

  // Phase 1 — sit silently for 2 seconds to see if relay pushes anything first
  console.log('[Phase 1] Waiting 2s for any unsolicited data from relay …');
  setTimeout(phase2, 2000);
});

function phase2() {
  console.log('\n[Phase 2] Sending null byte (common simple terminator) …');
  client.write(Buffer.from([0x00]));

  setTimeout(phase3, 1000);
}

function phase3() {
  console.log('\n[Phase 3] Sending CRLF …');
  client.write(Buffer.from('\r\n', 'utf8'));

  setTimeout(phase4, 1000);
}

function phase4() {
  // Try registering as a "General" client with a JSON command — most likely format
  const msg = JSON.stringify({
    CommandType: 'General',
    ClientGUID: CLIENT_GUID,
    ClientType: 'General',
    ContentData: ''
  });
  console.log('\n[Phase 4] Sending JSON General registration:');
  console.log('  ', msg);
  client.write(Buffer.from(msg + '\n', 'utf8'));

  setTimeout(phase5, 2000);
}

function phase5() {
  // Try with a null-byte terminated JSON (common in .NET BeginReceive patterns)
  const msg = JSON.stringify({
    CommandType: 'General',
    ClientGUID: CLIENT_GUID
  });
  console.log('\n[Phase 5] Sending JSON + null terminator …');
  client.write(Buffer.from(msg + '\0', 'utf8'));

  setTimeout(phase6, 2000);
}

function phase6() {
  // Try a plain GUID string — maybe the protocol is just a raw GUID
  console.log('\n[Phase 6] Sending bare GUID + newline …');
  client.write(Buffer.from(CLIENT_GUID + '\n', 'utf8'));

  setTimeout(() => {
    console.log('\n[Done] Closing probe after 15s total. Received', messageCount, 'chunk(s).');
    client.destroy();
    process.exit(0);
  }, 3000);
}

client.on('data', (data) => {
  messageCount++;
  rawChunks.push(data);
  printChunk(`RECV chunk #${messageCount}`, data);

  // Try to detect separator bytes
  const bytes = [...data];
  const seps = bytes.filter(b => b < 0x20).map(b => `0x${b.toString(16).padStart(2,'0')}`);
  if (seps.length > 0) {
    console.log('  → Control bytes found:', [...new Set(seps)].join(', '));
  }
});

client.on('timeout', () => {
  console.log('\n[Timeout] No activity for 10s.');
  client.destroy();
  process.exit(0);
});

client.on('error', (err) => {
  console.error(`\n✗ Connection error: ${err.message}`);
  if (err.code === 'ECONNREFUSED') {
    console.log('  → Is Arma 3 running with the Athena mod? Is Relay.exe active?');
  }
  process.exit(1);
});

client.on('close', () => {
  console.log('\n[Closed] Connection closed.');
  if (rawChunks.length === 0) {
    console.log('  → No data received at all.');
    console.log('  Conclusions:');
    console.log('  1. Relay may need Arma3 to be running to push data');
    console.log('  2. The relay may wait for a valid handshake before sending');
    console.log('  3. Check that Relay.exe is running: look for it in Task Manager');
  }
});
