using System;

namespace Athena.App.W7
{
	// Token: 0x0200000A RID: 10
	public class RoomPermission
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00004F43 File Offset: 0x00003143
		public RoomPermission()
		{
			this.RoomGUID = Guid.Empty;
			this.SessionGUID = Guid.Empty;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004F61 File Offset: 0x00003161
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00004F69 File Offset: 0x00003169
		public Guid RoomGUID { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004F72 File Offset: 0x00003172
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00004F7A File Offset: 0x0000317A
		public Guid SessionGUID { get; set; }
	}
}
