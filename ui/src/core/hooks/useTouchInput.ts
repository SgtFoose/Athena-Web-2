/**
 * Detects whether the user is on a touch device (shared core, see
 * docs/ui-core-sync.md). Touch devices get the tap-to-set-cursor +
 * SAVE TGT flow instead of the T key / middle mouse button.
 *
 * Requires both a coarse pointer (media query) and real touch support, so
 * touch-screen laptops used with a mouse keep the desktop flow.
 */
import { useEffect, useState } from 'react'

export function useTouchInput(): boolean {
  const [isTouchInput, setIsTouchInput] = useState(false)

  useEffect(() => {
    if (typeof window === 'undefined') return
    const mq = window.matchMedia('(pointer: coarse)')

    const detectInput = () => {
      const hasTouch = (navigator.maxTouchPoints ?? 0) > 0 || 'ontouchstart' in window
      setIsTouchInput(Boolean(mq.matches && hasTouch))
    }

    detectInput()
    if (typeof mq.addEventListener === 'function') {
      mq.addEventListener('change', detectInput)
      return () => mq.removeEventListener('change', detectInput)
    }
    // Older Safari only supports the deprecated listener API.
    mq.addListener(detectInput)
    return () => mq.removeListener(detectInput)
  }, [])

  return isTouchInput
}
