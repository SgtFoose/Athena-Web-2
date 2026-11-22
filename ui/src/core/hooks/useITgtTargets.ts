/**
 * State for user-stored I-TGT targets (shared core, see docs/ui-core-sync.md).
 *
 * Targets are captured from the map cursor (T key, middle mouse button or
 * SAVE TGT on touch) and listed in the I-TGT panel. Labels count up from
 * `TGT_0`; "Delete All" (and `reset`) restarts the counter at `TGT_0`
 * (behaviour requested for Web2 v0.0.7).
 */
import { useCallback, useRef, useState } from 'react'
import type { StoredITgtTarget } from '../components/AthenaMap'

export function useITgtTargets() {
  const [targets, setTargets] = useState<StoredITgtTarget[]>([])
  // Next label index; a ref because it must not trigger re-renders.
  const nextIndexRef = useRef(0)

  /** Stores a cursor position as a new target (newest first). */
  const store = useCallback((target: { x: number; y: number; code: string }) => {
    const idx = nextIndexRef.current++
    const id = `${Date.now()}-${Math.random().toString(36).slice(2, 7)}`
    setTargets(prev => [
      { id, label: `TGT_${idx}`, x: target.x, y: target.y, code: target.code },
      ...prev,
    ])
  }, [])

  const rename = useCallback((id: string, nextLabel: string) => {
    setTargets(prev => prev.map(t => t.id === id ? { ...t, label: nextLabel } : t))
  }, [])

  const remove = useCallback((id: string) => {
    setTargets(prev => prev.filter(t => t.id !== id))
  }, [])

  /** Clears all targets and restarts labels at TGT_0. */
  const reset = useCallback(() => {
    setTargets([])
    nextIndexRef.current = 0
  }, [])

  /** Copies a code to the clipboard; silently ignored if the browser refuses. */
  const copy = useCallback(async (code: string) => {
    try {
      await navigator.clipboard.writeText(code)
    } catch {
      // Clipboard access can be denied (for example on plain-HTTP LAN origins).
    }
  }, [])

  return { targets, store, rename, remove, reset, copy }
}
