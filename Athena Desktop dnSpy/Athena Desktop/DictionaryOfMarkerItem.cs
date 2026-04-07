using System;
using Athena.Objects.v2;

namespace Athena.App.W7
{
	// Token: 0x0200001C RID: 28
	public class DictionaryOfMarkerItem
	{
		// Token: 0x06000141 RID: 321 RVA: 0x00007F9C File Offset: 0x0000619C
		public DictionaryOfMarkerItem()
		{
			this.Marker = null;
			this.Icon = null;
			this.Updated = true;
		}

		// Token: 0x04000089 RID: 137
		public Marker Marker;

		// Token: 0x0400008A RID: 138
		public Marker Icon;

		// Token: 0x0400008B RID: 139
		public bool Updated;
	}
}
