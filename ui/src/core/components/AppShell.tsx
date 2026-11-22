/**
 * Three-column app layout (shared core, see docs/ui-core-sync.md):
 * left sidebar | map area | right panel.
 *
 * Owns the collapse state of both side columns and renders the edge toggle
 * buttons, so the map can be maximised on tablets (added in Web2 v0.0.5 for
 * iPad use). The column contents are supplied by the app as slots.
 * CSS classes (`app-shell`, `left-collapsed`, `sidebar`, `event-panel`,
 * `sidebar-toggle`, …) are defined in App.css.
 */
import { useState } from 'react'
import type { ReactNode } from 'react'

interface AppShellProps {
  /** Content of the left sidebar (header + layer/ORBAT sidebar). */
  sidebar: ReactNode
  /** Content of the right panel (for example the I-TGT targets panel). */
  rightPanel: ReactNode
  /** Content of the map area (map, overlays, banners). */
  children: ReactNode
}

export function AppShell({ sidebar, rightPanel, children }: AppShellProps) {
  const [leftSidebarCollapsed, setLeftSidebarCollapsed] = useState(false)
  const [rightSidebarCollapsed, setRightSidebarCollapsed] = useState(false)

  return (
    <div className={`app-shell ${leftSidebarCollapsed ? 'left-collapsed' : ''} ${rightSidebarCollapsed ? 'right-collapsed' : ''}`}>
      <aside className={`sidebar ${leftSidebarCollapsed ? 'collapsed' : ''}`}>
        {sidebar}
      </aside>
      <main className="map-area">
        <button
          className={`sidebar-toggle left ${leftSidebarCollapsed ? 'collapsed' : ''}`}
          type="button"
          onClick={() => setLeftSidebarCollapsed(prev => !prev)}
          title={leftSidebarCollapsed ? 'Expand left sidebar' : 'Collapse left sidebar'}
        >
          {leftSidebarCollapsed ? '>' : '<'}
        </button>
        <button
          className={`sidebar-toggle right ${rightSidebarCollapsed ? 'collapsed' : ''}`}
          type="button"
          onClick={() => setRightSidebarCollapsed(prev => !prev)}
          title={rightSidebarCollapsed ? 'Expand right sidebar' : 'Collapse right sidebar'}
        >
          {rightSidebarCollapsed ? '<' : '>'}
        </button>
        {children}
      </main>
      <aside className={`event-panel ${rightSidebarCollapsed ? 'collapsed' : ''}`}>
        {rightPanel}
      </aside>
    </div>
  )
}
