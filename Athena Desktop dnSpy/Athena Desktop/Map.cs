using System;

namespace Athena.App.W7
{
	// Token: 0x0200000E RID: 14
	public class Map
	{
		// Token: 0x060000A0 RID: 160 RVA: 0x00005097 File Offset: 0x00003297
		public Map()
		{
			this.Name = string.Empty;
			this.World = string.Empty;
			this.WorldSize = string.Empty;
			this.FileSize = string.Empty;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000050CB File Offset: 0x000032CB
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000050D3 File Offset: 0x000032D3
		public string Name { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000050DC File Offset: 0x000032DC
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000050E4 File Offset: 0x000032E4
		public string World { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000050ED File Offset: 0x000032ED
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x000050F5 File Offset: 0x000032F5
		public string WorldSize { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000050FE File Offset: 0x000032FE
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00005106 File Offset: 0x00003306
		public string FileSize { get; set; }
	}
}
