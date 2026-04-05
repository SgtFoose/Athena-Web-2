import { MapContainer, useMap } from 'react-leaflet';
import L from 'leaflet';
import { useEffect, useMemo, useRef, useState } from 'react';
import type { Dispatch, SetStateAction } from 'react';
import type { Unit, Vehicle, Group, Road, ForestsData, MapLocation, MapStructure, ElevationsData, ContourLine, FiredEvent, FiredImpactEvent, ActiveLaze } from '../types/game';
import { API_BASE } from '../apiBase';
import 'leaflet/dist/leaflet.css';

// Arma uses a square world coordinate system [0..worldSize].
// We project it onto Leaflet CRS.Simple with a normalised 0100 space.
// Y is flipped so "North" (higher Y in Arma) appears at the top.

// Side  colour matching Arma 3 faction conventions
function sideColor(side: string): string {
  switch (side.toLowerCase()) {
    case 'west':     return '#4e9de0'; // BLUFOR  blue
    case 'east':     return '#d93b3b'; // OPFOR   red
    case 'guer':     return '#4ec94e'; // INDFOR  green
    case 'civ':      return '#9b59b6'; // CIV     purple
    default:         return '#cccccc';
  }
}

//  Road styling 

function roadStyle(type: string, foot: boolean): { color: string; weight: number } | null {
  if (foot) return null;  // skip footpaths
  // Bus exact road colors: MapHelper.cs PopulateRoads
  // Our Arma type strings map to Bus's RoadType enum:
  //   '' / 'road'  Highway (main paved), 'main road'  Concrete,
  //   'track'  Dirt, 'hide'  airport surfaces (Concrete)
  switch (type.toLowerCase()) {
    case '':           return { color: '#F4A460', weight: 7.0  };  // Highway  SandyBrown
    case 'road':       return { color: '#F4A460', weight: 7.0  };  // Highway  SandyBrown
    case 'main road':  return { color: '#D3D3D3', weight: 5.0  };  // Concrete  LightGray
    case 'track':      return { color: '#D2B48C', weight: 3.0  };  // Dirt  Tan
    case 'hide':       return { color: '#D3D3D3', weight: 2.0  };  // airport  LightGray
    default:           return { color: '#A52A2A', weight: 3.0  };  // Unknown  Brown
  }
}

function rotatedRoadRect(road: Road, scale: number): [number, number][] {
  // For "hide" roads (airport surfaces), use the object centre from getPosASL;
  // beg1/end2 from getRoadInfo are road-segment endpoints and wrong for area objects.
  // Fall back to midpoint of beg/end for old cached data lacking posX/posY.
  const cx = (road.posX ? road.posX : (road.beg1X + road.end2X) / 2) * scale;
  const cy = (road.posY ? road.posY : (road.beg1Y + road.end2Y) / 2) * scale;
  // Pad each tile by 1m so adjacent 2020 tiles overlap slightly, hiding
  // sub-pixel gaps from floating-point position/rotation precision.
  const pad = 1;
  const halfWidth = ((road.width + pad) / 2) * scale;
  const halfLength = ((road.length + pad) / 2) * scale;
  const angle = (road.dir * Math.PI) / 180;
  const sin = Math.sin(angle);
  const cos = Math.cos(angle);

  const corners: [number, number][] = [
    [-halfWidth, -halfLength],
    [halfWidth, -halfLength],
    [halfWidth, halfLength],
    [-halfWidth, halfLength],
  ].map(([dx, dy]) => [
    cy + dx * sin + dy * cos,
    cx + dx * cos - dy * sin,
  ]);

  return corners;
}

function rotatedStructureRect(s: MapStructure, scale: number): [number, number][] {
  const cx = s.posX * scale;
  const cy = s.posY * scale;
  const halfWidth = (s.width / 2) * scale;
  const halfLength = (s.length / 2) * scale;
  const angle = (s.dir * Math.PI) / 180;
  const sin = Math.sin(angle);
  const cos = Math.cos(angle);

  return [
    [-halfWidth, -halfLength],
    [halfWidth, -halfLength],
    [halfWidth, halfLength],
    [-halfWidth, halfLength],
  ].map(([dx, dy]) => [
    // Arma heading: 0=north, 90=east. Convert local right/forward axes to map lat/lng.
    cy + (-dx * sin) + (dy * cos),
    cx + (dx * cos) + (dy * sin),
  ]);
}

function isWallFenceLike(s: MapStructure): boolean {
  const sig = `${s.type} ${s.model}`.toLowerCase();
  return /(wall|fence|barrier|railing|hedge)/.test(sig);
}

function isPowerWireLike(s: MapStructure): boolean {
  const sig = `${s.type} ${s.model}`.toLowerCase();
  // Keep pole bases on polygon rendering; only classify the cable/span objects as lines.
  if (/(pole|post|pylon|mast)/.test(sig)) return false;
  return /(powerline|power_line|overhead|cable|wire)/.test(sig);
}

function isPowerPoleLike(s: MapStructure): boolean {
  const sig = `${s.type} ${s.model}`.toLowerCase();
  return /(pole|post|pylon|mast)/.test(sig) && /(powerline|power_line|power|electric|utility|cable|wire)/.test(sig);
}

const MAP_MIN_ZOOM = 3;
const MAP_MAX_ZOOM = 10.5;
const DISPLAY_MIN_SCALE = 0.1;
const DISPLAY_MAX_SCALE = 3.0;

function zoomToDisplayScale(zoom: number): number {
  const t = Math.max(0, Math.min(1, (zoom - MAP_MIN_ZOOM) / (MAP_MAX_ZOOM - MAP_MIN_ZOOM)));
  return DISPLAY_MIN_SCALE + t * (DISPLAY_MAX_SCALE - DISPLAY_MIN_SCALE);
}

interface TreeRecord {
  x: number;
  worldY: number;
  lat: number;
  lng: number;
}

interface TreeGridLayerContext {
  cells: Map<number, TreeRecord[]>;
  scale: number;
  cellW: number;
  gridSize: number;
}

class TreeGridLayer extends L.GridLayer {
  private readonly context: TreeGridLayerContext;

  constructor(context: TreeGridLayerContext, options?: L.GridLayerOptions) {
    super(options);
    this.context = context;
  }

  override createTile(coords: L.Coords): HTMLCanvasElement {
    const tile = document.createElement('canvas');
    const size = this.getTileSize();
    tile.width = size.x;
    tile.height = size.y;

    const ctx = tile.getContext('2d');
    if (!ctx) return tile;

    const map = this._map;
    if (!map) return tile;

    const { cells, scale, cellW, gridSize } = this.context;

    const nw = new L.Point(coords.x * size.x, coords.y * size.y);
    const nwLL = map.unproject(nw, coords.z);
    const seLL = map.unproject(new L.Point(nw.x + size.x, nw.y + size.y), coords.z);

    const minLng = Math.min(nwLL.lng, seLL.lng);
    const maxLng = Math.max(nwLL.lng, seLL.lng);
    const minLat = Math.min(nwLL.lat, seLL.lat);
    const maxLat = Math.max(nwLL.lat, seLL.lat);

    const wMinX = minLng / scale;
    const wMaxX = maxLng / scale;
    const wMinY = minLat / scale;
    const wMaxY = maxLat / scale;

    const gxMin = Math.max(0, Math.floor(wMinX / cellW));
    const gxMax = Math.min(gridSize - 1, Math.floor(wMaxX / cellW));
    const gyMin = Math.max(0, Math.floor(wMinY / cellW));
    const gyMax = Math.min(gridSize - 1, Math.floor(wMaxY / cellW));

    // Bus exact: EllipseGeometry radius 2.02.0 (fixed size)
    const radius = 2.0;
    ctx.fillStyle = 'rgba(34, 139, 34, 1)';  // Forest green, solid fill

    for (let gy = gyMin; gy <= gyMax; gy++) {
      for (let gx = gxMin; gx <= gxMax; gx++) {
        const bucket = cells.get(gy * gridSize + gx);
        if (!bucket) continue;

        for (const tree of bucket) {
          if (
            tree.x < wMinX - 2 || tree.x > wMaxX + 2 ||
            tree.worldY < wMinY - 2 || tree.worldY > wMaxY + 2
          ) {
            continue;
          }

          const point = map.project([tree.lat, tree.lng], coords.z);
          const px = point.x - nw.x;
          const py = point.y - nw.y;

          ctx.beginPath();
          ctx.arc(px, py, radius, 0, Math.PI * 2);
          ctx.fill();
        }
      }
    }

    return tile;
  }
}

//  Unit icons 
// Match Athena Desktop Unit.GetMarkerType() logic exactly.
function unitIconFile(unit: Unit): string {
  const t = (unit.type ?? '').trim().toLowerCase();
  const r = (unit.rank ?? '').toLowerCase();
  // Rank >= Lieutenant  officer
  if (/colonel|major|captain|lieutenant/.test(r)) return 'iconmanofficer';
  // Squad/team leader
  if (t.includes('_sl') || t.includes('_tl_')) return 'iconmanleader';
  // Medic
  if (t.includes('medic') || unit.hasMediKit) return 'iconmanmedic';
  // Engineer
  if (t.includes('engineer')) return 'iconmanengineer';
  // Weapons (primary then secondary)
  const w1 = (unit.weaponPrimary ?? '').toLowerCase();
  const w2 = (unit.weaponSecondary ?? '').toLowerCase();
  if (w1 === 'machinegun' || w2 === 'machinegun') return 'iconmanmg';
  if (/rocketlauncher|missilelauncher/.test(w1) || /rocketlauncher|missilelauncher/.test(w2)) return 'iconmanat';
  return 'iconman';
}

function escapeHtml(text: string): string {
  return text
    .replaceAll('&', '&amp;')
    .replaceAll('<', '&lt;')
    .replaceAll('>', '&gt;')
    .replaceAll('"', '&quot;')
    .replaceAll("'", '&#39;');
}

function unitLabel(unit: Unit): string {
  if (unit.playerName?.trim()) return unit.playerName.trim();
  return unit.name?.trim() || unit.type;
}

// Match Athena Desktop: just the tinted PNG icon, rotated by direction, no circle/arrow.
function unitIcon(unit: Unit): L.DivIcon {
  const color    = sideColor(unit.side);
  const isPlayer = unit.playerName !== '';
  const size     = isPlayer ? 34 : 28;
  const icon     = unitIconFile(unit);
  const dir      = Number.isFinite(unit.dir) ? unit.dir : 0;
  const label = escapeHtml(unitLabel(unit));
  const html = `<div class="${isPlayer ? 'map-marker-player' : 'map-marker-unit'}" style="position:relative;width:${size}px;height:${size}px;pointer-events:none;">` +
    `<div style="width:${size}px;height:${size}px;` +
    `background-color:${color};` +
    `-webkit-mask-image:url(/icons/vehicles/${icon}.png);` +
    `mask-image:url(/icons/vehicles/${icon}.png);` +
    `-webkit-mask-size:contain;mask-size:contain;` +
    `-webkit-mask-repeat:no-repeat;mask-repeat:no-repeat;` +
    `-webkit-mask-position:center;mask-position:center;` +
    `transform:rotate(${dir}deg);transform-origin:center;` +
    `filter:drop-shadow(1px 0 0 #000) drop-shadow(-1px 0 0 #000) drop-shadow(0 1px 0 #000) drop-shadow(0 -1px 0 #000);` +
    `"></div>` +
    `<div class="map-marker-label" style="position:absolute;top:${size + 2}px;left:50%;transform:translateX(-50%);white-space:nowrap;">${label}</div>` +
    `</div>`;
  return L.divIcon({ className: '', iconSize: [size, size], iconAnchor: [size / 2, size / 2], html });
}

// Resolve APP-6 / NATO icon type from group members' vehicle classes and unit types.
function resolveGroupType(members: Unit[], vehicles: Record<string, Vehicle>, vehicleMap: Map<string, string>): string {
  const mountedVeh = members.map(u => u.vehicleId ? vehicles[u.vehicleId] : undefined).find(v => v);
  if (mountedVeh) {
    const cat = resolveVehicleCategory(mountedVeh.class, vehicleMap);
    switch (cat) {
      case 'Planes': return 'plane';
      case 'Helicopters': return 'air';
      case 'Tanks': return 'armor';
      case 'APCs': return 'mech_inf';
      case 'Cars': return 'motor_inf';
      case 'Drones': return 'uav';
      case 'Artillery': return 'art';
      case 'AAs': return 'art';
      case 'Boats': case 'Submersibles': return 'naval';
      case 'Turrets': return 'mortar';
    }
    const cls = mountedVeh.class.toLowerCase();
    if (cls.includes('uav') || cls.includes('ugv')) return 'uav';
    if (cls.includes('plane') || cls.includes('jet')) return 'plane';
    if (cls.includes('heli')) return 'air';
    if (cls.includes('tank') || cls.includes('mbt')) return 'armor';
    if (cls.includes('apc') || cls.includes('ifv')) return 'mech_inf';
    return 'motor_inf';
  }
  const types = members.map(u => u.type.toLowerCase()).join(' ');
  if (/medic|corpsman/.test(types)) return 'med';
  if (/sniper|marksman|recon|spotter/.test(types)) return 'recon';
  if (/officer|commander|jtac/.test(types)) return 'hq';
  return 'inf';
}

// Resolve NATO marker filename prefix from side (Desktop: CIV/GUER/UNKNOWN all use 'n').
function natoSidePrefix(side: string): string {
  switch (side.toLowerCase()) {
    case 'west': return 'b';
    case 'east': return 'o';
    default:     return 'n';
  }
}

function shortRank(rank: string): string {
  const r = rank.toLowerCase();
  if (r.includes('private'))   return 'Pvt';
  if (r.includes('corporal'))  return 'Cpl';
  if (r.includes('sergeant'))  return 'Sgt';
  if (r.includes('lieutenant'))return 'Lt';
  if (r.includes('captain'))   return 'Capt';
  if (r.includes('major'))     return 'Maj';
  if (r.includes('colonel'))   return 'Col';
  if (r === '' || r === 'none') return '';
  return rank;
}

//  Waypoint line styling per type 
function waypointStyle(wpType: string): { color: string; endColor: string; endIcon: string } {
  const t = wpType.toUpperCase();
  // Attack / aggressive types  red
  if (t === 'DESTROY' || t === 'SAD' || t === 'SEEK AND DESTROY' || t === 'ATTACK')
    return { color: '#d32f2f', endColor: '#d32f2f', endIcon: '<line x1="4" y1="4" x2="10" y2="10" stroke="#fff" stroke-width="1.5"/><line x1="10" y1="4" x2="4" y2="10" stroke="#fff" stroke-width="1.5"/>' };
  // Guard / support  amber
  if (t === 'GUARD' || t === 'SUPPORT')
    return { color: '#f9a825', endColor: '#f9a825', endIcon: '' };
  // Get in / board  blue
  if (t === 'GETIN' || t === 'GET IN' || t === 'GETOUT' || t === 'GET OUT' || t === 'LOAD' || t === 'UNLOAD')
    return { color: '#1976d2', endColor: '#1976d2', endIcon: '' };
  // Sentry / hold  purple
  if (t === 'SENTRY' || t === 'HOLD')
    return { color: '#9c27b0', endColor: '#9c27b0', endIcon: '' };
  // Move / default  dark grey
  return { color: '#333', endColor: '#aaa', endIcon: '' };
}

type ProjectileProfile = {
  speedMps: number;
  rangeM: number;
  lateralFactor: number;
  color: string;
  headingOffsetDeg?: number;
  maxTrackAngleDeg: number;
  requiresLock: boolean;
  allowEnemyTargetInference: boolean;
  preferLiveLaze: boolean;
  launchDelayMs: number;
  lingerAfterImpactMs: number;
  // Ballistic trajectory: ETA (s) = ballA * distance^ballB  derived from live calibration.
  // When set, overrides the fixed-speed speedMps formula for ETA calculation.
  useBallistics?: boolean;
  ballA?: number;
  ballB?: number;
};

type TargetEstimate = {
  x: number;
  y: number;
  source: 'event' | 'laze' | 'waypoint' | 'enemy' | 'unit' | 'heading';
  eventSource?: string;
};

type GuidedLock = {
  kind: 'unit' | 'laze';
  unitId: string;
};

function clamp(v: number, min: number, max: number): number {
  return Math.min(max, Math.max(min, v));
}

function normDeg(deg: number): number {
  let d = deg % 360;
  if (d < 0) d += 360;
  return d;
}

function shortestAngleDiff(a: number, b: number): number {
  const d = Math.abs(normDeg(a) - normDeg(b));
  return d > 180 ? 360 - d : d;
}

function getProjectileProfile(ev: FiredEvent): ProjectileProfile | null {
  const text = `${ev.weapon} ${ev.ammo} ${ev.projectile}`.toLowerCase();
  if (!text.trim()) return null;

  if (text.includes('cruise')) {
    // Calibrated from live VLS tests: practical time-of-flight is about 14s at ~1500m.
    return { speedMps: 110, rangeM: 14000, lateralFactor: 0.12, color: '#ff6b6b', maxTrackAngleDeg: 70, requiresLock: true, allowEnemyTargetInference: true, preferLiveLaze: true, launchDelayMs: 1200, lingerAfterImpactMs: 9500 };
  }
  if ((text.includes('artillery') || text.includes('shell') || text.includes('mortar')) &&
      !text.includes('shipcannon') &&
      !text.includes('weapon_shipcannon_120mm')) {
    // Calibrated from M4 Scorcher: 1391m29s, 11178m72s  ETA  1.235  d^0.4363
     return { speedMps: 230, rangeM: 9000, lateralFactor: 0, color: '#ffd43b', maxTrackAngleDeg: 28, requiresLock: false, allowEnemyTargetInference: false, preferLiveLaze: false, launchDelayMs: 0, lingerAfterImpactMs: 2500, useBallistics: true, ballA: 1.235, ballB: 0.4363 };
  }
  if (text.includes('rocket') || text.includes('mlrs') || text.includes('230mm')) {
    // Calibrated from MLRS rockets_230mm_GAT: 1393m23s  ETA  0.979  d^0.4363 (same exponent as SPG)
     return { speedMps: 190, rangeM: 5000, lateralFactor: 0, color: '#74c0fc', maxTrackAngleDeg: 22, requiresLock: false, allowEnemyTargetInference: false, preferLiveLaze: false, launchDelayMs: 0, lingerAfterImpactMs: 2500, useBallistics: true, ballA: 0.979, ballB: 0.4363 };
  }
  if (text.includes('shipcannon')) {
    // MK45 naval gun (weapon_ShipCannon_120mm). Non-monotonic ETA curve  medium ranges are slowest
    // (high-arc trajectory). Ballistic fit anchored at 762m29s and 12352m76s.
    // fired_impact telemetry will correct the mid-range underestimate after the first shot lands.
    // lateralFactor: 0  indirect fire flies a straight horizontal path; curvature is only vertical.
    // A non-zero factor always curves left of the bearing which looks wrong (south for NW shots, etc.).
    return { speedMps: 90, rangeM: 14000, lateralFactor: 0, color: '#e599f7', maxTrackAngleDeg: 22, requiresLock: false, allowEnemyTargetInference: false, preferLiveLaze: false, launchDelayMs: 0, lingerAfterImpactMs: 2500, useBallistics: true, ballA: 3.424, ballB: 0.3457 };
  }
  if (text.includes('missile') || text.includes('sam') || text.includes('at')) {
    return { speedMps: 300, rangeM: 7000, lateralFactor: 0.06, color: '#ffa94d', maxTrackAngleDeg: 40, requiresLock: true, allowEnemyTargetInference: true, preferLiveLaze: false, launchDelayMs: 0, lingerAfterImpactMs: 2500 };
  }
  return null;
}

