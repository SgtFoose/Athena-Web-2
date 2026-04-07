using System;

namespace Athena.App.W7
{
	// Token: 0x02000022 RID: 34
	public class FrameSetChild
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00008160 File Offset: 0x00006360
		public FrameSetChild()
		{
			this.File = string.Empty;
			this.FrameIndexStart = -1;
			this.FrameIndexFinish = -1;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00008184 File Offset: 0x00006384
		public int TotalFrames
		{
			get
			{
				int result = 0;
				if (this.FrameIndexStart != -1 & this.FrameIndexFinish != -1)
				{
					result = checked(this.FrameIndexFinish - this.FrameIndexStart + 1);
				}
				return result;
			}
		}

		// Token: 0x040000A7 RID: 167
		public string File;

		// Token: 0x040000A8 RID: 168
		public int FrameIndexStart;

		// Token: 0x040000A9 RID: 169
		public int FrameIndexFinish;
	}
}
