/**
 * Small bottom-left box with the map-data status: "Loaded from cache",
 * "Exporting world data..." or "Export complete", with per-category counts
 * (shared core, see docs/ui-core-sync.md).
 *
 * Visibility:
 * - `idle`: hidden
 * - `exporting`: shown for as long as the export runs (useful progress)
 * - `cached` / `complete`: shown briefly, then hidden after
 *   {@link FINISHED_STATUS_VISIBLE_MS}, so the box doesn't permanently cover the
 *   map (owner request 2026-11-22)
 */
import { useEffect, useState } from 'react'
import type { ExportStatus } from '../types/game'

interface ExportStatusOverlayProps {
  status: ExportStatus
}

/** How long the "Loaded from cache" / "Export complete" box stays visible (ms). */
const FINISHED_STATUS_VISIBLE_MS = 4000

export function ExportStatusOverlay({ status }: ExportStatusOverlayProps) {
  const finished = status.phase === 'cached' || status.phase === 'complete'
  // True once the "finished" box has timed out for the current phase.
  const [dismissed, setDismissed] = useState(false)
  // Every phase change (e.g. a fresh export, or another world loaded from cache)
  // shows the box again. Adjusting state during render is React's recommended
  // pattern for resetting state when a prop changes (no extra effect pass).
  const [prevPhase, setPrevPhase] = useState(status.phase)
  if (status.phase !== prevPhase) {
    setPrevPhase(status.phase)
    setDismissed(false)
  }

  // Side effect: starts a timer that hides the finished box; cleared on phase change/unmount.
  useEffect(() => {
    if (!finished) return
    const timer = window.setTimeout(() => setDismissed(true), FINISHED_STATUS_VISIBLE_MS)
    return () => window.clearTimeout(timer)
  }, [finished, status.phase])

  if (status.phase === 'idle') return null
  if (finished && dismissed) return null

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
