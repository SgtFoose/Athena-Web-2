using System;
using System.Windows.Controls;
using System.Windows.Shapes;
using Athena.Objects.v2;

namespace Athena.App.W7
{
	// Token: 0x0200001D RID: 29
	public class DictionaryOfGroupItem
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00007FB9 File Offset: 0x000061B9
		public DictionaryOfGroupItem()
		{
			this.Group = null;
			this.NoUnitCount = 0;
			this.Icon = null;
			this.Line = null;
			this.Dot = null;
			this.ORBATPanel = null;
			this.Updated = true;
			this.UpdateIcon = true;
		}

		// Token: 0x0400008C RID: 140
		public Group Group;

		// Token: 0x0400008D RID: 141
		public int NoUnitCount;

		// Token: 0x0400008E RID: 142
		public Group Icon;

		// Token: 0x0400008F RID: 143
		public Line Line;

		// Token: 0x04000090 RID: 144
		public Ellipse Dot;

		// Token: 0x04000091 RID: 145
		public StackPanel ORBATPanel;

		// Token: 0x04000092 RID: 146
		public bool Updated;

		// Token: 0x04000093 RID: 147
		public bool UpdateIcon;
	}
}