function targetFromFiredEvent(ev: FiredEvent): TargetEstimate | null {
  if (!ev.targetSource?.trim()) return null;
  if (typeof ev.targetX !== 'number' || !Number.isFinite(ev.targetX)) return null;
  if (typeof ev.targetY !== 'number' || !Number.isFinite(ev.targetY)) return null;

  const source = ev.targetSource.toLowerCase();
  const text = `${ev.weapon} ${ev.ammo} ${ev.projectile}`.toLowerCase();
  const isUnguidedIndirect = text.includes('rocket') || text.includes('mlrs') || text.includes('230mm') || text.includes('artillery') || text.includes('shell') || text.includes('mortar') || text.includes('shipcannon');

  // Ignore broad laser/scan target inference for unguided indirect fire.
  if (isUnguidedIndirect && source !== 'assigned') return null;

  return {
    x: ev.targetX,
    y: ev.targetY,
    source: 'event',
    eventSource: ev.targetSource,
  };
}

function projectileSourceTag(target: TargetEstimate): string {
  if (target.source === 'event') {
    switch ((target.eventSource ?? '').toLowerCase()) {
      case 'assigned': return 'LOCK';
      case 'laser': return 'LASE';
      case 'scan': return 'SCAN';
      default: return 'RPT';
    }
  }
  if (target.source === 'laze') return 'LASE';
  if (target.source === 'unit') return 'TGT';
  return target.source === 'waypoint' ? 'WP' : target.source === 'enemy' ? 'TGT' : 'HDG';
}

function findNearestEnemyUnitId(
  srcX: number,
  srcY: number,
  dirDeg: number,
  shooterSide: string,
  allUnits: Record<string, Unit>,
  profile: ProjectileProfile,
): string | null {
  if (!profile.allowEnemyTargetInference) return null;

  const rad = (dirDeg * Math.PI) / 180;
  const fx = Math.sin(rad);
  const fy = Math.cos(rad);
  const maxTrackCos = Math.cos((profile.maxTrackAngleDeg * Math.PI) / 180);

  let best: { id: string; score: number } | null = null;
  for (const u of Object.values(allUnits)) {
    if (u.side === shooterSide) continue;
    if (!Number.isFinite(u.posX) || !Number.isFinite(u.posY)) continue;
    if (u.posX === 0 && u.posY === 0) continue;

    const dx = u.posX - srcX;
    const dy = u.posY - srcY;
    const dist = Math.hypot(dx, dy);
    if (dist < 50 || dist > profile.rangeM * 1.35) continue;

    const nx = dx / dist;
    const ny = dy / dist;
    const dot = nx * fx + ny * fy;
    if (dot < maxTrackCos) continue;

    const anglePenalty = 1 - dot;
    const rangePenalty = dist / Math.max(profile.rangeM, 1);
    const score = anglePenalty * 0.7 + rangePenalty * 0.3;
    if (!best || score < best.score) best = { id: u.id, score };
  }

  return best?.id ?? null;
}

function findNearestLazeUnitId(
  srcX: number,
  srcY: number,
  dirDeg: number,
  shooterUnitId: string,
  lazes: ActiveLaze[],
  profile: ProjectileProfile,
): string | null {
  if (lazes.length === 0) return null;

  const ownLaze = lazes.find(l => l.unitId === shooterUnitId);
  if (ownLaze && Number.isFinite(ownLaze.posX) && Number.isFinite(ownLaze.posY)) {
    return ownLaze.unitId;
  }

  const rad = (dirDeg * Math.PI) / 180;
  const fx = Math.sin(rad);
  const fy = Math.cos(rad);
  const maxTrackCos = Math.cos((Math.min(85, profile.maxTrackAngleDeg + 15) * Math.PI) / 180);

  let best: { unitId: string; score: number } | null = null;
  for (const l of lazes) {
    if (!Number.isFinite(l.posX) || !Number.isFinite(l.posY)) continue;

    const dx = l.posX - srcX;
    const dy = l.posY - srcY;
    const dist = Math.hypot(dx, dy);
    if (dist < 50 || dist > profile.rangeM * 1.35) continue;

    const nx = dx / dist;
    const ny = dy / dist;
    const dot = nx * fx + ny * fy;
    if (dot < maxTrackCos) continue;

    const anglePenalty = 1 - dot;
    const rangePenalty = dist / Math.max(profile.rangeM, 1);
    const score = anglePenalty * 0.75 + rangePenalty * 0.25;
    if (!best || score < best.score) best = { unitId: l.unitId, score };
  }

  return best?.unitId ?? null;
}

function findNearestLazeUnitIdByTargetPoint(
  targetX: number,
  targetY: number,
  lazes: ActiveLaze[],
  maxDist = 1200,
): string | null {
  let best: { unitId: string; dist: number } | null = null;
  for (const l of lazes) {
    if (!Number.isFinite(l.posX) || !Number.isFinite(l.posY)) continue;
    const dist = Math.hypot(l.posX - targetX, l.posY - targetY);
    if (dist > maxDist) continue;
    if (!best || dist < best.dist) {
      best = { unitId: l.unitId, dist };
    }
  }
  return best?.unitId ?? null;
}

function resolveGuidedLockTarget(
  lock: GuidedLock,
  units: Record<string, Unit>,
  lazes: ActiveLaze[],
  worldSize: number,
): TargetEstimate | null {
  if (lock.kind === 'unit') {
    const unit = units[lock.unitId];
    if (!unit) return null;
    if (!Number.isFinite(unit.posX) || !Number.isFinite(unit.posY)) return null;
    if (unit.posX === 0 && unit.posY === 0) return null;
    return {
      x: clamp(unit.posX, 0, worldSize),
      y: clamp(unit.posY, 0, worldSize),
      source: 'unit',
    };
  }

  const laze = lazes.find(l => l.unitId === lock.unitId);
  if (!laze) return null;
  if (!Number.isFinite(laze.posX) || !Number.isFinite(laze.posY)) return null;
  return {
    x: clamp(laze.posX, 0, worldSize),
    y: clamp(laze.posY, 0, worldSize),
    source: 'laze',
  };
}

function acquireGuidedLock(
  ev: FiredEvent,
  launchX: number,
  launchY: number,
  launchDirDeg: number,
  shooterSide: string,
  units: Record<string, Unit>,
  lazes: ActiveLaze[],
  profile: ProjectileProfile,
): GuidedLock | null {
  const source = (ev.targetSource ?? '').toLowerCase();
  const preferLaze = profile.preferLiveLaze || source === 'laser' || source === 'scan';

  // If telemetry says this was laser-based, keep lock on active laze first.
  if (preferLaze) {
    if (typeof ev.targetX === 'number' && Number.isFinite(ev.targetX) && typeof ev.targetY === 'number' && Number.isFinite(ev.targetY)) {
      const byTargetPoint = findNearestLazeUnitIdByTargetPoint(ev.targetX, ev.targetY, lazes);
      if (byTargetPoint) {
        return { kind: 'laze', unitId: byTargetPoint };
      }
    }

    const lazeUnitId = findNearestLazeUnitId(launchX, launchY, launchDirDeg, ev.unitId, lazes, profile);
    if (lazeUnitId) {
      return { kind: 'laze', unitId: lazeUnitId };
    }
  }

  const eventEntityId = ev.targetEntityId?.trim();
  if (eventEntityId) {
    const byId = units[eventEntityId];
    if (byId && Number.isFinite(byId.posX) && Number.isFinite(byId.posY)) {
      return { kind: 'unit', unitId: byId.id };
    }
  }

  if (typeof ev.targetX === 'number' && Number.isFinite(ev.targetX) && typeof ev.targetY === 'number' && Number.isFinite(ev.targetY)) {
    let nearest: { id: string; dist: number } | null = null;
    for (const u of Object.values(units)) {
      if (u.side === shooterSide) continue;
      if (!Number.isFinite(u.posX) || !Number.isFinite(u.posY)) continue;
      const dist = Math.hypot(u.posX - ev.targetX, u.posY - ev.targetY);
      if (dist > 1000) continue;
      if (!nearest || dist < nearest.dist) nearest = { id: u.id, dist };
    }
    if (nearest) {
      return { kind: 'unit', unitId: nearest.id };
    }
  }

  const inferredUnitId = findNearestEnemyUnitId(launchX, launchY, launchDirDeg, shooterSide, units, profile);
  if (inferredUnitId) {
    return { kind: 'unit', unitId: inferredUnitId };
  }

  const lazeUnitId = findNearestLazeUnitId(launchX, launchY, launchDirDeg, ev.unitId, lazes, profile);
  if (lazeUnitId) {
    return { kind: 'laze', unitId: lazeUnitId };
  }

  return null;
}

function targetFromLazes(
  srcX: number,
  srcY: number,
  dirDeg: number,
  shooterUnitId: string,
  lazes: ActiveLaze[],
  worldSize: number,
  profile: ProjectileProfile,
): TargetEstimate | null {
  if (lazes.length === 0) return null;

  const ownLaze = lazes.find(l => l.unitId === shooterUnitId);
  if (ownLaze && Number.isFinite(ownLaze.posX) && Number.isFinite(ownLaze.posY)) {
    return {
      x: clamp(ownLaze.posX, 0, worldSize),
      y: clamp(ownLaze.posY, 0, worldSize),
      source: 'laze',
    };
  }

  const rad = (dirDeg * Math.PI) / 180;
  const fx = Math.sin(rad);
  const fy = Math.cos(rad);
  const maxTrackCos = Math.cos((Math.min(85, profile.maxTrackAngleDeg + 15) * Math.PI) / 180);

  let best: { x: number; y: number; score: number } | null = null;
  for (const l of lazes) {
    if (!Number.isFinite(l.posX) || !Number.isFinite(l.posY)) continue;
    const dx = l.posX - srcX;
    const dy = l.posY - srcY;
    const dist = Math.hypot(dx, dy);
    if (dist < 50 || dist > profile.rangeM * 1.35) continue;
    const nx = dx / dist;
    const ny = dy / dist;
    const dot = nx * fx + ny * fy;
    if (dot < maxTrackCos) continue;

    const anglePenalty = 1 - dot;
    const rangePenalty = dist / Math.max(profile.rangeM, 1);
    const score = anglePenalty * 0.75 + rangePenalty * 0.25;
    if (!best || score < best.score) best = { x: l.posX, y: l.posY, score };
  }

  if (!best) return null;
  return {
    x: clamp(best.x, 0, worldSize),
    y: clamp(best.y, 0, worldSize),
    source: 'laze',
  };
}

function lazeLabel(unit: Unit | undefined): string {
  if (!unit) return 'Laser';
  if (unit.playerName?.trim()) return unit.playerName.trim();
  return unit.name?.trim() || unit.type || 'Laser';
}

function estimateTarget(
  srcX: number,
  srcY: number,
  dirDeg: number,
  shooterSide: string,
  grp: Group | undefined,
  allUnits: Record<string, Unit>,
  worldSize: number,
  profile: ProjectileProfile,
): TargetEstimate {
  if (grp && (grp.wpX !== 0 || grp.wpY !== 0)) {
    return { x: grp.wpX, y: grp.wpY, source: 'waypoint' };
  }

  const rad = (dirDeg * Math.PI) / 180;
  const fx = Math.sin(rad);
  const fy = Math.cos(rad);
  const maxTrackCos = Math.cos((profile.maxTrackAngleDeg * Math.PI) / 180);

  if (profile.allowEnemyTargetInference) {
    let best: { x: number; y: number; score: number } | null = null;
    for (const u of Object.values(allUnits)) {
      if (u.side === shooterSide) continue;
      if (!Number.isFinite(u.posX) || !Number.isFinite(u.posY)) continue;
      if (u.posX === 0 && u.posY === 0) continue;
      const dx = u.posX - srcX;
      const dy = u.posY - srcY;
      const dist = Math.hypot(dx, dy);
      if (dist < 50 || dist > profile.rangeM * 1.25) continue;
      const nx = dx / dist;
      const ny = dy / dist;
      const dot = nx * fx + ny * fy;
      if (dot < maxTrackCos) continue;
      const anglePenalty = 1 - dot;
      const rangePenalty = dist / Math.max(profile.rangeM, 1);
      const score = anglePenalty * 0.7 + rangePenalty * 0.3;
      if (!best || score < best.score) best = { x: u.posX, y: u.posY, score };
    }

    if (best) return { x: best.x, y: best.y, source: 'enemy' };
  }

  const dx = Math.sin(rad) * profile.rangeM;
  const dy = Math.cos(rad) * profile.rangeM;
  return {
    x: clamp(srcX + dx, 0, worldSize),
    y: clamp(srcY + dy, 0, worldSize),
    source: 'heading',
  };
}

function buildPredictedPath(
  srcX: number,
  srcY: number,
  dstX: number,
  dstY: number,
  lateralFactor: number,
  progress: number,
  samples = 42,
): [number, number][] {
  const dx = dstX - srcX;
  const dy = dstY - srcY;
  const dist = Math.hypot(dx, dy);
  const nx = dist > 0.001 ? dx / dist : 1;
  const ny = dist > 0.001 ? dy / dist : 0;
  const px = -ny;
  const py = nx;
  const offset = dist * lateralFactor;

  const p0x = srcX;
  const p0y = srcY;
  const p1x = srcX + dx * 0.5 + px * offset;
  const p1y = srcY + dy * 0.5 + py * offset;
  const p2x = dstX;
  const p2y = dstY;

  const tMax = clamp(progress, 0, 1);
  const points: [number, number][] = [];
  const count = Math.max(2, Math.floor(samples * tMax));
  for (let i = 0; i <= count; i++) {
    const t = (i / count) * tMax;
    const inv = 1 - t;
    const x = inv * inv * p0x + 2 * inv * t * p1x + t * t * p2x;
    const y = inv * inv * p0y + 2 * inv * t * p1y + t * t * p2y;
    points.push([x, y]);
  }
  return points;
}

function firingPulseIcon(size = 28): L.DivIcon {
  const half = size / 2;
  return L.divIcon({
    className: '',
    iconSize: [size, size],
    iconAnchor: [half, half],
    html:
      `<div style="position:relative;width:${size}px;height:${size}px;pointer-events:none;">`
      + `<div style="position:absolute;left:0;top:0;width:${size}px;height:${size}px;border-radius:50%;border:2px solid rgba(255,58,58,0.95);box-shadow:0 0 10px rgba(255,58,58,0.85);animation:athena-fire-blink 0.45s steps(2,end) infinite;"></div>`
      + `</div>`,
  });
}

// Use actual NATO marker PNGs from Athena Desktop, color-tinted via CSS mask.
function groupIcon(side: string, _unitCount: number, groupType: string): L.DivIcon {
  const color = sideColor(side);
  const prefix = natoSidePrefix(side);
  const size = 48;
  const natoFile = `/icons/nato/${prefix}_${groupType}.png`;
  const html = `<div style="width:${size}px;height:${size}px;` +
    `background-color:${color};` +
    `-webkit-mask-image:url(${natoFile});` +
    `mask-image:url(${natoFile});` +
    `-webkit-mask-size:contain;mask-size:contain;` +
    `-webkit-mask-repeat:no-repeat;mask-repeat:no-repeat;` +
    `-webkit-mask-position:center;mask-position:center;` +
    `filter:drop-shadow(1px 0 0 #000) drop-shadow(-1px 0 0 #000) drop-shadow(0 1px 0 #000) drop-shadow(0 -1px 0 #000);` +
    `"></div>`;
  return L.divIcon({ className: '', iconSize: [size, size], iconAnchor: [size / 2, size / 2], html });
}

function groupLabel(group: Group): string {
  return (group.name || 'GROUP').trim();
}

function resolveVehicleCategory(vehicleClass: string, vehicleMap: Map<string, string>): string {
  const text = vehicleClass.toLowerCase();

  // Parachutes must never inherit helicopter mapping from the vehicle library.
  if (text.includes('parachute') || text.includes('chute')) return 'Parachutes';

  const mapped = vehicleMap.get(vehicleClass);
  if (mapped) return mapped;
  if (text.includes('plane') || text.includes('jet')) return 'Planes';
  if (text.includes('heli')) return 'Helicopters';
  if (text.includes('uav') || text.includes('ugv') || text.includes('drone')) return 'Drones';
  if (text.includes('sub') || text.includes('sdv')) return 'Submersibles';
  if (text.includes('boat') || text.includes('ship')) return 'Boats';
  if (text.includes('tank') || text.includes('mbt')) return 'Tanks';
  if (text.includes('apc') || text.includes('tracked')) return 'APCs';
  if (text.includes('aa')) return 'AAs';
  if (text.includes('mortar') || text.includes('art') || text.includes('mlrs')) return 'Artillery';
  if (text.includes('turret')) return 'Turrets';
  if (text.includes('car') || text.includes('mrap') || text.includes('truck') || text.includes('quadbike')) return 'Cars';
  if (/mine|slam|claymore|explosive|ied|satchel|demo_charge/.test(text)) return 'Mines';
  return '';
}

//  Vehicle subtype resolution 

function resolveDroneSubtype(vehicleClass: string): string {
  const t = vehicleClass.toLowerCase();
  if (t.includes('uav_05'))                         return 'sentinel';    // flying-wing UCAV
  if (t.includes('uav_02') || t.includes('uav_04')) return 'fixedwing';   // Greyhawk / YABHON-R3
  if (t.includes('ugv_01'))                         return 'ugv_tracked'; // Stomper
  if (t.includes('ugv_02') || t.includes('uav_06')) return 'ugv_wheeled'; // ED-1D / Pelican
  return 'quadcopter';
}

function resolveHeliSubtype(vehicleClass: string): string {
  const t = vehicleClass.toLowerCase();
  if (t.includes('heli_attack'))                    return 'attack';      // AH-99 Blackfoot / Mi-48
  if (t.includes('heli_transport_03'))              return 'heavy';       // CH-67 Huron (Chinook)
  if (t.includes('vtol'))                           return 'tiltrotor';   // V-44 Blackfish
  if (t.includes('heli_light'))                     return 'light';       // MH-9 / Orca
  return 'transport';                                                     // Ghost Hawk / Mohawk
}

function resolveAPCSubtype(vehicleClass: string): string {
  const t = vehicleClass.toLowerCase();
  if (t.includes('wheeled'))                        return 'wheeled';     // Marshall / Marid / Gorgon
  return 'tracked';                                                       // Panther / Mora / BMP
}

function resolveBoatSubtype(vehicleClass: string): string {
  const t = vehicleClass.toLowerCase();
  if (t.includes('ship'))                           return 'ship';        // destroyer
  if (t.includes('boat_armed'))                     return 'armed';       // speedboat HMG/minigun
  if (t.includes('boat_civil'))                     return 'civilian';    // civilian speedboat
  return 'transport';                                                     // assault boat / RHIB
}

