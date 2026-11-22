import type { KilledEvent, FiredEvent } from '../types/game';

interface Props {
  kills: KilledEvent[];
  fired: FiredEvent[];
}

export function EventFeed({ kills, fired }: Props) {
  return (
    <div style={{ display: 'flex', flexDirection: 'column', gap: 8, height: '100%', overflowY: 'auto' }}>
      <div>
        <h3 style={{ color: '#e74c3c', margin: '0 0 6px' }}>☠ Kills</h3>
        {kills.length === 0 && <p style={{ color: '#888', fontSize: 12 }}>No kills yet</p>}
        {kills.map((k, i) => (
          <div key={i} style={{ padding: '4px 8px', background: '#2a1a1a', borderRadius: 4, marginBottom: 4, fontSize: 12 }}>
            <span style={{ color: '#e74c3c' }}><b>{k.killer || '?'}</b></span>
            {' '}eliminated{' '}
            <span style={{ color: '#fff' }}><b>{k.victim}</b></span>
          </div>
        ))}
      </div>
      <div style={{ marginTop: 8 }}>
        <h3 style={{ color: '#f39c12', margin: '0 0 6px' }}>🔫 Recent Shots</h3>
        {fired.length === 0 && <p style={{ color: '#888', fontSize: 12 }}>No shots fired</p>}
        {fired.slice(0, 30).map((f, i) => (
          <div key={i} style={{ padding: '2px 8px', background: '#1a1a2a', borderRadius: 4, marginBottom: 2, fontSize: 11 }}>
            <span style={{ color: '#f39c12' }}>{f.weapon}</span> · {f.ammo}
          </div>
        ))}
      </div>
    </div>
  );
}
