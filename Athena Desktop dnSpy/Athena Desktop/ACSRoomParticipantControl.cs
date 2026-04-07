using System;
using System.Windows.Controls;

namespace Athena.App.W7
{
	// Token: 0x02000012 RID: 18
	public class ACSRoomParticipantControl
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00005F55 File Offset: 0x00004155
		public ACSRoomParticipantControl()
		{
			this.Updated = false;
			this.Participant = null;
			this.Control = new TextBlock();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00005F76 File Offset: 0x00004176
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00005F7E File Offset: 0x0000417E
		public bool Updated { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005F87 File Offset: 0x00004187
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00005F8F File Offset: 0x0000418F
		public RoomParticipant Participant { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005F98 File Offset: 0x00004198
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00005FA0 File Offset: 0x000041A0
		public TextBlock Control { get; set; }
	}
}
