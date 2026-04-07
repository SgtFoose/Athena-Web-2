using System;
using System.Windows.Controls;
using Athena.Objects.v2;

namespace Athena.App.W7
{
	// Token: 0x0200001E RID: 30
	public class DictionaryOfInkItem
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00007FF9 File Offset: 0x000061F9
		public DictionaryOfInkItem()
		{
			this.Ink = null;
			this.Canvas = null;
			this.Strokes = string.Empty;
			this.Updated = true;
		}

		// Token: 0x04000094 RID: 148
		public Ink Ink;

		// Token: 0x04000095 RID: 149
		public InkCanvas Canvas;

		// Token: 0x04000096 RID: 150
		public string Strokes;

		// Token: 0x04000097 RID: 151
		public bool Updated;
	}
}
