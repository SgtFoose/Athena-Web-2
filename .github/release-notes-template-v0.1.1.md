# Athena Web 2 v0.1.1

A second-screen tactical map for Arma 3 in your browser — on your PC, phone or tablet. Runs as a single EXE: no installation, no Node.js, no extra mods in Arma beyond the original Athena mod.

## Quick Start (Windows EXE)

### What you need

- A Windows 10/11 PC that runs Arma 3
- The original **Athena** mod by Bus, loaded in Arma 3: [Athena - An Arma 2nd Screen Application (Steam Workshop)](https://steamcommunity.com/sharedfiles/filedetails/?id=1181881736)
- **Athena Desktop** by Bus, only needed once per map to export it: [Athena Desktop (Steam Workshop)](https://steamcommunity.com/sharedfiles/filedetails/?id=668999292)
- Optional: a phone or tablet on the same Wi-Fi/network as the PC

### Start Athena Web 2

1. Scroll down to **Assets** and download **AthenaWeb-0.1.1.exe**. Put it in any folder you like (for example your Desktop). There is nothing to install.
2. Double-click **AthenaWeb-0.1.1.exe**. If Windows shows "Windows protected your PC", see [SmartScreen](#windows-11-smartscreen-workaround) below.
3. A console window opens. **Keep it open while you play** — closing it stops Athena Web 2.
4. If Windows Firewall asks for access, allow it on **private networks** (needed for phones and tablets).
5. Open the address from the console in your browser:
   - On the same PC: the `Local:` address, usually **http://localhost:3000**
   - On a phone or tablet: the `Network:` address, for example `http://192.168.x.x:3000`
6. Start Arma 3 with the Athena mod and start or join a mission. The sidebar shows **Connected** and your units appear on the map.

### First time on a new map (once per map)

Athena Web 2 reads roads, buildings, terrain and the map size from the Athena Desktop map cache. If you see **"No cached data for &lt;map&gt;"**, export that map once:

1. Keep Arma 3 running on that map.
2. Open **Athena Desktop** and let it connect.
3. Click **Export** and wait until Arma says *"All world data supplied to relay"* and Athena Desktop has finished the import.
4. Close the Athena Web 2 console window, start **AthenaWeb-0.1.1.exe** again, and refresh your browser.

Exported maps are stored in `Documents\Athena\Maps\` and only need to be exported once.

### Troubleshooting

| Problem | What to do |
|---|---|
| The page does not load | Wait a few seconds after starting the EXE, then refresh. |
| Port 3000 is already in use | Athena Web 2 automatically uses the next free port (3001, 3002, …). Use the address shown in the console, and make sure only one Athena Web 2 is running. |
| "Relay not connected" / no units appear | Make sure the Athena mod is loaded in Arma 3 and its relay is running. In the Athena mod folder, `AthenaExtensionSettings.txt` should contain `ath_launch_relay=true`. |
| Phone or tablet cannot connect | Use the `Network:` address (not `localhost`), make sure the device is on the same network, and allow Athena Web 2 in Windows Firewall on private networks. |
| Map looks empty or squeezed into a corner | Export the map once with Athena Desktop (see above), then restart the EXE. |
| Installed via Steam Workshop | Run `AthenaWeb.exe` from the Workshop folder. Do **not** load `@AthenaWeb2` as a mod in Arma 3 or on a server. |

## Windows 11 SmartScreen Workaround

If you see "Windows protected your PC":

1. Click **More info**.
2. Click **Run anyway**.

If Run anyway is not shown:

1. Right-click **AthenaWeb-0.1.1.exe** and choose **Properties**.
2. In General, check **Unblock** (if available).
3. Click **Apply**, then **OK**.
4. Launch the EXE again.

PowerShell alternative:

```powershell
Unblock-File -Path .\AthenaWeb-0.1.1.exe
```

SmartScreen warnings are normal for new, unsigned programs. Only download the EXE from this official GitHub Releases page.

## What's New in v0.1.1

### Custom / Community / CDLC Map Fix

- Live sessions on maps outside the built-in vanilla list (for example **Kunduz**, **VR**, CDLC and community terrains) no longer squeeze all map content into the bottom-left corner
- The map now uses the real world size from the Athena Desktop cache (`Map.txt` → `WorldSize`)
- Relay and cache world names are matched case-insensitively (`kunduz` vs `Kunduz`)
- Verified live on VR (`WorldSize` 8192): map edge and player position match Athena Desktop

### UI

- Donate link repaired: now opens PayPal.Me (https://paypal.me/jeroenvdvelden)
- Sidebar header and welcome screen now read **ATHENA WEB 2** (previously showed leftover "ATHENA REMASTERED" branding); browser tab title is "Athena Web 2"

## Known Limitations

- Without an Athena Desktop export for the current map, maps outside the built-in vanilla list fall back to a 10240 m extent, and roads, buildings and terrain are not shown
- Taxi/runway tile coverage approximately 95% on Tanoa (unchanged from v0.1.0)
