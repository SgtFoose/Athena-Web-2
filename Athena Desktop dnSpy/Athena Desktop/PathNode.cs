using System;
using System.Collections.Generic;
using Athena.Objects.v2;

namespace Athena.App.W7
{
	// Token: 0x02000024 RID: 36
	public class PathNode
	{
		// Token: 0x0600015D RID: 349 RVA: 0x000086E4 File Offset: 0x000068E4
		public PathNode()
		{
			this.mySegment = null;
			this.myPreviousNode = null;
			this.myFirstSegment = null;
			this.myObjectIDs = new List<string>();
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000870C File Offset: 0x0000690C
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00008714 File Offset: 0x00006914
		public MapRoadSegment mySegment { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000871D File Offset: 0x0000691D
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00008725 File Offset: 0x00006925
		public PathNode myPreviousNode { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000872E File Offset: 0x0000692E
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00008736 File Offset: 0x00006936
		public MapRoadSegment myFirstSegment { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000873F File Offset: 0x0000693F
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00008747 File Offset: 0x00006947
		public List<string> myObjectIDs { get; set; }
	}
}
