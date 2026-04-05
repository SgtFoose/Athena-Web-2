import { useState, useMemo } from 'react';
import type { GameFrame, Group, Unit, MapLocation, ServerSettings } from '../types/game';
import type { LayerVisibility, RenderMode } from '../App';
import { APP_VERSION } from '../version';

//  Helpers 

function formatXY(posX: number, posY: number): string {
  return `X:${posX.toFixed(2)} Y:${posY.toFixed(2)}`;
}

function sideColor(side: string): string {
  switch (side.toLowerCase()) {
    case 'west':     return '#4e9de0';
    case 'east':     return '#d93b3b';
    case 'guer':     return '#4ec94e';
    case 'civ':      return '#9b59b6';
    default:         return '#888';
  }
}

//  Styles 

const tabBarStyle: React.CSSProperties = {
  display: 'flex', gap: 0, background: '#0d0d18', borderBottom: '1px solid #222',
};

function tabStyle(active: boolean): React.CSSProperties {
  return {
    flex: 1, padding: '7px 0', textAlign: 'center', cursor: 'pointer',
    fontSize: 11, fontWeight: 700, letterSpacing: '0.08em',
    background: active ? '#1a1a2e' : 'transparent',
    color: active ? '#2ecc71' : '#555',
    border: 'none', borderBottom: active ? '2px solid #2ecc71' : '2px solid transparent',
  };
}

const panelStyle: React.CSSProperties = {
  background: '#161622', borderRadius: 6, padding: 10, fontSize: 12,
};

const sectionLabel: React.CSSProperties = {
  color: '#888', marginBottom: 8, fontWeight: 600, letterSpacing: '0.05em', fontSize: 11,
};

//  Props 

interface Props {
  frame: GameFrame | null;
  connected: boolean;
  onRequestWorld: () => void;
  roadCount: number;
  treeCount: number;
  forestCellCount: number;
  locationCount: number;
  structureCount: number;
  elevationCellCount: number;
  cachedWorlds: string[];
  selectedWorld: string;
  liveWorld: string;
  onSelectWorld: (worldName: string) => void;
  layers: LayerVisibility;
  onToggleLayer: (key: keyof LayerVisibility) => void;
  followActivePlayer: boolean;
  activePlayerName: string | null;
  onToggleFollowActivePlayer: () => void;
  renderMode: RenderMode;
  onChangeRenderMode: (m: RenderMode) => void;
  serverSettings: ServerSettings;
  locations: MapLocation[];
  groups: Record<string, Group>;
  units: Record<string, Unit>;
  onFocusPosition: (posX: number, posY: number) => void;
}

type TopTab = 'MAP' | 'ORBAT';
type MapSubTab = 'COMMON' | 'LOCATIONS';
type OrbatSide = 'WEST' | 'EAST' | 'GUER' | 'CIV';

//  Component 

