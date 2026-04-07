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
	// Token: 0x02000027 RID: 39
	[DesignerGenerated]
	public class Group : UserControl, IComponentConnector
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00009C5B File Offset: 0x00007E5B
		// (set) Token: 0x06000185 RID: 389 RVA: 0x00009C63 File Offset: 0x00007E63
		public Group Group { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00009C6C File Offset: 0x00007E6C
		// (set) Token: 0x06000187 RID: 391 RVA: 0x00009C74 File Offset: 0x00007E74
		public Unit GroupLeader { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00009C7D File Offset: 0x00007E7D
		// (set) Token: 0x06000189 RID: 393 RVA: 0x00009C85 File Offset: 0x00007E85
		public Vehicle GroupVehicle { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00009C8E File Offset: 0x00007E8E
		// (set) Token: 0x0600018B RID: 395 RVA: 0x00009C96 File Offset: 0x00007E96
		public bool GroupVehicleIsPrimary { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00009C9F File Offset: 0x00007E9F
		// (set) Token: 0x0600018D RID: 397 RVA: 0x00009CA7 File Offset: 0x00007EA7
		public double MaximumZoomFactor { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00009CB0 File Offset: 0x00007EB0
		public Marker MarkerIcon
		{
			get
			{
				return this.Icon;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00009CB8 File Offset: 0x00007EB8
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public Enums.RenderMode RenderMode { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00009CC9 File Offset: 0x00007EC9
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00009CD1 File Offset: 0x00007ED1
		public int CurrentSizeStep { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00009CDA File Offset: 0x00007EDA
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public bool ZoomedPastGroup { get; set; }

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000195 RID: 405 RVA: 0x00009CEC File Offset: 0x00007EEC
		// (remove) Token: 0x06000196 RID: 406 RVA: 0x00009D24 File Offset: 0x00007F24
		public event Group.GroupIconSizeChangedEventHandler GroupIconSizeChanged;

		// Token: 0x06000197 RID: 407 RVA: 0x00009D5C File Offset: 0x00007F5C
		public Group(ref Group Group, ref Unit Leader, ref Vehicle Vehicle, bool IsVehicleGroup, Enums.RenderMode RenderMode, double MaximumZoomFactor)
		{
			base.MouseDown += this.Been_Click;
			base.TouchDown += this.Been_Tapped;
			base.SizeChanged += this.GroupIcon_SizeChanged;
			this.GroupVehicleIsPrimary = true;
			this.MaximumZoomFactor = 2.2;
			this.RenderMode = Enums.RenderMode.Elevations;
			this.CurrentSizeStep = 0;
			this.ZoomedPastGroup = false;
			this.InitializeComponent();
			this.Group = Group;
			this.GroupLeader = Leader;
			this.GroupVehicle = Vehicle;
			this.GroupVehicleIsPrimary = IsVehicleGroup;
			this.MaximumZoomFactor = MaximumZoomFactor;
			this.Update();
			this.HandleRenderModeChange(RenderMode);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009E0C File Offset: 0x0000800C
		public void Update()
		{
			try
			{
				if (this.Group != null && this.GroupLeader != null)
				{
					this.LoadMarker();
					this.SetVisibility();
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00009E58 File Offset: 0x00008058
		private void LoadMarker()
		{
			Marker marker = new Marker();
			marker.Alpha = 1.0;
			marker.SizeX = "1";
			marker.SizeY = "1";
			marker.Height = 1.0;
			marker.Width = 1.0;
			marker.ShapeID = Enums.MarkerShape.ICON;
			marker.ColorID = this.GetMarkerColor();
			marker.TypeID = this.GetMarkerType();
			Marker marker2 = marker;
			string displayName = this.Group.DisplayName;
			string newLine = Environment.NewLine;
			Unit groupLeader = this.GroupLeader;
			string text = Common.CreateUnitString(ref groupLeader, true, false);
			this.GroupLeader = groupLeader;
			marker2.Text = displayName + newLine + text.Trim();
			this.Icon.ParentType = Enums.MarkerIconParentType.Group;
			this.Icon.ShowText = true;
			this.Icon.TextColor = Colors.Black;
			this.Icon.Update(marker);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00009F38 File Offset: 0x00008138
		private Enums.MarkerColor GetMarkerColor()
		{
			Enums.MarkerColor result = Enums.MarkerColor.ColorBlack;
			switch (this.Group.SideID)
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

		// Token: 0x0600019B RID: 411 RVA: 0x00009F8C File Offset: 0x0000818C
		private Enums.MarkerType GetMarkerType()
		{
			Enums.MarkerType result = Enums.MarkerType.c_unknown;
			switch (this.Group.SideID)
			{
			case Enums.Side.UNDEFINED:
			case Enums.Side.CIV:
			case Enums.Side.UNKNOWN:
				result = Enums.MarkerType.n_inf;
				if (this.GroupVehicle != null && !string.IsNullOrEmpty(this.GroupVehicle.Class))
				{
					string text = this.GroupVehicle.Class.Trim().ToLower();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 856466825U)
					{
						if (num <= 822911587U)
						{
							if (num != 806133968U)
							{
								if (num == 822911587U)
								{
									if (Operators.CompareString(text, "4", false) == 0)
									{
										result = Enums.MarkerType.n_air;
									}
								}
							}
							else if (Operators.CompareString(text, "5", false) == 0)
							{
								result = Enums.MarkerType.n_plane;
							}
						}
						else if (num != 839689206U)
						{
							if (num == 856466825U)
							{
								if (Operators.CompareString(text, "6", false) == 0)
								{
									result = Enums.MarkerType.n_uav;
								}
							}
						}
						else if (Operators.CompareString(text, "7", false) == 0)
						{
							result = Enums.MarkerType.n_naval;
						}
					}
					else if (num <= 906799682U)
					{
						if (num != 873244444U)
						{
							if (num == 906799682U)
							{
								if (Operators.CompareString(text, "3", false) == 0)
								{
									result = Enums.MarkerType.n_armor;
								}
							}
						}
						else if (Operators.CompareString(text, "1", false) == 0)
						{
							result = Enums.MarkerType.n_motor_inf;
						}
					}
					else if (num != 923577301U)
					{
						if (num == 1024243015U)
						{
							if (Operators.CompareString(text, "8", false) == 0)
							{
								result = Enums.MarkerType.n_naval;
							}
						}
					}
					else if (Operators.CompareString(text, "2", false) == 0)
					{
						result = Enums.MarkerType.n_mech_inf;
					}
				}
				break;
			case Enums.Side.EAST:
				result = Enums.MarkerType.o_inf;
				if (this.GroupVehicle != null && !string.IsNullOrEmpty(this.GroupVehicle.Class))
				{
					string text2 = this.GroupVehicle.Class.Trim().ToLower();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
					if (num <= 856466825U)
					{
						if (num <= 822911587U)
						{
							if (num != 806133968U)
							{
								if (num == 822911587U)
								{
									if (Operators.CompareString(text2, "4", false) == 0)
									{
										result = Enums.MarkerType.o_air;
									}
								}
							}
							else if (Operators.CompareString(text2, "5", false) == 0)
							{
								result = Enums.MarkerType.o_plane;
							}
						}
						else if (num != 839689206U)
						{
							if (num == 856466825U)
							{
								if (Operators.CompareString(text2, "6", false) == 0)
								{
									result = Enums.MarkerType.o_uav;
								}
							}
						}
						else if (Operators.CompareString(text2, "7", false) == 0)
						{
							result = Enums.MarkerType.o_naval;
						}
					}
					else if (num <= 906799682U)
					{
						if (num != 873244444U)
						{
							if (num == 906799682U)
							{
								if (Operators.CompareString(text2, "3", false) == 0)
								{
									result = Enums.MarkerType.o_armor;
								}
							}
						}
						else if (Operators.CompareString(text2, "1", false) == 0)
						{
							result = Enums.MarkerType.o_motor_inf;
						}
					}
					else if (num != 923577301U)
					{
						if (num == 1024243015U)
						{
							if (Operators.CompareString(text2, "8", false) == 0)
							{
								result = Enums.MarkerType.o_naval;
							}
						}
					}
					else if (Operators.CompareString(text2, "2", false) == 0)
					{
						result = Enums.MarkerType.o_mech_inf;
					}
				}
				break;
			case Enums.Side.WEST:
				result = Enums.MarkerType.b_inf;
				if (this.GroupVehicle != null && !string.IsNullOrEmpty(this.GroupVehicle.Class))
				{
					string text3 = this.GroupVehicle.Class.Trim().ToLower();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
					if (num <= 856466825U)
					{
						if (num <= 822911587U)
						{
							if (num != 806133968U)
							{
								if (num == 822911587U)
								{
									if (Operators.CompareString(text3, "4", false) == 0)
									{
										result = Enums.MarkerType.b_air;
									}
								}
							}
							else if (Operators.CompareString(text3, "5", false) == 0)
							{
								result = Enums.MarkerType.b_plane;
							}
						}
						else if (num != 839689206U)
						{
							if (num == 856466825U)
							{
								if (Operators.CompareString(text3, "6", false) == 0)
								{
									result = Enums.MarkerType.b_uav;
								}
							}
						}
						else if (Operators.CompareString(text3, "7", false) == 0)
						{
							result = Enums.MarkerType.b_naval;
						}
					}
					else if (num <= 906799682U)
					{
						if (num != 873244444U)
						{
							if (num == 906799682U)
							{
								if (Operators.CompareString(text3, "3", false) == 0)
								{
									result = Enums.MarkerType.b_armor;
								}
							}
						}
						else if (Operators.CompareString(text3, "1", false) == 0)
						{
							result = Enums.MarkerType.b_motor_inf;
						}
					}
					else if (num != 923577301U)
					{
						if (num == 1024243015U)
						{
							if (Operators.CompareString(text3, "8", false) == 0)
							{
								result = Enums.MarkerType.b_naval;
							}
						}
					}
					else if (Operators.CompareString(text3, "2", false) == 0)
					{
						result = Enums.MarkerType.b_mech_inf;
					}
				}
				break;
			case Enums.Side.GUER:
				result = Enums.MarkerType.n_inf;
				if (this.GroupVehicle != null && !string.IsNullOrEmpty(this.GroupVehicle.Class))
				{
					string text4 = this.GroupVehicle.Class.Trim().ToLower();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text4);
					if (num <= 856466825U)
					{
						if (num <= 822911587U)
						{
							if (num != 806133968U)
							{
								if (num == 822911587U)
								{
									if (Operators.CompareString(text4, "4", false) == 0)
									{
										result = Enums.MarkerType.n_air;
									}
								}
							}
							else if (Operators.CompareString(text4, "5", false) == 0)
							{
								result = Enums.MarkerType.n_plane;
							}
						}
						else if (num != 839689206U)
						{
							if (num == 856466825U)
							{
								if (Operators.CompareString(text4, "6", false) == 0)
								{
									result = Enums.MarkerType.n_uav;
								}
							}
						}
						else if (Operators.CompareString(text4, "7", false) == 0)
						{
							result = Enums.MarkerType.n_naval;
						}
					}
					else if (num <= 906799682U)
					{
						if (num != 873244444U)
						{
							if (num == 906799682U)
							{
								if (Operators.CompareString(text4, "3", false) == 0)
								{
									result = Enums.MarkerType.n_armor;
								}
							}
						}
						else if (Operators.CompareString(text4, "1", false) == 0)
						{
							result = Enums.MarkerType.n_motor_inf;
						}
					}
					else if (num != 923577301U)
					{
						if (num == 1024243015U)
						{
							if (Operators.CompareString(text4, "8", false) == 0)
							{
								result = Enums.MarkerType.n_naval;
							}
						}
					}
					else if (Operators.CompareString(text4, "2", false) == 0)
					{
						result = Enums.MarkerType.n_mech_inf;
					}
				}
				break;
			}
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000A640 File Offset: 0x00008840
		private void SetVisibility()
		{
			if (this.GroupVehicle == null)
			{
				if (this.ZoomedPastGroup)
				{
					base.Visibility = Visibility.Collapsed;
					return;
				}
				base.Visibility = Visibility.Visible;
				return;
			}
			else
			{
				if (this.GroupVehicleIsPrimary)
				{
					base.Visibility = Visibility.Visible;
					return;
				}
				base.Visibility = Visibility.Collapsed;
				return;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000A679 File Offset: 0x00008879
		private void Been_Click(object sender, MouseButtonEventArgs e)
		{
			if (e.RightButton == MouseButtonState.Pressed)
			{
				this.HandleRightMouseClick();
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000A68A File Offset: 0x0000888A
		private void Been_Tapped(object sender, TouchEventArgs e)
		{
			this.HandleRightMouseClick();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000A694 File Offset: 0x00008894
		private void HandleRightMouseClick()
		{
			checked
			{
				switch (this.CurrentSizeStep)
				{
				case 0:
					this.Icon.UpdateSizeMultiplier(1.5);
					this.CurrentSizeStep++;
					return;
				case 1:
					this.Icon.UpdateSizeMultiplier(2.0);
					this.CurrentSizeStep++;
					return;
				case 2:
					this.Icon.UpdateSizeMultiplier(1.0);
					this.CurrentSizeStep = 0;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000A71C File Offset: 0x0000891C
		private void GroupIcon_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			Group.GroupIconSizeChangedEventHandler groupIconSizeChangedEvent = this.GroupIconSizeChangedEvent;
			if (groupIconSizeChangedEvent != null)
			{
				groupIconSizeChangedEvent(this);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000A73C File Offset: 0x0000893C
		public void HandleScaleChange(double ScaleX, double ScaleY)
		{
			if (this.Icon != null)
			{
				this.Icon.HandleScaleChange(ScaleX, ScaleY);
				if (ScaleX >= this.MaximumZoomFactor)
				{
					if (!this.ZoomedPastGroup)
					{
						this.ZoomedPastGroup = true;
						this.SetVisibility();
						return;
					}
				}
				else if (this.ZoomedPastGroup)
				{
					this.ZoomedPastGroup = false;
					this.SetVisibility();
				}
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000A794 File Offset: 0x00008994
		public void HandleRenderModeChange(Enums.RenderMode Mode)
		{
			this.RenderMode = Mode;
			switch (Mode)
			{
			case Enums.RenderMode.Elevations:
				this.Icon.TextColorBackground = Colors.Transparent;
				this.Icon.TextColorBackgroundOpacity = 1.0;
				break;
			case Enums.RenderMode.HeightMapColor:
				this.Icon.TextColorBackground = Colors.Transparent;
				this.Icon.TextColorBackgroundOpacity = 1.0;
				break;
			case Enums.RenderMode.HeightMapGray:
				this.Icon.TextColorBackground = Colors.White;
				this.Icon.TextColorBackgroundOpacity = 0.5;
				break;
			case Enums.RenderMode.Image:
				this.Icon.TextColorBackground = Colors.Transparent;
				this.Icon.TextColorBackgroundOpacity = 1.0;
				break;
			}
			this.Icon.LoadText();
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000A868 File Offset: 0x00008A68
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000A870 File Offset: 0x00008A70
		internal virtual Grid GroupGrid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000A879 File Offset: 0x00008A79
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000A881 File Offset: 0x00008A81
		internal virtual Marker Icon { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A88C File Offset: 0x00008A8C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Athena Desktop;component/controls/general/group.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A8BB File Offset: 0x00008ABB
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A8C5 File Offset: 0x00008AC5
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.GroupGrid = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.Icon = (Marker)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000C7 RID: 199
		private bool _contentLoaded;

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x06000756 RID: 1878
		public delegate void GroupIconSizeChangedEventHandler(Group Icon);
	}
}
