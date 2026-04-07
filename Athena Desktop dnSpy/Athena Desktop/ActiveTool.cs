using System;

namespace Athena.App.W7
{
	// Token: 0x02000019 RID: 25
	public class ActiveTool
	{
		// Token: 0x06000119 RID: 281 RVA: 0x000060C8 File Offset: 0x000042C8
		public ActiveTool()
		{
			this._Current = Enums.ActiveToolEnum.None;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x0600011A RID: 282 RVA: 0x000060D8 File Offset: 0x000042D8
		// (remove) Token: 0x0600011B RID: 283 RVA: 0x00006110 File Offset: 0x00004310
		public event ActiveTool.ToolChangedEventHandler ToolChanged;

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006145 File Offset: 0x00004345
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00006150 File Offset: 0x00004350
		public Enums.ActiveToolEnum Current
		{
			get
			{
				return this._Current;
			}
			set
			{
				Enums.ActiveToolEnum current = this._Current;
				this._Current = value;
				ActiveTool.ToolChangedEventHandler toolChangedEvent = this.ToolChangedEvent;
				if (toolChangedEvent != null)
				{
					toolChangedEvent(current, this._Current);
				}
			}
		}

		// Token: 0x0400007F RID: 127
		private Enums.ActiveToolEnum _Current;

		// Token: 0x02000064 RID: 100
		// (Invoke) Token: 0x06000749 RID: 1865
		public delegate void ToolChangedEventHandler(Enums.ActiveToolEnum Previous, Enums.ActiveToolEnum Current);
	}
}