export function Sidebar({
  frame, connected, onRequestWorld,
  roadCount, treeCount, forestCellCount, locationCount, structureCount, elevationCellCount,
  cachedWorlds, selectedWorld, liveWorld, onSelectWorld,
  layers, onToggleLayer, followActivePlayer, activePlayerName, onToggleFollowActivePlayer, renderMode, onChangeRenderMode,
  serverSettings, locations, groups, units, onFocusPosition,
}: Props) {
  const [topTab, setTopTab]       = useState<TopTab>('MAP');
  const [mapSub, setMapSub]       = useState<MapSubTab>('COMMON');
  const [orbatSide, setOrbatSide] = useState<OrbatSide>('WEST');

  //  Computed data 

  const t = frame?.time;
  const timeStr = t
    ? `${t.year}/${String(t.month).padStart(2,'0')}/${String(t.day).padStart(2,'0')} ${String(t.hour).padStart(2,'0')}:${String(t.minute).padStart(2,'0')}`
    : '--';

  const unitCount  = frame ? Object.keys(frame.units).length : 0;
  const vehCount   = frame ? Object.keys(frame.vehicles).length : 0;
  const groupCount = frame ? Object.keys(frame.groups).length : 0;

  // Groups filtered by currently selected ORBAT side
  const sideKey = orbatSide.toLowerCase();
  const sideGroups = useMemo(() => {
    const unitList = Object.values(units);
    // Map groupId  side from the group's leader unit
    const groupSides = new Map<string, string>();
    for (const u of unitList) {
      if (u.id === u.leaderId || !groupSides.has(u.groupId)) {
        groupSides.set(u.groupId, u.side.toLowerCase());
      }
    }
    return Object.values(groups).filter(g => groupSides.get(g.id) === sideKey);
  }, [groups, units, sideKey]);

  // Sorted locations for the LOCATIONS panel
  const sortedLocations = useMemo(
    () => [...locations].filter(l => l.name).sort((a, b) => a.name.localeCompare(b.name)),
    [locations],
  );

  // Render

  return (
    <div style={{ display: 'flex', flexDirection: 'column', height: '100%', overflow: 'hidden' }}>
      {/* Connection + mission header */}
      <div style={{ padding: '12px 16px 0' }}>
        <div style={{ display: 'flex', alignItems: 'center', gap: 8, marginBottom: 8 }}>
          <div style={{
            width: 10, height: 10, borderRadius: '50%',
            background: connected ? '#2ecc71' : '#e74c3c',
          }} />
          <span style={{ color: '#ccc', fontSize: 13 }}>
            {connected ? 'Connected' : 'Disconnected'}
          </span>
        </div>

        {frame?.mission && (
          <div style={{ background: '#1e2e1e', borderRadius: 6, padding: 10, marginBottom: 8 }}>
            <div style={{ color: '#2ecc71', fontWeight: 700, fontSize: 14 }}>{frame.mission.name}</div>
            <div style={{ color: '#aaa', fontSize: 12 }}>by {frame.mission.author}</div>
            <div style={{ color: '#7fb069', fontSize: 12 }}>Map: {frame.mission.world}</div>
          </div>
        )}

        <div style={{ color: '#ddd', fontSize: 13, marginBottom: 8 }}>Time: {timeStr}</div>

        {/* Stats row */}
        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr 1fr', gap: 8, textAlign: 'center', marginBottom: 8 }}>
          {[
            { label: 'Groups',   value: groupCount },
            { label: 'Units',    value: unitCount },
            { label: 'Vehicles', value: vehCount },
          ].map(s => (
            <div key={s.label} style={{ background: '#1e2e1e', borderRadius: 6, padding: 8 }}>
              <div style={{ color: '#2ecc71', fontSize: 18, fontWeight: 700 }}>{s.value}</div>
              <div style={{ color: '#888', fontSize: 11 }}>{s.label}</div>
            </div>
          ))}
        </div>
      </div>

      {/*  Top-level tabs: MAP | ORBAT  */}
      <div style={tabBarStyle}>
        {(['MAP', 'ORBAT'] as TopTab[]).map(tb => (
          <button key={tb} style={tabStyle(topTab === tb)} onClick={() => setTopTab(tb)}>{tb}</button>
        ))}
      </div>

      {/*  Scrollable body  */}
      <div style={{ flex: 1, overflowY: 'auto', padding: '8px 16px 16px' }}>
        {topTab === 'MAP' && (
          <>
            {/* Sub-tabs: COMMON | LOCATIONS */}
            <div style={{ ...tabBarStyle, marginBottom: 10, borderBottom: 'none' }}>
              {(['COMMON', 'LOCATIONS'] as MapSubTab[]).map(st => (
                <button key={st} style={tabStyle(mapSub === st)} onClick={() => setMapSub(st)}>{st}</button>
              ))}
            </div>

            {mapSub === 'COMMON' && <CommonPanel
              layers={layers} onToggleLayer={onToggleLayer}
              followActivePlayer={followActivePlayer}
              activePlayerName={activePlayerName}
              onToggleFollowActivePlayer={onToggleFollowActivePlayer}
              renderMode={renderMode} onChangeRenderMode={onChangeRenderMode}
              onRequestWorld={onRequestWorld} connected={connected}
              roadCount={roadCount} treeCount={treeCount} forestCellCount={forestCellCount}
              locationCount={locationCount} structureCount={structureCount}
              elevationCellCount={elevationCellCount}
              cachedWorlds={cachedWorlds}
              selectedWorld={selectedWorld}
              liveWorld={liveWorld}
              onSelectWorld={onSelectWorld}
            />}

            {mapSub === 'LOCATIONS' && <LocationsPanel
              locations={sortedLocations} onFocusPosition={onFocusPosition}
            />}
          </>
        )}

        {topTab === 'ORBAT' && (
          <>
            {/* Side sub-tabs */}
            <div style={{ ...tabBarStyle, marginBottom: 10, borderBottom: 'none' }}>
              {(['WEST', 'EAST', 'GUER', 'CIV'] as OrbatSide[]).map(side => {
                const disabled =
                  (side === 'EAST' && !serverSettings.showEast) ||
                  (side === 'GUER' && !serverSettings.showGuer) ||
                  (side === 'CIV'  && !serverSettings.showCiv);
                return (
                  <button
                    key={side}
                    style={{
                      ...tabStyle(orbatSide === side),
                      color: orbatSide === side
                        ? sideColor(side === 'CIV' ? 'civ' : side)
                        : disabled ? '#333' : '#555',
                      borderBottomColor: orbatSide === side
                        ? sideColor(side === 'CIV' ? 'civ' : side)
                        : 'transparent',
                      cursor: disabled ? 'not-allowed' : 'pointer',
                      opacity: disabled ? 0.4 : 1,
                    }}
                    onClick={() => !disabled && setOrbatSide(side)}
                  >{side}</button>
                );
              })}
            </div>

            <OrbatPanel
              side={orbatSide}
              groups={sideGroups}
              units={units}
              onFocusPosition={onFocusPosition}
              serverSettings={serverSettings}
            />
          </>
        )}
      </div>

      {/* Footer  version */}
      <div style={{ padding: '6px 16px', borderTop: '1px solid #222', color: '#444', fontSize: 10, textAlign: 'center' }}>
        v{APP_VERSION}
      </div>
    </div>
  );
}