function resolveTurretSubtype(vehicleClass: string): string {
  const t = vehicleClass.toLowerCase();
  if (t.includes('sam_system'))                     return 'sam';         // SAM launcher
  if (t.includes('aaa_system'))                     return 'aaa';        // Praetorian AA
  if (t.includes('radar'))                          return 'radar';       // AN/MPQ / R-750
  if (t.includes('ship_gun') || t.includes('ship_mrls')) return 'naval'; // ship-mounted weapons
  if (t.includes('mortar'))                         return 'mortar';      // Mk6 mortar
  if (t.includes('gmg'))                            return 'gmg';        // grenade MG
  return 'hmg';                                                           // default HMG / static
}

//  Vehicle icons 


function categoryToIconFile(category: string, vehicleClass?: string): string {
  const cl = (vehicleClass ?? '').toLowerCase();
  switch (category) {
    case 'Parachutes':    return 'iconparachute';
    case 'Cars':          return cl.includes('truck') ? 'icontruck' : cl.includes('motorcycle') ? 'iconmotorcycle' : 'iconcar';
    case 'APCs':          return 'iconapc';
    case 'Tanks':         return 'icontank';
    case 'Helicopters':   return 'iconhelicopter';
    case 'Planes':        return 'iconplane';
    case 'Boats':         return 'iconship';
    case 'Artillery':     return 'iconstaticcannon';
    case 'AAs':           return 'iconapc';  // Desktop: AAs inherit tank/APC, show as APC
    case 'Submersibles':  return 'iconship';
    case 'Drones':        return 'iconplane'; // Desktop: drones use plane icon
    case 'Turrets':       return 'iconstaticmg';
    default:              return 'iconvehicle';
  }
}

function vehicleIconHtml(category: string, color: string, dir: number, size: number, vehicleClass?: string): string {
  const icon = categoryToIconFile(category, vehicleClass);
  return `<div style="width:${size}px;height:${size}px;` +
    `background-color:${color};` +
    `-webkit-mask-image:url(/icons/vehicles/${icon}.png);` +
    `mask-image:url(/icons/vehicles/${icon}.png);` +
    `-webkit-mask-size:contain;mask-size:contain;` +
    `-webkit-mask-repeat:no-repeat;mask-repeat:no-repeat;` +
    `-webkit-mask-position:center;mask-position:center;` +
    `transform:rotate(${dir}deg);transform-origin:center;` +
    `filter:drop-shadow(1px 0 0 rgba(0,0,0,0.6)) drop-shadow(-1px 0 0 rgba(0,0,0,0.6)) drop-shadow(0 1px 0 rgba(0,0,0,0.6)) drop-shadow(0 -1px 0 rgba(0,0,0,0.6));` +
    `"></div>`;
}

