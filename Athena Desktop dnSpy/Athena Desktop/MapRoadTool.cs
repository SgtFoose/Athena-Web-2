using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using Athena.Objects.v2;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000025 RID: 37
	public class MapRoadTool
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00008750 File Offset: 0x00006950
		public MapRoadTool()
		{
			this.Roads = new Dictionary<string, MapRoad>();
			this.RoadSegments = new List<MapRoadSegment>();
			this.AllNodePaths = new List<PathNode>();
			this.SegmentDictionary = new Dictionary<string, MapRoadSegment>();
			this.PathDictionary = new Dictionary<Guid, MapRoadSegmentPath>();
			this.UsedDictionary = new Dictionary<string, MapRoadSegment>();
			this.ResetVars();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000087AB File Offset: 0x000069AB
		public void ResetVars()
		{
			this.Roads = new Dictionary<string, MapRoad>();
			this.RoadSegments = new List<MapRoadSegment>();
			this.AllNodePaths = new List<PathNode>();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000087D0 File Offset: 0x000069D0
		public void WalkSegmentsNew()
		{
			try
			{
				this.GenerateRoads();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008804 File Offset: 0x00006A04
		private void PrepSegments()
		{
			checked
			{
				try
				{
					int num = this.RoadSegments.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						List<MapRoadSegment> roadSegments;
						int index;
						MapRoadSegment value = (roadSegments = this.RoadSegments)[index = i];
						this.UpdateRoadIntersectionFlag(ref value);
						roadSegments[index] = value;
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008870 File Offset: 0x00006A70
		private void UpdateRoadIntersectionFlag(ref MapRoadSegment RoadSegment)
		{
			if (RoadSegment != null && RoadSegment.Connections != null && RoadSegment.Connections.Count > 2)
			{
				RoadSegment.IsIntersection = true;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008898 File Offset: 0x00006A98
		private void FillDictionary()
		{
			if (this.RoadSegments != null)
			{
				try
				{
					foreach (MapRoadSegment mapRoadSegment in this.RoadSegments)
					{
						this.SegmentDictionary.Add(mapRoadSegment.ObjectID, mapRoadSegment);
					}
				}
				finally
				{
					List<MapRoadSegment>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008900 File Offset: 0x00006B00
		private void CreatePathNodes()
		{
			if (this.RoadSegments != null && this.RoadSegments.Count != 0)
			{
				try
				{
					foreach (MapRoadSegment mapRoadSegment in this.RoadSegments)
					{
						if (mapRoadSegment.Connections.Count > 2)
						{
							try
							{
								foreach (string text in mapRoadSegment.Connections)
								{
									if (this.SegmentDictionary.ContainsKey(text))
									{
										PathNode pathNode = new PathNode();
										pathNode.mySegment = this.SegmentDictionary[text];
										pathNode.myPreviousNode = new PathNode
										{
											mySegment = mapRoadSegment
										};
										pathNode.myFirstSegment = mapRoadSegment;
										pathNode.myObjectIDs.Add(text);
										pathNode.myObjectIDs.Add(mapRoadSegment.ObjectID);
										this.GetNext(ref pathNode);
										this.AllNodePaths.Add(pathNode);
									}
								}
								continue;
							}
							finally
							{
								List<string>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
						}
						if (mapRoadSegment.Connections.Count == 1 && this.SegmentDictionary.ContainsKey(mapRoadSegment.Connections[0]))
						{
							PathNode pathNode2 = new PathNode();
							pathNode2.mySegment = this.SegmentDictionary[mapRoadSegment.Connections[0]];
							pathNode2.myPreviousNode = new PathNode
							{
								mySegment = mapRoadSegment
							};
							pathNode2.myFirstSegment = mapRoadSegment;
							pathNode2.myObjectIDs.Add(mapRoadSegment.ObjectID);
							this.GetNext(ref pathNode2);
							this.AllNodePaths.Add(pathNode2);
						}
					}
				}
				finally
				{
					List<MapRoadSegment>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00008AE0 File Offset: 0x00006CE0
		private void GetNext(ref PathNode x)
		{
			if (x != null)
			{
				switch (x.mySegment.Connections.Count)
				{
				case 0:
					break;
				case 1:
					if (x.myPreviousNode == null)
					{
						if (this.SegmentDictionary.ContainsKey(x.mySegment.Connections[0]))
						{
							PathNode pathNode = new PathNode();
							pathNode.mySegment = x.mySegment;
							pathNode.myPreviousNode = x.myPreviousNode;
							x.mySegment = this.SegmentDictionary[x.mySegment.Connections[0]];
							x.myPreviousNode = pathNode;
							x.myObjectIDs.Add(x.mySegment.ObjectID);
							this.GetNext(ref x);
							return;
						}
					}
					else if (Operators.CompareString(x.mySegment.Connections[0], x.myPreviousNode.mySegment.ObjectID, false) != 0 && this.SegmentDictionary.ContainsKey(x.mySegment.Connections[0]))
					{
						PathNode pathNode2 = new PathNode();
						pathNode2.mySegment = x.mySegment;
						pathNode2.myPreviousNode = x.myPreviousNode;
						x.mySegment = this.SegmentDictionary[x.mySegment.Connections[0]];
						x.myPreviousNode = pathNode2;
						x.myObjectIDs.Add(x.mySegment.ObjectID);
						this.GetNext(ref x);
						return;
					}
					break;
				case 2:
					if (Operators.CompareString(x.mySegment.Connections[0], x.myPreviousNode.mySegment.ObjectID, false) == 0)
					{
						if (!x.myObjectIDs.Contains(x.mySegment.Connections[1]) && this.SegmentDictionary.ContainsKey(x.mySegment.Connections[1]))
						{
							PathNode pathNode3 = new PathNode();
							pathNode3.mySegment = x.mySegment;
							pathNode3.myPreviousNode = x.myPreviousNode;
							x.mySegment = this.SegmentDictionary[x.mySegment.Connections[1]];
							x.myPreviousNode = pathNode3;
							x.myObjectIDs.Add(x.mySegment.ObjectID);
							this.GetNext(ref x);
							return;
						}
					}
					else if (!x.myObjectIDs.Contains(x.mySegment.Connections[0]) && !x.myObjectIDs.Contains(x.mySegment.Connections[0]) && this.SegmentDictionary.ContainsKey(x.mySegment.Connections[0]))
					{
						PathNode pathNode4 = new PathNode();
						pathNode4.mySegment = x.mySegment;
						pathNode4.myPreviousNode = x.myPreviousNode;
						x.mySegment = this.SegmentDictionary[x.mySegment.Connections[0]];
						x.myPreviousNode = pathNode4;
						x.myObjectIDs.Add(x.mySegment.ObjectID);
						this.GetNext(ref x);
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00008E24 File Offset: 0x00007024
		private void CreateRoadsFromPaths()
		{
			if (this.AllNodePaths != null)
			{
				try
				{
					foreach (PathNode pathNode in this.AllNodePaths)
					{
						MapRoad mapRoad = new MapRoad();
						bool flag = true;
						while (flag)
						{
							mapRoad.Segments.Add(pathNode.mySegment.ObjectID, pathNode.mySegment);
							mapRoad.Objects.Add(pathNode.mySegment.ObjectID);
							if (pathNode.myPreviousNode == null)
							{
								flag = false;
							}
							else
							{
								pathNode = pathNode.myPreviousNode;
							}
						}
						mapRoad.Objects.Sort();
						if (mapRoad.HasSegmentOnConcrete())
						{
							mapRoad.RoadType = Enums.RoadType.Concrete;
						}
					}
				}
				finally
				{
					List<PathNode>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008EE8 File Offset: 0x000070E8
		private void GenerateRoads()
		{
			try
			{
				foreach (MapRoadSegment mapRoadSegment in this.RoadSegments)
				{
					if (!this.SegmentDictionary.ContainsKey(mapRoadSegment.ObjectID))
					{
						mapRoadSegment.ObjectIDNumeric = Convert.ToInt32(mapRoadSegment.ObjectID, CultureInfo.InvariantCulture);
						this.SegmentDictionary.Add(mapRoadSegment.ObjectID, mapRoadSegment);
					}
				}
			}
			finally
			{
				List<MapRoadSegment>.Enumerator enumerator;
				((IDisposable)enumerator).Dispose();
			}
			this.RepairConnections();
			this.EnsureConnectionIntegrity();
			try
			{
				foreach (MapRoadSegment mapRoadSegment2 in this.RoadSegments)
				{
					try
					{
						foreach (string key in mapRoadSegment2.Connections)
						{
							if (this.SegmentDictionary.ContainsKey(key))
							{
								mapRoadSegment2.ConnectedSegments.Add(this.SegmentDictionary[key]);
							}
						}
					}
					finally
					{
						List<string>.Enumerator enumerator3;
						((IDisposable)enumerator3).Dispose();
					}
				}
			}
			finally
			{
				List<MapRoadSegment>.Enumerator enumerator2;
				((IDisposable)enumerator2).Dispose();
			}
			try
			{
				foreach (MapRoadSegment mapRoadSegment3 in this.RoadSegments)
				{
					if (mapRoadSegment3.ConnectedSegments.Count == 1 | mapRoadSegment3.ConnectedSegments.Count > 2)
					{
						try
						{
							foreach (MapRoadSegment mapRoadSegment4 in mapRoadSegment3.ConnectedSegments)
							{
								if (mapRoadSegment4.ConnectedSegments.Count == 2)
								{
									if (!this.UsedDictionary.ContainsKey(mapRoadSegment4.ObjectID))
									{
										MapRoadSegmentPath mapRoadSegmentPath = new MapRoadSegmentPath
										{
											PathGUID = Guid.NewGuid()
										};
										mapRoadSegmentPath.Segments.Add(mapRoadSegment3.ObjectID, mapRoadSegment3);
										this.WalkPath(ref mapRoadSegment4, ref mapRoadSegmentPath);
										this.PathDictionary.Add(mapRoadSegmentPath.PathGUID, mapRoadSegmentPath);
									}
								}
								else
								{
									MapRoadSegmentPath mapRoadSegmentPath2 = new MapRoadSegmentPath
									{
										PathGUID = Guid.NewGuid()
									};
									mapRoadSegmentPath2.Segments.Add(mapRoadSegment3.ObjectID, mapRoadSegment3);
									mapRoadSegmentPath2.Segments.Add(mapRoadSegment4.ObjectID, mapRoadSegment4);
									this.PathDictionary.Add(mapRoadSegmentPath2.PathGUID, mapRoadSegmentPath2);
								}
							}
						}
						finally
						{
							List<MapRoadSegment>.Enumerator enumerator5;
							((IDisposable)enumerator5).Dispose();
						}
					}
				}
			}
			finally
			{
				List<MapRoadSegment>.Enumerator enumerator4;
				((IDisposable)enumerator4).Dispose();
			}
			List<MapRoadSegmentPath> list = this.PathDictionary.Values.OrderBy((MapRoadTool._Closure$__.$I15-0 == null) ? (MapRoadTool._Closure$__.$I15-0 = ((MapRoadSegmentPath x) => x.Segments.Count)) : MapRoadTool._Closure$__.$I15-0).ToList<MapRoadSegmentPath>();
			if (list != null)
			{
				try
				{
					foreach (MapRoadSegmentPath mapRoadSegmentPath3 in list)
					{
						MapRoad mapRoad = new MapRoad();
						try
						{
							foreach (object obj in mapRoadSegmentPath3.Segments.Values)
							{
								MapRoadSegment mapRoadSegment5 = (MapRoadSegment)obj;
								mapRoad.Segments.Add(mapRoadSegment5.ObjectID, mapRoadSegment5);
								mapRoad.Objects.Add(mapRoadSegment5.ObjectID);
								mapRoad.Objects.Sort();
							}
						}
						finally
						{
							IEnumerator enumerator7;
							if (enumerator7 is IDisposable)
							{
								(enumerator7 as IDisposable).Dispose();
							}
						}
						string text = string.Join(".", mapRoad.Objects.ToArray());
						if (!this.Roads.ContainsKey(text))
						{
							bool flag = true;
							List<string> list2 = new List<string>();
							try
							{
								foreach (KeyValuePair<string, MapRoad> keyValuePair in this.Roads)
								{
									if (text.Contains(keyValuePair.Key))
									{
										list2.Add(keyValuePair.Key);
									}
									if (keyValuePair.Key.Contains(text))
									{
										flag = false;
										break;
									}
								}
							}
							finally
							{
								Dictionary<string, MapRoad>.Enumerator enumerator8;
								((IDisposable)enumerator8).Dispose();
							}
							if (flag)
							{
								this.Roads.Add(text, mapRoad);
							}
							if (list2.Count != 0)
							{
								try
								{
									foreach (string key2 in list2)
									{
										this.Roads.Remove(key2);
									}
								}
								finally
								{
									List<string>.Enumerator enumerator9;
									((IDisposable)enumerator9).Dispose();
								}
							}
						}
					}
				}
				finally
				{
					List<MapRoadSegmentPath>.Enumerator enumerator6;
					((IDisposable)enumerator6).Dispose();
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009404 File Offset: 0x00007604
		private void RepairConnections()
		{
			List<MapRoadSegment> list = this.SegmentDictionary.Values.OrderBy((MapRoadTool._Closure$__.$I16-0 == null) ? (MapRoadTool._Closure$__.$I16-0 = ((MapRoadSegment x) => x.ObjectIDNumeric)) : MapRoadTool._Closure$__.$I16-0).ToList<MapRoadSegment>();
			checked
			{
				if (list != null && list.Count != 0)
				{
					int num = list.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						if (list[i].Connections.Count == 0)
						{
							if (i != 0 && list[i - 1].Connections.Count != 0 && !list[i - 1].Connections.Contains(list[i].ObjectID))
							{
								int num2 = list[i - 1].Connections.Count - 1;
								for (int j = 0; j <= num2; j++)
								{
									if (!this.SegmentDictionary.ContainsKey(list[i - 1].Connections[j]) && this.GetDistanceBetweenSegments(new Point(Convert.ToDouble(list[i].PosX, CultureInfo.InvariantCulture), Convert.ToDouble(list[i].PosY, CultureInfo.InvariantCulture)), new Point(Convert.ToDouble(list[i - 1].PosX, CultureInfo.InvariantCulture), Convert.ToDouble(list[i - 1].PosY, CultureInfo.InvariantCulture))) < 100.0)
									{
										list[i].Connections.Add(list[i - 1].ObjectID);
										list[i].Connections.Add(list[i - 1].Connections[j]);
										list[i - 1].Connections[j] = list[i].ObjectID;
									}
								}
							}
							if (i != list.Count - 1 && list[i + 1].Connections.Count != 0 && !list[i + 1].Connections.Contains(list[i].ObjectID))
							{
								int num3 = list[i + 1].Connections.Count - 1;
								for (int k = 0; k <= num3; k++)
								{
									if (!this.SegmentDictionary.ContainsKey(list[i + 1].Connections[k]) && this.GetDistanceBetweenSegments(new Point(Convert.ToDouble(list[i].PosX, CultureInfo.InvariantCulture), Convert.ToDouble(list[i].PosY, CultureInfo.InvariantCulture)), new Point(Convert.ToDouble(list[i + 1].PosX, CultureInfo.InvariantCulture), Convert.ToDouble(list[i + 1].PosY, CultureInfo.InvariantCulture))) < 100.0)
									{
										list[i].Connections.Add(list[i + 1].ObjectID);
										list[i + 1].Connections[k] = list[i].ObjectID;
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00009754 File Offset: 0x00007954
		private void EnsureConnectionIntegrity()
		{
			List<MapRoadSegment> list = this.SegmentDictionary.Values.OrderBy((MapRoadTool._Closure$__.$I17-0 == null) ? (MapRoadTool._Closure$__.$I17-0 = ((MapRoadSegment x) => x.ObjectIDNumeric)) : MapRoadTool._Closure$__.$I17-0).ToList<MapRoadSegment>();
			checked
			{
				if (list != null && list.Count != 0)
				{
					int num = list.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						if (list[i].Connections.Count != 0)
						{
							try
							{
								foreach (string key in list[i].Connections)
								{
									if (this.SegmentDictionary.ContainsKey(key))
									{
										MapRoadSegment mapRoadSegment = this.SegmentDictionary[key];
										if (!mapRoadSegment.Connections.Contains(list[i].ObjectID))
										{
											mapRoadSegment.Connections.Add(list[i].ObjectID);
										}
									}
								}
							}
							finally
							{
								List<string>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
						}
					}
				}
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00009874 File Offset: 0x00007A74
		private double GetDistanceBetweenSegments(Point Point1, Point Point2)
		{
			return Math.Sqrt(Math.Pow(Math.Abs(Point1.X - Point2.X), 2.0) + Math.Pow(Math.Abs(Point1.Y - Point2.Y), 2.0));
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000098CC File Offset: 0x00007ACC
		private void WalkPath(ref MapRoadSegment Segment, ref MapRoadSegmentPath Path)
		{
			if (Segment != null && Segment.ConnectedSegments != null && Path != null && !Path.Segments.Contains(Segment.ObjectID) && !this.UsedDictionary.ContainsKey(Segment.ObjectID))
			{
				this.UsedDictionary.Add(Segment.ObjectID, Segment);
				Path.Segments.Add(Segment.ObjectID, Segment);
				switch (Segment.ConnectedSegments.Count)
				{
				case 0:
					break;
				case 1:
					if (!Path.Segments.Contains(Segment.ConnectedSegments[0].ObjectID))
					{
						if (Segment.ConnectedSegments[0].ConnectedSegments.Count == 2)
						{
							List<MapRoadSegment> connectedSegments;
							MapRoadSegment value = (connectedSegments = Segment.ConnectedSegments)[0];
							this.WalkPath(ref value, ref Path);
							connectedSegments[0] = value;
							return;
						}
						if (!Path.Segments.Contains(Segment.ConnectedSegments[0].ObjectID))
						{
							Path.Segments.Add(Segment.ConnectedSegments[0].ObjectID, Segment.ConnectedSegments[0]);
							return;
						}
					}
					break;
				case 2:
					try
					{
						foreach (MapRoadSegment mapRoadSegment in Segment.ConnectedSegments)
						{
							if (mapRoadSegment.ConnectedSegments.Count == 2)
							{
								this.WalkPath(ref mapRoadSegment, ref Path);
							}
							else if (!Path.Segments.Contains(mapRoadSegment.ObjectID))
							{
								Path.Segments.Add(mapRoadSegment.ObjectID, mapRoadSegment);
							}
						}
					}
					finally
					{
						List<MapRoadSegment>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x040000B1 RID: 177
		public Dictionary<string, MapRoad> Roads;

		// Token: 0x040000B2 RID: 178
		public List<MapRoadSegment> RoadSegments;

		// Token: 0x040000B3 RID: 179
		public List<PathNode> AllNodePaths;

		// Token: 0x040000B4 RID: 180
		public Dictionary<string, MapRoadSegment> SegmentDictionary;

		// Token: 0x040000B5 RID: 181
		private Dictionary<Guid, MapRoadSegmentPath> PathDictionary;

		// Token: 0x040000B6 RID: 182
		private Dictionary<string, MapRoadSegment> UsedDictionary;
	}
}
