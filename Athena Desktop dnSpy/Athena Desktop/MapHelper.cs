using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Athena.Objects.v2;
using Athena.Tools;
using Newtonsoft.Json;

namespace Athena.App.W7
{
	// Token: 0x0200001B RID: 27
	public class MapHelper
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000061BC File Offset: 0x000043BC
		public MapHelper()
		{
			this.RenderMode = Enums.RenderMode.Elevations;
			this.MapHeights = new List<ElevationCells>();
			this.DropModels = new string[]
			{
				"sloup",
				"powline",
				"powerwire",
				"highvoltage",
				"terrace",
				"podesta_",
				"coltan_heap"
			};
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006225 File Offset: 0x00004425
		// (set) Token: 0x06000121 RID: 289 RVA: 0x0000622D File Offset: 0x0000442D
		public Enums.RenderMode RenderMode { get; set; }

		// Token: 0x06000122 RID: 290 RVA: 0x00006238 File Offset: 0x00004438
		public void DisplayMap(ref Map Map, ref VirtualCanvas VC)
		{
			try
			{
				if (!(Map == null | VC == null))
				{
					GC.Collect();
					GC.WaitForPendingFinalizers();
					VC.CanvasSize = new Size((double)Map.WorldSize, (double)Map.WorldSize);
					VC.ScrollOwner.UpdateLayout();
					VC.UpdateGridSize(Map.WorldSize, 2048);
					List<GeometryHeightLine> list = new List<GeometryHeightLine>();
					List<GeometryHeightShape> list2 = new List<GeometryHeightShape>();
					this.PopulateHeights(ref Map, ref list, ref list2);
					this.DistributeHeights(ref VC, ref list, ref list2);
					List<GeometryForest> list3 = new List<GeometryForest>();
					List<GeometryForest> list4 = new List<GeometryForest>();
					this.PopulateForests(ref Map, ref list3, ref list4);
					this.DistributeForests(ref VC, ref list3, ref list4);
					List<GeometryObject> list5 = new List<GeometryObject>();
					this.PopulateObjects(ref Map, ref list5);
					this.DistributeObjects(ref VC, ref list5);
					List<GeometryRoad> list6 = new List<GeometryRoad>();
					List<Geometry> list7 = new List<Geometry>();
					this.PopulateRoads(ref Map, ref list6, ref list7);
					this.DistributeRoads(ref VC, ref list6);
					this.DistributeRoadObjects(ref VC, ref list7);
					List<Geometry> list8 = new List<Geometry>();
					this.PopulateTrees(ref Map, ref list8);
					this.DistributeTrees(ref VC, ref list8);
					this.GenerateBackground(ref Map, ref VC);
					VC.InvalidateArrange();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000637C File Offset: 0x0000457C
		private void PopulateHeights(ref Map Map, ref List<GeometryHeightLine> GeometryHeightLines, ref List<GeometryHeightShape> GeometryHeightShapes)
		{
			checked
			{
				if (!(Map == null | GeometryHeightLines == null | GeometryHeightShapes == null))
				{
					try
					{
						this.MapHeights.Clear();
						this.MapHeights = new List<ElevationCells>();
						List<string> list = Directory.EnumerateFiles(Map.Folder + "\\Height").ToList<string>();
						if (list != null)
						{
							try
							{
								foreach (string path in list)
								{
									string value = File.ReadAllText(path);
									this.MapHeights.Add(JsonConvert.DeserializeObject<ElevationCells>(value));
								}
							}
							finally
							{
								List<string>.Enumerator enumerator;
								((IDisposable)enumerator).Dispose();
							}
						}
						if (this.MapHeights != null)
						{
							int num = this.MapHeights.Max((MapHelper._Closure$__.$I8-0 == null) ? (MapHelper._Closure$__.$I8-0 = ((ElevationCells x) => x.Z)) : MapHelper._Closure$__.$I8-0);
							try
							{
								foreach (ElevationCells elevationCells in this.MapHeights)
								{
									if (elevationCells.PointGroups != null && elevationCells.PointGroups.Count != 0)
									{
										Color strokeColor = Colors.MediumBlue;
										double strokeThickness = 0.75;
										if (elevationCells.Z != 0)
										{
											if (elevationCells.Z < 0)
											{
												strokeColor = Colors.DodgerBlue;
											}
											else
											{
												strokeColor = Colors.RosyBrown;
											}
										}
										double minimumZoom = 2.0;
										if (elevationCells.Z % 10 == 0)
										{
											minimumZoom = 1.2;
										}
										if (elevationCells.Z % 30 == 0)
										{
											minimumZoom = 0.5;
										}
										if (elevationCells.Z % 60 == 0)
										{
											minimumZoom = 0.3;
										}
										if (elevationCells.Z % 120 == 0)
										{
											minimumZoom = 0.0;
										}
										if (elevationCells.Z == num)
										{
											minimumZoom = 0.0;
										}
										try
										{
											foreach (List<Point> list2 in elevationCells.PointGroups)
											{
												List<Point> list3 = this.ResizePoints(ref list2, Map.WorldCell);
												List<Point> list4 = this.TrimPoints(ref list3);
												if (list4 != null)
												{
													int num2 = list4.Count - 1;
													for (int i = 0; i <= num2; i++)
													{
														if (i != list4.Count - 1)
														{
															GeometryHeightLines.Add(new GeometryHeightLine
															{
																Geometry = this.GenerateHeightGeometryLine(list4[i], list4[i + 1]),
																StrokeColor = strokeColor,
																StrokeThickness = strokeThickness,
																MinimumZoom = minimumZoom
															});
														}
														else
														{
															GeometryHeightLines.Add(new GeometryHeightLine
															{
																Geometry = this.GenerateHeightGeometryLine(list4[i], list4[0]),
																StrokeColor = strokeColor,
																StrokeThickness = strokeThickness,
																MinimumZoom = minimumZoom
															});
														}
													}
												}
											}
										}
										finally
										{
											List<List<Point>>.Enumerator enumerator3;
											((IDisposable)enumerator3).Dispose();
										}
									}
								}
							}
							finally
							{
								List<ElevationCells>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
						}
					}
					catch (Exception ex)
					{
					}
				}
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000066E0 File Offset: 0x000048E0
		private Geometry GenerateHeightGeometryLine(Point Point1, Point Point2)
		{
			StreamGeometry streamGeometry = new StreamGeometry();
			StreamGeometryContext streamGeometryContext = streamGeometry.Open();
			streamGeometryContext.BeginFigure(Point1, false, false);
			streamGeometryContext.LineTo(Point2, true, true);
			streamGeometry.Freeze();
			streamGeometryContext.Close();
			return streamGeometry;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006718 File Offset: 0x00004918
		private Geometry GenerateHeightGeometryShape(List<Point> Points)
		{
			StreamGeometry streamGeometry = new StreamGeometry();
			StreamGeometryContext streamGeometryContext = streamGeometry.Open();
			streamGeometryContext.BeginFigure(Points[0], true, false);
			streamGeometryContext.PolyLineTo(Points.Skip(1).ToList<Point>(), false, false);
			streamGeometry.Freeze();
			streamGeometryContext.Close();
			return streamGeometry;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006760 File Offset: 0x00004960
		private void DistributeHeights(ref VirtualCanvas VC, ref List<GeometryHeightLine> GeometryHeightLines, ref List<GeometryHeightShape> GeometryHeightShapes)
		{
			if (!(VC == null | GeometryHeightLines == null | GeometryHeightShapes == null))
			{
				try
				{
					foreach (GeometryHeightLine geometryHeightLine in GeometryHeightLines)
					{
						try
						{
							foreach (VirtualHost virtualHost in VC.VHChildren)
							{
								if (geometryHeightLine.Geometry.Bounds.IntersectsWith(virtualHost.ChildCell.cellRect))
								{
									virtualHost.ChildCell.GeometryHeightLines.Add(geometryHeightLine);
									if (virtualHost.ChildCell.cellRect.Contains(geometryHeightLine.Geometry.Bounds))
									{
										break;
									}
								}
							}
						}
						finally
						{
							List<VirtualHost>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					List<GeometryHeightLine>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006858 File Offset: 0x00004A58
		public static int SortElevations(ElevationCells Elevation1, ElevationCells Elevation2)
		{
			int result;
			if (Elevation1 == null)
			{
				if (Elevation2 == null)
				{
					result = 0;
				}
				else
				{
					result = -1;
				}
			}
			else if (Elevation2 == null)
			{
				result = 1;
			}
			else if (Elevation1.Z == Elevation2.Z)
			{
				result = 0;
			}
			else if (Elevation1.Z < Elevation2.Z)
			{
				result = -1;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000068A4 File Offset: 0x00004AA4
		private void PopulateForests(ref Map Map, ref List<GeometryForest> GeometryForestsLight, ref List<GeometryForest> GeometryForestsHeavy)
		{
			if (!(Map == null | GeometryForestsLight == null | GeometryForestsHeavy == null))
			{
				try
				{
					MapForests mapForests = JsonConvert.DeserializeObject<MapForests>(File.ReadAllText(Map.Folder + "\\Forests.txt"));
					if (mapForests != null)
					{
						if (mapForests.CellsLight != null && mapForests.CellsLight.Count != 0)
						{
							Color fillColor = Color.FromArgb(70, 0, 181, 18);
							switch (this.RenderMode)
							{
							default:
								try
								{
									foreach (MapForestCells mapForestCells in mapForests.CellsLight)
									{
										try
										{
											foreach (Point forestPoint in mapForestCells.Cells)
											{
												GeometryForestsLight.Add(new GeometryForest
												{
													Geometry = this.GenerateForestGeometry(ref mapForestCells, forestPoint),
													FillColor = fillColor
												});
											}
										}
										finally
										{
											List<Point>.Enumerator enumerator2;
											((IDisposable)enumerator2).Dispose();
										}
									}
								}
								finally
								{
									List<MapForestCells>.Enumerator enumerator;
									((IDisposable)enumerator).Dispose();
								}
								break;
							}
						}
						if (mapForests.CellsHeavy != null && mapForests.CellsHeavy.Count != 0)
						{
							Color fillColor2 = Color.FromArgb(70, 0, 127, 12);
							switch (this.RenderMode)
							{
							default:
								try
								{
									foreach (MapForestCells mapForestCells2 in mapForests.CellsHeavy)
									{
										try
										{
											foreach (Point forestPoint2 in mapForestCells2.Cells)
											{
												GeometryForestsHeavy.Add(new GeometryForest
												{
													Geometry = this.GenerateForestGeometry(ref mapForestCells2, forestPoint2),
													FillColor = fillColor2
												});
											}
										}
										finally
										{
											List<Point>.Enumerator enumerator4;
											((IDisposable)enumerator4).Dispose();
										}
									}
								}
								finally
								{
									List<MapForestCells>.Enumerator enumerator3;
									((IDisposable)enumerator3).Dispose();
								}
								break;
							}
						}
					}
					mapForests = null;
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006B1C File Offset: 0x00004D1C
		private Geometry GenerateForestGeometry(ref MapForestCells ForestCell, Point ForestPoint)
		{
			RectangleGeometry rectangleGeometry = null;
			if (ForestCell != null)
			{
				rectangleGeometry = new RectangleGeometry(new Rect(ForestPoint.X * 16.0, ForestPoint.Y * 16.0, 16.0, 16.0));
				rectangleGeometry.Freeze();
			}
			return rectangleGeometry;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006B78 File Offset: 0x00004D78
		private void DistributeForests(ref VirtualCanvas VC, ref List<GeometryForest> GeometryForestsLight, ref List<GeometryForest> GeometryForestsHeavy)
		{
			if (!(VC == null | GeometryForestsHeavy == null | GeometryForestsLight == null))
			{
				try
				{
					foreach (GeometryForest geometryForest in GeometryForestsHeavy)
					{
						try
						{
							foreach (VirtualHost virtualHost in VC.VHChildren)
							{
								if (geometryForest.Geometry.Bounds.IntersectsWith(virtualHost.ChildCell.cellRect))
								{
									virtualHost.ChildCell.GeometryForestsHeavy.Add(geometryForest);
									if (virtualHost.ChildCell.cellRect.Contains(geometryForest.Geometry.Bounds))
									{
										break;
									}
								}
							}
						}
						finally
						{
							List<VirtualHost>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					List<GeometryForest>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				try
				{
					foreach (GeometryForest geometryForest2 in GeometryForestsLight)
					{
						try
						{
							foreach (VirtualHost virtualHost2 in VC.VHChildren)
							{
								if (geometryForest2.Geometry.Bounds.IntersectsWith(virtualHost2.ChildCell.cellRect))
								{
									virtualHost2.ChildCell.GeometryForestsLight.Add(geometryForest2);
									if (virtualHost2.ChildCell.cellRect.Contains(geometryForest2.Geometry.Bounds))
									{
										break;
									}
								}
							}
						}
						finally
						{
							List<VirtualHost>.Enumerator enumerator4;
							((IDisposable)enumerator4).Dispose();
						}
					}
				}
				finally
				{
					List<GeometryForest>.Enumerator enumerator3;
					((IDisposable)enumerator3).Dispose();
				}
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00006D48 File Offset: 0x00004F48
		private void PopulateObjects(ref Map Map, ref List<GeometryObject> GeometryObjects)
		{
			if (!(Map == null | GeometryObjects == null))
			{
				try
				{
					List<MapObject> list = JsonConvert.DeserializeObject<List<MapObject>>(File.ReadAllText(Map.Folder + "\\Objects.txt"));
					if (list != null)
					{
						try
						{
							foreach (MapObject mapObject in list)
							{
								bool flag = true;
								foreach (string value in this.DropModels)
								{
									if (mapObject.Model.Contains(value))
									{
										flag = false;
									}
								}
								if (flag)
								{
									if (mapObject.CanvasX == -1.0)
									{
										Common.ConvertPos(Map.WorldSize, mapObject.PosX, mapObject.PosY, ref mapObject.CanvasX, ref mapObject.CanvasY);
									}
									Geometry geometry = this.GenerateObjectGeometry(ref mapObject);
									double minimumZoom = 0.0;
									if (geometry.Bounds.Width < 10.0 & geometry.Bounds.Height < 10.0)
									{
										minimumZoom = 0.5;
									}
									if (geometry.Bounds.Width < 5.0 & geometry.Bounds.Height < 5.0)
									{
										minimumZoom = 0.8;
									}
									if (geometry.Bounds.Width < 2.0 & geometry.Bounds.Height < 2.0)
									{
										minimumZoom = 1.2;
									}
									GeometryObjects.Add(new GeometryObject
									{
										Geometry = geometry,
										MinimumZoom = minimumZoom
									});
								}
							}
						}
						finally
						{
							List<MapObject>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006F6C File Offset: 0x0000516C
		private Geometry GenerateObjectGeometry(ref MapObject MapObject)
		{
			RectangleGeometry rectangleGeometry = null;
			if (MapObject != null)
			{
				double num = Convert.ToDouble(MapObject.Width, CultureInfo.InvariantCulture);
				if (num < 1.0)
				{
					num = 1.0;
				}
				double num2 = Convert.ToDouble(MapObject.Length, CultureInfo.InvariantCulture);
				if (num2 < 1.0)
				{
					num2 = 1.0;
				}
				double angle = Convert.ToDouble(MapObject.Dir, CultureInfo.InvariantCulture);
				rectangleGeometry = new RectangleGeometry(new Rect(MapObject.CanvasX - num / 2.0, MapObject.CanvasY - num2 / 2.0, num, num2), 0.0, 0.0);
				rectangleGeometry.Transform = new TransformGroup
				{
					Children = 
					{
						new RotateTransform(angle, MapObject.CanvasX, MapObject.CanvasY)
					}
				};
			}
			rectangleGeometry.Freeze();
			return rectangleGeometry;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00007060 File Offset: 0x00005260
		private void DistributeObjects(ref VirtualCanvas VC, ref List<GeometryObject> GeometryObjects)
		{
			if (!(VC == null | GeometryObjects == null))
			{
				try
				{
					foreach (GeometryObject geometryObject in GeometryObjects)
					{
						try
						{
							foreach (VirtualHost virtualHost in VC.VHChildren)
							{
								if (geometryObject.Geometry.Bounds.IntersectsWith(virtualHost.ChildCell.cellRect))
								{
									virtualHost.ChildCell.GeometryObjects.Add(geometryObject);
									if (virtualHost.ChildCell.cellRect.Contains(geometryObject.Geometry.Bounds))
									{
										break;
									}
								}
							}
						}
						finally
						{
							List<VirtualHost>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					List<GeometryObject>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00007150 File Offset: 0x00005350
		private void PopulateRoads(ref Map Map, ref List<GeometryRoad> GeometryRoads, ref List<Geometry> GeometryRoadObjects)
		{
			if (!(Map == null | GeometryRoads == null | GeometryRoadObjects == null))
			{
				try
				{
					MapRoads mapRoads = JsonConvert.DeserializeObject<MapRoads>(File.ReadAllText(Map.Folder + "\\Roads.txt"));
					if (mapRoads != null)
					{
						try
						{
							foreach (MapRoad mapRoad in mapRoads.Roads)
							{
								bool flag = false;
								if (string.IsNullOrEmpty(mapRoad.Model))
								{
									flag = true;
								}
								if (flag)
								{
									switch (mapRoad.RoadType)
									{
									case Enums.RoadType.Highway:
										GeometryRoads.Add(new GeometryRoad
										{
											Geometry = this.GenerateRoadGeometry(ref mapRoad),
											StrokeColor = Colors.SandyBrown
										});
										break;
									case Enums.RoadType.Concrete:
										GeometryRoads.Add(new GeometryRoad
										{
											Geometry = this.GenerateRoadGeometry(ref mapRoad),
											StrokeColor = Colors.LightGray
										});
										break;
									case Enums.RoadType.Asphalt:
										GeometryRoads.Add(new GeometryRoad
										{
											Geometry = this.GenerateRoadGeometry(ref mapRoad),
											StrokeColor = Colors.DimGray
										});
										break;
									case Enums.RoadType.Dirt:
										GeometryRoads.Add(new GeometryRoad
										{
											Geometry = this.GenerateRoadGeometry(ref mapRoad),
											StrokeColor = Colors.Tan
										});
										break;
									default:
										GeometryRoads.Add(new GeometryRoad
										{
											Geometry = this.GenerateRoadGeometry(ref mapRoad),
											StrokeColor = Colors.Brown
										});
										break;
									}
								}
								else
								{
									GeometryRoadObjects.Add(this.GenerateRoadObjectGeometry(ref mapRoad));
								}
							}
						}
						finally
						{
							List<MapRoad>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00007328 File Offset: 0x00005528
		private Geometry GenerateRoadGeometry(ref MapRoad Road)
		{
			StreamGeometry streamGeometry = new StreamGeometry();
			StreamGeometryContext streamGeometryContext = streamGeometry.Open();
			if (Road != null && Road.Nodes != null && Road.Nodes.Count != 0)
			{
				PointCollection pointCollection = new PointCollection(Road.Nodes);
				streamGeometryContext.BeginFigure(pointCollection[0], false, false);
				streamGeometryContext.PolyLineTo(pointCollection, true, true);
			}
			streamGeometry.Freeze();
			streamGeometryContext.Close();
			return streamGeometry;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00007390 File Offset: 0x00005590
		private Geometry GenerateRoadObjectGeometry(ref MapRoad Road)
		{
			RectangleGeometry rectangleGeometry = null;
			if (Road != null && Road.Nodes != null && Road.Nodes.Count == 1)
			{
				double num = Convert.ToDouble(Road.Width, CultureInfo.InvariantCulture);
				if (num < 1.0)
				{
					num = 1.0;
				}
				double num2 = Convert.ToDouble(Road.Length, CultureInfo.InvariantCulture);
				if (num2 < 1.0)
				{
					num2 = 1.0;
				}
				double angle = Convert.ToDouble(Road.Dir, CultureInfo.InvariantCulture);
				rectangleGeometry = new RectangleGeometry(new Rect(Road.Nodes[0].X - num / 2.0, Road.Nodes[0].Y - num2 / 2.0, num, num2), 0.0, 0.0);
				rectangleGeometry.Transform = new TransformGroup
				{
					Children = 
					{
						new RotateTransform(angle, Road.Nodes[0].X, Road.Nodes[0].Y)
					}
				};
			}
			rectangleGeometry.Freeze();
			return rectangleGeometry;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000074EC File Offset: 0x000056EC
		private void DistributeRoads(ref VirtualCanvas VC, ref List<GeometryRoad> GeometryRoads)
		{
			if (!(VC == null | GeometryRoads == null))
			{
				try
				{
					foreach (GeometryRoad geometryRoad in GeometryRoads)
					{
						try
						{
							foreach (VirtualHost virtualHost in VC.VHChildren)
							{
								if (geometryRoad.Geometry.Bounds.IntersectsWith(virtualHost.ChildCell.cellRect))
								{
									virtualHost.ChildCell.GeometryRoads.Add(geometryRoad);
									if (virtualHost.ChildCell.cellRect.Contains(geometryRoad.Geometry.Bounds))
									{
										break;
									}
								}
							}
						}
						finally
						{
							List<VirtualHost>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					List<GeometryRoad>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000075DC File Offset: 0x000057DC
		private void DistributeRoadObjects(ref VirtualCanvas VC, ref List<Geometry> GeometryRoadObjects)
		{
			if (!(VC == null | GeometryRoadObjects == null))
			{
				try
				{
					foreach (Geometry geometry in GeometryRoadObjects)
					{
						try
						{
							foreach (VirtualHost virtualHost in VC.VHChildren)
							{
								if (geometry.Bounds.IntersectsWith(virtualHost.ChildCell.cellRect))
								{
									virtualHost.ChildCell.GeometryRoadObjects.Add(geometry);
									if (virtualHost.ChildCell.cellRect.Contains(geometry.Bounds))
									{
										break;
									}
								}
							}
						}
						finally
						{
							List<VirtualHost>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					List<Geometry>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000076C4 File Offset: 0x000058C4
		private void PopulateTrees(ref Map Map, ref List<Geometry> GeometryTrees)
		{
			if (!(Map == null | GeometryTrees == null))
			{
				try
				{
					List<MapBush> list = JsonConvert.DeserializeObject<List<MapBush>>(File.ReadAllText(Map.Folder + "\\Trees.txt"));
					if (list != null)
					{
						try
						{
							foreach (MapBush mapBush in list)
							{
								GeometryTrees.Add(this.GenerateTreeGeometry(ref mapBush));
							}
						}
						finally
						{
							List<MapBush>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007768 File Offset: 0x00005968
		private Geometry GenerateTreeGeometry(ref MapBush Tree)
		{
			EllipseGeometry ellipseGeometry = null;
			if (Tree != null)
			{
				ellipseGeometry = new EllipseGeometry(new Point(Tree.CanvasX, Tree.CanvasY), 2.0, 2.0);
				ellipseGeometry.Freeze();
			}
			return ellipseGeometry;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000077B0 File Offset: 0x000059B0
		private void DistributeTrees(ref VirtualCanvas VC, ref List<Geometry> GeometryTrees)
		{
			if (!(VC == null | GeometryTrees == null))
			{
				try
				{
					foreach (Geometry geometry in GeometryTrees)
					{
						try
						{
							foreach (VirtualHost virtualHost in VC.VHChildren)
							{
								if (geometry.Bounds.IntersectsWith(virtualHost.ChildCell.cellRect))
								{
									virtualHost.ChildCell.GeometryTrees.Add(geometry);
									if (virtualHost.ChildCell.cellRect.Contains(geometry.Bounds))
									{
										break;
									}
								}
							}
						}
						finally
						{
							List<VirtualHost>.Enumerator enumerator2;
							((IDisposable)enumerator2).Dispose();
						}
					}
				}
				finally
				{
					List<Geometry>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00007898 File Offset: 0x00005A98
		public List<Point> ResizePoints(ref List<Point> Points, int Multiplier)
		{
			List<Point> list = new List<Point>();
			if (Points != null)
			{
				try
				{
					foreach (Point point in Points)
					{
						list.Add(new Point(point.X * (double)Multiplier, point.Y * (double)Multiplier));
					}
				}
				finally
				{
					List<Point>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			return list;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000790C File Offset: 0x00005B0C
		public List<Point> DesizePoints(ref List<Point> Points, int Multiplier)
		{
			List<Point> list = new List<Point>();
			if (Points != null)
			{
				try
				{
					foreach (Point point in Points)
					{
						list.Add(new Point(point.X / (double)Multiplier, point.Y / (double)Multiplier));
					}
				}
				finally
				{
					List<Point>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			return list;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007980 File Offset: 0x00005B80
		public List<Point> TrimPoints(ref List<Point> Points)
		{
			new List<Point>();
			return new SimplifyUtility().Simplify(Points.ToArray(), 5.0, true);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000079A4 File Offset: 0x00005BA4
		public void GenerateBackground(ref Map Map, ref VirtualCanvas VC)
		{
			VC.Content.Background = null;
			GC.Collect();
			GC.WaitForPendingFinalizers();
			try
			{
				if (this.MapHeights != null)
				{
					this.MapHeights.Sort(new Comparison<ElevationCells>(MapHelper.SortElevations));
					List<ElevationCells> list = new List<ElevationCells>();
					try
					{
						foreach (ElevationCells elevationCells in this.MapHeights)
						{
							ElevationCells elevationCells2 = new ElevationCells
							{
								Z = elevationCells.Z
							};
							if (elevationCells.PointGroups != null)
							{
								try
								{
									foreach (List<Point> list2 in elevationCells.PointGroups)
									{
										List<Point> list3 = this.ResizePoints(ref list2, Map.WorldCell);
										List<Point> list4 = this.TrimPoints(ref list3);
										List<Point> item = this.DesizePoints(ref list4, Map.WorldCell);
										elevationCells2.PointGroups.Add(item);
									}
								}
								finally
								{
									List<List<Point>>.Enumerator enumerator2;
									((IDisposable)enumerator2).Dispose();
								}
							}
							list.Add(elevationCells2);
						}
					}
					finally
					{
						List<ElevationCells>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					ImageBrush imageBrush = this.GenerateHeightMap(ref list, this.RenderMode, checked((int)Math.Round((double)Map.WorldSize / (double)Map.WorldCell)));
					if (imageBrush != null)
					{
						VC.Content.Background = imageBrush;
						VC.Content.Background.Freeze();
						VC.ScrollOwner.InvalidateVisual();
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007B68 File Offset: 0x00005D68
		public ImageBrush GenerateHeightMap(ref List<ElevationCells> Elevations, Enums.RenderMode RenderMode, int Size)
		{
			ImageBrush imageBrush = new ImageBrush();
			try
			{
				DrawingVisual drawingVisual = this.GenerateVisual(ref Elevations, RenderMode, Size);
				if (drawingVisual != null)
				{
					RenderTargetBitmap renderTargetBitmap = this.GenerateBMP(ref drawingVisual, Size);
					if (renderTargetBitmap != null)
					{
						imageBrush.ImageSource = BitmapFrame.Create(renderTargetBitmap);
						imageBrush.Freeze();
					}
				}
			}
			catch (Exception ex)
			{
				imageBrush = null;
			}
			return imageBrush;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007BCC File Offset: 0x00005DCC
		private RenderTargetBitmap GenerateBMP(ref DrawingVisual Visual, int Size)
		{
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(Size, Size, 96.0, 96.0, PixelFormats.Default);
			renderTargetBitmap.Render(Visual);
			renderTargetBitmap.Freeze();
			return renderTargetBitmap;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007BFC File Offset: 0x00005DFC
		private DrawingVisual GenerateVisual(ref List<ElevationCells> Elevations, Enums.RenderMode RenderMode, int Size)
		{
			DrawingVisual drawingVisual = new DrawingVisual();
			DrawingContext drawingContext = drawingVisual.RenderOpen();
			int elevationMax = Elevations.Max((MapHelper._Closure$__.$I33-0 == null) ? (MapHelper._Closure$__.$I33-0 = ((ElevationCells x) => x.Z)) : MapHelper._Closure$__.$I33-0);
			DrawingContext drawingContext2 = drawingContext;
			int z = -1000;
			Brush brush = this.GenerateBrush(ref z, elevationMax, ref RenderMode);
			Pen pen = this.GeneratePen();
			List<Point> list = new Point[]
			{
				new Point(0.0, 0.0),
				new Point(0.0, (double)Size),
				new Point((double)Size, (double)Size),
				new Point((double)Size, 0.0)
			}.ToList<Point>();
			drawingContext2.DrawGeometry(brush, pen, this.GenerateGeometry(ref list));
			if (Elevations != null)
			{
				try
				{
					foreach (ElevationCells elevationCells in Elevations)
					{
						if (elevationCells.PointGroups != null)
						{
							try
							{
								foreach (List<Point> list2 in elevationCells.PointGroups)
								{
									DrawingContext drawingContext3 = drawingContext;
									ElevationCells elevationCells2;
									z = (elevationCells2 = elevationCells).Z;
									Brush brush2 = this.GenerateBrush(ref z, elevationMax, ref RenderMode);
									elevationCells2.Z = z;
									drawingContext3.DrawGeometry(brush2, this.GeneratePen(), this.GenerateGeometry(ref list2));
								}
							}
							finally
							{
								List<List<Point>>.Enumerator enumerator2;
								((IDisposable)enumerator2).Dispose();
							}
						}
					}
				}
				finally
				{
					List<ElevationCells>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
			drawingContext.Close();
			BitmapCache bitmapCache = new BitmapCache();
			bitmapCache.RenderAtScale = 0.5;
			bitmapCache.Freeze();
			drawingVisual.CacheMode = bitmapCache;
			drawingVisual.CacheMode.Freeze();
			return drawingVisual;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007DC8 File Offset: 0x00005FC8
		private Geometry GenerateGeometry(ref List<Point> Points)
		{
			StreamGeometry streamGeometry = new StreamGeometry();
			if (Points != null && Points.Count != 0)
			{
				StreamGeometryContext streamGeometryContext = streamGeometry.Open();
				streamGeometryContext.BeginFigure(Points[0], true, false);
				streamGeometryContext.PolyLineTo(Points, false, false);
				streamGeometryContext.Close();
			}
			streamGeometry.Freeze();
			return streamGeometry;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007E14 File Offset: 0x00006014
		public Color GetElevationFillColor(int Elevation, int MaxElevation, Enums.RenderMode RenderMode)
		{
			Color result = Colors.Transparent;
			double num = (double)Elevation / (double)MaxElevation;
			switch (RenderMode)
			{
			case Enums.RenderMode.Elevations:
				if (Elevation == 0)
				{
					result = Colors.Ivory;
				}
				else if (Elevation < 0)
				{
					result = Colors.PowderBlue;
				}
				else
				{
					result = Colors.Transparent;
				}
				break;
			case Enums.RenderMode.HeightMapColor:
				if (num >= 0.0)
				{
					double h = 120.0 - num * 120.0;
					double s = 1.0;
					double num2 = (num * 50.0 + 50.0) / 100.0;
					if (num2 > 0.8)
					{
						num2 -= num2 - 0.8;
					}
					HSVtoRGB.RGB rgb = HSVtoRGB.HSVToRGB(new HSVtoRGB.HSV(h, s, num2));
					result = Color.FromRgb(rgb.R, rgb.G, rgb.B);
				}
				else
				{
					result = Colors.PowderBlue;
				}
				break;
			case Enums.RenderMode.HeightMapGray:
				if (num >= 0.0)
				{
					double v = num;
					HSVtoRGB.RGB rgb2 = HSVtoRGB.HSVToRGB(new HSVtoRGB.HSV(0.0, 0.0, v));
					result = Color.FromRgb(rgb2.R, rgb2.G, rgb2.B);
				}
				else
				{
					result = Colors.PowderBlue;
				}
				break;
			}
			return result;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007F68 File Offset: 0x00006168
		private Brush GenerateBrush(ref int Elevation, int ElevationMax, ref Enums.RenderMode RenderMode)
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush(this.GetElevationFillColor(Elevation, ElevationMax, RenderMode));
			solidColorBrush.Freeze();
			return solidColorBrush;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007F80 File Offset: 0x00006180
		private Pen GeneratePen()
		{
			Pen pen = new Pen();
			pen.Thickness = 1.0;
			pen.Freeze();
			return pen;
		}

		// Token: 0x04000087 RID: 135
		private List<ElevationCells> MapHeights;

		// Token: 0x04000088 RID: 136
		private string[] DropModels;
	}
}