function vehicleSvg(category: string, color: string, dir: number, vehicleClass?: string): string {
  const s = '#000';
  const sw = '1.2';
  const wrap = (body: string) =>
    `<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 40 40"` +
    ` style="display:block;transform:rotate(${dir}deg);transform-origin:center;">${body}</svg>`;
  switch (category) {
    case 'Cars':
      // Realistic top-view car: body with rounded hood and rear, wheel arches
      return wrap(
        `<rect x="12" y="6" width="16" height="28" rx="5" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
        `<rect x="10" y="10" width="3" height="5" rx="1" fill="#222"/>` +  // left front wheel
        `<rect x="27" y="10" width="3" height="5" rx="1" fill="#222"/>` +  // right front wheel
        `<rect x="10" y="25" width="3" height="5" rx="1" fill="#222"/>` +  // left rear wheel
        `<rect x="27" y="25" width="3" height="5" rx="1" fill="#222"/>` +  // right rear wheel
        `<rect x="15" y="11" width="10" height="6" rx="1.5" fill="rgba(180,220,255,0.5)" stroke="${s}" stroke-width="0.5"/>`  // windshield
      );
    case 'APCs': {
      const sub = resolveAPCSubtype(vehicleClass ?? '');
      if (sub === 'wheeled') {
        // Wheeled APC (Marshall / Marid / Gorgon): boxy hull, 4 wheels, turret
        return wrap(
          `<rect x="10" y="4" width="20" height="32" rx="3" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
          `<circle cx="10" cy="10" r="3.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +   // left front wheel
          `<circle cx="30" cy="10" r="3.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +   // right front wheel
          `<circle cx="10" cy="20" r="3.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +   // left mid wheel
          `<circle cx="30" cy="20" r="3.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +   // right mid wheel
          `<circle cx="10" cy="30" r="3.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +   // left rear wheel
          `<circle cx="30" cy="30" r="3.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +   // right rear wheel
          `<circle cx="20" cy="16" r="5" fill="${color}" stroke="${s}" stroke-width="1.2"/>` +
          `<line x1="20" y1="11" x2="20" y2="2" stroke="${s}" stroke-width="2.5"/>`
        );
      }
      // Tracked APC (Panther / Mora): boxy hull, tracks, small turret
      return wrap(
        `<rect x="9" y="4" width="22" height="32" rx="3" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
        `<rect x="7" y="7" width="4" height="6" rx="1" fill="#333"/>` +
        `<rect x="29" y="7" width="4" height="6" rx="1" fill="#333"/>` +
        `<rect x="7" y="27" width="4" height="6" rx="1" fill="#333"/>` +
        `<rect x="29" y="27" width="4" height="6" rx="1" fill="#333"/>` +
        `<circle cx="20" cy="16" r="5" fill="${color}" stroke="${s}" stroke-width="1.2"/>` +
        `<line x1="20" y1="11" x2="20" y2="2" stroke="${s}" stroke-width="2.5"/>`
      );
    }
    case 'Tanks':
      // Main battle tank: wide hull with tracks, large turret, long barrel
      return wrap(
        `<rect x="6" y="5" width="28" height="30" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
        `<rect x="4" y="6" width="5" height="28" rx="1.5" fill="#333" stroke="${s}" stroke-width="0.6"/>` +  // left track
        `<rect x="31" y="6" width="5" height="28" rx="1.5" fill="#333" stroke="${s}" stroke-width="0.6"/>` + // right track
        `<circle cx="20" cy="20" r="7" fill="${color}" stroke="${s}" stroke-width="1.4"/>` +
        `<line x1="20" y1="13" x2="20" y2="1" stroke="${s}" stroke-width="3"/>`
      );
    case 'Helicopters': {
      const sub = resolveHeliSubtype(vehicleClass ?? '');
      switch (sub) {
        case 'attack':
          // Attack helicopter: narrow fuselage, stub wings, aggressive profile
          return wrap(
            `<ellipse cx="20" cy="20" rx="4" ry="11" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="16" y1="18" x2="8" y2="16" stroke="${color}" stroke-width="3"/>` +   // left stub wing
            `<line x1="24" y1="18" x2="32" y2="16" stroke="${color}" stroke-width="3"/>` +   // right stub wing
            `<line x1="16" y1="18" x2="8" y2="16" stroke="${s}" stroke-width="0.8"/>` +
            `<line x1="24" y1="18" x2="32" y2="16" stroke="${s}" stroke-width="0.8"/>` +
            `<line x1="20" y1="31" x2="20" y2="38" stroke="${s}" stroke-width="1.5"/>` +     // tail boom
            `<line x1="16" y1="37" x2="24" y2="37" stroke="${s}" stroke-width="1.2"/>` +     // tail rotor
            `<circle cx="20" cy="20" r="14" fill="none" stroke="${s}" stroke-width="0.6" stroke-dasharray="3 3"/>` +
            `<line x1="6" y1="20" x2="34" y2="20" stroke="${s}" stroke-width="0.8"/>`
          );
        case 'heavy':
          // Heavy transport (Chinook style): wide body, tandem rotors
          return wrap(
            `<rect x="14" y="6" width="12" height="28" rx="6" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<circle cx="20" cy="10" r="10" fill="none" stroke="${s}" stroke-width="0.6" stroke-dasharray="3 3"/>` +
            `<line x1="10" y1="10" x2="30" y2="10" stroke="${s}" stroke-width="0.8"/>` +
            `<circle cx="20" cy="30" r="10" fill="none" stroke="${s}" stroke-width="0.6" stroke-dasharray="3 3"/>` +
            `<line x1="10" y1="30" x2="30" y2="30" stroke="${s}" stroke-width="0.8"/>`
          );
        case 'tiltrotor':
          // Tiltrotor (V-44 Blackfish): wide fuselage, two engine nacelles at wingtips
          return wrap(
            `<ellipse cx="20" cy="20" rx="5" ry="12" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="8" y1="18" x2="32" y2="18" stroke="${color}" stroke-width="3.5"/>` +  // wing
            `<line x1="8" y1="18" x2="32" y2="18" stroke="${s}" stroke-width="0.8"/>` +
            `<circle cx="8" cy="18" r="5" fill="none" stroke="${s}" stroke-width="0.6" stroke-dasharray="2 2"/>` +  // left rotor
            `<circle cx="32" cy="18" r="5" fill="none" stroke="${s}" stroke-width="0.6" stroke-dasharray="2 2"/>` + // right rotor
            `<polygon points="20,30 27,36 13,36" fill="${color}" stroke="${s}" stroke-width="0.8"/>`  // tail
          );
        case 'light':
          // Light helicopter: small open-frame body, skids
          return wrap(
            `<ellipse cx="20" cy="19" rx="5" ry="7" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="20" y1="26" x2="20" y2="36" stroke="${s}" stroke-width="1"/>` +       // tail boom
            `<line x1="17" y1="35" x2="23" y2="35" stroke="${s}" stroke-width="0.8"/>` +     // tail rotor
            `<line x1="12" y1="24" x2="12" y2="28" stroke="${s}" stroke-width="0.8"/>` +     // left skid strut
            `<line x1="28" y1="24" x2="28" y2="28" stroke="${s}" stroke-width="0.8"/>` +     // right skid strut
            `<line x1="10" y1="28" x2="14" y2="28" stroke="${s}" stroke-width="1"/>` +       // left skid
            `<line x1="26" y1="28" x2="30" y2="28" stroke="${s}" stroke-width="1"/>` +       // right skid
            `<circle cx="20" cy="19" r="13" fill="none" stroke="${s}" stroke-width="0.6" stroke-dasharray="3 3"/>` +
            `<line x1="7" y1="19" x2="33" y2="19" stroke="${s}" stroke-width="0.8"/>`
          );
        default:
          // Standard transport helicopter
          return wrap(
            `<ellipse cx="20" cy="20" rx="6" ry="10" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="20" y1="30" x2="20" y2="38" stroke="${s}" stroke-width="1.5"/>` +
            `<line x1="16" y1="37" x2="24" y2="37" stroke="${s}" stroke-width="1.2"/>` +
            `<circle cx="20" cy="20" r="14" fill="none" stroke="${s}" stroke-width="0.6" stroke-dasharray="3 3"/>` +
            `<line x1="6" y1="20" x2="34" y2="20" stroke="${s}" stroke-width="0.8"/>`
          );
      }
    }
    case 'Planes':
      // Fixed-wing aircraft: fuselage, swept wings, tail
      return wrap(
        `<ellipse cx="20" cy="18" rx="3.5" ry="14" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
        `<polygon points="20,12 38,22 20,19 2,22" fill="${color}" stroke="${s}" stroke-width="0.8" stroke-linejoin="round"/>` +  // main wings
        `<polygon points="20,30 27,34 20,32 13,34" fill="${color}" stroke="${s}" stroke-width="0.8" stroke-linejoin="round"/>`   // tail wings
      );
    case 'Boats': {
      const sub = resolveBoatSubtype(vehicleClass ?? '');
      switch (sub) {
        case 'ship':
          // Large warship / destroyer: long hull with superstructure
          return wrap(
            `<path d="M20,2 L30,10 L30,32 Q30,38 20,38 Q10,38 10,32 L10,10 Z" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<rect x="15" y="12" width="10" height="8" rx="1" fill="${color}" stroke="${s}" stroke-width="0.8"/>` +  // bridge
            `<line x1="20" y1="12" x2="20" y2="6" stroke="${s}" stroke-width="1.5"/>` +  // mast
            `<line x1="17" y1="8" x2="23" y2="8" stroke="${s}" stroke-width="0.8"/>`     // yardarm
          );
        case 'armed':
          // Armed speedboat: pointed bow, gun mount
          return wrap(
            `<path d="M20,3 L30,14 L30,32 Q30,37 20,37 Q10,37 10,32 L10,14 Z" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<circle cx="20" cy="12" r="3" fill="${color}" stroke="${s}" stroke-width="0.8"/>` +  // gun turret
            `<line x1="20" y1="9" x2="20" y2="4" stroke="${s}" stroke-width="2"/>`               // gun barrel
          );
        case 'civilian':
          // Civilian boat: simple hull, no weapons
          return wrap(
            `<path d="M20,5 L28,14 L28,32 Q28,36 20,36 Q12,36 12,32 L12,14 Z" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="20" y1="10" x2="20" y2="22" stroke="rgba(255,255,255,0.3)" stroke-width="0.8"/>`
          );
        default:
          // Transport / assault boat
          return wrap(
            `<path d="M20,3 L30,14 L30,32 Q30,37 20,37 Q10,37 10,32 L10,14 Z" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="20" y1="8" x2="20" y2="20" stroke="rgba(255,255,255,0.4)" stroke-width="1"/>`
          );
      }
    }
    case 'Artillery':
      // Self-propelled artillery: hull, long barrel
      return wrap(
        `<rect x="9" y="12" width="22" height="24" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
        `<rect x="7" y="14" width="4" height="20" rx="1.5" fill="#333"/>` +
        `<rect x="29" y="14" width="4" height="20" rx="1.5" fill="#333"/>` +
        `<circle cx="20" cy="22" r="5" fill="${color}" stroke="${s}" stroke-width="1"/>` +
        `<line x1="20" y1="17" x2="20" y2="2" stroke="${s}" stroke-width="3"/>`
      );
    case 'AAs':
      // Anti-air: hull with twin barrels diverging
      return wrap(
        `<rect x="10" y="14" width="20" height="22" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
        `<rect x="8" y="16" width="4" height="18" rx="1.5" fill="#333"/>` +
        `<rect x="28" y="16" width="4" height="18" rx="1.5" fill="#333"/>` +
        `<circle cx="20" cy="22" r="5" fill="${color}" stroke="${s}" stroke-width="1"/>` +
        `<line x1="17" y1="17" x2="14" y2="3" stroke="${s}" stroke-width="2.2"/>` +
        `<line x1="23" y1="17" x2="26" y2="3" stroke="${s}" stroke-width="2.2"/>`
      );
    case 'Drones': {
      const sub = resolveDroneSubtype(vehicleClass ?? '');
      switch (sub) {
        case 'sentinel':
          // Flying-wing UCAV (like RQ-170 / X-47B): swept delta flying wing, no tail
          return wrap(
            `<polygon points="20,4 38,30 32,28 20,34 8,28 2,30" fill="${color}" stroke="${s}" stroke-width="${sw}" stroke-linejoin="round"/>` +
            `<line x1="20" y1="12" x2="20" y2="26" stroke="${s}" stroke-width="1.2"/>` +
            `<circle cx="20" cy="18" r="2.5" fill="none" stroke="${s}" stroke-width="0.8"/>`
          );
        case 'fixedwing':
          // Conventional fixed-wing drone (Greyhawk/Reaper/YABHON): fuselage, straight wings, V-tail
          return wrap(
            `<ellipse cx="20" cy="18" rx="2.8" ry="12" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="4" y1="20" x2="36" y2="20" stroke="${color}" stroke-width="3"/>` +
            `<line x1="4" y1="20" x2="36" y2="20" stroke="${s}" stroke-width="0.8"/>` +
            `<line x1="20" y1="30" x2="14" y2="36" stroke="${color}" stroke-width="2.5"/>` +
            `<line x1="20" y1="30" x2="26" y2="36" stroke="${color}" stroke-width="2.5"/>` +
            `<line x1="20" y1="30" x2="14" y2="36" stroke="${s}" stroke-width="0.7"/>` +
            `<line x1="20" y1="30" x2="26" y2="36" stroke="${s}" stroke-width="0.7"/>`
          );
        case 'ugv_tracked':
          // Tracked UGV (Stomper): small tracked vehicle with sensor mast
          return wrap(
            `<rect x="10" y="8" width="20" height="24" rx="3" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<rect x="7" y="10" width="4" height="20" rx="1.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +
            `<rect x="29" y="10" width="4" height="20" rx="1.5" fill="#333" stroke="${s}" stroke-width="0.5"/>` +
            `<circle cx="20" cy="16" r="3" fill="none" stroke="${s}" stroke-width="1.2"/>` +
            `<line x1="20" y1="13" x2="20" y2="8" stroke="${s}" stroke-width="1.5"/>`
          );
        case 'ugv_wheeled':
          // Small wheeled UGV (ED-1D / Pelican): compact body, 4 wheels
          return wrap(
            `<rect x="13" y="10" width="14" height="20" rx="4" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<circle cx="12" cy="14" r="3" fill="#333" stroke="${s}" stroke-width="0.5"/>` +
            `<circle cx="28" cy="14" r="3" fill="#333" stroke="${s}" stroke-width="0.5"/>` +
            `<circle cx="12" cy="26" r="3" fill="#333" stroke="${s}" stroke-width="0.5"/>` +
            `<circle cx="28" cy="26" r="3" fill="#333" stroke="${s}" stroke-width="0.5"/>` +
            `<circle cx="20" cy="17" r="2" fill="none" stroke="${s}" stroke-width="0.8"/>`
          );
        default:
          // Quadcopter (Darter / Falcon / unknown): X-frame body, 4 rotors
          return wrap(
            `<line x1="12" y1="12" x2="28" y2="28" stroke="${s}" stroke-width="2"/>` +
            `<line x1="28" y1="12" x2="12" y2="28" stroke="${s}" stroke-width="2"/>` +
            `<circle cx="20" cy="20" r="4" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<circle cx="12" cy="12" r="4" fill="none" stroke="${s}" stroke-width="0.8"/>` +
            `<circle cx="28" cy="12" r="4" fill="none" stroke="${s}" stroke-width="0.8"/>` +
            `<circle cx="12" cy="28" r="4" fill="none" stroke="${s}" stroke-width="0.8"/>` +
            `<circle cx="28" cy="28" r="4" fill="none" stroke="${s}" stroke-width="0.8"/>`
          );
      }
    }
    case 'Turrets': {
      const sub = resolveTurretSubtype(vehicleClass ?? '');
      switch (sub) {
        case 'sam':
          // SAM launcher: base platform with angled missile tubes
          return wrap(
            `<rect x="12" y="18" width="16" height="14" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<rect x="14" y="10" width="5" height="12" rx="1" fill="${color}" stroke="${s}" stroke-width="0.8"/>` + // left launcher
            `<rect x="21" y="10" width="5" height="12" rx="1" fill="${color}" stroke="${s}" stroke-width="0.8"/>` + // right launcher
            `<line x1="16" y1="10" x2="16" y2="4" stroke="${s}" stroke-width="1.5"/>` +  // left missile
            `<line x1="24" y1="10" x2="24" y2="4" stroke="${s}" stroke-width="1.5"/>` +  // right missile
            `<polygon points="16,4 14,6 18,6" fill="${s}"/>` +                            // left warhead
            `<polygon points="24,4 22,6 26,6" fill="${s}"/>`                              // right warhead
          );
        case 'aaa':
          // Anti-air autocannon: rotating platform with rapid-fire barrels
          return wrap(
            `<rect x="12" y="18" width="16" height="14" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<circle cx="20" cy="22" r="5" fill="${color}" stroke="${s}" stroke-width="1"/>` +
            `<line x1="16" y1="18" x2="13" y2="4" stroke="${s}" stroke-width="1.8"/>` +
            `<line x1="20" y1="18" x2="20" y2="3" stroke="${s}" stroke-width="1.8"/>` +
            `<line x1="24" y1="18" x2="27" y2="4" stroke="${s}" stroke-width="1.8"/>`
          );
        case 'radar':
          // Radar station: base with dish antenna
          return wrap(
            `<rect x="14" y="24" width="12" height="10" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="20" y1="24" x2="20" y2="14" stroke="${s}" stroke-width="2"/>` +   // mast
            `<path d="M10,16 Q20,6 30,16" fill="none" stroke="${s}" stroke-width="2.5"/>` + // dish
            `<circle cx="20" cy="16" r="2" fill="${s}"/>`                                  // feed horn
          );
        case 'naval':
          // Ship-mounted weapon (Mk45 / VLS): deck mount with large barrel or launcher
          return wrap(
            `<rect x="10" y="18" width="20" height="16" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<rect x="14" y="12" width="12" height="10" rx="2" fill="${color}" stroke="${s}" stroke-width="0.8"/>` +
            `<line x1="20" y1="12" x2="20" y2="3" stroke="${s}" stroke-width="3.5"/>`
          );
        case 'mortar':
          // Mortar: baseplate with tube
          return wrap(
            `<ellipse cx="20" cy="28" rx="10" ry="5" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<line x1="20" y1="28" x2="20" y2="8" stroke="${s}" stroke-width="3"/>` +
            `<circle cx="20" cy="8" r="2" fill="${s}"/>`
          );
        default:
          // HMG / GMG static weapon: tripod base + barrel
          return wrap(
            `<rect x="12" y="16" width="16" height="16" rx="2" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
            `<circle cx="20" cy="24" r="5" fill="${color}" stroke="${s}" stroke-width="1.2"/>` +
            `<line x1="20" y1="19" x2="20" y2="6" stroke="${s}" stroke-width="3"/>`
          );
      }
    }
    case 'Submersibles':
      // Submarine: elongated hull, conning tower
      return wrap(
        `<ellipse cx="20" cy="20" rx="7" ry="16" fill="${color}" stroke="${s}" stroke-width="${sw}"/>` +
        `<rect x="16" y="16" width="8" height="6" rx="1.5" fill="${color}" stroke="${s}" stroke-width="0.8"/>` +
        `<line x1="20" y1="16" x2="20" y2="12" stroke="${s}" stroke-width="1.2"/>`
      );
    case 'Mines':
      // Red caution triangle   warning icon (not rotated by dir)
      return `<svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 40 40" style="display:block;">` +
        `<polygon points="20,4 37,34 3,34" fill="#cc2222" stroke="#880000" stroke-width="2" stroke-linejoin="round"/>` +
        `<text x="20" y="29" text-anchor="middle" fill="#fff" font-weight="bold" font-size="20" font-family="Arial,sans-serif">!</text>` +
        `</svg>`;
    default:
      return wrap(`<rect x="10" y="10" width="20" height="20" rx="3" fill="${color}" stroke="${s}" stroke-width="${sw}"/>`);
  }
}

function mineLabel(vehicleClass: string): string {
  const t = vehicleClass.toLowerCase();
  if (t.includes('apers') && t.includes('trip'))     return 'Trip Mine';
  if (t.includes('apers') && t.includes('bound'))    return 'Bounding Mine';
  if (t.includes('apers'))                           return 'APERS Mine';
  if (t.includes('atmine') || (t.includes('at') && t.includes('mine'))) return 'AT Mine';
  if (t.includes('claymore'))                        return 'Claymore';
  if (t.includes('slam'))                            return 'SLAM';
  if (t.includes('satchel'))                         return 'Satchel';
  if (t.includes('demo'))                            return 'Demo Charge';
  if (t.includes('ied'))                             return 'IED';
  return 'Mine';
}

function vehicleIcon(vehicle: Vehicle, units: Record<string, Unit>, category: string): L.DivIcon {
  // Mine: red caution triangle with type label, no side color
  if (category === 'Mines') {
    const size = 40;
    const label = escapeHtml(mineLabel(vehicle.class));
    const svg = `<div class="map-marker-wrap"><div style="display:flex;flex-direction:column;align-items:center;">` +
      vehicleSvg('Mines', '', 0).replace('width="20" height="20"', `width="${size}" height="${size}"`) +
      `<div class="map-marker-label" style="color:#cc2222;font-weight:700;white-space:nowrap;">${label}</div>` +
      `</div></div>`;
    return L.divIcon({ className: '', iconSize: [size, size + 14], iconAnchor: [size / 2, size / 2], html: svg });
  }
  const firstCrew = vehicle.crew[0];
  let side = firstCrew ? (units[firstCrew.unitId]?.side ?? '') : '';
  if (!side) {
    const occupants = Object.values(units).filter(u => u.vehicleId === vehicle.id);
    const preferred = occupants.find(u => u.playerName?.trim()) ?? occupants[0];
    side = preferred?.side ?? 'unknown';
  }
  const color = sideColor(side);
  const size = 80;
  return L.divIcon({
    className:  '',
    iconSize:   [size, size],
    iconAnchor: [size / 2, size / 2],
    html: vehicleIconHtml(category, color, vehicle.dir, size, vehicle.class),
  });
}

//  Layer manager 

// Static contour styling (from pre-computed Athena Desktop cache)
// sea level -> blue; index (div 100m) -> darker/thicker brown; major (div 20m) -> medium brown
function contourStyle(z: number): { color: string; weight: number; opacity: number } {
  // Bus exact: MapHelper.cs PopulateHeights  uniform strokeThickness 0.75
  // Colors: MediumBlue (Z=0), DodgerBlue (Z<0), RosyBrown (Z>0)
  if (z === 0) return { color: '#0000CD', weight: 0.75, opacity: 1 };  // MediumBlue
  if (z < 0)   return { color: '#1E90FF', weight: 0.75, opacity: 1 };  // DodgerBlue
  return              { color: '#BC8F8F', weight: 0.75, opacity: 1 };  // RosyBrown
}

import type { LayerVisibility, RenderMode } from '../App';

// Elevation colour ramp helpers  Bus Athena Desktop palette.
// HeatMap 1 (ground forces): green #0A7002  red smooth transition
function topoColorHeatmap1(t: number): [number, number, number] {
  // Simple 2-stop ramp: green at low elevation  red at high elevation
  const r = Math.round(10 + t * (200 - 10));    // 10  200
  const g = Math.round(112 - t * (112 - 10));   // 112  10
  const b = Math.round(2 + t * (5 - 2));        // 2  5
  return [r, g, b];
}

interface LayerManagerProps {
  units:      Record<string, Unit>;
  vehicles:   Record<string, Vehicle>;
  groups:     Record<string, Group>;
  lazes:      ActiveLaze[];
  firedEvents: FiredEvent[];
  firedImpacts: FiredImpactEvent[];
  world:      string;
  worldSize:  number;
  roads:      Road[];
  forests:    ForestsData | null;
  locations:  MapLocation[];
  structures: MapStructure[];
  elevations: ElevationsData | null;
  contours:   ContourLine[];
  layers:     LayerVisibility;
  onLayersChange?: Dispatch<SetStateAction<LayerVisibility>>;
  renderMode: RenderMode;
  vehicleMap:  Map<string, string>;
  locationMap: Map<string, { DrawStyle: string; SizeText: number; Name: string }>;
  shorelineRefreshToken: number;
  onProjectileDebugChange?: (entries: ProjectileDebugEntry[]) => void;
}

type ProjectileDebugEntry = {
  id: string;
  trackKey: string;
  shooter: string;
  weapon: string;
  lock: 'LAZE' | 'UNIT' | 'FREE';
  source: string;
  etaSec: number;
  target: string;
  lockSwitched: boolean;
};

function LayerManager({ units, vehicles, groups, lazes, firedEvents, firedImpacts, world, worldSize, roads, forests: _forests, locations, structures, elevations, contours, layers, onLayersChange, renderMode, vehicleMap, locationMap, shorelineRefreshToken, onProjectileDebugChange }: LayerManagerProps) {
  const map = useMap();

  useEffect(() => {
    const styleId = 'athena-fire-blink-style';
    if (document.getElementById(styleId)) return;
    const style = document.createElement('style');
    style.id = styleId;
    style.textContent = `@keyframes athena-fire-blink { 0% { opacity: 1; transform: scale(1); } 100% { opacity: 0.1; transform: scale(1.2); } }`;
    document.head.appendChild(style);
  }, []);

  // Canvas renderers are pane-bound; keep separate ones so road and structure visibility are independent.
  const roadCanvasRef = useRef<L.Canvas | null>(null);
  const structureCanvasRef = useRef<L.Canvas | null>(null);

  // Static-layer caching: these layers render once per world and are never rebuilt.
  // When the world name changes all flags reset so they re-render for the new map.
  const staticCacheRef = useRef({ world: '', roads: false, locations: false, structures: false, objects: false, forests: false, trees: false, treesWorld: '', objectsWorld: '', roadsWorld: '' });
  const landCacheRef = useRef<{ world: string; ready: boolean; source?: string }>({ world: '', ready: false });
  const landRefreshTokenRef = useRef(shorelineRefreshToken);

  useEffect(() => {
    if (!world || world === staticCacheRef.current.world) return;
    staticCacheRef.current = { world, roads: false, locations: false, structures: false, objects: false, forests: false, trees: false, treesWorld: '', objectsWorld: '', roadsWorld: '' };
    hasStaticTreesRef.current = false;
    setHasRuntimeTrees(false);
    landCacheRef.current = { world, ready: false, source: undefined };
    landLayerRef.current.clearLayers();
  }, [world]);

  useEffect(() => {
    if (!world) return;
    if (shorelineRefreshToken === landRefreshTokenRef.current) return;
    landRefreshTokenRef.current = shorelineRefreshToken;
    landCacheRef.current = { world: '', ready: false, source: undefined };
    landLayerRef.current.clearLayers();
  }, [world, shorelineRefreshToken]);

  // Layer groups (created once, never recreated)
  const forestLayerRef     = useRef<L.LayerGroup>(L.layerGroup());
  const roadLayerRef       = useRef<L.LayerGroup>(L.layerGroup());
  const locationLayerRef   = useRef<L.LayerGroup>(L.layerGroup());
  const peakLayerRef       = useRef<L.LayerGroup>(L.layerGroup());
  const structureLayerRef  = useRef<L.LayerGroup>(L.layerGroup());
  const topoLayerRef       = useRef<L.LayerGroup>(L.layerGroup());
  const groupLayerRef      = useRef<L.LayerGroup>(L.layerGroup());
  const waypointLayerRef    = useRef<L.LayerGroup>(L.layerGroup());
  const lazeLayerRef        = useRef<L.LayerGroup>(L.layerGroup());
  const projectileLayerRef = useRef<L.LayerGroup>(L.layerGroup());
  const projectileLaunchRef = useRef<Map<string, { x: number; y: number; dir: number }>>(new Map());
  const projectileLockRef = useRef<Map<string, GuidedLock>>(new Map());
  const projectileTerminalRef = useRef<Map<string, number>>(new Map());
  const projectileLastEtaRef = useRef<Map<string, number>>(new Map());
  const projectileLockModeRef = useRef<Map<string, 'LAZE' | 'UNIT' | 'FREE'>>(new Map());
  const projectileLockSwitchAtRef = useRef<Map<string, number>>(new Map());
  const vehicleLayerRef    = useRef<L.LayerGroup>(L.layerGroup());
  const contourLayerRef    = useRef<L.LayerGroup>(L.layerGroup());
  const coastLayerRef      = useRef<L.LayerGroup>(L.layerGroup());
  const unitLayerRef       = useRef<L.LayerGroup>(L.layerGroup());
  const treeLayerRef       = useRef<L.LayerGroup>(L.layerGroup());
  const objectLayerRef     = useRef<L.LayerGroup>(L.layerGroup());
  const landLayerRef       = useRef<L.LayerGroup>(L.layerGroup());
  const hasStaticTreesRef  = useRef(false);
  const [hasRuntimeTrees, setHasRuntimeTrees] = useState(false);

  // Track map zoom level so vehicle crew labels can appear/disappear reactively
  const [mapZoom, setMapZoom] = useState(() => map.getZoom());
  const [clockMs, setClockMs] = useState(() => Date.now());
  useEffect(() => {
    const onZoom = () => setMapZoom(map.getZoom());
    map.on('zoomend', onZoom);
    return () => { map.off('zoomend', onZoom); };
  }, [map]);

  useEffect(() => {
    const timer = window.setInterval(() => setClockMs(Date.now()), 1000);
    return () => window.clearInterval(timer);
  }, []);

  const recentShooterIds = useMemo(() => {
    const set = new Set<string>();
    const blinkWindowMs = 2200;
    firedEvents.forEach(ev => {
      if (!ev.unitId) return;
      const atMs = Date.parse(ev.at);
      if (!Number.isFinite(atMs)) return;
      const age = clockMs - atMs;
      if (age >= 0 && age <= blinkWindowMs) {
        set.add(ev.unitId);
      }
    });
    return set;
  }, [firedEvents, clockMs]);

  // Create custom panes with fixed z-indices so layer order is always maintained
  // regardless of toggle sequence (addTo re-inserts at DOM end, breaking stacking).
  useEffect(() => {
    const mk = (name: string, z: number) => {
      const p = map.createPane(name);
      p.style.zIndex = String(z);
      p.style.pointerEvents = 'none';
    };
    mk('athena-land',       200);  // permanent land base (below topo)
    mk('athena-topo',       201);
    mk('athena-contour',    202);
    mk('athena-forest',     203);
    mk('athena-coast',      299);
    mk('athena-road',       300);
    mk('athena-structure',  310);
    mk('athena-peak',       360);
    mk('athena-objects',    205);  // buildings/objects  auto-shown at zoom 2x
    mk('athena-tree',       207);  // individual trees above objects
    mk('athena-location',   600);
    mk('athena-vehicle',    700);
    mk('athena-unit',       710);
    mk('athena-waypoint',   715);  // waypoint lines  always visible, between units and groups
    mk('athena-laze',       716);  // active laser designation points
    mk('athena-projectile', 717);  // simulated projectile paths
    mk('athena-group',      720);  // groups on top of units/vehicles
    // Pane-specific canvas renderers keep road and structure toggles independent.
    roadCanvasRef.current = L.canvas({ padding: 0.5, pane: 'athena-road' });
    structureCanvasRef.current = L.canvas({ padding: 0.5, pane: 'athena-structure' });
    // Bus exact: ocean base = Colors.PowderBlue (#B0E0E6)
    map.getContainer().style.background = '#B0E0E6';
    // Add all layer groups once  they stay on the map forever;
    // visibility is controlled by pane CSS (and addTo/removeLayer for locations)
    landLayerRef.current.addTo(map);
    contourLayerRef.current.addTo(map);
    topoLayerRef.current.addTo(map);
    forestLayerRef.current.addTo(map);
    roadLayerRef.current.addTo(map);
    structureLayerRef.current.addTo(map);
    coastLayerRef.current.addTo(map);
    locationLayerRef.current.addTo(map);
    peakLayerRef.current.addTo(map);
    treeLayerRef.current.addTo(map);
    objectLayerRef.current.addTo(map);
    groupLayerRef.current.addTo(map);
    waypointLayerRef.current.addTo(map);
    lazeLayerRef.current.addTo(map);
    projectileLayerRef.current.addTo(map);
    vehicleLayerRef.current.addTo(map);
    unitLayerRef.current.addTo(map);
  }, [map]);

  // Toggle layer visibility.
  // Image / vector layers: toggle the custom pane's CSS display (no DOM churn, z-order preserved).
  // Locations: addTo/removeLayer so that permanent tooltip elements are also removed from the DOM.
  useEffect(() => {
    const showPane = (name: string, on: boolean) => {
      const p = map.getPane(name);
      if (p) p.style.display = on ? '' : 'none';
    };
    const currentScale = zoomToDisplayScale(map.getZoom());
    const showTrees = hasRuntimeTrees && layers.trees;
    const showForest = layers.forest && currentScale < 2.0;
    // Keep land base visible in all modes so the map never collapses to all-blue.
    showPane('athena-land',      true);
    showPane('athena-topo',      renderMode !== '2d');  // topo elevation: only in heatmap modes
    showPane('athena-contour',   layers.contours);
    // Trees are user-toggleable and always visible when available; forest auto-hides above 2.0x.
    showPane('athena-forest',    showForest);
    showPane('athena-tree',      showTrees);
    // Coastline remains visible in all map styles regardless of contour toggle.
    showPane('athena-coast',     true);
    showPane('athena-road',      layers.roads);
    // Structures are user-toggleable and independent from roads.
    showPane('athena-structure', layers.structures && structures.length > 0);
    showPane('athena-objects',   true);
    // In heatmap modes, reduce forest/tree/object opacity so the topo elevation layer is visible.
    const forestOpacity  = renderMode === '2d' ? '1' : renderMode === 'heatmap1' ? '0.25' : '0.15';
    const treeOpacity    = renderMode === '2d' ? '1' : '0.15';
    const objectOpacity  = renderMode === '2d' ? '1' : '0.4';
    const fp = map.getPane('athena-forest'); if (fp) fp.style.opacity = forestOpacity;
    const tp2 = map.getPane('athena-tree');  if (tp2) tp2.style.opacity = treeOpacity;
    const op2 = map.getPane('athena-objects'); if (op2) op2.style.opacity = objectOpacity;
    showPane('athena-peak',      layers.locations);
    showPane('athena-group',     layers.groups);
    showPane('athena-waypoint',  layers.waypoints);
    showPane('athena-laze',      layers.lazes);
    showPane('athena-projectile', layers.projectiles);
    showPane('athena-vehicle',   layers.vehicles);
    showPane('athena-unit',      layers.units);
    // objects are always visible
    // Locations use addTo/removeLayer so permanent tooltip DOM elements are also hidden
    if (layers.locations) {
      if (!map.hasLayer(locationLayerRef.current)) locationLayerRef.current.addTo(map);
    } else {
      if (map.hasLayer(locationLayerRef.current)) map.removeLayer(locationLayerRef.current);
    }
  }, [map, layers, renderMode, structures.length, hasRuntimeTrees]);

  //  Trees  precise points from static cache (zoomed-in only) 
  useEffect(() => {
    // Reset cache when world changes
    if (staticCacheRef.current.treesWorld !== world) {
      treeLayerRef.current.clearLayers();
      staticCacheRef.current.treesWorld = world;
    }
    if (staticCacheRef.current.trees && staticCacheRef.current.treesWorld === world) return;
    if (!world) return;
    if (!worldSize) return;
    let cancelled = false;
    let retryTimer: number | null = null;
    const scale = 100 / worldSize;
    const GRID = 128;
    const cellW = worldSize / GRID;

    treeLayerRef.current.clearLayers();
    console.log(`[Trees] Fetching runtime trees for world: ${world}`);
    const buildTreeGridFromPoints = (points: Array<{ x: number; y: number }>, source: string) => {
      const cells = new Map<number, TreeRecord[]>();
      for (const point of points) {
        const x = Number(point.x);
        const y = Number(point.y);
        if (!Number.isFinite(x) || !Number.isFinite(y)) continue;

        const worldY = y;
        const lat = worldY * scale;
        const lng = x * scale;
        const gx = Math.min(GRID - 1, Math.max(0, Math.floor(x / cellW)));
        const gy = Math.min(GRID - 1, Math.max(0, Math.floor(worldY / cellW)));
        const key = gy * GRID + gx;
        let bucket = cells.get(key);
        if (!bucket) {
          bucket = [];
          cells.set(key, bucket);
        }
        bucket.push({ x, worldY, lat, lng });
      }

      if (cancelled) return;
      const treeGrid = new TreeGridLayer({ cells, scale, cellW, gridSize: GRID }, {
        pane: 'athena-tree',
        tileSize: 256,
        updateWhenZooming: false,
        updateWhenIdle: true,
      });
      treeLayerRef.current.addLayer(treeGrid);
      console.log(`[Trees] Rendered tree grid with ${cells.size} buckets from ${source}`);
      staticCacheRef.current.trees = true;
      staticCacheRef.current.treesWorld = world;
    };

    const fetchTrees = () => fetch(`${API_BASE}/api/game/trees`)
      .then(r => {
        console.log(`[Trees] Runtime API response status: ${r.status}`);
        return r.ok ? r.json() : null;
      })
      .then((runtime: Array<{ x: number; y: number }> | null) => {
        if (!runtime || runtime.length === 0 || cancelled) {
          hasStaticTreesRef.current = false;
          setHasRuntimeTrees(false);
          console.warn(`[Trees] No runtime tree data returned for ${world}`);

          if (!cancelled) {
            retryTimer = window.setTimeout(() => {
              if (!cancelled && !staticCacheRef.current.trees) fetchTrees().catch(() => {});
            }, 3000);
          }
          return;
        }

        hasStaticTreesRef.current = true;
        setHasRuntimeTrees(true);
        const runtimePoints = runtime
          .map(t => ({ x: Number((t as { x: number; y: number }).x), y: Number((t as { x: number; y: number }).y) }))
          .filter(t => Number.isFinite(t.x) && Number.isFinite(t.y));

        console.log(`[Trees] Loaded ${runtimePoints.length} runtime trees for ${world}`);
        buildTreeGridFromPoints(runtimePoints, `runtime trees (${runtimePoints.length})`);
      })
      .catch((err) => { console.error('[Trees] Fetch error:', err); });

    fetchTrees();

    return () => {
      cancelled = true;
      if (retryTimer !== null) window.clearTimeout(retryTimer);
    };
  }, [world, worldSize, map]);

  //  Objects  vector tile rendering (sharp at every zoom, like roads) 
  useEffect(() => {
    objectLayerRef.current.clearLayers();
    // Do not render raw Objects.txt from Athena Desktop cache here.
    // Runtime/cached structures from our own export pipeline render via the structure layer.
  }, [world, worldSize]);

  //  Zoom auto-show for tree pane + unit/vehicle  group auto-toggle 
  useEffect(() => {
    const UNIT_GROUP_THRESHOLD = 2.5;  // Swap groups  units at 2.5x display scale
    // Initialise to opposite of current zone so the first update() establishes correct state.
    const prevZoneRef = { current: !(zoomToDisplayScale(map.getZoom()) >= UNIT_GROUP_THRESHOLD) };
    const update = () => {
      const z = map.getZoom();
      const scale = zoomToDisplayScale(z);
      const showTrees = hasRuntimeTrees && layers.trees;
      const showForest = layers.forest && scale < 2.0;
      console.log(`[Zoom] z=${z.toFixed(2)}, scale=${scale.toFixed(2)}, hasTrees=${hasRuntimeTrees}, treesVisible=${showTrees}, forestVisible=${showForest}`);
      const tp = map.getPane('athena-tree');
      if (tp) {
        tp.style.display = showTrees ? '' : 'none';
      }
      const fp = map.getPane('athena-forest');
      if (fp) fp.style.display = showForest ? '' : 'none';
      const op = map.getPane('athena-objects');
      if (op) op.style.display = '';
      // At scale < 2.5x (zoomed out): show groups, hide units + vehicles
      // At scale  2.5x (zoomed in):  show units + vehicles, hide groups
      // Auto-toggle fires only when crossing the threshold;
      // user can still override manually within a zoom zone.
      const inUnitZone = scale >= UNIT_GROUP_THRESHOLD;
      if (inUnitZone !== prevZoneRef.current) {
        prevZoneRef.current = inUnitZone;
        onLayersChange?.(prev => ({
          ...prev,
          groups:   !inUnitZone,
          vehicles:  inUnitZone,
          units:     inUnitZone,
        }));
      }
    };
    map.on('zoomend', update);
    update(); // apply immediately on mount
    return () => { map.off('zoomend', update); };
  }, [map, onLayersChange, layers.forest, layers.trees, hasRuntimeTrees]);

  //  Land silhouette  permanent base showing land (Ivory #FFFFF0) vs ocean (transparent) 
  // Tries vector fill from static Z=0 contour first (smooth/exact coastline),
  // then falls back to static raster land mask, then Arma elevation data.
  useEffect(() => {
    if (!world) {
      landCacheRef.current = { world: '', ready: false, source: undefined };
      landLayerRef.current.clearLayers();
      return;
    }

    // If we already rendered the Z=0 vector coastline for this world, keep it stable.
    // But if only a fallback was used, allow re-render when contours arrive.
    const hasZ0 = contours.some(c => c.z === 0 && c.lines.length > 0);
    if (landCacheRef.current.world === world && landCacheRef.current.ready) {
      if (landCacheRef.current.source === 'z0' || !hasZ0) return;
      // Z=0 contours just arrived and we only had a fallback  upgrade below.
    }

    // Don't clear layers upfront  only clear when we have replacement content ready.
    // This prevents the map flashing blue during async fetches.
    let cancelled = false;

    async function buildLandLayer() {
      const dilateMask = (src: Uint8Array, width: number, height: number): Uint8Array => {
        const out = src.slice();
        for (let y = 0; y < height; y += 1) {
          for (let x = 0; x < width; x += 1) {
            const idx = y * width + x;
            if (!src[idx]) continue;
            for (let oy = -1; oy <= 1; oy += 1) {
              for (let ox = -1; ox <= 1; ox += 1) {
                const nx = x + ox;
                const ny = y + oy;
                if (nx < 0 || ny < 0 || nx >= width || ny >= height) continue;
                out[ny * width + nx] = 1;
              }
            }
          }
        }
        return out;
      };

      const applyLastResortLandFallback = () => {
        if (cancelled) return;
        // Only apply if nothing better is already showing
        if (landCacheRef.current.world === world && landCacheRef.current.ready) return;
        landLayerRef.current.clearLayers();
        L.rectangle([[0, 0], [100, 100]], {
          pane: 'athena-land',
          stroke: false,
          fillColor: '#FFFFF0',
          fillOpacity: 1,
          interactive: false,
        }).addTo(landLayerRef.current);
        landCacheRef.current = { world, ready: true, source: 'fallback' };
      };

      // Path 0 (primary): Z=0 contour polygon fill (vector coastline).
      // Prefer this first to avoid blocky shoreline edges from downscaled raster masks.
      const z0 = contours.find(c => c.z === 0);
      if (z0 && z0.lines.length > 0) {
        const scale = 100 / worldSize;
        const rings: L.LatLngExpression[][] = z0.lines
          .map(flat => {
            const pts: L.LatLngExpression[] = [];
            for (let i = 0; i + 1 < flat.length; i += 2) {
              // [lat=Y, lng=X] in normalised 0..100 map space
              pts.push([flat[i + 1] * scale, flat[i] * scale]);
            }
            return pts;
          })
          .filter(r => r.length >= 3);

        if (rings.length > 0) {
          if (cancelled) return;
          landLayerRef.current.clearLayers();
          L.polygon(rings, {
            pane:        'athena-land',
            stroke:      true,
            color:       '#FFFFF0',
            weight:      2,
            lineJoin:    'round',
            lineCap:     'round',
            fillColor:   '#FFFFF0',
            fillOpacity: 1,
            fillRule:    'evenodd',
            smoothFactor: 1,
            interactive: false,
          }).addTo(landLayerRef.current);
          landCacheRef.current = { world, ready: true, source: 'z0' };
          return;
        }
      }

      // Path 1 (fallback): static raster land mask for fully solid land/ocean fill.
      if (world) {
        try {
          const resp = await fetch(
            `${API_BASE}/api/staticmap/${encodeURIComponent(world)}/landmask?gridSize=1536`);
          if (!cancelled && resp.ok) {
            const data: { width: number; height: number; worldSize: number; mask: string } =
              await resp.json();
            const bytes = Uint8Array.from(atob(data.mask), c => c.charCodeAt(0));
            const grown = dilateMask(bytes, data.width, data.height);
            let landPixels = 0;
            for (let i = 0; i < grown.length; i += 1) if (grown[i]) landPixels += 1;
            if (landPixels === 0) throw new Error('Land mask contained no land pixels');
            const cvs   = document.createElement('canvas');
            cvs.width = data.width; cvs.height = data.height;
            const ctx = cvs.getContext('2d')!;
            const img = ctx.createImageData(data.width, data.height);
            for (let gy = 0; gy < data.height; gy++) {
              // Mask row 0 = worldY0 (south). Canvas row 0 = top of image = north.
              // Flip vertically so south maps to bottom of overlay.
              const srcRow = (data.height - 1 - gy) * data.width;
              for (let gx = 0; gx < data.width; gx++) {
                if (grown[srcRow + gx]) {
                  const o = (gy * data.width + gx) * 4;
                  img.data[o] = 255; img.data[o+1] = 255; img.data[o+2] = 240; img.data[o+3] = 255;
                }
              }
            }
            ctx.putImageData(img, 0, 0);
            landLayerRef.current.clearLayers();
            const ov = L.imageOverlay(cvs.toDataURL('image/png'), [[0, 0], [100, 100]], {
              opacity: 1, interactive: false, pane: 'athena-land',
            }).addTo(landLayerRef.current);
            const el = ov.getElement();
            if (el) {
              el.classList.add('athena-landmask-overlay');
              el.style.imageRendering = 'auto';
              el.style.willChange = 'transform';
            }
            landCacheRef.current = { world, ready: true, source: 'fallback' };
            return;
          }
        } catch { /* fall through to elevation fallback */ }
      }

      if (cancelled) return;

      //  Path 2: Arma elevation data (arrives after slow in-game export) 
      if (!elevations || elevations.cells.length === 0) {
        applyLastResortLandFallback();
        return;
      }
      const step = elevations.sampleSize;
      if (!Number.isFinite(step) || step <= 0) {
        applyLastResortLandFallback();
        return;
      }
      let minX = Infinity, minY = Infinity, maxX = -Infinity, maxY = -Infinity;
      elevations.cells.forEach(c => {
        if (c.x        < minX) minX = c.x;
        if (c.y        < minY) minY = c.y;
        if (c.x + step > maxX) maxX = c.x + step;
        if (c.y + step > maxY) maxY = c.y + step;
      });
      if (!isFinite(minX) || !isFinite(maxX) || !isFinite(maxY)) {
        applyLastResortLandFallback();
        return;
      }
      const cols = Math.round((maxX - minX) / step);
      const rows = Math.round((maxY - minY) / step);
      if (!Number.isFinite(cols) || !Number.isFinite(rows) || cols <= 0 || rows <= 0) {
        applyLastResortLandFallback();
        return;
      }
      const cvs  = document.createElement('canvas');
      cvs.width = cols; cvs.height = rows;
      const ctx  = cvs.getContext('2d')!;
      const img  = ctx.createImageData(cols, rows);
      elevations.cells.forEach(c => {
        const col = Math.round((c.x - minX) / step);
        const row = rows - 1 - Math.round((c.y - minY) / step);
        if (col < 0 || col >= cols || row < 0 || row >= rows) return;
        if (c.z > 0) {
          const idx = (row * cols + col) * 4;
          img.data[idx] = 255; img.data[idx+1] = 255; img.data[idx+2] = 240; img.data[idx+3] = 255;
        }
      });
      ctx.putImageData(img, 0, 0);
      const scale = 100 / worldSize;
      const bounds: L.LatLngBoundsExpression = [
        [minY * scale, minX * scale],
        [maxY * scale, maxX * scale],
      ];
      landLayerRef.current.clearLayers();
      const ov = L.imageOverlay(cvs.toDataURL('image/png'), bounds, {
        opacity: 1, interactive: false, pane: 'athena-land',
      }).addTo(landLayerRef.current);
      const el = ov.getElement();
      if (el) { el.style.imageRendering = 'pixelated'; el.style.willChange = 'transform'; }
      landCacheRef.current = { world, ready: true, source: 'fallback' };
    }

    buildLandLayer();
    return () => { cancelled = true; };
  }, [contours, world, elevations, worldSize]);

  //  Elevation layers (topo colour ramp + greyscale + contour lines) 
  useEffect(() => {
    topoLayerRef.current.clearLayers();
    if (!elevations || elevations.cells.length === 0) return;

    const step = elevations.sampleSize;
    if (!Number.isFinite(step) || step <= 0) return;
    let minX = Infinity, minY = Infinity, maxX = -Infinity, maxY = -Infinity;
    let minZ = Infinity, maxZ = -Infinity;
    elevations.cells.forEach(c => {
      if (c.x        < minX) minX = c.x;
      if (c.y        < minY) minY = c.y;
      if (c.x + step > maxX) maxX = c.x + step;
      if (c.y + step > maxY) maxY = c.y + step;
      if (c.z < minZ) minZ = c.z;
      if (c.z > maxZ) maxZ = c.z;
    });
    if (!isFinite(minX) || !isFinite(maxX) || !isFinite(maxY) || !isFinite(minZ) || !isFinite(maxZ) || maxZ === minZ) return;

    const cols   = Math.round((maxX - minX) / step);
    const rows   = Math.round((maxY - minY) / step);
    if (!Number.isFinite(cols) || !Number.isFinite(rows) || cols <= 1 || rows <= 1) return;
    const zRange = maxZ - minZ;

    // Render at 3 the grid resolution so contour lines are anti-aliased and smooth
    // when the image overlay is stretched to fill the Leaflet map bounds.
    const UP = 3;
    const W  = cols * UP;
    const H  = rows * UP;
    if (!Number.isFinite(W) || !Number.isFinite(H) || W <= 0 || H <= 0) return;

    // Per-cell height grid (at grid resolution, not upscaled) for marching squares.
    const heightGrid = new Float32Array(cols * rows).fill(NaN);

    const topoCvs = document.createElement('canvas');
    topoCvs.width = W; topoCvs.height = H;
    const topoCtx = topoCvs.getContext('2d')!;
    const topoImg = topoCtx.createImageData(W, H);



    elevations.cells.forEach(c => {
      const t   = Math.max(0, Math.min(1, (c.z - minZ) / zRange));
      const col = Math.round((c.x - minX) / step);
      const row = rows - 1 - Math.round((c.y - minY) / step);
      if (col < 0 || col >= cols || row < 0 || row >= rows) return;

      heightGrid[row * cols + col] = c.z;

      // Choose fill colour by render mode
      let fr: number, fg: number, fb: number, fa: number;
      if (renderMode === '2d') {
        fr = fg = fb = fa = 0;  // fully transparent  land base layer provides the colour
      } else if (renderMode === 'heatmap2') {
        const v = Math.round(255 - t * 255); fr = fg = fb = v; fa = 200;  // white  black
      } else {
        [fr, fg, fb] = topoColorHeatmap1(t); fa = 200;       // heatmap1 (default)
      }
      // Fill UPUP block per cell in the upscaled image data
      for (let dy = 0; dy < UP; dy++) {
        for (let dx = 0; dx < UP; dx++) {
          const idx = ((row * UP + dy) * W + (col * UP + dx)) * 4;
          topoImg.data[idx] = fr; topoImg.data[idx+1] = fg;
          topoImg.data[idx+2] = fb; topoImg.data[idx+3] = fa;
        }
      }
    });

    // Commit colour-fill pixels first so contour strokes render on top.
    topoCtx.putImageData(topoImg, 0, 0);
    // Scale context so all existing moveTo/lineTo coordinates (in grid pixels)
    // automatically map to the upscaled canvas  no changes needed below.
    topoCtx.scale(UP, UP);

    //  Marching squares contour lines 
    // Contour style cadence matches original Athena Desktop:
    //   every 10 m  thin brown (faint texture)
    //   every 30 m  medium stroke
    //   every 60 m  bolder
    //   every 120 m  boldest, always visible
    //   sea level (z=0)  blue
    const minZ10 = Math.ceil (minZ / 10) * 10;
    const maxZ10 = Math.floor(maxZ / 10) * 10;

    for (let z = minZ10; z <= maxZ10; z += 10) {
      // Bus exact: uniform strokeThickness 0.75, full opacity
      // Colors: MediumBlue (Z=0), DodgerBlue (Z<0), RosyBrown (Z>0)
      if (z === 0) {
        topoCtx.strokeStyle = '#0000CD';   // MediumBlue
      } else if (z < 0) {
        topoCtx.strokeStyle = '#1E90FF';   // DodgerBlue
      } else {
        topoCtx.strokeStyle = '#BC8F8F';   // RosyBrown
      }
      topoCtx.lineWidth = 0.75;
      topoCtx.beginPath();

      // Standard 16-case marching squares lookup.
      // Bit ordering: tl=8, tr=4, br=2, bl=1.
      // Edge midpoints (in canvas px): top=(c+0.5,r), right=(c+1,r+0.5),
      //                                bottom=(c+0.5,r+1), left=(c,r+0.5).
      for (let r = 0; r < rows - 1; r++) {
        for (let c2 = 0; c2 < cols - 1; c2++) {
          const vTL = heightGrid[ r      * cols + c2    ];
          const vTR = heightGrid[ r      * cols + c2 + 1];
          const vBL = heightGrid[(r + 1) * cols + c2    ];
          const vBR = heightGrid[(r + 1) * cols + c2 + 1];
          if (isNaN(vTL) || isNaN(vTR) || isNaN(vBL) || isNaN(vBR)) continue;

          const ci = ((vTL >= z ? 8 : 0) | (vTR >= z ? 4 : 0)
                    | (vBR >= z ? 2 : 0) | (vBL >= z ? 1 : 0));

          // Each case draws 0-2 line segments between edge midpoints.
          // Using moveTo+lineTo batched inside a single beginPath for performance.
          switch (ci) {
            case  1: case 14:  // left  bottom
              topoCtx.moveTo(c2 + 0.5, r + 1);  topoCtx.lineTo(c2,       r + 0.5); break;
            case  2: case 13:  // bottom  right
              topoCtx.moveTo(c2 + 0.5, r + 1);  topoCtx.lineTo(c2 + 1,   r + 0.5); break;
            case  3: case 12:  // left  right
              topoCtx.moveTo(c2,       r + 0.5); topoCtx.lineTo(c2 + 1,   r + 0.5); break;
            case  4: case 11:  // top  right
              topoCtx.moveTo(c2 + 0.5, r);       topoCtx.lineTo(c2 + 1,   r + 0.5); break;
            case  5:           // saddle: (tl+br above, tr+bl below)
              topoCtx.moveTo(c2,       r + 0.5); topoCtx.lineTo(c2 + 0.5, r);
              topoCtx.moveTo(c2 + 0.5, r + 1);   topoCtx.lineTo(c2 + 1,   r + 0.5); break;
            case  6: case  9:  // top  bottom
              topoCtx.moveTo(c2 + 0.5, r);       topoCtx.lineTo(c2 + 0.5, r + 1);   break;
            case  7: case  8:  // top  left
              topoCtx.moveTo(c2 + 0.5, r);       topoCtx.lineTo(c2,       r + 0.5); break;
            case 10:           // saddle: (tl+br above, tr+bl below)
              topoCtx.moveTo(c2,       r + 0.5); topoCtx.lineTo(c2 + 0.5, r + 1);
              topoCtx.moveTo(c2 + 0.5, r);       topoCtx.lineTo(c2 + 1,   r + 0.5); break;
            // 0, 15: no crossings
          }
        }
      }
      topoCtx.stroke();
    }
    //  End contour lines 

    const scale  = 100 / worldSize;
    const bounds: L.LatLngBoundsExpression = [
      [minY * scale, minX * scale],
      [maxY * scale, maxX * scale],
    ];
    const topoOv = L.imageOverlay(topoCvs.toDataURL('image/png'), bounds,
      { opacity: 1, interactive: false, pane: 'athena-topo' })
      .addTo(topoLayerRef.current);
    const topoEl = topoOv.getElement();
    if (topoEl) topoEl.style.willChange = 'transform';

    //  Peak markers (local terrain maxima) 
    // Detect cells that are the highest point within a sliding-window radius,
    // matching the summit triangles Arma renders on its in-game map.
    peakLayerRef.current.clearLayers();
    const RADIUS   = 16;  // cells (~512 m at 32 m step)  only dominant summits
    const MIN_ELEV = 50;  // ignore low hills and coastal bumps
    const MIN_PROM = 30;  // must be 30 m above all neighbors within radius
    const sc = 100 / worldSize;
    for (let r = RADIUS; r < rows - RADIUS; r++) {
      for (let c = RADIUS; c < cols - RADIUS; c++) {
        const z = heightGrid[r * cols + c];
        if (isNaN(z) || z < MIN_ELEV) continue;
        let isMax = true;
        let minN  = Infinity;
        outer: for (let dr = -RADIUS; dr <= RADIUS; dr++) {
          for (let dc = -RADIUS; dc <= RADIUS; dc++) {
            if (dr === 0 && dc === 0) continue;
            const nz = heightGrid[(r + dr) * cols + (c + dc)];
            if (isNaN(nz)) continue;
            if (nz >= z) { isMax = false; break outer; }
            if (nz < minN) minN = nz;
          }
        }
        if (!isMax || z - minN < MIN_PROM) continue;
        const worldX = minX + c * step;
        const worldY = minY + (rows - 1 - r) * step;
        const ll: [number, number] = [worldY * sc, worldX * sc];
        const elev = Math.round(z);
        // White-filled triangle with dark stroke + small centre dot, Arma-style
        const peakSvg =
          `<svg xmlns="http://www.w3.org/2000/svg" width="10" height="9" viewBox="0 0 10 9">` +
          `<polygon points="5,0.5 9.5,8.5 0.5,8.5" fill="white" stroke="#333" stroke-width="1"/>` +
          `<circle cx="5" cy="6.5" r="1" fill="#333"/>` +
          `</svg>`;
        L.marker(ll, {
          icon: L.divIcon({ className: '', html: peakSvg, iconSize: [10, 9], iconAnchor: [5, 9] }),
          interactive: false,
          pane: 'athena-peak',
        })
          .bindTooltip(
            `<span style="font-size:8px;color:#333;font-weight:600">${elev}</span>`,
            { permanent: true, direction: 'right', className: 'location-label', offset: [4, -5] }
          )
          .addTo(peakLayerRef.current);
      }
    }
  }, [elevations, worldSize, renderMode]);

  // -- Static Athena contour lines (pre-computed, loaded from Athena Desktop cache) --------
  useEffect(() => {
    contourLayerRef.current.clearLayers();
    coastLayerRef.current.clearLayers();
    if (contours.length === 0) return;
    const scale = 100 / worldSize;
    contours.forEach(cl => {
      const style = contourStyle(cl.z);
      // Each ContourLine.lines entry is a flat [x0,y0,x1,y1,...] array.
      // Build a multi-polyline (one Leaflet layer per elevation) for efficiency.
      const latlngs: [number, number][][] = cl.lines
        .map(flat => {
          const pts: [number, number][] = [];
          for (let i = 0; i + 1 < flat.length; i += 2)
            pts.push([flat[i + 1] * scale, flat[i] * scale]); // [lat=Y, lng=X]
          return pts;
        })
        .filter(p => p.length >= 2);
      if (latlngs.length === 0) return;

      // Sea-level coastline gets a dedicated pane in 2D mode so it stays visible
      // above forests and land fill, with a crisp outer + inner stroke.
      if (cl.z === 0) {
        // Bus exact: Z=0 coastline = MediumBlue, stroke 0.75
        L.polyline(latlngs, {
          color:       '#0000CD',
          weight:      0.75,
          opacity:     1,
          lineJoin:    'round',
          lineCap:     'round',
          interactive: false,
          pane:        'athena-coast',
        }).addTo(coastLayerRef.current);
      } else {
        L.polyline(latlngs, {
          color:       style.color,
          weight:      style.weight,
          opacity:     style.opacity,
          lineJoin:    'miter',
          lineCap:     'butt',
          interactive: false,
          pane:        'athena-contour',
        }).addTo(contourLayerRef.current);
      }
    });
  }, [contours, worldSize]);

  // -- Forest layer (sampled density grid from Arma export) -------------------------------
  useEffect(() => {
    if (staticCacheRef.current.forests) return;
    forestLayerRef.current.clearLayers();
    if (!_forests || _forests.cells.length === 0) return;
    if (!worldSize || _forests.sampleSize <= 0) return;

    const sample = _forests.sampleSize;
    const cols = Math.max(1, Math.ceil(worldSize / sample));
    const rows = Math.max(1, Math.ceil(worldSize / sample));
    const cvs = document.createElement('canvas');
    cvs.width = cols;
    cvs.height = rows;
    const ctx = cvs.getContext('2d');
    if (!ctx) return;

    const img = ctx.createImageData(cols, rows);
    for (const cell of _forests.cells) {
      const col = Math.floor(cell.x / sample);
      const rowFromSouth = Math.floor(cell.y / sample);
      const row = rows - 1 - rowFromSouth;
      if (col < 0 || col >= cols || row < 0 || row >= rows) continue;

      const idx = (row * cols + col) * 4;
      // Bus exact forest colors: ARGB(70, 0, 127, 12) heavy, ARGB(70, 0, 181, 18) light
      // Alpha 70/255  27.5% opacity. level>=3 = heavy, else = light.
      if (cell.level >= 3) {
        img.data[idx] = 0;
        img.data[idx + 1] = 127;
        img.data[idx + 2] = 12;
        img.data[idx + 3] = 70;
      } else {
        img.data[idx] = 0;
        img.data[idx + 1] = 181;
        img.data[idx + 2] = 18;
        img.data[idx + 3] = 70;
      }
    }
    ctx.putImageData(img, 0, 0);

    const overlay = L.imageOverlay(cvs.toDataURL('image/png'), [[0, 0], [100, 100]], {
      opacity: 1,
      interactive: false,
      pane: 'athena-forest',
    }).addTo(forestLayerRef.current);

    const el = overlay.getElement();
    if (el) el.style.imageRendering = 'pixelated';
    staticCacheRef.current.forests = true;
  }, [_forests, worldSize]);

  //  Road layer 
  useEffect(() => {
    if (staticCacheRef.current.roads) return; // already rendered for this world
    roadLayerRef.current.clearLayers();
    if (roads.length === 0) return;
    const scale = 100 / worldSize;
    const canvas = roadCanvasRef.current;
    if (!canvas) return;

    // Group road segments by style for batched rendering (~6 layers vs thousands)
    const groups = new Map<string, { color: string; weight: number; segments: [number,number][][] }>();

    // Collect airport surfaces and render them first as base pavement.
    const airportRoads: Road[] = [];

    roads.forEach(road => {
      // Airport surfaces  collect for later
      if (road.type.toLowerCase() === 'hide' && road.width > 0 && road.length > 0) {
        airportRoads.push(road);
        return;
      }

      const style = roadStyle(road.type, road.foot);
      if (!style) return;

      const beg: [number, number] = [road.beg1Y * scale, road.beg1X * scale];
      const end: [number, number] = [road.end2Y * scale, road.end2X * scale];
      if (beg[0] === 0 && beg[1] === 0 && end[0] === 0 && end[1] === 0) return;

      const key = `${style.color}_${style.weight}`;
      let grp = groups.get(key);
      if (!grp) { grp = { color: style.color, weight: style.weight, segments: [] }; groups.set(key, grp); }
      grp.segments.push([beg, end]);
    });

    // Pass 1: airport surface tiles first (base layer)
    airportRoads.forEach(road => {
      const latlngs = rotatedRoadRect(road, scale);
      L.polygon(latlngs, {
        fillColor:   '#D3D3D3',
        fillOpacity: 1,
        color:       '#a5adb5',
        weight:      0.45,
        stroke:      true,
        opacity:     1,
        interactive: false,
        pane:        'athena-road',
        renderer:    canvas,
      }).addTo(roadLayerRef.current);
    });

    // Pass 2: all black borders (drawn first = behind colored road fills)
    groups.forEach(({ weight, segments }) => {
      L.polyline(segments, {
        color:       '#222222',
        weight:      weight + 2,
        opacity:     1,
        lineCap:     'butt',
        interactive: false,
        renderer:    canvas,
      }).addTo(roadLayerRef.current);
    });

    // Pass 3: all coloured fills (drawn on top)
    groups.forEach(({ color, weight, segments }) => {
      L.polyline(segments, {
        color,
        weight,
        opacity:     1,
        lineCap:     'butt',
        interactive: false,
        renderer:    canvas,
      }).addTo(roadLayerRef.current);
    });

    // Airport surfaces were rendered in Pass 1 so taxi/road lines remain visible.
    staticCacheRef.current.roads = true;
  }, [roads, worldSize]);

  //  Location labels 
  useEffect(() => {
    if (staticCacheRef.current.locations) return; // already rendered for this world
    locationLayerRef.current.clearLayers();
    if (locations.length === 0) return;
    const scale = 100 / worldSize;
    locations.forEach(loc => {
      if (!loc.name) return;
      const locClass  = locationMap.get(loc.type);
      const drawStyle = locClass?.DrawStyle ?? 'name';
      const ll: [number, number] = [loc.posY * scale, loc.posX * scale];

      // Text label (DrawStyle === 'name')
      if (drawStyle !== 'name') return;
      // Keep labels fixed screen-size regardless map zoom level.
      const sizeText = locClass?.SizeText ?? 5;
      const fontSize = `${Math.round(10 + (sizeText - 4) * 3)}px`;
      const spacing  = sizeText >= 7 ? '1.8px' : sizeText >= 5 ? '1.2px' : '0.8px';
      L.marker(ll, {
        icon: L.divIcon({
          className: 'location-label',
          iconSize: [0, 0],
          iconAnchor: [0, 0],
          html: `<span style="font-size:${fontSize};letter-spacing:${spacing}">${escapeHtml(loc.name.toUpperCase())}</span>`,
        }),
        interactive: false,
        pane: 'athena-location',
      })
        .addTo(locationLayerRef.current);
    });
    staticCacheRef.current.locations = true;
  }, [locations, worldSize, locationMap]);

  //  Structures as vector geometry (road-like visual quality) 
  // Rendered in batches via requestIdleCallback/setTimeout to avoid freezing the browser
  // on large maps (Altis: 40K+ structures after server-side limit).
  useEffect(() => {
    if (staticCacheRef.current.structures) return; // already rendered for this world
    structureLayerRef.current.clearLayers();
    if (structures.length === 0) return;
    let cancelled = false;
    const scale = 100 / worldSize;
    const canvas = structureCanvasRef.current ?? undefined;
    const polePoints = structures
      .filter(p => p.posX !== 0 && p.posY !== 0 && isPowerPoleLike(p))
      .map(p => ({ x: p.posX, y: p.posY }));

    const estimatePowerWireSpanMeters = (s: MapStructure): number | null => {
      if (polePoints.length < 2) return null;

      const angle = (s.dir * Math.PI) / 180;
      // Arma heading basis in world space: 0=north, 90=east.
      const ux = Math.sin(angle);
      const uy = Math.cos(angle);

      let nearestPos = Number.POSITIVE_INFINITY;
      let nearestNeg = Number.POSITIVE_INFINITY;
      const perpTol = 2.5;
      const searchMax = Math.max(40, Math.max(s.width, s.length) * 2.5);

      for (const pole of polePoints) {
        const dx = pole.x - s.posX;
        const dy = pole.y - s.posY;
        const along = (dx * ux) + (dy * uy);
        const alongAbs = Math.abs(along);
        if (alongAbs < 0.5 || alongAbs > searchMax) continue;

        const perp = Math.abs((-dx * uy) + (dy * ux));
        if (perp > perpTol) continue;

        if (along > 0) {
          if (along < nearestPos) nearestPos = along;
        } else {
          const neg = -along;
          if (neg < nearestNeg) nearestNeg = neg;
        }
      }

      if (!Number.isFinite(nearestPos) || !Number.isFinite(nearestNeg)) return null;
      const span = nearestPos + nearestNeg;
      return span >= 3 ? span : null;
    };

    // Render in chunks of 2000 to keep the main thread responsive.
    const CHUNK = 2000;
    let idx = 0;
    const renderChunk = () => {
      if (cancelled) return;
      const end = Math.min(idx + CHUNK, structures.length);
      for (; idx < end; idx++) {
        const s = structures[idx];
      if (s.posX === 0 && s.posY === 0) return;
      if (s.width <= 0 || s.length <= 0) return;

      const minDim = Math.min(s.width, s.length);
      const maxDim = Math.max(s.width, s.length);
      const aspect = maxDim / Math.max(minDim, 0.01);
      const area = s.width * s.length;

      // Skip tiny artifacts/noise.
      if (area < 0.8) return;

      // Restrict line rendering to truly linear assets to avoid pole/prop overshoot.
      const isLinearByShape = minDim < 0.8 && aspect >= 8.0 && maxDim >= 2.5;
      const isWallFence = isWallFenceLike(s);
      const isPowerWire = isPowerWireLike(s);
      const isLine = isWallFence || isPowerWire || isLinearByShape;
      if (isLine) {
        const cx = s.posX * scale;
        const cy = s.posY * scale;
        let segLength = maxDim;
        if (isPowerWire) {
          const measuredSpan = estimatePowerWireSpanMeters(s);
          if (measuredSpan !== null) {
            segLength = measuredSpan;
          } else {
            // Fallback when nearby poles can't be reliably detected.
            const lengthTrim = Math.min(1.0, maxDim * 0.08);
            segLength = Math.max(0.5, maxDim - (lengthTrim * 2));
          }
        }
        const halfLong = (segLength / 2) * scale;
        // Fence/wall model forward vectors are often perpendicular to their visual segment.
        // Powerline cable spans generally align directly with model direction.
        const dirOffset = isWallFence ? 90 : 0;
        const a = ((s.dir + dirOffset) * Math.PI) / 180;
        // Arma headings are 0=north, 90=east; map lng is X/east and lat is Y/north.
        const dx = Math.sin(a) * halfLong;
        const dy = Math.cos(a) * halfLong;
        L.polyline(
          [[cy - dy, cx - dx], [cy + dy, cx + dx]],
          {
            color: '#6f7b87',
            weight: 0.9,
            opacity: 1,
            lineCap: 'butt',
            lineJoin: 'miter',
            interactive: false,
            pane: 'athena-structure',
            renderer: canvas,
          }
        ).addTo(structureLayerRef.current);
        return;
      }

      const corners = rotatedStructureRect(s, scale);
      const isBuilding = area >= 40;
      L.polygon(corners, {
        color: '#5b6b7d',
        weight: isBuilding ? 1.0 : 0.85,
        opacity: 1,
        fill: isBuilding,
        fillColor: '#78889a',
        fillOpacity: 1,
        interactive: false,
        pane: 'athena-structure',
        renderer: canvas,
      }).addTo(structureLayerRef.current);
      }
      if (idx < structures.length) {
        setTimeout(renderChunk, 0);
      } else {
        staticCacheRef.current.structures = true;
      }
    };
    renderChunk();
    return () => { cancelled = true; };
  }, [structures, worldSize]);

  //  Group waypoints 
  useEffect(() => {
    groupLayerRef.current.clearLayers();
    waypointLayerRef.current.clearLayers();
    const scale = 100 / worldSize;
    Object.values(groups).forEach(grp => {
      const members = Object.values(units).filter(u => u.groupId === grp.id);
      if (members.length === 0) return;
      const leader = members.find(u => u.id === grp.leaderId);
      const playerMember = members.find(u => u.playerName?.trim());
      const mountedMember = members.find(u => !!u.vehicleId);
      const anchorUnit = mountedMember ?? playerMember ?? leader ?? members[0];
      const mountedVehicle = anchorUnit.vehicleId ? vehicles[anchorUnit.vehicleId] : undefined;
      const posX = mountedVehicle?.posX ?? anchorUnit.posX;
      const posY = mountedVehicle?.posY ?? anchorUnit.posY;
      if (posX === 0 && posY === 0) return;
      const ll: [number, number] = [posY * scale, posX * scale];
      const side = anchorUnit.side ?? 'unknown';
      const groupCount = members.length;
      const groupType = resolveGroupType(members, vehicles, vehicleMap);

      //  Waypoint line + endpoint  (only for explicit orders, not formation positions)
      if (grp.wpX !== 0 || grp.wpY !== 0) {
        const wpLL: [number, number] = [grp.wpY * scale, grp.wpX * scale];
        const wpType = grp.wpType || 'MOVE';
        const { color, endColor, endIcon } = waypointStyle(wpType);
        const wpLabel = wpType.toUpperCase();
        // Dashed line from group to waypoint
        L.polyline([ll, wpLL], {
          color, weight: 2, dashArray: '6,4', opacity: 1, pane: 'athena-waypoint', interactive: false,
        }).addTo(waypointLayerRef.current);
        // Endpoint circle + type label
        L.marker(wpLL, {
          pane: 'athena-waypoint',
          interactive: false,
          icon: L.divIcon({
            className: '',
            iconSize: [14, 14],
            iconAnchor: [7, 7],
            html: `<svg width="14" height="14" viewBox="0 0 14 14"><circle cx="7" cy="7" r="5.5" fill="${endColor}" stroke="#000" stroke-width="1.5"/>${endIcon}</svg>`
              + `<div style="position:absolute;top:14px;left:50%;transform:translateX(-50%);white-space:nowrap;font-size:9px;font-weight:700;color:${color};text-shadow:-1px 0 #000,1px 0 #000,0 -1px #000,0 1px #000;letter-spacing:0.5px;pointer-events:none">${wpLabel}</div>`,
          }),
        }).addTo(waypointLayerRef.current);
      }

      L.marker(ll, { icon: groupIcon(side, groupCount, groupType), pane: 'athena-group' })
        .bindTooltip(`<b>${escapeHtml(groupLabel(grp))}</b><br>${groupType} (${groupCount})${grp.wpType ? `<br>WP: ${grp.wpType}` : ''}`)
        .addTo(groupLayerRef.current);

      const groupFiring = members.some(m => recentShooterIds.has(m.id));
      if (groupFiring) {
        L.marker(ll, {
          pane: 'athena-group',
          interactive: false,
            icon: firingPulseIcon(56),
        }).addTo(groupLayerRef.current);
      }

      // Group name + unit info label below marker
      const leaderUnit = leader ?? members[0];
      const rankStr = shortRank(leaderUnit.rank);
      const nameStr = escapeHtml(leaderUnit.playerName?.trim() || leaderUnit.name || '');
      const unitInfo = rankStr ? `(${rankStr}) ${nameStr}` : nameStr;
      L.marker(ll, {
        pane: 'athena-group',
        interactive: false,
        icon: L.divIcon({
          className: '',
          iconSize: [0, 0],
          iconAnchor: [-26, 0],
          html: `<div class="map-marker-label map-marker-label-group">${escapeHtml(groupLabel(grp))}<br><span style="font-weight:400;font-size:10px">${unitInfo}</span></div>`,
        }),
      }).addTo(groupLayerRef.current);
    });
  }, [groups, units, vehicles, worldSize, vehicleMap, recentShooterIds]);

  //  Active laser designations 
  useEffect(() => {
    lazeLayerRef.current.clearLayers();
    if (lazes.length === 0) return;

    const scale = 100 / worldSize;
    lazes.forEach(laze => {
      if (!Number.isFinite(laze.posX) || !Number.isFinite(laze.posY)) return;
      const ll: [number, number] = [laze.posY * scale, laze.posX * scale];
      const label = escapeHtml(lazeLabel(units[laze.unitId]));

      L.marker(ll, {
        pane: 'athena-laze',
        interactive: false,
        icon: L.divIcon({
          className: '',
          iconSize: [0, 0],
          iconAnchor: [0, 0],
          html: `<div style="position:relative;transform:translate(-50%,-50%);pointer-events:none;">`
            + `<div style="position:absolute;left:-14px;top:-14px;width:28px;height:28px;border:2px solid rgba(15,15,20,0.95);border-radius:50%;box-shadow:0 0 0 1px rgba(255,255,255,0.8),0 0 10px rgba(0,0,0,0.55);"></div>`
            + `<div style="position:absolute;left:-11px;top:-11px;width:22px;height:22px;border:2px solid rgba(255,96,96,0.98);border-radius:50%;box-shadow:0 0 10px rgba(255,72,72,0.85), inset 0 0 0 1px rgba(255,255,255,0.5);"></div>`
            + `<div style="position:absolute;left:-1px;top:-13px;width:2px;height:8px;background:rgba(255,96,96,1);"></div>`
            + `<div style="position:absolute;left:-1px;top:5px;width:2px;height:8px;background:rgba(255,96,96,1);"></div>`
            + `<div style="position:absolute;left:-13px;top:-1px;width:8px;height:2px;background:rgba(255,96,96,1);"></div>`
            + `<div style="position:absolute;left:5px;top:-1px;width:8px;height:2px;background:rgba(255,96,96,1);"></div>`
            + `<div style="position:absolute;left:-4px;top:-4px;width:8px;height:8px;border-radius:50%;background:#fff;border:1px solid rgba(20,20,30,0.85);box-shadow:0 0 6px rgba(255,96,96,0.9);"></div>`
            + `<div style="position:absolute;left:20px;top:-11px;white-space:nowrap;font-size:10px;font-weight:700;color:#ffd1d1;padding:1px 5px;border-radius:4px;background:rgba(20,20,25,0.72);border:1px solid rgba(255,170,170,0.7);text-shadow:none;">${label}</div>`
            + `</div>`,
        }),
      }).addTo(lazeLayerRef.current);
    });
  }, [lazes, units, worldSize]);

  //  Simulated projectile tracking (prediction-only) 
  useEffect(() => {
    projectileLayerRef.current.clearLayers();
    if (firedEvents.length === 0) {
      onProjectileDebugChange?.([]);
      return;
    }

    const scale = 100 / worldSize;
    const now = clockMs;
    const activeKeys = new Set<string>();
    const debugEntries: ProjectileDebugEntry[] = [];

    // Deduplicate repeated events in the rolling fired queue
    const dedup = new Map<string, FiredEvent>();
    firedEvents.forEach(ev => {
      const key = `${ev.at}|${ev.unitId}|${ev.vehicleId}|${ev.weapon}|${ev.ammo}|${ev.projectile}`;
      if (!dedup.has(key)) dedup.set(key, ev);
    });

    // Build lookup of the most recent actual impact per shooter/weapon combination.
    // Key strategy is intentionally tolerant because Arma can report different unit IDs
    // for the same vehicle-fired shot (e.g. gunner/commander variants across handlers).
    const impactLookup = new Map<string, FiredImpactEvent>();
    for (const imp of firedImpacts) {
      const keys = [
        `${imp.unitId}|${imp.vehicleId}|${imp.weapon}|${imp.muzzle}`,
        `${imp.vehicleId}|${imp.weapon}|${imp.muzzle}`,
        `${imp.unitId}|${imp.weapon}|${imp.muzzle}`,
        `${imp.weapon}|${imp.muzzle}`,
      ];
      for (const k of keys) {
        const existing = impactLookup.get(k);
        if (!existing || Date.parse(imp.at) > Date.parse(existing.at)) {
          impactLookup.set(k, imp);
        }
      }
    }

    dedup.forEach((ev, key) => {
      const profile = getProjectileProfile(ev);
      if (!profile) return;

      const terminalUntil = projectileTerminalRef.current.get(key);
      if (terminalUntil !== undefined && terminalUntil > now) return;
      if (terminalUntil !== undefined && terminalUntil <= now) {
        projectileTerminalRef.current.delete(key);
      }

      const firedAt = Date.parse(ev.at);
      if (!Number.isFinite(firedAt)) return;

      const unit = units[ev.unitId];
      const vehicleByVehicleId = ev.vehicleId ? vehicles[ev.vehicleId] : undefined;
      const vehicleByUnitLink = unit?.vehicleId ? vehicles[unit.vehicleId] : undefined;
      // Some vehicle-fired events can report vehicle netId in unitId; support that shape too.
      const vehicleByUnitId = vehicles[ev.unitId];
      const vehicle = vehicleByVehicleId ?? vehicleByUnitLink ?? vehicleByUnitId;

      const srcX = vehicle?.posX ?? unit?.posX;
      const srcY = vehicle?.posY ?? unit?.posY;
      const eventDir = typeof ev.fireDir === 'number' && Number.isFinite(ev.fireDir) ? normDeg(ev.fireDir) : undefined;
      let dirDeg = normDeg(eventDir ?? vehicle?.dir ?? unit?.dir ?? 0);

      // If telemetry contains a concrete target point and heading points away from it,
      // flip heading by 180 for this event (some platforms report inverted weapon vectors).
      if (typeof ev.targetX === 'number' && Number.isFinite(ev.targetX) && typeof ev.targetY === 'number' && Number.isFinite(ev.targetY)) {
        const toTargetX = ev.targetX - srcX;
        const toTargetY = ev.targetY - srcY;
        const toTargetLen = Math.hypot(toTargetX, toTargetY);
        if (toTargetLen > 1) {
          const targetBearing = normDeg((Math.atan2(toTargetX, toTargetY) * 180) / Math.PI);
          if (shortestAngleDiff(dirDeg, targetBearing) > 90) {
            dirDeg = normDeg(dirDeg + 180);
          }
        }
      }

      if (profile.headingOffsetDeg) {
        dirDeg = normDeg(dirDeg + profile.headingOffsetDeg);
      }
      if (!Number.isFinite(srcX) || !Number.isFinite(srcY)) return;

      let launch = projectileLaunchRef.current.get(key);
      if (!launch) {
        launch = { x: srcX, y: srcY, dir: dirDeg };
        projectileLaunchRef.current.set(key, launch);
      }

      const shooterSide = unit?.side ?? 'UNKNOWN';
      let target: TargetEstimate | null = null;
      let lockMode: 'LAZE' | 'UNIT' | 'FREE' = profile.requiresLock ? 'UNIT' : 'FREE';
      if (profile.requiresLock) {
        const prevLockMode = projectileLockModeRef.current.get(key);
        const lastEtaSec = projectileLastEtaRef.current.get(key);
        if (profile.preferLiveLaze && prevLockMode === 'LAZE' && lazes.length === 0 && lastEtaSec !== undefined && lastEtaSec <= 3) {
          projectileTerminalRef.current.set(key, now + 5000);
          projectileLaunchRef.current.delete(key);
          projectileLockRef.current.delete(key);
          projectileLockModeRef.current.delete(key);
          projectileLockSwitchAtRef.current.delete(key);
          projectileLastEtaRef.current.delete(key);
          return;
        }

        let lock: GuidedLock | null = projectileLockRef.current.get(key) ?? null;
        if (!lock) {
          lock = acquireGuidedLock(ev, launch.x, launch.y, launch.dir, shooterSide, units, lazes, profile);
          if (lock) projectileLockRef.current.set(key, lock);
        }

        if (profile.preferLiveLaze && lock?.kind === 'unit' && lazes.length > 0) {
          const preferredLazeUnitId = findNearestLazeUnitId(launch.x, launch.y, launch.dir, ev.unitId, lazes, profile);
          if (preferredLazeUnitId) {
            lock = { kind: 'laze', unitId: preferredLazeUnitId };
            projectileLockRef.current.set(key, lock);
          }
        }

        if (lock) {
          lockMode = lock.kind === 'laze' ? 'LAZE' : 'UNIT';
          target = resolveGuidedLockTarget(lock, units, lazes, worldSize);
          if (!target) {
            // If the original lock vanished, try to reacquire while preserving lock type semantics.
            let reacquired: GuidedLock | null = null;
            if (lock.kind === 'laze') {
              if (typeof ev.targetX === 'number' && Number.isFinite(ev.targetX) && typeof ev.targetY === 'number' && Number.isFinite(ev.targetY)) {
                const byTargetPoint = findNearestLazeUnitIdByTargetPoint(ev.targetX, ev.targetY, lazes);
                if (byTargetPoint) {
                  reacquired = { kind: 'laze', unitId: byTargetPoint };
                }
              }
              if (!reacquired) {
                const lazeUnitId = findNearestLazeUnitId(launch.x, launch.y, launch.dir, ev.unitId, lazes, profile);
                if (lazeUnitId) {
                  reacquired = { kind: 'laze', unitId: lazeUnitId };
                }
              }
            } else {
              reacquired = acquireGuidedLock(ev, launch.x, launch.y, launch.dir, shooterSide, units, lazes, profile);
            }

            if (reacquired) {
              projectileLockRef.current.set(key, reacquired);
              lockMode = reacquired.kind === 'laze' ? 'LAZE' : 'UNIT';
              target = resolveGuidedLockTarget(reacquired, units, lazes, worldSize);
            }
          }
        }

        if (!target) {
          return;
        }
      } else {
        const grp = unit?.groupId ? groups[unit.groupId] : undefined;
        target = targetFromFiredEvent(ev)
          ?? targetFromLazes(launch.x, launch.y, launch.dir, ev.unitId, lazes, worldSize, profile)
          ?? estimateTarget(launch.x, launch.y, launch.dir, shooterSide, grp, units, worldSize, profile);
      }

      let dstX = target.x;
      let dstY = target.y;

      // For unguided weapons, use actual impact coordinates once the shell has landed.
      // The SQF tracker sends fired_impact 0.1 s after impact with the last-known position.
      // Also accept data from a previous shot of the same fire mission (same unit+weapon aimed
      // in the same direction) to correct the predicted range before the current shell lands.
      if (!profile.requiresLock) {
        const impactKeys = [
          `${ev.unitId}|${ev.vehicleId}|${ev.weapon}|${ev.muzzle}`,
          `${ev.vehicleId}|${ev.weapon}|${ev.muzzle}`,
          `${ev.unitId}|${ev.weapon}|${ev.muzzle}`,
          `${ev.weapon}|${ev.muzzle}`,
        ];
        const impact = impactKeys.map(k => impactLookup.get(k)).find((v): v is FiredImpactEvent => !!v);
        if (impact) {
          const impactReceivedAt = Date.parse(impact.at);
          const impactAge = now - impactReceivedAt;
          // Current shot: impact arrived after firing with wall-clock delta  TOF
          const isCurrentShot = impactReceivedAt > firedAt &&
              Math.abs((impactReceivedAt - firedAt) - impact.timeOfFlight * 1000) < 10_000;
          // Previous shot, same fire mission: recent (<10 min) and aimed in the same direction (<25)
          const toImpactX = impact.impactX - launch.x;
          const toImpactY = impact.impactY - launch.y;
          const impactBearing = normDeg((Math.atan2(toImpactX, toImpactY) * 180) / Math.PI);
          const isPrevShot = !isCurrentShot && impactAge < 600_000 &&
              shortestAngleDiff(dirDeg, impactBearing) < 25;
          if (isCurrentShot || isPrevShot) {
            dstX = impact.impactX;
            dstY = impact.impactY;
          }
        }
      }

      if (!Number.isFinite(dstX) || !Number.isFinite(dstY)) return;

      const distance = Math.hypot(dstX - launch.x, dstY - launch.y);
      if (distance < 5) return;

      const launchAt = firedAt + profile.launchDelayMs;
      if (now < launchAt) return;

      // The ballistic model gives a more accurate ETA for artillery/rockets whose
      // time-of-flight is non-linear with range (high arc at short range, flat at long).
      // Fallback to fixed-speed formula for non-ballistic profiles (missiles, etc.).
      // For unguided weapons, prefer the actual TOF from the fired_impact report.
      let etaMs: number;
      if (!profile.requiresLock) {
        const impactKeys = [
          `${ev.unitId}|${ev.vehicleId}|${ev.weapon}|${ev.muzzle}`,
          `${ev.vehicleId}|${ev.weapon}|${ev.muzzle}`,
          `${ev.unitId}|${ev.weapon}|${ev.muzzle}`,
          `${ev.weapon}|${ev.muzzle}`,
        ];
        const impact = impactKeys.map(k => impactLookup.get(k)).find((v): v is FiredImpactEvent => !!v);
        if (impact) {
          const impactReceivedAt = Date.parse(impact.at);
          const impactAge = now - impactReceivedAt;
          const isCurrentShot = impactReceivedAt > firedAt &&
              Math.abs((impactReceivedAt - firedAt) - impact.timeOfFlight * 1000) < 10_000;
          const toImpactX = impact.impactX - launch.x;
          const toImpactY = impact.impactY - launch.y;
          const impactBearing = normDeg((Math.atan2(toImpactX, toImpactY) * 180) / Math.PI);
          const isPrevShot = !isCurrentShot && impactAge < 600_000 &&
              shortestAngleDiff(dirDeg, impactBearing) < 25;
          if (isCurrentShot || isPrevShot) {
            etaMs = impact.timeOfFlight * 1000;
          } else {
            etaMs = (profile.useBallistics && profile.ballA && profile.ballB)
              ? profile.ballA * Math.pow(Math.max(distance, 1), profile.ballB) * 1000
              : (distance / Math.max(profile.speedMps, 1)) * 1000;
          }
        } else {
          etaMs = (profile.useBallistics && profile.ballA && profile.ballB)
            ? profile.ballA * Math.pow(Math.max(distance, 1), profile.ballB) * 1000
            : (distance / Math.max(profile.speedMps, 1)) * 1000;
        }
      } else {
        etaMs = (profile.useBallistics && profile.ballA && profile.ballB)
          ? profile.ballA * Math.pow(Math.max(distance, 1), profile.ballB) * 1000
          : (distance / Math.max(profile.speedMps, 1)) * 1000;
      }
      const impactAt = launchAt + etaMs;
      const expiresAt = impactAt + profile.lingerAfterImpactMs;
      if (now >= expiresAt) return;

      const progress = clamp((now - launchAt) / etaMs, 0, 1);
      const worldPts = buildPredictedPath(launch.x, launch.y, dstX, dstY, profile.lateralFactor, progress);
      const mapPts = worldPts.map(([x, y]) => [y * scale, x * scale] as [number, number]);
      if (mapPts.length < 2) return;
      activeKeys.add(key);

      L.polyline(mapPts, {
        color: profile.color,
        weight: 2.2,
        opacity: 1,
        dashArray: '6,5',
        interactive: false,
        pane: 'athena-projectile',
      }).addTo(projectileLayerRef.current);

      const cur = mapPts[mapPts.length - 1];
  const etaSec = Math.max(0, Math.ceil((impactAt - now) / 1000));
      projectileLastEtaRef.current.set(key, etaSec);
      const sourceTag = projectileSourceTag(target);
      const shooterName = unit ? (unit.playerName?.trim() || unit.name || unit.id) : ev.unitId;
      const weaponLabel = ev.weapon?.trim() || ev.projectile?.trim() || ev.ammo?.trim() || 'Unknown';
      debugEntries.push({
        id: key.slice(-8),
        trackKey: key,
        shooter: shooterName,
        weapon: weaponLabel,
        lock: lockMode,
        source: sourceTag,
        etaSec,
        target: `${Math.round(dstX)},${Math.round(dstY)}`,
        lockSwitched: false,
      });

      L.marker(cur, {
        pane: 'athena-projectile',
        interactive: false,
        icon: L.divIcon({
          className: '',
          iconSize: [0, 0],
          iconAnchor: [0, 0],
          html: `<div style="width:8px;height:8px;border-radius:50%;background:${profile.color};box-shadow:0 0 6px ${profile.color};"></div>` +
            `<div style="margin-top:2px;white-space:nowrap;font-size:9px;font-weight:700;color:${profile.color};text-shadow:-1px 0 #000,1px 0 #000,0 -1px #000,0 1px #000;">${sourceTag} ETA ${etaSec}s</div>`,
        }),
      }).addTo(projectileLayerRef.current);
    });

    // Drop cached launch points for projectiles that are no longer active.
    for (const key of projectileLaunchRef.current.keys()) {
      if (!activeKeys.has(key)) projectileLaunchRef.current.delete(key);
    }
    for (const key of projectileLockRef.current.keys()) {
      if (!activeKeys.has(key)) projectileLockRef.current.delete(key);
    }
    for (const key of projectileLastEtaRef.current.keys()) {
      if (!activeKeys.has(key)) projectileLastEtaRef.current.delete(key);
    }
    for (const key of projectileTerminalRef.current.keys()) {
      if (projectileTerminalRef.current.get(key)! <= now) projectileTerminalRef.current.delete(key);
    }
    for (const key of projectileLockModeRef.current.keys()) {
      if (!activeKeys.has(key)) projectileLockModeRef.current.delete(key);
    }
    for (const key of projectileLockSwitchAtRef.current.keys()) {
      if (!activeKeys.has(key)) projectileLockSwitchAtRef.current.delete(key);
    }

    const switchFlashMs = 2500;
    const debugWithSwitch = debugEntries.map(entry => {
      const prev = projectileLockModeRef.current.get(entry.trackKey);
      const current = entry.lock;
      if (prev && prev !== current) {
        projectileLockSwitchAtRef.current.set(entry.trackKey, now);
      }
      projectileLockModeRef.current.set(entry.trackKey, current);

      const switchedAt = projectileLockSwitchAtRef.current.get(entry.trackKey);
      const lockSwitched = switchedAt !== undefined && now - switchedAt <= switchFlashMs;
      return {
        ...entry,
        lockSwitched,
      };
    });

    onProjectileDebugChange?.(debugWithSwitch.slice(0, 5));
  }, [firedEvents, firedImpacts, units, vehicles, groups, lazes, worldSize, clockMs, onProjectileDebugChange]);

  //  Vehicle markers 
  useEffect(() => {
    vehicleLayerRef.current.clearLayers();
    const scale = 100 / worldSize;
    const zoom = mapZoom;
    // Crew labels visible at display zoom >= 2.5x (internal zoom >= 9.2)
    const showCrew = zoom >= 9.2;
    Object.values(vehicles).forEach(veh => {
      if (veh.posX === 0 && veh.posY === 0) return;
      const ll: [number, number] = [veh.posY * scale, veh.posX * scale];
      // Resolve crew from units referencing this vehicle (crew array may be empty in live data)
      const occupants = Object.values(units).filter(u => u.vehicleId === veh.id);
      const crewNames = occupants.map(u => `${u.name} (${u.type})`).join(', ');
      const category = resolveVehicleCategory(veh.class, vehicleMap);
      L.marker(ll, { icon: vehicleIcon(veh, units, category), pane: 'athena-vehicle' })
        .bindTooltip(`<b>${veh.class}</b><br>${crewNames}`, { sticky: true })
        .addTo(vehicleLayerRef.current);

      const vehicleFiring = occupants.some(u => recentShooterIds.has(u.id));
      if (vehicleFiring) {
        L.marker(ll, {
          pane: 'athena-vehicle',
          interactive: false,
          icon: firingPulseIcon(30),
        }).addTo(vehicleLayerRef.current);
      }

      // Crew role labels at high zoom (C: Commander, G: Gunner, D: Driver)
      if (showCrew && occupants.length > 0) {
        const rolePrefix: Record<string, string> = { driver: 'D', gunner: 'G', commander: 'C', turret: 'T', cargo: 'P' };
        const roleOrder = Object.keys(rolePrefix);
        const sorted = [...occupants].sort((a, b) => {
          const ai = roleOrder.findIndex(r => a.type.toLowerCase().includes(r));
          const bi = roleOrder.findIndex(r => b.type.toLowerCase().includes(r));
          return (ai === -1 ? 99 : ai) - (bi === -1 ? 99 : bi);
        });
        const lines = sorted.map(u => {
          const rKey = roleOrder.find(r => u.type.toLowerCase().includes(r)) ?? 'cargo';
          const prefix = rolePrefix[rKey] ?? 'P';
          const name = u.playerName?.trim() || u.name || '';
          return `${prefix}: ${escapeHtml(name)}`;
        }).join('<br>');
        L.marker(ll, {
          pane: 'athena-vehicle',
          interactive: false,
          icon: L.divIcon({
            className: '',
            iconSize: [0, 0],
            iconAnchor: [-30, -4],
            html: `<div class="map-marker-label map-marker-label-crew">${lines}</div>`,
          }),
        }).addTo(vehicleLayerRef.current);
      }
    });
  }, [vehicles, units, worldSize, vehicleMap, mapZoom, recentShooterIds]);

  //  Unit markers 
  useEffect(() => {
    unitLayerRef.current.clearLayers();
    const scale = 100 / worldSize;
    Object.values(units).forEach(unit => {
      const mountedVehicle = unit.vehicleId ? vehicles[unit.vehicleId] : undefined;
      // Bus/in-game behavior: mounted infantry/pilots are represented by the vehicle icon only.
      if (mountedVehicle) return;
      const posX = unit.posX;
      const posY = unit.posY;
      if (posX === 0 && posY === 0) return;
      const ll: [number, number] = [posY * scale, posX * scale];
      const markerIcon = unitIcon(unit);
      L.marker(ll, { icon: markerIcon, pane: 'athena-unit' })
        .bindTooltip(`<b>${unit.name}</b><br>${unit.side}  ${unit.rank}<br>${unit.type}`, { sticky: true })
        .addTo(unitLayerRef.current);

      if (recentShooterIds.has(unit.id)) {
        L.marker(ll, {
          pane: 'athena-unit',
          interactive: false,
          icon: firingPulseIcon(24),
        }).addTo(unitLayerRef.current);
      }
    });
  }, [units, vehicles, worldSize, vehicleMap, recentShooterIds]);

  return null;
}

//  Main map component 

interface MapProps {
  units:      Record<string, Unit>;
  vehicles:   Record<string, Vehicle>;
  groups:     Record<string, Group>;
  lazes?:     ActiveLaze[];
  firedEvents?: FiredEvent[];
  firedImpacts?: FiredImpactEvent[];
  worldSize?: number;
  world?:     string;
  roads:      Road[];
  forests:    ForestsData | null;
  locations:  MapLocation[];
  structures: MapStructure[];
  elevations: ElevationsData | null;
  contours:   ContourLine[];
  layers:     LayerVisibility;
  onLayersChange?: Dispatch<SetStateAction<LayerVisibility>>;
  renderMode?: RenderMode;
  vehicleMap?:  Map<string, string>;
  locationMap?: Map<string, { DrawStyle: string; SizeText: number; Name: string }>;
  shorelineRefreshToken?: number;
  onRegisterFocus?: (fn: (posX: number, posY: number) => void) => void;
  onRegisterPan?: (fn: (posX: number, posY: number) => void) => void;
  onUserInteraction?: () => void;
}

const DEFAULT_MAP_CENTER: [number, number] = [50, 50];
const DEFAULT_MAP_ZOOM = 4;

//  Vertical zoom slider rendered inside the MapContainer (has access to the Leaflet map) 
function ZoomSliderControl() {
  const map = useMap();
  // Keep zoom focused on useful tactical ranges:
  // remove very far-out views, allow much deeper zoom-in, and use fine increments.
  // BASE_ZOOM=3 keeps the displayed x-scale stable vs previous behavior.
  const MIN_ZOOM = 3, MAX_ZOOM = 10.5, ZOOM_STEP = 0.1;
  const [zoom, setZoom] = useState(() => map.getZoom());
  const isDragging = useRef(false);
  const containerRef = useRef<HTMLDivElement>(null);

  // Prevent Leaflet from consuming mouse/wheel/touch events on the slider
  useEffect(() => {
    if (!containerRef.current) return;
    L.DomEvent.disableClickPropagation(containerRef.current);
    L.DomEvent.disableScrollPropagation(containerRef.current);
    const el = containerRef.current;
    const stopWheel = (e: WheelEvent) => e.stopPropagation();
    el.addEventListener('wheel', stopWheel, { passive: false });
    return () => el.removeEventListener('wheel', stopWheel);
  }, []);

  // Keep slider in sync when map zoom changes from scroll wheel / other sources
  useEffect(() => {
    const onZoom = () => {
      if (isDragging.current) return;
      setZoom(Math.round(map.getZoom() / ZOOM_STEP) * ZOOM_STEP);
    };
    map.on('zoomend', onZoom);
    return () => { map.off('zoomend', onZoom); };
  }, [map]);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const z = parseFloat(e.target.value);
    setZoom(z);
    map.setZoom(z, { animate: false });
  };

  const t = (zoom - MIN_ZOOM) / (MAX_ZOOM - MIN_ZOOM);
  const scale = 0.1 + t * (3.0 - 0.1);
  const scaleLabel = `${Math.round(scale * 10) / 10}`;

  return (
    <div ref={containerRef} style={{
      position: 'absolute', right: 12, top: '50%', transform: 'translateY(-50%)',
      zIndex: 1000, display: 'flex', flexDirection: 'column', alignItems: 'center',
      gap: 4, pointerEvents: 'auto',
      background: 'rgba(0,0,0,0.35)', borderRadius: 8, padding: '8px 6px',
    }}>
      <span style={{ color: 'rgba(255,255,255,0.7)', fontSize: 10 }}>+</span>
      <input
        type="range"
        min={MIN_ZOOM} max={MAX_ZOOM} step={ZOOM_STEP}
        value={zoom}
        onMouseDown={() => { isDragging.current = true; }}
        onMouseUp={() => { isDragging.current = false; }}
        onTouchEnd={() => { isDragging.current = false; }}
        onChange={handleChange}
        style={{
          appearance: 'slider-vertical' as React.CSSProperties['appearance'],
          WebkitAppearance: 'slider-vertical',
          width: 20,
          height: 180,
          direction: 'rtl' as React.CSSProperties['direction'],
          writingMode: 'vertical-lr' as React.CSSProperties['writingMode'],
          accentColor: '#d8cc9a',
          cursor: 'pointer',
          background: 'transparent',
        }}
      />
      <span style={{ color: 'rgba(255,255,255,0.7)', fontSize: 10 }}></span>
      <span style={{
        color: '#fff', fontSize: 11, fontWeight: 600, fontVariantNumeric: 'tabular-nums',
        marginTop: 2, minWidth: 36, textAlign: 'center',
      }}>{scaleLabel}</span>
    </div>
  );
}

