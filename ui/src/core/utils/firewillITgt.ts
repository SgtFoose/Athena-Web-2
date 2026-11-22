/**
 * Helpers for Firewill I-TGT markers (shared core, see docs/ui-core-sync.md).
 *
 * The Firewill I-TGT mod places in-game map markers whose text starts with
 * `TGT_` (for example name `MKR_0_NightHawk`, text `TGT_0_NightHawk`,
 * type `mil_triangle`, colour `ColorBlue`). Apps whose data source streams
 * mission markers (Web2 via Bus's relay) convert them into
 * `FirewillITgtTarget`s for the map's I-TGT layer.
 */
import type { RelayMarker } from '../types/game'
import type { FirewillITgtTarget } from '../components/AthenaMap'

/** Marker label: its text, or its name when the text is empty. */
export function firewillITgtMarkerLabel(marker: RelayMarker): string {
  const text = String(marker.text || '').trim()
  if (text) return text
  return String(marker.name || '').trim()
}

/** True when the marker is a Firewill I-TGT marker (label starts with `TGT_`). */
export function isFirewillITgtRelayMarker(marker: RelayMarker): boolean {
  return firewillITgtMarkerLabel(marker).toUpperCase().startsWith('TGT_')
}

/**
 * True when Firewill marked the target as deleted: the marker text/name
 * mentions "delete", or the marker colour is red.
 */
export function isDeletedFirewillITgtMarker(marker: RelayMarker): boolean {
  const text = String(marker.text || '').toLowerCase()
  const name = String(marker.name || '').toLowerCase()
  if (text.includes('delete') || name.includes('delete')) return true
  return String(marker.color || '').toLowerCase().includes('red')
}

/** Converts all Firewill I-TGT markers in a marker list into map targets. */
export function toFirewillITgtTargets(markers: RelayMarker[]): FirewillITgtTarget[] {
  return markers
    .filter(isFirewillITgtRelayMarker)
    .map((marker, index) => ({
      id: `${marker.name || marker.text || 'firewill-itgt'}:${index}`,
      name: String(marker.name || '').trim(),
      label: firewillITgtMarkerLabel(marker),
      x: marker.posX,
      y: marker.posY,
      deletedInFirewill: isDeletedFirewillITgtMarker(marker),
    }))
}
