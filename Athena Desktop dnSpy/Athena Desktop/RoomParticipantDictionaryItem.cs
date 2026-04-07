using System;
using System.Windows.Controls;

namespace Athena.App.W7
{
	// Token: 0x02000016 RID: 22
	public class RoomParticipantDictionaryItem
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006062 File Offset: 0x00004262
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000606A File Offset: 0x0000426A
		public RoomParticipant Participant { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006073 File Offset: 0x00004273
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000607B File Offset: 0x0000427B
		public TextBlock Control { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006084 File Offset: 0x00004284
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000608C File Offset: 0x0000428C
		public ACSRoomLayerControl LayerControl { get; set; }
	}
}