//  User-interaction bridge  fires callback on manual drag / zoom 
function UserInteractionBridge({ onInteraction }: { onInteraction: () => void }) {
  const map = useMap();
  const callbackRef = useRef(onInteraction);
  callbackRef.current = onInteraction;

  useEffect(() => {
    const handler = () => callbackRef.current();
    map.on('dragstart', handler);
    map.on('zoomstart', handler);
    return () => {
      map.off('dragstart', handler);
      map.off('zoomstart', handler);
    };
  }, [map]);
  return null;
}

//  Focus bridge  lets sidebar pan the map to a world coordinate 
function FocusBridge({
  worldSize,
  onRegisterFocus,
  onRegisterPan,
}: {
  worldSize: number;
  onRegisterFocus?: (fn: (posX: number, posY: number) => void) => void;
  onRegisterPan?: (fn: (posX: number, posY: number) => void) => void;
}) {
  const map = useMap();
  useEffect(() => {
    if (!onRegisterFocus) return;
    onRegisterFocus((posX: number, posY: number) => {
      const scale = 100 / worldSize;
      map.setView([posY * scale, posX * scale], 10.5, { animate: true });
    });
  }, [map, worldSize, onRegisterFocus]);

  useEffect(() => {
    if (!onRegisterPan) return;
    onRegisterPan((posX: number, posY: number) => {
      const scale = 100 / worldSize;
      map.panTo([posY * scale, posX * scale], { animate: true });
    });
  }, [map, worldSize, onRegisterPan]);
  return null;
}

