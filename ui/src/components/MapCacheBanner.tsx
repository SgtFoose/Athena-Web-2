import type { HealthStatus } from '../hooks/useHealthCheck';

interface Props {
  health: HealthStatus | null;
  healthError: string | null;
  activeWorld: string;
  onDismiss: () => void;
}

/**
 * Banner shown when the Athena Desktop map cache is missing or incomplete.
 * Renders at the top of the map area with first-time-setup instructions.
 */
export function MapCacheBanner({ health, healthError, activeWorld, onDismiss }: Props) {
  // Don't show if we haven't loaded health yet and there's no error
  if (!health && !healthError) return null;

  // Determine the problem (if any)
  const cacheExists = health?.mapCache?.exists ?? false;
  const worldCount = health?.mapCache?.worldCount ?? 0;
  const activeWorldCached = health?.activeWorldCached ?? false;
  const cachePath = health?.mapCache?.path ?? '%USERPROFILE%\\Documents\\Athena\\Maps';

  // World-specific: active world not cached but cache folder exists with other worlds
  const worldMissing = activeWorld && cacheExists && worldCount > 0 && !activeWorldCached;
  // Cache folder completely missing
  const cacheNotFound = health && !cacheExists;
  // Cache folder exists but empty
  const cacheEmpty = health && cacheExists && worldCount === 0;

  // Nothing wrong ΓÇö don't show
  if (health && !worldMissing && !cacheNotFound && !cacheEmpty && !healthError) return null;

  let title: string;
  let message: string;

  if (healthError) {
    title = 'Cannot reach AthenaWeb bridge';
    message = `The UI could not connect to the bridge server to check map cache status. Make sure AthenaWeb.exe is running.\n\nError: ${healthError}`;
  } else if (cacheNotFound) {
    title = 'Map cache not found';
    message =
      `No Athena map cache folder was found at:\n${cachePath}\n\n` +
      'This means static map layers (roads, buildings, trees, contour lines, locations) will not render.\n\n' +
      'To fix this:\n' +
      '1. Download and install the original Athena Desktop app\n' +
      '2. Launch Arma 3 with the @Athena mod and join a server\n' +
      '3. Open Athena Desktop and connect to the server\n' +
      '4. Click "Export" to cache the map data for that world\n' +
      '5. Restart AthenaWeb.exe ΓÇö the cached map will be detected automatically\n\n' +
      'You only need to do this once per map. The cache is stored locally and reused every time.';
  } else if (cacheEmpty) {
    title = 'Map cache is empty';
    message =
      `The Athena map cache folder exists at:\n${cachePath}\n\n` +
      'But it contains no exported world data.\n\n' +
      'To export map data:\n' +
      '1. Launch Arma 3 with the @Athena mod and join a server\n' +
      '2. Open Athena Desktop and connect to the server\n' +
      '3. Click "Export" to cache the map data\n' +
      '4. Restart AthenaWeb.exe after exporting\n\n' +
      'You only need to do this once per map.';
  } else if (worldMissing) {
    const cachedWorlds = health?.mapCache?.worlds
      ?.filter(w => w.hasMapTxt)
      .map(w => w.name)
      .join(', ') || 'none';
    title = `No cached data for "${activeWorld}"`;
    message =
      `You have cached map data for: ${cachedWorlds}\n` +
      `But the current world "${activeWorld}" has not been exported yet.\n\n` +
      'To export this map:\n' +
      '1. In Athena Desktop, connect to a server running this map\n' +
      '2. Click "Export" to cache the map geometry\n' +
      '3. Restart AthenaWeb.exe ΓÇö the new map will load automatically\n\n' +
      'Without this, the map will work but roads, buildings, trees, and locations won\'t render.';
  } else {
    return null;
  }

  return (
    <div className="map-cache-banner">
      <div className="map-cache-banner-inner">
        <button className="map-cache-banner-close" onClick={onDismiss} title="Dismiss">Γ£ò</button>
        <div className="map-cache-banner-icon">ΓÜá</div>
        <div className="map-cache-banner-title">{title}</div>
        <pre className="map-cache-banner-message">{message}</pre>
        <div className="map-cache-banner-footer">
          <a
            href="https://steamcommunity.com/sharedfiles/filedetails/?id=668999292"
            target="_blank"
            rel="noreferrer"
          >
            Download Athena Desktop (Steam Workshop)
          </a>
        </div>
      </div>
    </div>
  );
}
