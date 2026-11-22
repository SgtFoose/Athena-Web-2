/**
 * Error boundary around the Leaflet map (shared core, see docs/ui-core-sync.md).
 *
 * A render error inside the map (for example malformed geometry from a data
 * source) would otherwise blank the whole app. This keeps the sidebars usable
 * and shows the error message in the map area instead.
 */
import { Component } from 'react'
import type { ErrorInfo, ReactNode } from 'react'

interface MapErrorBoundaryProps {
  children: ReactNode
}

interface MapErrorBoundaryState {
  error: Error | null
}

export class MapErrorBoundary extends Component<MapErrorBoundaryProps, MapErrorBoundaryState> {
  state: MapErrorBoundaryState = { error: null }

  static getDerivedStateFromError(error: Error): MapErrorBoundaryState {
    return { error }
  }

  /** Logs the error with React's component stack for debugging. */
  componentDidCatch(error: Error, info: ErrorInfo) {
    console.error('AthenaMap render error', error, info)
  }

  render() {
    if (this.state.error) {
      return (
        <div style={{
          width: '100%',
          height: '100%',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          background: '#12181d',
          color: '#f0f3f6',
          padding: 24,
          textAlign: 'center',
          fontFamily: 'Segoe UI, system-ui, sans-serif',
        }}>
          <div>
            <div style={{ fontSize: 18, fontWeight: 700, marginBottom: 8 }}>Map render failed</div>
            <div style={{ fontSize: 13, opacity: 0.88 }}>{this.state.error.message}</div>
          </div>
        </div>
      )
    }
    return this.props.children
  }
}
