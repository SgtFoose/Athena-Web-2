import { useState, useEffect } from 'react';
import type { StaticWorldInfo, ContourLine } from '../types/game';
import { API_BASE } from '../apiBase';

const API = `${API_BASE}/api/staticmap`;

/**
 * Fetches pre-computed contour data from the Athena Desktop cache via the
 * backend REST endpoints.
 *
 * On worldName change:
 *  1. Fetches /api/staticmap/{worldName} for metadata (cellSize, availableZ, …)
 *  2. Fetches all major contours (Z divisible by 20, plus sea level) in parallel.
 *
 * Contour lines are returned as ContourLine[], where each `lines` entry is a
 * flat array of alternating world-metre coordinates [x0,y0,x1,y1,...].
 * Convert to Leaflet LatLng: lat = y * scale, lng = x * scale.
 */
export function useStaticMap(worldName: string | null) {
  const [staticInfoState, setStaticInfoState] = useState<{ worldName: string; data: StaticWorldInfo } | null>(null);
  const [contoursState, setContoursState] = useState<{ worldName: string; data: ContourLine[] } | null>(null);

  // Fetch world metadata whenever the active world changes
  useEffect(() => {
    if (!worldName) return;

    let cancelled = false;
    fetch(`${API}/${encodeURIComponent(worldName)}`)
      .then(r => (r.ok ? r.json() : null))
      .then((data: StaticWorldInfo | null) => {
        if (!cancelled && data) {
          setStaticInfoState({ worldName, data });
        }
      })
      .catch(() => {});

    return () => { cancelled = true; };
  }, [worldName]);

  // Once we have metadata, fetch major contour levels in parallel
  useEffect(() => {
    if (!worldName || !staticInfoState || staticInfoState.worldName !== worldName) return;

    let cancelled = false;
    const { data: staticInfo } = staticInfoState;

    // Major = sea level + every 20 m (index contours every 100 m are a subset)
    const majorZ = staticInfo.availableZ.filter(z => z === 0 || z % 20 === 0);

    Promise.all(
      majorZ.map(z =>
        fetch(`${API}/${encodeURIComponent(staticInfo.worldName)}/contours/${z}`)
          .then(r => (r.ok ? (r.json() as Promise<ContourLine>) : null))
          .catch(() => null),
      ),
    ).then(results => {
      if (!cancelled) {
        setContoursState({
          worldName,
          data: results.filter((r): r is ContourLine => r !== null),
        });
      }
    });

    return () => { cancelled = true; };
  }, [staticInfoState, worldName]);

  const staticInfo = worldName && staticInfoState?.worldName === worldName
    ? staticInfoState.data
    : null;
  const contours = worldName && contoursState?.worldName === worldName
    ? contoursState.data
    : [];

  return { staticInfo, contours };
}
