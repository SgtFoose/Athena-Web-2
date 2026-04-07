using System;

namespace Athena.App.W7
{
	// Token: 0x02000013 RID: 19
	public class MapDictionaryItem
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005FB1 File Offset: 0x000041B1
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00005FB9 File Offset: 0x000041B9
		public Map Map { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005FC2 File Offset: 0x000041C2
		// (set) Token: 0x060000FB RID: 251 RVA: 0x00005FCA File Offset: 0x000041CA
		public ACSMapControl MapControl { get; set; }
	}
}
