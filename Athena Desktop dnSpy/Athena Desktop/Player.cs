using System;

namespace Athena.App.W7
{
	// Token: 0x0200000C RID: 12
	public class Player
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00004FC3 File Offset: 0x000031C3
		public Player()
		{
			this.RoomGUID = Guid.Empty;
			this.SteamID = string.Empty;
			this.Callsign = string.Empty;
			this.Side = string.Empty;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004FF7 File Offset: 0x000031F7
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004FFF File Offset: 0x000031FF
		public Guid RoomGUID { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00005008 File Offset: 0x00003208
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00005010 File Offset: 0x00003210
		public string SteamID { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00005019 File Offset: 0x00003219
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00005021 File Offset: 0x00003221
		public string Callsign { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000502A File Offset: 0x0000322A
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005032 File Offset: 0x00003232
		public string Side { get; set; }
	}
}
