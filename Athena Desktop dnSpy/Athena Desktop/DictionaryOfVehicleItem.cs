using System;
using Athena.Objects.v2;

namespace Athena.App.W7
{
	// Token: 0x0200001F RID: 31
	public class DictionaryOfVehicleItem
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00008021 File Offset: 0x00006221
		public DictionaryOfVehicleItem()
		{
			this.Vehicle = null;
			this.Group = null;
			this.Icon = null;
			this.Updated = true;
			this.UpdateIcon = true;
		}

		// Token: 0x04000098 RID: 152
		public Vehicle Vehicle;

		// Token: 0x04000099 RID: 153
		public Group Group;

		// Token: 0x0400009A RID: 154
		public Vehicle Icon;

		// Token: 0x0400009B RID: 155
		public bool Updated;

		// Token: 0x0400009C RID: 156
		public bool UpdateIcon;
	}
}
