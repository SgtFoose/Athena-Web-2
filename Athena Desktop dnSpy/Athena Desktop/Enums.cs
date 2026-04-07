using System;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000037 RID: 55
	[StandardModule]
	public sealed class Enums
	{
		// Token: 0x02000075 RID: 117
		public enum ActiveToolEnum
		{
			// Token: 0x040002B9 RID: 697
			None,
			// Token: 0x040002BA RID: 698
			Ink = 5,
			// Token: 0x040002BB RID: 699
			MapNote,
			// Token: 0x040002BC RID: 700
			MarkerCircle = 1,
			// Token: 0x040002BD RID: 701
			MarkerIcon,
			// Token: 0x040002BE RID: 702
			MarkerRect,
			// Token: 0x040002BF RID: 703
			MarkerText,
			// Token: 0x040002C0 RID: 704
			MapAnchor = 7
		}

		// Token: 0x02000076 RID: 118
		public enum LayerType
		{
			// Token: 0x040002C2 RID: 706
			Ink = 1,
			// Token: 0x040002C3 RID: 707
			Canvas,
			// Token: 0x040002C4 RID: 708
			Unspecified = 0
		}

		// Token: 0x02000077 RID: 119
		public enum MarkerIconParentType
		{
			// Token: 0x040002C6 RID: 710
			Anchor = 1,
			// Token: 0x040002C7 RID: 711
			Group,
			// Token: 0x040002C8 RID: 712
			Marker,
			// Token: 0x040002C9 RID: 713
			Unit,
			// Token: 0x040002CA RID: 714
			Vehicle
		}

		// Token: 0x02000078 RID: 120
		public enum SourceStatus
		{
			// Token: 0x040002CC RID: 716
			Offline = 1,
			// Token: 0x040002CD RID: 717
			ACS,
			// Token: 0x040002CE RID: 718
			Relay,
			// Token: 0x040002CF RID: 719
			Recording
		}

		// Token: 0x02000079 RID: 121
		public enum RelayStatus
		{
			// Token: 0x040002D1 RID: 721
			Connecting = 1,
			// Token: 0x040002D2 RID: 722
			Disconnecting
		}
	}
}