function CursorCoordinateBridge({
  worldSize,
  elevLookup,
  onChange,
}: {
  worldSize: number;
  elevLookup: { m: Map<string, number>; step: number } | null;
  onChange: (coords: { x: number; y: number; z: number | null } | null) => void;
}) {
  const map = useMap();

  useEffect(() => {
    const scale = 100 / worldSize;
    const onMove = (e: L.LeafletMouseEvent) => {
      const x = e.latlng.lng / scale;
      const y = e.latlng.lat / scale;
      const clampedX = Math.min(worldSize, Math.max(0, x));
      const clampedY = Math.min(worldSize, Math.max(0, y));
      let z: number | null = null;
      if (elevLookup) {
        const gx = Math.round(clampedX / elevLookup.step);
        const gy = Math.round(clampedY / elevLookup.step);
        z = elevLookup.m.get(`${gx},${gy}`) ?? null;
      }
      onChange({ x: clampedX, y: clampedY, z });
    };

    const onOut = () => onChange(null);

    map.on('mousemove', onMove);
    map.on('mouseout', onOut);

    return () => {
      map.off('mousemove', onMove);
      map.off('mouseout', onOut);
    };
  }, [map, worldSize, elevLookup, onChange]);

  return null;
}

// Keep startup/world-switch rendering deterministic: force Leaflet to recalc size,
// then restore the canonical map center/zoom used when a world is freshly loaded.
function StartupRecenterControl({ world, worldSize }: { world: string; worldSize: number }) {
  const map = useMap();
  const centeredKeyRef = useRef('');

  useEffect(() => {
    if (!world || !Number.isFinite(worldSize) || worldSize <= 0) return;

    const key = `${world}|${worldSize}`;
    if (centeredKeyRef.current === key) return;
    centeredKeyRef.current = key;

    const recenter = () => {
      map.invalidateSize({ pan: false, animate: false });
      map.setView(DEFAULT_MAP_CENTER, DEFAULT_MAP_ZOOM, { animate: false });
    };

    const scheduleRecenter = () => {
      window.requestAnimationFrame(() => {
        recenter();
        window.requestAnimationFrame(recenter);
      });
    };

    scheduleRecenter();
    const t1 = window.setTimeout(scheduleRecenter, 120);
    const t2 = window.setTimeout(scheduleRecenter, 350);
    const t3 = window.setTimeout(scheduleRecenter, 900);

    const container = map.getContainer();
    const resizeObserver = new ResizeObserver(() => {
      scheduleRecenter();
    });
    resizeObserver.observe(container);

    return () => {
      window.clearTimeout(t1);
      window.clearTimeout(t2);
      window.clearTimeout(t3);
      resizeObserver.disconnect();
    };
  }, [map, world, worldSize]);

  return null;
}

