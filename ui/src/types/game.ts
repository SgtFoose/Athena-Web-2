// Mirrors the C# models in the backend exactly

export interface Mission {
  name: string;
  author: string;
  world: string;
  description: string;
  isMulti: boolean;
  player: string;
  steamId: string;
}

export interface WorldInfo {
  nameDisplay: string;
  nameWorld: string;
  author: string;
  size: number;       // worldSize in metres (e.g. 30720 for Altis)
  forestMin: number;
  offsetX: number;
  offsetY: number;
  centerX: number;
  centerY: number;
}

export interface Road {
  id: string;
  type: string;       // "", "main road", "track", "hide", etc.
  foot: boolean;
  bridge: boolean;
  posX: number;       // object centre X (metres) — from getPosASL
  posY: number;       // object centre Y (metres) — from getPosASL
  beg1X: number;
  beg1Y: number;
  end2X: number;
  end2Y: number;
  width: number;
  length: number;
  dir: number;
}

export interface ForestCell {
  x: number;
  y: number;
  level: number;      // 1-3
}

export interface ForestsData {
  sampleSize: number;
  cells: ForestCell[];
}

export interface MapLocation {
  type: string;       // e.g. "NameCity", "NameVillage"
  name: string;
  posX: number;
  posY: number;
  dir: number;
  sizeX: number;
  sizeY: number;
}

export interface MapStructure {
  id: string;
  type: string;       // "house", "fence", "church", "wall", etc.
  model: string;      // model filename
  posX: number;
  posY: number;
  dir: number;        // degrees
  width: number;      // bounding box X (metres)
  length: number;     // bounding box Y (metres)
  height: number;     // bounding box Z (metres)
}

export interface ElevationCell {
  x: number;
  y: number;
  z: number;          // height ASL in metres
}

export interface ElevationsData {
  sampleSize: number;
  worldSize:  number;
  cells:      ElevationCell[];
}

export interface GameTime {
  year: number;
  month: number;
  day: number;
  hour: number;
  minute: number;
}

export interface Group {
  id: string;
  leaderId: string;
  name: string;
  wpX: number;
  wpY: number;
  wpType: string;
}

export interface Unit {
  id: string;
  groupId: string;
  leaderId: string;
  vehicleId: string;
  playerName: string;
  sessionId: string;
  steamId: string;
  name: string;
  faction: string;
  side: string;
  team: string;
  type: string;
  rank: string;
  isActivePlayer?: boolean;
  hasMediKit: boolean;
  weaponPrimary: string;
  weaponSecondary: string;
  weaponHandgun: string;
  posX: number;
  posY: number;
  posZ: number;
  dir: number;
  speed: number;
}

export interface CrewMember {
  unitId: string;
  role: string;
}

export interface Vehicle {
  id: string;
  class: string;
  crew: CrewMember[];
  posX: number;
  posY: number;
  posZ: number;
  dir: number;
  speed: number;
}

export interface ActiveLaze {
  unitId: string;
  posX: number;
  posY: number;
}

export interface RelayMarker {
  brush: string;
  color: string;
  shape: string;
  type: string;
  name: string;
  text: string;
  alpha: number;
  dir: number;
  posX: number;
  posY: number;
  posZ: number;
  sizeX: number;
  sizeY: number;
}

export interface FiredEvent {
  unitId: string;
  vehicleId: string;
  weapon: string;
  muzzle: string;
  mode: string;
  ammo: string;
  magazine: string;
  projectile: string;
  targetX?: number | null;
  targetY?: number | null;
  targetSource?: string;
  targetEntityId?: string;
  targetAmbiguous?: boolean;
  fireDir?: number;
  at: string;
}

export interface KilledEvent {
  victim: string;
  killer: string;
  instigator: string;
  at: string;
}

export interface FiredImpactEvent {
  unitId: string;
  vehicleId: string;
  weapon: string;
  muzzle: string;
  impactX: number;
  impactY: number;
  timeOfFlight: number;
  at: string;
}

export interface GameFrame {
  mission: Mission | null;
  world: WorldInfo | null;
  time: GameTime | null;
  groups: Record<string, Group>;
  units: Record<string, Unit>;
  vehicles: Record<string, Vehicle>;
  markers: RelayMarker[];
  lazes: ActiveLaze[];
  fired: FiredEvent[];
  killed: KilledEvent[];
}

// ── Server admin settings (synced across all browsers via SignalR) ────────

export interface ServerSettings {
  showEast: boolean;
  showGuer: boolean;
  showCiv:  boolean;
}

// ── Export status (tracks ongoing world-data export progress) ────────────

export interface ExportStatus {
  phase:              string;  // idle | exporting | cached | complete
  roadCount:          number;
  roadsComplete:      boolean;
  treeCount:          number;
  treesComplete:      boolean;
  forestCount:        number;
  forestsComplete:    boolean;
  locationCount:      number;
  locationsComplete:  boolean;
  structureCount:     number;
  structuresComplete: boolean;
  elevationCount:     number;
  elevationsComplete: boolean;
}

// ── Static Athena cache types ────────────────────────────────────────────────

/** Metadata returned from GET /api/staticmap/{worldName} */
export interface StaticWorldInfo {
  worldName:  string;
  worldSize:  number;
  cellSize:   number;    // metres per height grid cell (8 for Altis)
  maxZ:       number;
  minZ:       number;
  availableZ: number[];  // sorted list of available elevation levels
  hasTrees:   boolean;
}

/** One elevation level from GET /api/staticmap/{worldName}/contours/{z}.
 *  lines: flat arrays of alternating world-metres [x0,y0,x1,y1,...] */
export interface ContourLine {
  z:     number;
  major: boolean;
  lines: number[][];
}
