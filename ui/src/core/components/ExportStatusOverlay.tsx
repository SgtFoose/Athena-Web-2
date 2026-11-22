/**
 * Small bottom-left box with the map-data status: "Loaded from cache",
 * "Exporting world data..." or "Export complete", with per-category counts
 * (shared core, see docs/ui-core-sync.md). Hidden while the phase is `idle`.
 */
import type { ExportStatus } from '../types/game'

interface ExportStatusOverlayProps {
  status: ExportStatus
}

export function ExportStatusOverlay({ status }: ExportStatusOverlayProps) {
  if (status.phase === 'idle') return null

  const rows = [
    { label: 'Roads',      count: status.roadCount,      done: status.roadsComplete },
    { label: 'Trees',      count: status.treeCount,      done: status.treesComplete },
    { label: 'Forests',    count: status.forestCount,    done: status.forestsComplete },
    { label: 'Locations',  count: status.locationCount,  done: status.locationsComplete },
    { label: 'Structures', count: status.structureCount, done: status.structuresComplete },
    { label: 'Elevations', count: status.elevationCount, done: status.elevationsComplete },
  ]

  return (
    <div className="export-status-overlay">
      <div className="export-status-title">
        {status.phase === 'cached' ? 'Loaded from cache'
         : status.phase === 'complete' ? 'Export complete'
         : 'Exporting world data...'}
      </div>
      {rows.map(row => (
        <div key={row.label} className="export-status-row">
          <span className={row.done ? 'done' : 'pending'}>
            {row.label}: {row.count}{row.done ? ' OK' : '...'}
          </span>
        </div>
      ))}
    </div>
  )
}
