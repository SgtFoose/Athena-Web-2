using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using Athena.Objects.v2;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x0200002E RID: 46
	[DesignerGenerated]
	public class Marker : UserControl, IComponentConnector
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000E09B File Offset: 0x0000C29B
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000E0A3 File Offset: 0x0000C2A3
		public Enums.MarkerIconParentType ParentType { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000E0AC File Offset: 0x0000C2AC
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000E0B4 File Offset: 0x0000C2B4
		public System.Windows.Media.Color Color { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000E0BD File Offset: 0x0000C2BD
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000E0C5 File Offset: 0x0000C2C5
		public System.Windows.Media.Color TextColor { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000E0CE File Offset: 0x0000C2CE
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000E0D6 File Offset: 0x0000C2D6
		public System.Windows.Media.Color TextColorBackground { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000E0DF File Offset: 0x0000C2DF
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000E0E7 File Offset: 0x0000C2E7
		public double TextColorBackgroundOpacity { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		public bool ShowText { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000E101 File Offset: 0x0000C301
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000E109 File Offset: 0x0000C309
		public double IconSizeX { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000E112 File Offset: 0x0000C312
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000E11A File Offset: 0x0000C31A
		public double IconSizeY { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000E123 File Offset: 0x0000C323
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000E12B File Offset: 0x0000C32B
		public double IconSizeMultiplier { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000E134 File Offset: 0x0000C334
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000E13C File Offset: 0x0000C33C
		public double IconSizeMultiplierHost { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000E145 File Offset: 0x0000C345
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000E14D File Offset: 0x0000C34D
		public double RotationalDiameter { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000E156 File Offset: 0x0000C356
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000E15E File Offset: 0x0000C35E
		public double ScaleFactor { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000E167 File Offset: 0x0000C367
		public double IconWidth
		{
			get
			{
				return this.IconHolder.Width;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000E174 File Offset: 0x0000C374
		public double IconHeight
		{
			get
			{
				return this.IconHolder.Height;
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600027B RID: 635 RVA: 0x0000E184 File Offset: 0x0000C384
		// (remove) Token: 0x0600027C RID: 636 RVA: 0x0000E1BC File Offset: 0x0000C3BC
		public event Marker.MarkerIconSizeChangedEventHandler MarkerIconSizeChanged;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600027D RID: 637 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		// (remove) Token: 0x0600027E RID: 638 RVA: 0x0000E22C File Offset: 0x0000C42C
		public event Marker.OnMarkerUpdateEventHandler OnMarkerUpdate;

		// Token: 0x0600027F RID: 639 RVA: 0x0000E264 File Offset: 0x0000C464
		public Marker()
		{
			base.SizeChanged += this.MarkerIcon_SizeChanged;
			this.BackgroundBrush = null;
			this.ShapeDrawing = null;
			this.ShapeImage = null;
			this.Marker = new Marker();
			this.ParentType = Enums.MarkerIconParentType.Marker;
			this.Color = Colors.White;
			this.TextColor = Colors.Transparent;
			this.TextColorBackground = Colors.Transparent;
			this.TextColorBackgroundOpacity = 1.0;
			this.ShowText = false;
			this.IconSizeX = 0.0;
			this.IconSizeY = 0.0;
			this.IconSizeMultiplier = 1.0;
			this.IconSizeMultiplierHost = 1.0;
			this.RotationalDiameter = -1.0;
			this.ScaleFactor = 1.0;
			this.InitializeComponent();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E348 File Offset: 0x0000C548
		public Marker(Marker Marker, bool ShowText = false)
		{
			base.SizeChanged += this.MarkerIcon_SizeChanged;
			this.BackgroundBrush = null;
			this.ShapeDrawing = null;
			this.ShapeImage = null;
			this.Marker = new Marker();
			this.ParentType = Enums.MarkerIconParentType.Marker;
			this.Color = Colors.White;
			this.TextColor = Colors.Transparent;
			this.TextColorBackground = Colors.Transparent;
			this.TextColorBackgroundOpacity = 1.0;
			this.ShowText = false;
			this.IconSizeX = 0.0;
			this.IconSizeY = 0.0;
			this.IconSizeMultiplier = 1.0;
			this.IconSizeMultiplierHost = 1.0;
			this.RotationalDiameter = -1.0;
			this.ScaleFactor = 1.0;
			this.InitializeComponent();
			this.ShowText = ShowText;
			this.Update(Marker);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000E43C File Offset: 0x0000C63C
		public void Update(Marker MarkerNew)
		{
			bool markerChanged = this.GetMarkerChanged(ref MarkerNew);
			bool rotationChanged = this.GetRotationChanged(ref MarkerNew);
			bool textChanged = this.GetTextChanged(ref MarkerNew);
			this.Marker = MarkerNew;
			if (markerChanged)
			{
				this.BackgroundBrush = null;
				this.ShapeDrawing = null;
				this.ShapeImage = null;
				this.IconSizeX = 0.0;
				this.IconSizeY = 0.0;
				this.IconSizeMultiplier = 1.0;
				if (this.Marker.Width == 0.0 | this.Marker.Height == 0.0)
				{
					this.Icon.Source = null;
					this.Icon.Visibility = Visibility.Collapsed;
				}
				else
				{
					try
					{
						Marker marker = this.Marker;
						System.Windows.Media.Color color = this.Color;
						this.LoadColor(ref marker.ColorID, ref color);
						this.Color = color;
						switch (this.Marker.ShapeID)
						{
						case Enums.MarkerShape.ICON:
							this.LoadIcon();
							break;
						case Enums.MarkerShape.ELLIPSE:
							this.LoadShape();
							break;
						case Enums.MarkerShape.RECTANGLE:
							this.LoadShape();
							break;
						}
						this.LoadSize();
					}
					catch (Exception ex)
					{
					}
				}
			}
			if (rotationChanged)
			{
				this.LoadDir();
			}
			if (textChanged)
			{
				this.LoadText();
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000E590 File Offset: 0x0000C790
		private void LoadShape()
		{
			this.ShapeDrawing = new GeometryDrawing();
			Enums.MarkerShape shapeID = this.Marker.ShapeID;
			if (shapeID != Enums.MarkerShape.ELLIPSE)
			{
				if (shapeID == Enums.MarkerShape.RECTANGLE)
				{
					this.ShapeDrawing.Geometry = this.GetRectangleGeometry();
				}
			}
			else
			{
				this.ShapeDrawing.Geometry = this.GetEllipseGeometry();
			}
			if (this.ShapeDrawing.Geometry == null)
			{
				this.Icon.Source = null;
				this.Icon.Visibility = Visibility.Collapsed;
				return;
			}
			switch (this.Marker.BrushID)
			{
			case Enums.MarkerBrush.Solid:
			case Enums.MarkerBrush.SolidFull:
				this.ShapeDrawing.Brush = new SolidColorBrush(this.Color)
				{
					Opacity = this.Marker.Alpha
				};
				break;
			case Enums.MarkerBrush.Horizontal:
			case Enums.MarkerBrush.Vertical:
			case Enums.MarkerBrush.Grid:
			case Enums.MarkerBrush.FDiagonal:
			case Enums.MarkerBrush.BDiagonal:
			case Enums.MarkerBrush.DiagGrid:
			case Enums.MarkerBrush.Cross:
				this.BackgroundBrush = this.GetImageBrush();
				if (this.BackgroundBrush != null)
				{
					this.BackgroundBrush.Stretch = Stretch.Uniform;
					this.BackgroundBrush.ViewportUnits = BrushMappingMode.Absolute;
					this.BackgroundBrush.Viewport = new Rect(0.0, 0.0, this.BackgroundBrush.ImageSource.Width, this.BackgroundBrush.ImageSource.Height);
					this.BackgroundBrush.TileMode = TileMode.Tile;
					this.BackgroundBrush.Opacity = this.Marker.Alpha;
					this.ShapeDrawing.Brush = this.BackgroundBrush;
				}
				break;
			case Enums.MarkerBrush.Border:
				this.ShapeDrawing.Pen = new System.Windows.Media.Pen(new SolidColorBrush(this.Color)
				{
					Opacity = this.Marker.Alpha
				}, 1.0);
				break;
			case Enums.MarkerBrush.SolidBorder:
				this.ShapeDrawing.Brush = new SolidColorBrush(this.Color)
				{
					Opacity = this.Marker.Alpha
				};
				this.ShapeDrawing.Pen = new System.Windows.Media.Pen(new SolidColorBrush(this.Color)
				{
					Opacity = this.Marker.Alpha
				}, 1.0);
				break;
			}
			if (this.ShapeDrawing == null)
			{
				this.Icon.Source = null;
				this.Icon.Visibility = Visibility.Collapsed;
				return;
			}
			this.ShapeImage = new DrawingImage(this.ShapeDrawing);
			this.IconSizeX = this.ShapeImage.Width;
			this.IconSizeY = this.ShapeImage.Height;
			this.IconSizeMultiplier = 1.0;
			this.Icon.Source = this.ShapeImage;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000E829 File Offset: 0x0000CA29
		private Geometry GetEllipseGeometry()
		{
			return new EllipseGeometry(new System.Windows.Point(this.Marker.Width, this.Marker.Height), this.Marker.Width, this.Marker.Height);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000E864 File Offset: 0x0000CA64
		private Geometry GetRectangleGeometry()
		{
			return new RectangleGeometry(new Rect(0.0, 0.0, this.Marker.Width * 2.0, this.Marker.Height * 2.0));
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		private void LoadIcon()
		{
			if (this.Marker.Width == 0.0 | this.Marker.Height == 0.0)
			{
				this.Icon.Source = null;
				this.Icon.Visibility = Visibility.Collapsed;
				return;
			}
			string iconFile = this.GetIconFile();
			if (string.IsNullOrEmpty(iconFile))
			{
				this.Icon.Source = null;
				this.Icon.Visibility = Visibility.Collapsed;
				return;
			}
			try
			{
				BitmapSource iconAsBitmap = this.GetIconAsBitmap(iconFile, !this.Marker.Type.Contains("flag_"));
				if (iconAsBitmap == null)
				{
					this.Icon.Source = null;
					this.Icon.Visibility = Visibility.Collapsed;
				}
				else
				{
					this.Icon.Opacity = this.Marker.Alpha;
					this.IconText.Opacity = this.Marker.Alpha;
					this.IconSizeX = (double)iconAsBitmap.PixelWidth;
					this.IconSizeY = (double)iconAsBitmap.PixelHeight;
					this.IconSizeMultiplier = 0.6;
					this.Icon.Source = iconAsBitmap;
					this.LoadShadow();
				}
			}
			catch (Exception ex)
			{
				this.Icon.Source = null;
				this.Icon.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EA14 File Offset: 0x0000CC14
		private string GetIconFile()
		{
			string result = string.Empty;
			string text = Enum.GetName(typeof(Enums.MarkerType), this.Marker.TypeID);
			if (!string.IsNullOrEmpty(text))
			{
				text = text.ToLower();
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
				if (num <= 2608177081U)
				{
					if (num <= 1787721130U)
					{
						if (num != 1697318111U)
						{
							if (num != 1767436421U)
							{
								if (num != 1787721130U)
								{
									goto IL_1FD;
								}
								if (Operators.CompareString(text, "end", false) != 0)
								{
									goto IL_1FD;
								}
							}
							else if (Operators.CompareString(text, "pickup", false) != 0)
							{
								goto IL_1FD;
							}
						}
						else if (Operators.CompareString(text, "start", false) != 0)
						{
							goto IL_1FD;
						}
					}
					else if (num != 2032210671U)
					{
						if (num != 2220107398U)
						{
							if (num != 2608177081U)
							{
								goto IL_1FD;
							}
							if (Operators.CompareString(text, "unknown", false) != 0)
							{
								goto IL_1FD;
							}
						}
						else if (Operators.CompareString(text, "arrow", false) != 0)
						{
							goto IL_1FD;
						}
					}
					else if (Operators.CompareString(text, "warning", false) != 0)
					{
						goto IL_1FD;
					}
				}
				else if (num <= 3086496151U)
				{
					if (num != 2618666040U)
					{
						if (num != 3010117498U)
						{
							if (num != 3086496151U)
							{
								goto IL_1FD;
							}
							if (Operators.CompareString(text, "marker", false) != 0)
							{
								goto IL_1FD;
							}
						}
						else
						{
							if (Operators.CompareString(text, "waypoint", false) != 0)
							{
								goto IL_1FD;
							}
							result = "assets\\map\\mapcontrol\\waypoint.png";
							goto IL_1FD;
						}
					}
					else if (Operators.CompareString(text, "objective", false) != 0)
					{
						goto IL_1FD;
					}
				}
				else if (num <= 3294324549U)
				{
					if (num != 3186272471U)
					{
						if (num != 3294324549U)
						{
							goto IL_1FD;
						}
						if (Operators.CompareString(text, "destroy", false) != 0)
						{
							goto IL_1FD;
						}
					}
					else if (Operators.CompareString(text, "flag", false) != 0)
					{
						goto IL_1FD;
					}
				}
				else if (num != 3374496889U)
				{
					if (num != 3546849056U)
					{
						goto IL_1FD;
					}
					if (Operators.CompareString(text, "dot", false) != 0)
					{
						goto IL_1FD;
					}
				}
				else if (Operators.CompareString(text, "join", false) != 0)
				{
					goto IL_1FD;
				}
				text = "mil_" + text;
				IL_1FD:
				if (text.StartsWith("loc_"))
				{
					result = "assets\\map\\mapcontrol\\" + text.Replace("loc_", "") + ".png";
					if (this.Marker.ColorID == Enums.MarkerColor.ColorDefault)
					{
						if (new string[]
						{
							"loc_bunker",
							"loc_bush",
							"loc_chapel",
							"loc_fortress",
							"loc_fountain",
							"loc_rock",
							"loc_ruin",
							"loc_shipwreck",
							"loc_smalltree",
							"loc_stack",
							"loc_tourism",
							"loc_tree",
							"loc_viewtower",
							"loc_waypointeditor"
						}.Contains(text))
						{
							this.Color = Colors.Black;
						}
						else
						{
							this.Color = Colors.White;
						}
					}
				}
				if (text.StartsWith("flag_"))
				{
					result = "assets\\map\\markers\\flags\\" + text.Replace("flag_", "") + ".png";
				}
				if (text.StartsWith("hd_"))
				{
					result = "assets\\map\\markers\\handdrawn\\" + text.Replace("hd_", "") + ".png";
				}
				if (text.StartsWith("mil_"))
				{
					result = "assets\\map\\markers\\military\\" + text.Replace("mil_", "") + ".png";
				}
				if (text.StartsWith("b_"))
				{
					result = "assets\\map\\markers\\nato\\" + text + ".png";
					if (this.Marker.ColorID == Enums.MarkerColor.ColorDefault)
					{
						this.Color = Common.COLOR_WEST;
					}
				}
				if (text.StartsWith("c_"))
				{
					result = "assets\\map\\markers\\nato\\" + text + ".png";
					if (this.Marker.ColorID == Enums.MarkerColor.ColorDefault)
					{
						this.Color = Colors.White;
					}
				}
				if (text.StartsWith("n_"))
				{
					result = "assets\\map\\markers\\nato\\" + text + ".png";
					if (this.Marker.ColorID == Enums.MarkerColor.ColorDefault)
					{
						this.Color = Common.COLOR_GREEN;
					}
				}
				if (text.StartsWith("o_"))
				{
					result = "assets\\map\\markers\\nato\\" + text + ".png";
					if (this.Marker.ColorID == Enums.MarkerColor.ColorDefault)
					{
						this.Color = Common.COLOR_EAST;
					}
				}
				if (text.StartsWith("u_"))
				{
					result = "assets\\map\\markers\\nato\\" + text + ".png";
					if (this.Marker.ColorID == Enums.MarkerColor.ColorDefault)
					{
						this.Color = Common.COLOR_GREEN;
					}
				}
				if (text.StartsWith("respawn_"))
				{
					result = "assets\\map\\markers\\nato\\" + text + ".png";
				}
				if (text.StartsWith("vehicle_"))
				{
					result = "assets\\map\\vehicleicons\\icon" + text.Replace("vehicle_", "") + ".png";
				}
			}
			return result;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		private Bitmap ToWFBitmap(ref BitmapSource Source)
		{
			Bitmap result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new PngBitmapEncoder
				{
					Frames = 
					{
						BitmapFrame.Create(Source)
					}
				}.Save(memoryStream);
				using (Bitmap bitmap = new Bitmap(memoryStream))
				{
					result = new Bitmap(bitmap);
				}
			}
			return result;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EF5C File Offset: 0x0000D15C
		private BitmapSource ToWPFBitmap(ref Bitmap Source)
		{
			BitmapSource result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				Source.Save(memoryStream, ImageFormat.Png);
				memoryStream.Position = 0L;
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = memoryStream;
				bitmapImage.EndInit();
				bitmapImage.Freeze();
				result = bitmapImage;
			}
			return result;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		private bool GetMarkerChanged(ref Marker MarkerNew)
		{
			bool result = false;
			if (this.Marker.Alpha != MarkerNew.Alpha)
			{
				result = true;
			}
			if (this.Marker.BrushID != MarkerNew.BrushID)
			{
				result = true;
			}
			if (this.Marker.ColorID != MarkerNew.ColorID)
			{
				result = true;
			}
			if (this.Marker.ShapeID != MarkerNew.ShapeID)
			{
				result = true;
			}
			if (this.Marker.TypeID != MarkerNew.TypeID)
			{
				result = true;
			}
			if (Operators.CompareString(this.Marker.PosX, MarkerNew.PosX, false) != 0)
			{
				result = true;
			}
			if (Operators.CompareString(this.Marker.PosY, MarkerNew.PosY, false) != 0)
			{
				result = true;
			}
			if (this.Marker.Width != MarkerNew.Width)
			{
				result = true;
			}
			if (this.Marker.Height != MarkerNew.Height)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000F0AC File Offset: 0x0000D2AC
		private bool GetRotationChanged(ref Marker MarkerNew)
		{
			bool result = false;
			if (this.Marker.Direction != MarkerNew.Direction)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		private bool GetTextChanged(ref Marker MarkerNew)
		{
			bool result = false;
			if (Operators.CompareString(this.Marker.Text, MarkerNew.Text, false) != 0)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000F100 File Offset: 0x0000D300
		private BitmapSource GetIconAsBitmap(string IconFile, bool ApplyColor)
		{
			BitmapSource bitmapSource = null;
			if (!string.IsNullOrEmpty(IconFile))
			{
				try
				{
					bitmapSource = new PngBitmapDecoder(new Uri("pack://application:,,,/" + IconFile, UriKind.Absolute), BitmapCreateOptions.None, BitmapCacheOption.OnLoad).Frames[0];
					if (bitmapSource != null)
					{
						if (ApplyColor)
						{
							Bitmap bitmap = this.ToWFBitmap(ref bitmapSource);
							Graphics graphics = Graphics.FromImage(bitmap);
							float[][] array = new float[5][];
							int num = 0;
							float[] array2 = new float[5];
							array2[0] = (float)this.Color.R / 255f;
							array[num] = array2;
							int num2 = 1;
							float[] array3 = new float[5];
							array3[1] = (float)this.Color.G / 255f;
							array[num2] = array3;
							int num3 = 2;
							float[] array4 = new float[5];
							array4[2] = (float)this.Color.B / 255f;
							array[num3] = array4;
							int num4 = 3;
							float[] array5 = new float[5];
							array5[3] = 1f;
							array[num4] = array5;
							array[4] = new float[]
							{
								0f,
								0f,
								0f,
								0f,
								1f
							};
							ColorMatrix colorMatrix = new ColorMatrix(array);
							ImageAttributes imageAttributes = new ImageAttributes();
							imageAttributes.SetColorMatrix(colorMatrix);
							graphics.DrawImage(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height), 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
							graphics.Dispose();
							bitmapSource = this.ToWPFBitmap(ref bitmap);
						}
						bitmapSource.Freeze();
					}
				}
				catch (Exception ex)
				{
					bitmapSource = null;
				}
			}
			return bitmapSource;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000F270 File Offset: 0x0000D470
		private ImageBrush GetImageBrush()
		{
			ImageBrush imageBrush = new ImageBrush();
			string text = "assets\\map\\markerbrushes\\" + Enum.GetName(typeof(Enums.MarkerBrush), this.Marker.BrushID) + ".png";
			if (string.IsNullOrEmpty(text))
			{
				imageBrush = null;
			}
			else
			{
				try
				{
					BitmapSource iconAsBitmap = this.GetIconAsBitmap(text, true);
					if (iconAsBitmap == null)
					{
						imageBrush = null;
					}
					else
					{
						imageBrush.ImageSource = iconAsBitmap;
					}
				}
				catch (Exception ex)
				{
					imageBrush = null;
				}
			}
			return imageBrush;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000F2FC File Offset: 0x0000D4FC
		private void LoadColor(ref Enums.MarkerColor ColorSource, ref System.Windows.Media.Color ColorTarget)
		{
			switch (ColorSource)
			{
			case Enums.MarkerColor.ColorGrey:
				ColorTarget = Colors.Gray;
				return;
			case Enums.MarkerColor.ColorRed:
			case Enums.MarkerColor.ColorRedAlpha:
				ColorTarget = Colors.Red;
				return;
			case Enums.MarkerColor.ColorGreen:
			case Enums.MarkerColor.ColorGreenAlpha:
				ColorTarget = Colors.Green;
				return;
			case Enums.MarkerColor.ColorBlue:
				ColorTarget = Colors.Blue;
				return;
			case Enums.MarkerColor.ColorYellow:
				ColorTarget = Colors.Yellow;
				return;
			case Enums.MarkerColor.ColorOrange:
				ColorTarget = Colors.Orange;
				return;
			case Enums.MarkerColor.ColorWhite:
				ColorTarget = Colors.White;
				return;
			case Enums.MarkerColor.ColorPink:
				ColorTarget = Colors.Pink;
				return;
			case Enums.MarkerColor.ColorBrown:
				ColorTarget = Colors.SaddleBrown;
				return;
			case Enums.MarkerColor.ColorKhaki:
				ColorTarget = Colors.Khaki;
				return;
			case Enums.MarkerColor.ColorWEST:
			case Enums.MarkerColor.ColorBLUFOR:
				ColorTarget = Common.COLOR_WEST;
				return;
			case Enums.MarkerColor.ColorEAST:
			case Enums.MarkerColor.ColorOPFOR:
				ColorTarget = Common.COLOR_EAST;
				return;
			case Enums.MarkerColor.ColorGUER:
			case Enums.MarkerColor.ColorIndependent:
				ColorTarget = Common.COLOR_GREEN;
				return;
			case Enums.MarkerColor.ColorCIV:
			case Enums.MarkerColor.ColorCivilian:
				ColorTarget = Common.COLOR_CIV;
				return;
			}
			ColorTarget = Colors.Black;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000F434 File Offset: 0x0000D634
		private void LoadDir()
		{
			this.IconRotate.Angle = this.Marker.Direction;
			this.IconRotate.CenterX = 0.5;
			this.IconRotate.CenterY = 0.5;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000F474 File Offset: 0x0000D674
		private void LoadShadow()
		{
			switch (this.ParentType)
			{
			case Enums.MarkerIconParentType.Anchor:
			case Enums.MarkerIconParentType.Group:
			case Enums.MarkerIconParentType.Unit:
			case Enums.MarkerIconParentType.Vehicle:
				break;
			case Enums.MarkerIconParentType.Marker:
			{
				DropShadowEffect dropShadowEffect = new DropShadowEffect();
				dropShadowEffect.Color = new System.Windows.Media.Color
				{
					ScA = 1f,
					ScB = 0f,
					ScG = 0f,
					ScR = 0f
				};
				dropShadowEffect.Direction = 320.0;
				dropShadowEffect.BlurRadius = 0.0;
				dropShadowEffect.RenderingBias = RenderingBias.Performance;
				dropShadowEffect.ShadowDepth = 1.0;
				dropShadowEffect.Opacity = this.Marker.Alpha;
				this.Icon.Effect = dropShadowEffect;
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000F53C File Offset: 0x0000D73C
		private void LoadSize()
		{
			if (this.Marker != null)
			{
				double num = this.IconSizeX;
				double num2 = this.IconSizeY;
				if (num <= 0.0)
				{
					num = 1.0;
				}
				if (num2 <= 0.0)
				{
					num2 = 1.0;
				}
				this.Icon.Width = this.IconSizeMultiplier * this.IconSizeMultiplierHost * num;
				this.Icon.Height = this.IconSizeMultiplier * this.IconSizeMultiplierHost * num2;
				if (this.Marker.ShapeID == Enums.MarkerShape.ICON)
				{
					if (this.Marker.Width > 0.0)
					{
						System.Windows.Controls.Image icon;
						(icon = this.Icon).Width = icon.Width * this.Marker.Width;
					}
					if (this.Marker.Height > 0.0)
					{
						System.Windows.Controls.Image icon;
						(icon = this.Icon).Height = icon.Height * this.Marker.Height;
					}
				}
				this.RotationalDiameter = Math.Sqrt(Math.Pow(this.Icon.Width, 2.0) + Math.Pow(this.Icon.Height, 2.0)) * this.ScaleFactor;
				this.IconHolder.Width = this.RotationalDiameter;
				this.IconHolder.Height = this.RotationalDiameter;
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		public void LoadText()
		{
			try
			{
				if (this.ShowText & !string.IsNullOrEmpty(this.Marker.Text))
				{
					this.IconText.Visibility = Visibility.Visible;
					if (this.Marker.Text.Contains(Environment.NewLine))
					{
						this.IconText.Text = string.Empty;
						List<string> list = this.Marker.Text.Split(new char[]
						{
							Conversions.ToChar(Environment.NewLine)
						}).ToList<string>();
						if (list != null && list.Count != 0)
						{
							this.IconText.Inlines.Add(list[0]);
							if (list.Count > 1)
							{
								try
								{
									foreach (string text in list.Skip(1))
									{
										this.IconText.Inlines.Add(new Run(text)
										{
											FontSize = 20.0,
											FontWeight = FontWeights.Normal
										});
									}
								}
								finally
								{
									IEnumerator<string> enumerator;
									if (enumerator != null)
									{
										enumerator.Dispose();
									}
								}
							}
						}
					}
					else
					{
						this.IconText.Text = this.Marker.Text;
					}
					System.Windows.Media.Color color = this.TextColor;
					if (color == Colors.Transparent)
					{
						color = this.Color;
					}
					this.IconText.Foreground = new SolidColorBrush(color);
					this.IconText.Background = new SolidColorBrush(this.TextColorBackground)
					{
						Opacity = this.TextColorBackgroundOpacity
					};
					this.IconText.Opacity = this.Marker.Alpha;
				}
				else
				{
					this.IconText.Visibility = Visibility.Collapsed;
					this.IconText.Text = string.Empty;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000F898 File Offset: 0x0000DA98
		public void UpdateSizeMultiplier(double Multiplier)
		{
			this.IconSizeMultiplierHost = Multiplier;
			this.LoadSize();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public void HandleScaleChange(double ScaleX, double ScaleY)
		{
			this.ScaleFactor = 1.0 / ScaleX;
			if (this.Marker != null && this.Marker.ShapeID != Enums.MarkerShape.UNDEFINED)
			{
				if (this.Marker.ShapeID == Enums.MarkerShape.ICON)
				{
					this.IconScale.ScaleX = this.ScaleFactor;
					this.IconScale.ScaleY = this.ScaleFactor;
					this.LoadSize();
				}
				else
				{
					Enums.MarkerBrush brushID = this.Marker.BrushID;
					if (brushID - Enums.MarkerBrush.Horizontal <= 6)
					{
						double width = this.BackgroundBrush.ImageSource.Width * this.ScaleFactor;
						double height = this.BackgroundBrush.ImageSource.Height * this.ScaleFactor;
						this.BackgroundBrush.Viewport = new Rect(0.0, 0.0, width, height);
					}
				}
				this.IconTextScale.ScaleX = this.ScaleFactor * 0.5;
				this.IconTextScale.ScaleY = this.ScaleFactor * 0.5;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000F9B8 File Offset: 0x0000DBB8
		private void MarkerIcon_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Marker.MarkerIconSizeChangedEventHandler markerIconSizeChangedEvent = this.MarkerIconSizeChangedEvent;
			if (markerIconSizeChangedEvent != null)
			{
				markerIconSizeChangedEvent(this);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000F9D6 File Offset: 0x0000DBD6
		// (set) Token: 0x06000297 RID: 663 RVA: 0x0000F9DE File Offset: 0x0000DBDE
		internal virtual Grid MarkerGrid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000F9E7 File Offset: 0x0000DBE7
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000F9EF File Offset: 0x0000DBEF
		internal virtual Grid IconHolder { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000F9F8 File Offset: 0x0000DBF8
		// (set) Token: 0x0600029B RID: 667 RVA: 0x0000FA00 File Offset: 0x0000DC00
		internal virtual System.Windows.Controls.Image Icon { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000FA09 File Offset: 0x0000DC09
		// (set) Token: 0x0600029D RID: 669 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal virtual TransformGroup IconTG { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000FA1A File Offset: 0x0000DC1A
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000FA22 File Offset: 0x0000DC22
		internal virtual RotateTransform IconRotate { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000FA2B File Offset: 0x0000DC2B
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000FA33 File Offset: 0x0000DC33
		internal virtual ScaleTransform IconScale { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000FA3C File Offset: 0x0000DC3C
		// (set) Token: 0x060002A3 RID: 675 RVA: 0x0000FA44 File Offset: 0x0000DC44
		internal virtual TextBlock IconText { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x0000FA4D File Offset: 0x0000DC4D
		// (set) Token: 0x060002A5 RID: 677 RVA: 0x0000FA55 File Offset: 0x0000DC55
		internal virtual ScaleTransform IconTextScale { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060002A6 RID: 678 RVA: 0x0000FA60 File Offset: 0x0000DC60
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Athena Desktop;component/controls/general/marker.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000FA90 File Offset: 0x0000DC90
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.MarkerGrid = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.IconHolder = (Grid)target;
				return;
			}
			if (connectionId == 3)
			{
				this.Icon = (System.Windows.Controls.Image)target;
				return;
			}
			if (connectionId == 4)
			{
				this.IconTG = (TransformGroup)target;
				return;
			}
			if (connectionId == 5)
			{
				this.IconRotate = (RotateTransform)target;
				return;
			}
			if (connectionId == 6)
			{
				this.IconScale = (ScaleTransform)target;
				return;
			}
			if (connectionId == 7)
			{
				this.IconText = (TextBlock)target;
				return;
			}
			if (connectionId == 8)
			{
				this.IconTextScale = (ScaleTransform)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000116 RID: 278
		private ImageBrush BackgroundBrush;

		// Token: 0x04000117 RID: 279
		private GeometryDrawing ShapeDrawing;

		// Token: 0x04000118 RID: 280
		private DrawingImage ShapeImage;

		// Token: 0x04000119 RID: 281
		public Marker Marker;

		// Token: 0x04000130 RID: 304
		private bool _contentLoaded;

		// Token: 0x02000070 RID: 112
		// (Invoke) Token: 0x06000774 RID: 1908
		public delegate void MarkerIconSizeChangedEventHandler(Marker Icon);

		// Token: 0x02000071 RID: 113
		// (Invoke) Token: 0x06000778 RID: 1912
		public delegate void OnMarkerUpdateEventHandler();
	}
}