//  COMMON panel 

function CommonPanel({
  layers, onToggleLayer, followActivePlayer, activePlayerName, onToggleFollowActivePlayer, renderMode, onChangeRenderMode,
  onRequestWorld, connected,
  roadCount, treeCount, forestCellCount, locationCount, structureCount, elevationCellCount,
  cachedWorlds, selectedWorld, liveWorld, onSelectWorld,
}: {
  layers: LayerVisibility; onToggleLayer: (key: keyof LayerVisibility) => void;
  followActivePlayer: boolean;
  activePlayerName: string | null;
  onToggleFollowActivePlayer: () => void;
  renderMode: RenderMode; onChangeRenderMode: (m: RenderMode) => void;
  onRequestWorld: () => void; connected: boolean;
  roadCount: number; treeCount: number; forestCellCount: number; locationCount: number;
  structureCount: number; elevationCellCount: number;
  cachedWorlds: string[];
  selectedWorld: string;
  liveWorld: string;
  onSelectWorld: (worldName: string) => void;
}) {
  const isLive = Boolean(liveWorld);
  const displayWorld = liveWorld || selectedWorld || '';

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 10 }}>
      {/* World selector */}
      <div style={panelStyle}>
        <div style={sectionLabel}>MAP</div>
        {isLive ? (
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: 2 }}>
            <span style={{ color: '#888', fontSize: 11 }}>Live</span>
            <span style={{ color: '#2ecc71', fontSize: 12, fontWeight: 700 }}>{displayWorld}</span>
          </div>
        ) : (
          <>
            <select
              value={selectedWorld}
              onChange={(e) => onSelectWorld(e.target.value)}
              style={{
                width: '100%',
                boxSizing: 'border-box',
                background: '#1e2e1e',
                color: '#ddd',
                border: '1px solid #333',
                borderRadius: 4,
                padding: '5px 6px',
                fontSize: 12,
                marginBottom: 2,
              }}
            >
              <option value=""> select a map </option>
              {cachedWorlds.map(w => (
                <option key={w} value={w}>{w}</option>
              ))}
            </select>
            {cachedWorlds.length === 0 && (
              <div style={{ color: '#f0a500', fontSize: 10, lineHeight: 1.4, marginTop: 2 }}>
                No cached maps found. Use Athena Desktop to export a map first.
              </div>
            )}
          </>
        )}
      </div>

      {/* Map style */}
      <div style={panelStyle}>
        <div style={sectionLabel}>MAP STYLE</div>
        <div style={{ display: 'flex', gap: 4 }}>
          {([
            { id: '2d',       label: '2D' },
            { id: 'heatmap1', label: 'Ground' },
            { id: 'heatmap2', label: 'Pilot' },
          ] as { id: RenderMode; label: string }[]).map(({ id, label }) => (
            <button
              key={id}
              onClick={() => onChangeRenderMode(id)}
              style={{
                flex: 1, padding: '4px 0', fontSize: 11, fontWeight: 600,
                borderRadius: 4, border: '1px solid #333', cursor: 'pointer',
                background: renderMode === id ? '#2ecc71' : '#1e2e1e',
                color:      renderMode === id ? '#000'    : '#aaa',
              }}
            >{label}</button>
          ))}
        </div>
      </div>

      {/* Map layers */}
      <div style={panelStyle}>
        <div style={sectionLabel}>MAP LAYERS</div>
        {([
          { key: 'contours',   label: 'Contours' },
          { key: 'forest',     label: 'Forest Overlay' },
          { key: 'trees',      label: 'Trees' },
          { key: 'roads',      label: 'Roads' },
          { key: 'structures', label: 'Structures' },
          { key: 'locations',  label: 'Locations' },
          { key: 'groups',     label: 'Groups' },
          { key: 'waypoints',  label: 'Waypoints' },
          { key: 'vehicles',   label: 'Vehicles' },
          { key: 'units',      label: 'Units' },
        ] as { key: keyof LayerVisibility; label: string }[]).map(({ key, label }) => (
          <label key={key} style={{
            display: 'flex', alignItems: 'center', gap: 8,
            marginBottom: 5, cursor: 'pointer', userSelect: 'none',
          }}>
            <input
              type="checkbox"
              checked={layers[key]}
              onChange={() => onToggleLayer(key)}
              style={{ accentColor: '#2ecc71', width: 13, height: 13, cursor: 'pointer' }}
            />
            <span style={{ color: layers[key] ? '#ccc' : '#555' }}>{label}</span>
          </label>
        ))}
      </div>

      {/* Export button */}
      <button
        onClick={onRequestWorld}
        disabled={!connected}
        style={{
          background: connected ? '#2c5f2e' : '#333',
          color: connected ? '#fff' : '#777',
          border: 'none', borderRadius: 6, padding: '8px 12px',
          cursor: connected ? 'pointer' : 'not-allowed', fontSize: 13,
        }}
      >Export World Data</button>

      <button
        onClick={onToggleFollowActivePlayer}
        disabled={!activePlayerName}
        style={{
          background: !activePlayerName ? '#333' : followActivePlayer ? '#2ecc71' : '#1e2e1e',
          color: !activePlayerName ? '#777' : followActivePlayer ? '#000' : '#ddd',
          border: '1px solid #2ecc71', borderRadius: 6, padding: '8px 12px',
          cursor: activePlayerName ? 'pointer' : 'not-allowed', fontSize: 13,
        }}
      >
        {followActivePlayer ? 'Following Active Player' : 'Follow Active Player'}
        {activePlayerName ? `: ${activePlayerName}` : ' (unavailable)'}
      </button>

      {/* Geometry status */}
      <div style={{ ...panelStyle, background: '#1a1a2e' }}>
        <div style={sectionLabel}>MAP DATA</div>
        {[
          { label: 'Roads',      value: roadCount,          unit: 'segments' },
          { label: 'Trees',      value: treeCount,          unit: 'points' },
          { label: 'Forest',     value: forestCellCount,    unit: 'cells' },
          { label: 'Locations',  value: locationCount,      unit: 'labels' },
          { label: 'Structures', value: structureCount,     unit: 'objects' },
          { label: 'Elevations', value: elevationCellCount, unit: 'cells' },
        ].map(g => (
          <div key={g.label} style={{ display: 'flex', justifyContent: 'space-between', marginBottom: 3 }}>
            <span style={{ color: '#aaa' }}>{g.label}</span>
            <span style={{ color: g.value > 0 ? '#2ecc71' : '#555' }}>
              {g.value > 0 ? `${g.value.toLocaleString()} ${g.unit}` : ''}
            </span>
          </div>
        ))}
      </div>
    </div>
  );
}

