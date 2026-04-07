using System;

namespace Athena.App.W7
{
	// Token: 0x02000009 RID: 9
	public class Room
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00004E18 File Offset: 0x00003018
		public Room()
		{
			this.RoomGUID = Guid.Empty;
			this.Action = string.Empty;
			this.Security = string.Empty;
			this.OwnerSessionGUID = Guid.Empty;
			this.Side = string.Empty;
			this.Name = string.Empty;
			this.Server = string.Empty;
			this.Mission = string.Empty;
			this.WorldName = string.Empty;
			this.WorldDisplayName = string.Empty;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004E99 File Offset: 0x00003099
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00004EA1 File Offset: 0x000030A1
		public Guid RoomGUID { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004EAA File Offset: 0x000030AA
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00004EB2 File Offset: 0x000030B2
		public string Action { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004EBB File Offset: 0x000030BB
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00004EC3 File Offset: 0x000030C3
		public string Security { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004ECC File Offset: 0x000030CC
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00004ED4 File Offset: 0x000030D4
		public Guid OwnerSessionGUID { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00004EDD File Offset: 0x000030DD
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00004EE5 File Offset: 0x000030E5
		public string Side { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004EEE File Offset: 0x000030EE
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00004EF6 File Offset: 0x000030F6
		public string Name { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004EFF File Offset: 0x000030FF
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00004F07 File Offset: 0x00003107
		public string Server { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004F10 File Offset: 0x00003110
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00004F18 File Offset: 0x00003118
		public string Mission { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00004F21 File Offset: 0x00003121
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00004F29 File Offset: 0x00003129
		public string WorldName { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004F32 File Offset: 0x00003132
		// (set) Token: 0x06000085 RID: 133 RVA: 0x00004F3A File Offset: 0x0000313A
		public string WorldDisplayName { get; set; }
	}
}
