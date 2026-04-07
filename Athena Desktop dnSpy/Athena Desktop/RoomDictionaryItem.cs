using System;
using System.Collections.Generic;

namespace Athena.App.W7
{
	// Token: 0x02000015 RID: 21
	public class RoomDictionaryItem
	{
		// Token: 0x060000FF RID: 255 RVA: 0x00005FE4 File Offset: 0x000041E4
		public RoomDictionaryItem()
		{
			this.ParticipantDictionary = new Dictionary<Guid, RoomParticipantDictionaryItem>();
			this.PermissionDictionary = new Dictionary<Guid, RoomPermissionDictionaryItem>();
			this.PlayerDictionary = new Dictionary<string, PlayerDictionaryItem>();
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000600D File Offset: 0x0000420D
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00006015 File Offset: 0x00004215
		public Room Room { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000601E File Offset: 0x0000421E
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00006026 File Offset: 0x00004226
		public ACSRoomControl RoomControl { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000602F File Offset: 0x0000422F
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00006037 File Offset: 0x00004237
		public Dictionary<Guid, RoomParticipantDictionaryItem> ParticipantDictionary { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00006040 File Offset: 0x00004240
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00006048 File Offset: 0x00004248
		public Dictionary<Guid, RoomPermissionDictionaryItem> PermissionDictionary { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006051 File Offset: 0x00004251
		// (set) Token: 0x06000109 RID: 265 RVA: 0x00006059 File Offset: 0x00004259
		public Dictionary<string, PlayerDictionaryItem> PlayerDictionary { get; set; }
	}
}
