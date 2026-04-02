import { useState, useEffect } from 'react';

export interface LocationClass {
  DrawStyle: string;  // 'name' | 'icon' | 'mount' | 'area'
  SizeText:  number;  // maps to font size: 4→7px, 5→9px, 6→11px, 7→13px, 8→15px
  Name:      string;  // human-readable name
}

/**
 * Loads the Athena Desktop library JSON files (copied from the original app's
 * AppData/Roaming/Athena/Library folder) as static assets.
 *
 * vehicleMap: Arma class name → category string
 *   Categories: AAs | APCs | Artillery | Boats | Cars | Drones |
 *               Helicopters | Planes | Submersibles | Tanks | Turrets
 *
 * locationMap: location class string → LocationClass
 *   Only 'name' DrawStyle entries should be rendered as text labels.
 */
export function useAthenaLibrary() {
  const [vehicleMap,  setVehicleMap]  = useState<Map<string, string>>(new Map());
  const [locationMap, setLocationMap] = useState<Map<string, LocationClass>>(new Map());

  useEffect(() => {
    Promise.allSettled([
      fetch('/library/vehicleClasses.json').then(r => r.json()),
      fetch('/library/locationClasses.json').then(r => r.json()),
    ]).then(([vRes, lRes]) => {
      if (vRes.status === 'fulfilled') {
        const m = new Map<string, string>();
        for (const entry of vRes.value as { Class: string; Category: string }[])
          m.set(entry.Class, entry.Category);
        setVehicleMap(m);
      }
      if (lRes.status === 'fulfilled') {
        const m = new Map<string, LocationClass>();
        for (const entry of lRes.value as (LocationClass & { Class: string })[])
          m.set(entry.Class, { DrawStyle: entry.DrawStyle, SizeText: entry.SizeText, Name: entry.Name });
        setLocationMap(m);
      }
    });
  }, []);

  return { vehicleMap, locationMap };
}