export function AthenaMap({
  units, vehicles, groups,
  lazes = [],
  firedEvents = [],
  firedImpacts = [],
  world = '', worldSize = 10240,
  roads = [], forests = null, locations = [], structures = [], elevations = null, contours = [],
  layers, onLayersChange, renderMode = '2d',
  vehicleMap = new Map(), locationMap = new Map(),
  shorelineRefreshToken = 0,
  onRegisterFocus,
  onRegisterPan,
  onUserInteraction,
}: MapProps) {
  const bounds: L.LatLngBoundsExpression = [[0, 0], [100, 100]];
  const [projectileDebugEntries, setProjectileDebugEntries] = useState<ProjectileDebugEntry[]>([]);
  const [cursorCoords, setCursorCoords] = useState<{ x: number; y: number; z: number | null } | null>(null);
  const elevLookup = useMemo(() => {
    if (!elevations || elevations.cells.length === 0) return null;
    const step = elevations.sampleSize;
    const m = new Map<string, number>();
    for (const cell of elevations.cells) {
      m.set(`${Math.round(cell.x / step)},${Math.round(cell.y / step)}`, cell.z);
    }
    return { m, step };
  }, [elevations]);

  return (
    <div style={{ position: 'relative', width: '100%', height: '100%' }}>
    <MapContainer
      center={DEFAULT_MAP_CENTER}
      zoom={DEFAULT_MAP_ZOOM}
      minZoom={3}
      maxZoom={10.5}
      maxBounds={bounds}
      maxBoundsViscosity={1}
      style={{ width: '100%', height: '100%', background: '#111' }}
      crs={L.CRS.Simple}
      zoomControl={false}
      zoomSnap={0.1}
      zoomDelta={0.25}
      wheelPxPerZoomLevel={80}
    >
      <ZoomSliderControl />
      <StartupRecenterControl world={world} worldSize={worldSize} />
      {(onRegisterFocus || onRegisterPan) && <FocusBridge worldSize={worldSize} onRegisterFocus={onRegisterFocus} onRegisterPan={onRegisterPan} />}
      {onUserInteraction && <UserInteractionBridge onInteraction={onUserInteraction} />}
      <CursorCoordinateBridge worldSize={worldSize} elevLookup={elevLookup} onChange={setCursorCoords} />
      <LayerManager
        units={units}
        vehicles={vehicles}
        groups={groups}
        lazes={lazes}
        firedEvents={firedEvents}
        firedImpacts={firedImpacts}
        world={world}
        worldSize={worldSize}
        roads={roads}
        forests={forests}
        locations={locations}
        structures={structures}
        elevations={elevations}
        contours={contours}
        layers={layers}
        onLayersChange={onLayersChange}
        renderMode={renderMode}
        vehicleMap={vehicleMap}
        locationMap={locationMap}
        shorelineRefreshToken={shorelineRefreshToken}
        onProjectileDebugChange={setProjectileDebugEntries}
      />
    </MapContainer>
    {projectileDebugEntries.length > 0 && (
      <div
        style={{
          position: 'absolute',
          top: 12,
          right: 58,
          zIndex: 1200,
          pointerEvents: 'none',
          minWidth: 320,
          maxWidth: 420,
          background: 'linear-gradient(180deg, rgba(30,40,26,0.88), rgba(12,18,12,0.92))',
          border: '1px solid rgba(193,214,160,0.55)',
          boxShadow: '0 0 0 1px rgba(12,18,10,0.7), 0 8px 18px rgba(0,0,0,0.45)',
          color: '#D9EBC2',
          borderRadius: 6,
          fontFamily: 'Consolas, "Lucida Console", monospace',
          overflow: 'hidden',
        }}
      >
        <div
          style={{
            display: 'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            padding: '6px 10px',
            fontSize: 11,
            letterSpacing: 1,
            background: 'rgba(162,189,120,0.12)',
            borderBottom: '1px solid rgba(193,214,160,0.35)',
            textTransform: 'uppercase',
            fontWeight: 700,
          }}
        >
          <span>Fire Control</span>
          <span style={{ color: '#FFCD7E' }}>Track Live</span>
        </div>
        <div style={{ padding: '6px 8px 8px 8px' }}>
          <div
            style={{
              display: 'grid',
              gridTemplateColumns: '1.7fr 0.8fr 0.8fr 0.7fr 1fr',
              columnGap: 8,
              fontSize: 10,
              opacity: 0.78,
              padding: '0 2px 5px 2px',
              borderBottom: '1px dashed rgba(193,214,160,0.25)',
              marginBottom: 4,
            }}
          >
            <span>Callsign</span>
            <span>Lock</span>
            <span>Src</span>
            <span>ETA</span>
            <span>Target</span>
          </div>
          {projectileDebugEntries.map(entry => (
            <div
              key={entry.trackKey}
              style={{
                display: 'grid',
                gridTemplateColumns: '1.7fr 0.8fr 0.8fr 0.7fr 1fr',
                columnGap: 8,
                fontSize: 11,
                padding: '3px 2px',
                borderBottom: '1px solid rgba(193,214,160,0.10)',
                background: entry.lockSwitched ? 'linear-gradient(90deg, rgba(255,94,94,0.22), rgba(255,180,120,0.04))' : 'transparent',
              }}
            >
              <span style={{ color: '#E8F4D1', whiteSpace: 'nowrap', overflow: 'hidden', textOverflow: 'ellipsis' }} title={`${entry.shooter}  ${entry.weapon}`}>{entry.shooter}</span>
              <span style={{ color: entry.lock === 'LAZE' ? '#FF9B9B' : entry.lock === 'UNIT' ? '#FFC778' : '#A9D3FF', fontWeight: 700 }}>{entry.lock}</span>
              <span>{entry.source}</span>
              <span>{entry.etaSec}s</span>
              <span>{entry.target}{entry.lockSwitched ? ' SW' : ''}</span>
            </div>
          ))}
        </div>
      </div>
    )}
    <div
      style={{
        position: 'absolute',
        right: 12,
        bottom: 12,
        zIndex: 1200,
        pointerEvents: 'none',
        padding: '5px 8px',
        borderRadius: 6,
        background: 'rgba(10,10,12,0.72)',
        border: '1px solid rgba(220,220,220,0.25)',
        color: '#f2f2ec',
        fontSize: 11,
        fontFamily: 'Consolas, "Lucida Console", monospace',
        fontVariantNumeric: 'tabular-nums',
      }}
    >
      {cursorCoords
        ? `X:${cursorCoords.x.toFixed(2)} Y:${cursorCoords.y.toFixed(2)}${cursorCoords.z !== null ? ` Z:${cursorCoords.z.toFixed(1)}` : ''}`
        : 'X:-- Y:--'}
    </div>
    </div>
  );
}
