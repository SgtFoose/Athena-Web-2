using System;
using System.Windows.Controls;
using Athena.Objects.v2;

namespace Athena.App.W7
{
	// Token: 0x02000020 RID: 32
	public class DictionaryOfUnitItem
	{
		// Token: 0x06000145 RID: 325 RVA: 0x0000804C File Offset: 0x0000624C
		public DictionaryOfUnitItem()
		{
			this.Unit = null;
			this.Icon = null;
			this.ORBATLabel = null;
			this.ListItem = null;
			this.Updated = true;
			this.UpdateIcon = true;
		}

		// Token: 0x0400009D RID: 157
		public Unit Unit;

		// Token: 0x0400009E RID: 158
		public Unit Icon;

		// Token: 0x0400009F RID: 159
		public Label ORBATLabel;

		// Token: 0x040000A0 RID: 160
		public ComboBoxItem ListItem;

		// Token: 0x040000A1 RID: 161
		public bool Updated;

		// Token: 0x040000A2 RID: 162
		public bool UpdateIcon;
	}
}
