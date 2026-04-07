using System;

namespace Athena.App.W7
{
	// Token: 0x0200000B RID: 11
	public class RoomParticipant
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00004F83 File Offset: 0x00003183
		public RoomParticipant()
		{
			this.RoomGUID = Guid.Empty;
			this.SessionGUID = Guid.Empty;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004FA1 File Offset: 0x000031A1
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00004FA9 File Offset: 0x000031A9
		public Guid RoomGUID { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004FB2 File Offset: 0x000031B2
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004FBA File Offset: 0x000031BA
		public Guid SessionGUID { get; set; }
	}
}
