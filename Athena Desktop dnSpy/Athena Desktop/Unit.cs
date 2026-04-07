using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Athena.Objects.v2;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000033 RID: 51
	[DesignerGenerated]
	public class Unit : UserControl, IComponentConnector
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00011C8C File Offset: 0x0000FE8C
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00011C94 File Offset: 0x0000FE94
		public Unit Unit { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00011C9D File Offset: 0x0000FE9D
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00011CA5 File Offset: 0x0000FEA5
		public Group Group { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00011CAE File Offset: 0x0000FEAE
		// (set) Token: 0x0600031E RID: 798 RVA: 0x00011CB6 File Offset: 0x0000FEB6
		public bool ZoomedPastGroup { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00011CBF File Offset: 0x0000FEBF
		// (set) Token: 0x06000320 RID: 800 RVA: 0x00011CC7 File Offset: 0x0000FEC7
		public double MinimumZoomFactorText { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00011CD0 File Offset: 0x0000FED0
		// (set) Token: 0x06000322 RID: 802 RVA: 0x00011CD8 File Offset: 0x0000FED8
		public Enums.RenderMode RenderMode { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00011CE1 File Offset: 0x0000FEE1
		// (set) Token: 0x06000324 RID: 804 RVA: 0x00011CE9 File Offset: 0x0000FEE9
		public bool ForceTextVisible { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00011CF2 File Offset: 0x0000FEF2
		public Marker MarkerIcon
		{
			get
			{
				return this.Icon;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00011CFC File Offset: 0x0000FEFC
		public bool InVehicle
		{
			get
			{
				return this.Unit != null && !string.IsNullOrEmpty(this.Unit.VehicleNetID);
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00011D2C File Offset: 0x0000FF2C
		public Unit(ref Unit Unit, ref Group Group)
		{
			this.ZoomedPastGroup = false;
			this.MinimumZoomFactorText = 2.2;
			this.RenderMode = Enums.RenderMode.Elevations;
			this.ForceTextVisible = false;
			this.InitializeComponent();
			this.Unit = Unit;
			this.Group = Group;
			this.Update();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00011D80 File Offset: 0x0000FF80
		public void Update()
		{
			try
			{
				this.LoadMarker();
				this.LoadText();
				this.SetVisibility();
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		private void LoadMarker()
		{
			Marker marker = new Marker();
			marker.Alpha = 1.0;
			marker.Direction = this.Unit.Direction;
			marker.SizeX = "0.5";
			marker.Width = 0.5;
			marker.SizeY = "0.5";
			marker.Height = 0.5;
			marker.ShapeID = Enums.MarkerShape.ICON;
			marker.ColorID = this.GetMarkerColor();
			marker.TypeID = this.GetMarkerType();
			this.Icon.ParentType = Enums.MarkerIconParentType.Unit;
			this.Icon.Update(marker);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00011E60 File Offset: 0x00010060
		public void LoadText()
		{
			if (this.Unit != null)
			{
				TextBlock unitText = this.UnitText;
				Unit unit = this.Unit;
				string text = Common.CreateUnitString(ref unit, false, false);
				this.Unit = unit;
				unitText.Text = text;
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00011E98 File Offset: 0x00010098
		private Enums.MarkerColor GetMarkerColor()
		{
			Enums.MarkerColor result = Enums.MarkerColor.ColorBlack;
			switch (this.Unit.SideID)
			{
			case Enums.Side.UNDEFINED:
			case Enums.Side.CIV:
			case Enums.Side.UNKNOWN:
				result = Enums.MarkerColor.ColorCIV;
				break;
			case Enums.Side.EAST:
				result = Enums.MarkerColor.ColorEAST;
				break;
			case Enums.Side.WEST:
				result = Enums.MarkerColor.ColorWEST;
				break;
			case Enums.Side.GUER:
				result = Enums.MarkerColor.ColorGreen;
				break;
			}
			return result;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00011EEC File Offset: 0x000100EC
		private Enums.MarkerType GetMarkerType()
		{
			Enums.MarkerType markerType = Enums.MarkerType.vehicle_man;
			if (this.Unit != null)
			{
				switch (this.Unit.RankID)
				{
				case Enums.Rank.LIEUTENANT:
					markerType = Enums.MarkerType.vehicle_manofficer;
					break;
				case Enums.Rank.CAPTAIN:
					markerType = Enums.MarkerType.vehicle_manofficer;
					break;
				case Enums.Rank.MAJOR:
					markerType = Enums.MarkerType.vehicle_manofficer;
					break;
				case Enums.Rank.COLONEL:
					markerType = Enums.MarkerType.vehicle_manofficer;
					break;
				default:
					if (this.Unit.Type.Trim().ToLower().Contains("_sl") | this.Unit.Type.Trim().ToLower().Contains("_tl_"))
					{
						markerType = Enums.MarkerType.vehicle_manleader;
					}
					else if (this.Unit.Type.Trim().ToLower().Contains("medic") | Operators.CompareString(this.Unit.HasMedikit, "1", false) == 0)
					{
						markerType = Enums.MarkerType.vehicle_manmedic;
					}
					else if (this.Unit.Type.Trim().ToLower().Contains("engineer"))
					{
						markerType = Enums.MarkerType.vehicle_manengineer;
					}
					else
					{
						string left = this.Unit.Weapon1.ToLower();
						if (Operators.CompareString(left, "machinegun", false) != 0)
						{
							if (Operators.CompareString(left, "rocketlauncher", false) != 0)
							{
								if (Operators.CompareString(left, "missilelauncher", false) == 0)
								{
									markerType = Enums.MarkerType.vehicle_manat;
								}
							}
							else
							{
								markerType = Enums.MarkerType.vehicle_manat;
							}
						}
						else
						{
							markerType = Enums.MarkerType.vehicle_manmg;
						}
						if (markerType == Enums.MarkerType.vehicle_man && !string.IsNullOrEmpty(this.Unit.Weapon2))
						{
							string left2 = this.Unit.Weapon2.ToLower();
							if (Operators.CompareString(left2, "machinegun", false) != 0)
							{
								if (Operators.CompareString(left2, "rocketlauncher", false) != 0)
								{
									if (Operators.CompareString(left2, "missilelauncher", false) == 0)
									{
										markerType = Enums.MarkerType.vehicle_manat;
									}
								}
								else
								{
									markerType = Enums.MarkerType.vehicle_manat;
								}
							}
							else
							{
								markerType = Enums.MarkerType.vehicle_manmg;
							}
						}
					}
					break;
				}
			}
			return markerType;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000120DB File Offset: 0x000102DB
		public void SetVisibility()
		{
			this.SetVisibilityText();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x000120E4 File Offset: 0x000102E4
		private void SetVisibilityText()
		{
			if (this.Unit != null)
			{
				if (this.InVehicle)
				{
					base.Visibility = Visibility.Collapsed;
					return;
				}
				base.Visibility = Visibility.Visible;
				if (this.ForceTextVisible)
				{
					this.UnitText.Visibility = Visibility.Visible;
					return;
				}
				if (this.ZoomedPastGroup)
				{
					this.UnitText.Visibility = Visibility.Visible;
					return;
				}
				this.UnitText.Visibility = Visibility.Collapsed;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00012146 File Offset: 0x00010346
		public void Icon_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.ForceTextVisible = !this.ForceTextVisible;
			this.SetVisibilityText();
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0001215D File Offset: 0x0001035D
		public void HandleScaleChange(double ScaleX, double ScaleY)
		{
			if (this.Icon != null)
			{
				if (ScaleX >= this.MinimumZoomFactorText)
				{
					if (!this.ZoomedPastGroup)
					{
						this.ZoomedPastGroup = true;
						this.SetVisibilityText();
						return;
					}
				}
				else if (this.ZoomedPastGroup)
				{
					this.ZoomedPastGroup = false;
					this.SetVisibilityText();
				}
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001219C File Offset: 0x0001039C
		public void HandleRenderModeChange(Enums.RenderMode Mode)
		{
			this.RenderMode = Mode;
			switch (Mode)
			{
			case Enums.RenderMode.Elevations:
				this.UnitText.Background = new SolidColorBrush(Colors.Transparent)
				{
					Opacity = 1.0
				};
				return;
			case Enums.RenderMode.HeightMapColor:
				this.UnitText.Background = new SolidColorBrush(Colors.Transparent)
				{
					Opacity = 1.0
				};
				return;
			case Enums.RenderMode.HeightMapGray:
				this.UnitText.Background = new SolidColorBrush(Colors.White)
				{
					Opacity = 0.5
				};
				return;
			case Enums.RenderMode.Image:
				this.UnitText.Background = new SolidColorBrush(Colors.Transparent)
				{
					Opacity = 1.0
				};
				return;
			default:
				return;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0001225E File Offset: 0x0001045E
		// (set) Token: 0x06000333 RID: 819 RVA: 0x00012266 File Offset: 0x00010466
		internal virtual Grid UnitGrid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0001226F File Offset: 0x0001046F
		// (set) Token: 0x06000335 RID: 821 RVA: 0x00012278 File Offset: 0x00010478
		internal virtual Marker Icon
		{
			[CompilerGenerated]
			get
			{
				return this._Icon;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseButtonEventHandler value2 = new MouseButtonEventHandler(this.Icon_PreviewMouseDown);
				Marker icon = this._Icon;
				if (icon != null)
				{
					icon.MouseDown -= value2;
				}
				this._Icon = value;
				icon = this._Icon;
				if (icon != null)
				{
					icon.MouseDown += value2;
				}
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000336 RID: 822 RVA: 0x000122BB File Offset: 0x000104BB
		// (set) Token: 0x06000337 RID: 823 RVA: 0x000122C3 File Offset: 0x000104C3
		internal virtual TextBlock UnitText { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000338 RID: 824 RVA: 0x000122CC File Offset: 0x000104CC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Athena Desktop;component/controls/general/unit.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000A8BB File Offset: 0x00008ABB
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x000122FB File Offset: 0x000104FB
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.UnitGrid = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.Icon = (Marker)target;
				return;
			}
			if (connectionId == 3)
			{
				this.UnitText = (TextBlock)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000167 RID: 359
		private bool _contentLoaded;
	}
}
