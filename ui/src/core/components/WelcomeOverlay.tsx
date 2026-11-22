/**
 * Welcome screen shown over the map while no world is active
 * (shared core, see docs/ui-core-sync.md).
 *
 * Shows the app name, a connection status line, an optional hint, and either
 * the world-export progress (while data is loading) or the app's own
 * getting-started steps. The status texts and steps are supplied by each app,
 * because they depend on its data source (Web2: AthenaWeb.exe + relay;
 * AR: its own server + extension DLL).
 */
import type { ReactNode } from 'react'
import type { ExportStatus } from '../types/game'
import { APP_NAME } from '../../appConfig'

interface WelcomeOverlayProps {
  /** One-line connection status, for example "Connecting to server...". */
  statusText: string
  /** Optional troubleshooting hint shown under the status (empty = hidden). */
  hint?: string
  /** When set, the export progress is shown instead of `instructions`. */
  exportProgress?: ExportStatus | null
  /** App-specific getting-started steps. */
  instructions: ReactNode
}

export function WelcomeOverlay({ statusText, hint, exportProgress, instructions }: WelcomeOverlayProps) {
  return (
    <div className="welcome-overlay">
      <img
        className="welcome-bg"
        src="/athena-default-bg.png"
        alt={APP_NAME}
      />
      <div className="welcome-banner">
        <div className="welcome-title">{APP_NAME.toUpperCase()}</div>
        <div className="welcome-status">
          {statusText}
        </div>
        {hint && (
          <div style={{ fontSize: 12, color: '#ffd166', marginTop: 6 }}>
            {hint}
          </div>
        )}
        {exportProgress ? (
          <div className="welcome-export-progress">
            {[
              { label: 'Roads',      count: exportProgress.roadCount,      done: exportProgress.roadsComplete },
              { label: 'Trees',      count: exportProgress.treeCount,      done: exportProgress.treesComplete },
              { label: 'Forests',    count: exportProgress.forestCount,    done: exportProgress.forestsComplete },
              { label: 'Locations',  count: exportProgress.locationCount,  done: exportProgress.locationsComplete },
              { label: 'Structures', count: exportProgress.structureCount, done: exportProgress.structuresComplete },
              { label: 'Elevations', count: exportProgress.elevationCount, done: exportProgress.elevationsComplete },
            ].map(g => (
              <div key={g.label} className="welcome-export-row">
                <span style={{ color: g.done ? '#2ecc71' : '#f0a500' }}>{g.label}</span>
                <span style={{ color: g.done ? '#2ecc71' : '#888' }}>{g.count}{g.done ? ' OK' : '...'}</span>
              </div>
            ))}
          </div>
        ) : (
          <div className="welcome-instructions">
            {instructions}
          </div>
        )}
      </div>
    </div>
  )
}