//  LOCATIONS panel 

function LocationsPanel({
  locations, onFocusPosition,
}: {
  locations: MapLocation[];
  onFocusPosition: (posX: number, posY: number) => void;
}) {
  if (locations.length === 0) {
    return <div style={{ color: '#555', fontSize: 12 }}>No locations loaded.</div>;
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 2 }}>
      {locations.map((loc, i) => (
        <button
          key={`${loc.name}-${i}`}
          onClick={() => onFocusPosition(loc.posX, loc.posY)}
          style={{
            display: 'flex', justifyContent: 'space-between', alignItems: 'center',
            background: '#1e1e2e', border: 'none', borderRadius: 4,
            padding: '5px 8px', cursor: 'pointer', textAlign: 'left',
          }}
        >
          <span style={{ color: '#ddd', fontWeight: 700, fontSize: 12 }}>{loc.name}</span>
          <span style={{ color: '#666', fontSize: 11, fontFamily: 'monospace' }}>
            {formatXY(loc.posX, loc.posY)}
          </span>
        </button>
      ))}
    </div>
  );
}

//  ORBAT panel 

function OrbatPanel({
  side, groups, units, onFocusPosition, serverSettings,
}: {
  side: OrbatSide;
  groups: Group[];
  units: Record<string, Unit>;
  onFocusPosition: (posX: number, posY: number) => void;
  serverSettings: ServerSettings;
}) {
  const disabled =
    (side === 'EAST' && !serverSettings.showEast) ||
    (side === 'GUER' && !serverSettings.showGuer) ||
    (side === 'CIV'  && !serverSettings.showCiv);

  if (disabled) {
    return (
      <div style={{ color: '#555', fontSize: 12, padding: 8 }}>
        {side} side is disabled by the server admin.
      </div>
    );
  }

  if (groups.length === 0) {
    return <div style={{ color: '#555', fontSize: 12 }}>No groups on this side.</div>;
  }

  const color = sideColor(side === 'CIV' ? 'civ' : side);

  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 6 }}>
      {groups.map(g => {
        // Find units in this group
        const groupUnits = Object.values(units).filter(u => u.groupId === g.id);
        // Leader position for pan
        const leader = groupUnits.find(u => u.id === g.leaderId) ?? groupUnits[0];
        const posX = leader?.posX ?? 0;
        const posY = leader?.posY ?? 0;

        return (
          <div key={g.id} style={{ background: '#1e1e2e', borderRadius: 4, borderLeft: `3px solid ${color}` }}>
            {/* Group header  click to focus */}
            <button
              onClick={() => onFocusPosition(posX, posY)}
              style={{
                display: 'flex', justifyContent: 'space-between', width: '100%',
                background: 'transparent', border: 'none', padding: '6px 8px',
                cursor: 'pointer', textAlign: 'left',
              }}
            >
              <span style={{ color, fontWeight: 700, fontSize: 12 }}>{g.name || g.id}</span>
              <span style={{ color: '#555', fontSize: 10, fontFamily: 'monospace' }}>
                {formatXY(posX, posY)}  {groupUnits.length}
              </span>
            </button>
            {/* Unit list */}
            {groupUnits.length > 0 && (
              <div style={{ padding: '0 8px 6px', display: 'flex', flexDirection: 'column', gap: 2 }}>
                {groupUnits.map(u => (
                  <div key={u.id} style={{ fontSize: 11, display: 'flex', gap: 4 }}>
                    <span style={{ color: u.playerName ? '#fff' : '#aaa', fontWeight: u.playerName ? 600 : 400 }}>
                      {u.name}
                    </span>
                    {u.playerName && <span style={{ color: '#666' }}>({u.playerName})</span>}
                  </div>
                ))}
              </div>
            )}
          </div>
        );
      })}
    </div>
  );
}
