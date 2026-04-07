using System;
using System.Windows.Ink;

namespace Athena.App.W7
{
	// Token: 0x0200000D RID: 13
	public class Stroke
	{
		// Token: 0x06000099 RID: 153 RVA: 0x0000503B File Offset: 0x0000323B
		public Stroke()
		{
			this.RoomGUID = Guid.Empty;
			this.StrokeGUID = Guid.Empty;
			this.StrokeCollection = new StrokeCollection();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00005064 File Offset: 0x00003264
		// (set) Token: 0x0600009B RID: 155 RVA: 0x0000506C File Offset: 0x0000326C
		public Guid RoomGUID { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00005075 File Offset: 0x00003275
		// (set) Token: 0x0600009D RID: 157 RVA: 0x0000507D File Offset: 0x0000327D
		public Guid StrokeGUID { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00005086 File Offset: 0x00003286
		// (set) Token: 0x0600009F RID: 159 RVA: 0x0000508E File Offset: 0x0000328E
		public StrokeCollection StrokeCollection { get; set; }
	}
}
