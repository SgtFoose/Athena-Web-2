using System;
using System.Windows.Forms;

namespace Athena.App.W7
{
	// Token: 0x0200001A RID: 26
	public class AthenaHotKey
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00006182 File Offset: 0x00004382
		public AthenaHotKey()
		{
			this.Action = string.Empty;
			this.HotKeyString = string.Empty;
			this.HotKey = Keys.None;
			this.Shift = false;
			this.Control = false;
			this.AltMenu = false;
		}

		// Token: 0x04000080 RID: 128
		public string Action;

		// Token: 0x04000081 RID: 129
		public string HotKeyString;

		// Token: 0x04000082 RID: 130
		public Keys HotKey;

		// Token: 0x04000083 RID: 131
		public bool Shift;

		// Token: 0x04000084 RID: 132
		public bool Control;

		// Token: 0x04000085 RID: 133
		public bool AltMenu;
	}
}
