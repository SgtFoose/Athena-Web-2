/**
 * "Follow Active Player" logic (shared core, see docs/ui-core-sync.md).
 *
 * Finds the unit the local player controls and, while follow mode is on,
 * pans the map to it whenever it moves. The map registers its focus/pan
 * functions through the returned refs (`onRegisterFocus` / `onRegisterPan`).
 *
 * Follow mode turns itself off when:
 * - the user pans/zooms the map manually (the app calls `setFollowActivePlayer(false)`
 *   from the map's `onUserInteraction`), and
 * - the controlled unit changes (switching units can briefly report stale
 *   identities, so the user has to re-enable follow).
 */
import { useCallback, useEffect, useMemo, useRef, useState } from 'react'
import type { GameFrame, Unit, Vehicle } from '../types/game'

/**
 * Minimum movement in metres before the map re-pans to the followed unit.
 * Avoids re-panning on every frame for tiny position jitter.
 */
const FOLLOW_REPAN_MIN_DISTANCE_M = 2.5

/** The unit being followed, with its position (the vehicle's position when mounted). */
export interface ActivePlayerAnchor {
  id: string
  name: string
  x: number
  y: number
}

export function useFollowActivePlayer(
  frame: GameFrame | null,
  units: Record<string, Unit>,
  vehicles: Record<string, Vehicle>,
) {
  const [followActivePlayer, setFollowActivePlayer] = useState(false)

  // Set by AthenaMap: focus = jump to max zoom, pan = keep zoom.
  const mapFocusRef = useRef<(posX: number, posY: number) => void>(() => {})
  const mapPanRef = useRef<(posX: number, posY: number) => void>(() => {})
  const lastFollowPosRef = useRef<{ x: number; y: number } | null>(null)
  const lastFollowTargetIdRef = useRef<string | null>(null)
  const previousMissionPlayerRef = useRef('')

  const activePlayerAnchor = useMemo<ActivePlayerAnchor | null>(() => {
    const missionPlayer = frame?.mission?.player?.trim().toLowerCase() ?? ''
    const missionSteamId = frame?.mission?.steamId?.trim() ?? ''
    const unitList = Object.values(units)
    const isSameMissionSteam = (steamId: string) => missionSteamId !== '' && steamId.trim() === missionSteamId

    // Prefer explicit active marker from the game export.
    const primary = unitList.find(u => Boolean(u.isActivePlayer))
      // Then try direct unit-name match (important for switchable SP units).
      ?? (missionPlayer ? unitList.find(u => u.name?.trim().toLowerCase() === missionPlayer) : undefined)
      // Then try player profile name match.
      ?? (missionPlayer ? unitList.find(u => u.playerName?.trim().toLowerCase() === missionPlayer) : undefined)

    // Fallback only to mission SteamID-linked unit; avoid generic fallback that can lock onto the wrong unit.
    const fallback = primary
      ?? unitList.find(u => isSameMissionSteam(u.steamId))

    if (!fallback) return null

    const veh = fallback.vehicleId ? vehicles[fallback.vehicleId] : undefined
    return {
      id: fallback.id,
      name: fallback.playerName?.trim() || fallback.name || 'Active Player',
      x: veh?.posX ?? fallback.posX,
      y: veh?.posY ?? fallback.posY,
    }
  }, [frame?.mission?.player, frame?.mission?.steamId, units, vehicles])

  // Re-pan when the followed unit moved far enough or a different unit became active.
  useEffect(() => {
    if (!followActivePlayer || !activePlayerAnchor) return
    const last = lastFollowPosRef.current
    const dx = (last?.x ?? Number.NaN) - activePlayerAnchor.x
    const dy = (last?.y ?? Number.NaN) - activePlayerAnchor.y
    const moved = !Number.isFinite(dx) || !Number.isFinite(dy) || Math.hypot(dx, dy) >= FOLLOW_REPAN_MIN_DISTANCE_M
    const targetChanged = lastFollowTargetIdRef.current !== activePlayerAnchor.id
    if (!moved && !targetChanged) return
    lastFollowPosRef.current = { x: activePlayerAnchor.x, y: activePlayerAnchor.y }
    lastFollowTargetIdRef.current = activePlayerAnchor.id
    mapPanRef.current(activePlayerAnchor.x, activePlayerAnchor.y)
  }, [followActivePlayer, activePlayerAnchor])

  useEffect(() => {
    if (!followActivePlayer) {
      lastFollowPosRef.current = null
      lastFollowTargetIdRef.current = null
    }
  }, [followActivePlayer])

  useEffect(() => {
    const missionPlayer = frame?.mission?.player?.trim() ?? ''
    const previous = previousMissionPlayerRef.current
    if (previous && missionPlayer && previous !== missionPlayer && followActivePlayer) {
      // Switching controlled units can briefly produce stale identities; require manual re-enable.
      setFollowActivePlayer(false)
      lastFollowPosRef.current = null
      lastFollowTargetIdRef.current = null
    }
    previousMissionPlayerRef.current = missionPlayer
  }, [frame?.mission?.player, followActivePlayer])

  /** Sidebar button: toggles follow; enabling it jumps to the unit at max zoom. */
  const toggleFollowActivePlayer = useCallback(() => {
    setFollowActivePlayer(prev => {
      const next = !prev
      if (next && activePlayerAnchor) {
        mapFocusRef.current(activePlayerAnchor.x, activePlayerAnchor.y)
        lastFollowPosRef.current = { x: activePlayerAnchor.x, y: activePlayerAnchor.y }
        lastFollowTargetIdRef.current = activePlayerAnchor.id
      }
      return next
    })
  }, [activePlayerAnchor])

  return {
    followActivePlayer,
    setFollowActivePlayer,
    activePlayerAnchor,
    toggleFollowActivePlayer,
    mapFocusRef,
    mapPanRef,
  }
}
