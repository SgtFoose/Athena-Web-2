import { useEffect, useState, useCallback } from 'react';
import { API_BASE } from '../apiBase';

export interface MapCacheWorld {
  name: string;
  hasMapTxt: boolean;
}

export interface HealthStatus {
  relay: {
    connected: boolean;
    host: string;
    port: number;
  };
  mapCache: {
    path: string;
    exists: boolean;
    worldCount: number;
    worlds: MapCacheWorld[];
  };
  activeWorld: string | null;
  activeWorldCached: boolean;
  tips: string[];
}

/**
 * Poll the bridge /api/health endpoint and surface map-cache status to the UI.
 * Polls once on mount and then every `intervalMs` (default 10 s).
 */
export function useHealthCheck(intervalMs = 10_000) {
  const [health, setHealth] = useState<HealthStatus | null>(null);
  const [error, setError] = useState<string | null>(null);

  const fetchHealth = useCallback(async () => {
    try {
      const res = await fetch(`${API_BASE}/api/health`);
      // Bridge returns 503 when health is degraded, but the body is still valid JSON
      const data: HealthStatus = await res.json();
      setHealth(data);
      setError(null);
    } catch (err) {
      setHealth(null);
      setError(err instanceof Error ? err.message : 'Failed to reach bridge');
    }
  }, []);

  useEffect(() => {
    let cancelled = false;
    const poll = () => {
      if (!cancelled) fetchHealth();
    };
    // Defer initial fetch to avoid synchronous setState during effect
    const initialTimer = setTimeout(poll, 0);
    const id = setInterval(poll, intervalMs);
    return () => {
      cancelled = true;
      clearTimeout(initialTimer);
      clearInterval(id);
    };
  }, [fetchHealth, intervalMs]);

  return { health, error, refetch: fetchHealth };
}
