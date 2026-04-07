using System;

namespace Athena.App.W7
{
	// Token: 0x02000008 RID: 8
	public class Session
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00004D5C File Offset: 0x00002F5C
		public Session()
		{
			this.SessionGUID = Guid.Empty;
			this.Callsign = string.Empty;
			this.Player = string.Empty;
			this.SteamID = string.Empty;
			this.WorldName = string.Empty;
			this.WorldDisplayName = string.Empty;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004DB1 File Offset: 0x00002FB1
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00004DB9 File Offset: 0x00002FB9
		public Guid SessionGUID { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004DC2 File Offset: 0x00002FC2
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00004DCA File Offset: 0x00002FCA
		public string Callsign { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00004DD3 File Offset: 0x00002FD3
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00004DDB File Offset: 0x00002FDB
		public string Player { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00004DE4 File Offset: 0x00002FE4
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00004DEC File Offset: 0x00002FEC
		public string SteamID { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00004DF5 File Offset: 0x00002FF5
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00004DFD File Offset: 0x00002FFD
		public string WorldName { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00004E06 File Offset: 0x00003006
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00004E0E File Offset: 0x0000300E
		public string WorldDisplayName { get; set; }
	}
}
