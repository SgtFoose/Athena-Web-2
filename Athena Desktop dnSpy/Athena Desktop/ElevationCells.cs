using System;
using System.Collections.Generic;
using System.Windows;

namespace Athena.App.W7
{
	// Token: 0x02000021 RID: 33
	public class ElevationCells
	{
		// Token: 0x06000146 RID: 326 RVA: 0x0000807E File Offset: 0x0000627E
		public ElevationCells()
		{
			this.Z = 0;
			this.HitTests = new List<List<int>>();
			this.PointGroups = new List<List<Point>>();
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000080A3 File Offset: 0x000062A3
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000080AB File Offset: 0x000062AB
		public int Z { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000080B4 File Offset: 0x000062B4
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000080BC File Offset: 0x000062BC
		public bool[][] Cells { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000080C5 File Offset: 0x000062C5
		// (set) Token: 0x0600014C RID: 332 RVA: 0x000080CD File Offset: 0x000062CD
		public List<List<int>> HitTests { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000080D6 File Offset: 0x000062D6
		// (set) Token: 0x0600014E RID: 334 RVA: 0x000080DE File Offset: 0x000062DE
		public List<List<Point>> PointGroups { get; set; }

		// Token: 0x0600014F RID: 335 RVA: 0x000080E8 File Offset: 0x000062E8
		public bool ContainsPoint(int X, int Y)
		{
			bool result = false;
			if (this.PointGroups != null && this.PointGroups.Count != 0)
			{
				try
				{
					List<List<Point>>.Enumerator enumerator = this.PointGroups.GetEnumerator();
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Contains(new Point((double)Y, (double)X)))
						{
							result = true;
						}
					}
				}
				finally
				{
					List<List<Point>>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			return result;
		}
	}
}
