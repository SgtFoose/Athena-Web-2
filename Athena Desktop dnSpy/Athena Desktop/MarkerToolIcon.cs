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
	// Token: 0x0200002F RID: 47
	[DesignerGenerated]
	public class MarkerToolIcon : UserControl, IComponentConnector
	{
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060002A8 RID: 680 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		// (remove) Token: 0x060002A9 RID: 681 RVA: 0x0000FB64 File Offset: 0x0000DD64
		public event MarkerToolIcon.MarkerToolSavedEventHandler MarkerToolSaved;

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060002AA RID: 682 RVA: 0x0000FB9C File Offset: 0x0000DD9C
		// (remove) Token: 0x060002AB RID: 683 RVA: 0x0000FBD4 File Offset: 0x0000DDD4
		public event MarkerToolIcon.MarkerToolCancelledEventHandler MarkerToolCancelled;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060002AC RID: 684 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		// (remove) Token: 0x060002AD RID: 685 RVA: 0x0000FC44 File Offset: 0x0000DE44
		public event MarkerToolIcon.MarkerToolSizeChangedEventHandler MarkerToolSizeChanged;

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000FC79 File Offset: 0x0000DE79
		// (set) Token: 0x060002AF RID: 687 RVA: 0x0000FC81 File Offset: 0x0000DE81
		public double PosX { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000FC8A File Offset: 0x0000DE8A
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x0000FC92 File Offset: 0x0000DE92
		public double PosY { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000FC9C File Offset: 0x0000DE9C
		public double IconWidth
		{
			get
			{
				double result;
				if (this.MarkerIcon == null)
				{
					result = 0.0;
				}
				else
				{
					result = this.MarkerIcon.IconWidth * this.ToolScale.ScaleX;
				}
				return result;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		public double IconHeight
		{
			get
			{
				double result;
				if (this.MarkerIcon == null)
				{
					result = 0.0;
				}
				else
				{
					result = this.MarkerIcon.IconHeight * this.ToolScale.ScaleY;
				}
				return result;
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public MarkerToolIcon(double PosX, double PosY)
		{
			base.Loaded += this.Marker_Loaded;
			base.MouseDown += this.Marker_MouseDown;
			base.MouseMove += this.Marker_MouseMove;
			base.SizeChanged += this.MarkerTool_SizeChanged;
			this.Marker = new Marker();
			this.MarkerIcon = null;
			this.PosX = 0.0;
			this.PosY = 0.0;
			this.InitializeComponent();
			this.PosX = PosX;
			this.PosY = PosY;
			this.PrepMarker();
			this.PrepIcon();
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		private void PrepMarker()
		{
			this.Marker = new Marker();
			this.Marker.BrushID = Enums.MarkerBrush.Solid;
			this.Marker.ColorID = Enums.MarkerColor.ColorBlack;
			this.Marker.ShapeID = Enums.MarkerShape.ICON;
			this.Marker.TypeID = Enums.MarkerType.Dot;
			this.Marker.Name = DateTime.Now.Ticks.ToString();
			this.Marker.Text = string.Empty;
			this.Marker.Alpha = 1.0;
			this.Marker.Dir = Conversions.ToString(0);
			this.Marker.PosX = this.PosX.ToString();
			this.Marker.PosY = this.PosY.ToString();
			this.Marker.PosZ = "0";
			this.Marker.SizeX = "1";
			this.Marker.SizeY = "1";
			this.Marker.ScopeID = Enums.MarkerScope.Side;
			this.Marker.SideID = Enums.Side.UNKNOWN;
			this.Marker.GroupID = -1.0;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000FEEB File Offset: 0x0000E0EB
		private void PrepIcon()
		{
			if (this.Marker != null)
			{
				this.MarkerIcon = new Marker();
				if (this.MarkerIcon != null)
				{
					this.MarkerIconHolder.Children.Add(this.MarkerIcon);
					this.MarkerIcon.VerticalAlignment = VerticalAlignment.Top;
				}
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000FF2C File Offset: 0x0000E12C
		private void txtText_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (this.Marker != null)
				{
					this.Marker.Text = this.txtText.Text.Trim();
					if (this.MarkerIcon != null)
					{
						this.MarkerIcon.Update(this.Marker);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000FF98 File Offset: 0x0000E198
		private void ddlType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (this.Marker != null)
				{
					ComboBoxItem comboBoxItem = this.ddlType.SelectedItem as ComboBoxItem;
					if (comboBoxItem != null)
					{
						string text = comboBoxItem.Tag.ToString().Trim();
						uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
						if (num <= 806133968U)
						{
							if (num <= 468396612U)
							{
								if (num != 401286136U)
								{
									if (num != 418063755U)
									{
										if (num == 468396612U)
										{
											if (Operators.CompareString(text, "10", false) == 0)
											{
												this.Marker.TypeID = Enums.MarkerType.Pickup;
											}
										}
									}
									else if (Operators.CompareString(text, "15", false) == 0)
									{
										this.Marker.ShapeID = Enums.MarkerShape.RECTANGLE;
									}
								}
								else if (Operators.CompareString(text, "14", false) == 0)
								{
									this.Marker.ShapeID = Enums.MarkerShape.ELLIPSE;
								}
							}
							else if (num <= 501951850U)
							{
								if (num != 485174231U)
								{
									if (num == 501951850U)
									{
										if (Operators.CompareString(text, "12", false) == 0)
										{
											this.Marker.TypeID = Enums.MarkerType.Unknown;
										}
									}
								}
								else if (Operators.CompareString(text, "11", false) == 0)
								{
									this.Marker.TypeID = Enums.MarkerType.Start;
								}
							}
							else if (num != 518729469U)
							{
								if (num == 806133968U)
								{
									if (Operators.CompareString(text, "5", false) == 0)
									{
										this.Marker.TypeID = Enums.MarkerType.End;
									}
								}
							}
							else if (Operators.CompareString(text, "13", false) == 0)
							{
								this.Marker.TypeID = Enums.MarkerType.Warning;
							}
						}
						else if (num <= 873244444U)
						{
							if (num != 822911587U)
							{
								if (num != 839689206U)
								{
									if (num == 873244444U)
									{
										if (Operators.CompareString(text, "1", false) == 0)
										{
											this.Marker.TypeID = Enums.MarkerType.Arrow;
										}
									}
								}
								else if (Operators.CompareString(text, "7", false) == 0)
								{
									this.Marker.TypeID = Enums.MarkerType.Join;
								}
							}
							else if (Operators.CompareString(text, "4", false) == 0)
							{
								this.Marker.TypeID = Enums.MarkerType.Dot;
							}
						}
						else if (num <= 923577301U)
						{
							if (num != 906799682U)
							{
								if (num == 923577301U)
								{
									if (Operators.CompareString(text, "2", false) == 0)
									{
										this.Marker.TypeID = Enums.MarkerType.mil_circle;
									}
								}
							}
							else if (Operators.CompareString(text, "3", false) == 0)
							{
								this.Marker.TypeID = Enums.MarkerType.Destroy;
							}
						}
						else if (num != 1007465396U)
						{
							if (num == 1024243015U)
							{
								if (Operators.CompareString(text, "8", false) == 0)
								{
									this.Marker.TypeID = Enums.MarkerType.Marker;
								}
							}
						}
						else if (Operators.CompareString(text, "9", false) == 0)
						{
							this.Marker.TypeID = Enums.MarkerType.Objective;
						}
					}
					if (this.MarkerIcon != null)
					{
						this.MarkerIcon.Update(this.Marker);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00010304 File Offset: 0x0000E504
		private void ddlColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (this.Marker != null)
				{
					ComboBoxItem comboBoxItem = this.ddlColor.SelectedItem as ComboBoxItem;
					if (comboBoxItem != null)
					{
						string left = comboBoxItem.Tag.ToString().Trim();
						if (Operators.CompareString(left, "0", false) != 0)
						{
							if (Operators.CompareString(left, "1", false) != 0)
							{
								if (Operators.CompareString(left, "2", false) != 0)
								{
									if (Operators.CompareString(left, "3", false) != 0)
									{
										if (Operators.CompareString(left, "4", false) == 0)
										{
											this.Marker.ColorID = Enums.MarkerColor.ColorRed;
										}
									}
									else
									{
										this.Marker.ColorID = Enums.MarkerColor.ColorCIV;
									}
								}
								else
								{
									this.Marker.ColorID = Enums.MarkerColor.ColorGreen;
								}
							}
							else
							{
								this.Marker.ColorID = Enums.MarkerColor.ColorBlue;
							}
						}
						else
						{
							this.Marker.ColorID = Enums.MarkerColor.ColorBlack;
						}
					}
					if (this.MarkerIcon != null)
					{
						this.MarkerIcon.Update(this.Marker);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0001040C File Offset: 0x0000E60C
		private void ddlScope_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			try
			{
				if (this.Marker != null)
				{
					ComboBoxItem comboBoxItem = this.ddlScope.SelectedItem as ComboBoxItem;
					if (comboBoxItem != null)
					{
						string left = comboBoxItem.Tag.ToString().Trim();
						if (Operators.CompareString(left, "0", false) != 0)
						{
							if (Operators.CompareString(left, "1", false) != 0)
							{
								if (Operators.CompareString(left, "2", false) != 0)
								{
									if (Operators.CompareString(left, "3", false) != 0)
									{
										if (Operators.CompareString(left, "4", false) == 0)
										{
											this.Marker.ScopeID = Enums.MarkerScope.All;
										}
									}
									else
									{
										this.Marker.ScopeID = Enums.MarkerScope.Side;
									}
								}
								else
								{
									this.Marker.ScopeID = Enums.MarkerScope.Group;
								}
							}
							else
							{
								this.Marker.ScopeID = Enums.MarkerScope.Local;
							}
						}
						else
						{
							this.Marker.ScopeID = Enums.MarkerScope.Athena;
						}
					}
					if (this.MarkerIcon != null)
					{
						this.MarkerIcon.Update(this.Marker);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00010514 File Offset: 0x0000E714
		private void sldDir_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			try
			{
				if (this.Marker != null)
				{
					this.Marker.Dir = Conversions.ToString(this.sldDir.Value);
					if (this.MarkerIcon != null)
					{
						this.MarkerIcon.Update(this.Marker);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00010580 File Offset: 0x0000E780
		private void btnDone_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00010588 File Offset: 0x0000E788
		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			this.Cancel();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00010590 File Offset: 0x0000E790
		private void Marker_Loaded(object sender, RoutedEventArgs e)
		{
			this.txtText.Focus();
			Keyboard.Focus(this.txtText);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000105AA File Offset: 0x0000E7AA
		private void Marker_MouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000105AA File Offset: 0x0000E7AA
		private void Marker_MouseMove(object sender, MouseEventArgs e)
		{
			e.Handled = true;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000105B4 File Offset: 0x0000E7B4
		private void MarkerTool_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			MarkerToolIcon.MarkerToolSizeChangedEventHandler markerToolSizeChangedEvent = this.MarkerToolSizeChangedEvent;
			if (markerToolSizeChangedEvent != null)
			{
				markerToolSizeChangedEvent(this);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000105D4 File Offset: 0x0000E7D4
		public void HandleScaleChange(double ScaleX, double ScaleY)
		{
			if (this.Marker != null && this.Marker.ShapeID == Enums.MarkerShape.ICON && (ScaleX < 1.0 & ScaleY < 1.0))
			{
				this.ToolScale.ScaleX = 1.0 / ScaleX;
				this.ToolScale.ScaleY = 1.0 / ScaleY;
			}
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00010640 File Offset: 0x0000E840
		public void Cancel()
		{
			MarkerToolIcon.MarkerToolCancelledEventHandler markerToolCancelledEvent = this.MarkerToolCancelledEvent;
			if (markerToolCancelledEvent != null)
			{
				markerToolCancelledEvent(this);
			}
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00010660 File Offset: 0x0000E860
		public void Save()
		{
			try
			{
				this.MarkerIconHolder.Children.Remove(this.MarkerIcon);
				MarkerToolIcon.MarkerToolSavedEventHandler markerToolSavedEvent = this.MarkerToolSavedEvent;
				if (markerToolSavedEvent != null)
				{
					markerToolSavedEvent(this, this.MarkerIcon);
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x000106BC File Offset: 0x0000E8BC
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x000106C4 File Offset: 0x0000E8C4
		internal virtual Canvas MarkerIconHolder { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x000106CD File Offset: 0x0000E8CD
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x000106D8 File Offset: 0x0000E8D8
		internal virtual TextBox txtText
		{
			[CompilerGenerated]
			get
			{
				return this._txtText;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				TextChangedEventHandler value2 = new TextChangedEventHandler(this.txtText_TextChanged);
				TextBox txtText = this._txtText;
				if (txtText != null)
				{
					txtText.TextChanged -= value2;
				}
				this._txtText = value;
				txtText = this._txtText;
				if (txtText != null)
				{
					txtText.TextChanged += value2;
				}
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0001071B File Offset: 0x0000E91B
		// (set) Token: 0x060002CA RID: 714 RVA: 0x00010724 File Offset: 0x0000E924
		internal virtual ComboBox ddlType
		{
			[CompilerGenerated]
			get
			{
				return this._ddlType;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				SelectionChangedEventHandler value2 = new SelectionChangedEventHandler(this.ddlType_SelectionChanged);
				ComboBox ddlType = this._ddlType;
				if (ddlType != null)
				{
					ddlType.SelectionChanged -= value2;
				}
				this._ddlType = value;
				ddlType = this._ddlType;
				if (ddlType != null)
				{
					ddlType.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00010767 File Offset: 0x0000E967
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00010770 File Offset: 0x0000E970
		internal virtual ComboBox ddlColor
		{
			[CompilerGenerated]
			get
			{
				return this._ddlColor;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				SelectionChangedEventHandler value2 = new SelectionChangedEventHandler(this.ddlColor_SelectionChanged);
				ComboBox ddlColor = this._ddlColor;
				if (ddlColor != null)
				{
					ddlColor.SelectionChanged -= value2;
				}
				this._ddlColor = value;
				ddlColor = this._ddlColor;
				if (ddlColor != null)
				{
					ddlColor.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002CD RID: 717 RVA: 0x000107B3 File Offset: 0x0000E9B3
		// (set) Token: 0x060002CE RID: 718 RVA: 0x000107BC File Offset: 0x0000E9BC
		internal virtual ComboBox ddlScope
		{
			[CompilerGenerated]
			get
			{
				return this._ddlScope;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				SelectionChangedEventHandler value2 = new SelectionChangedEventHandler(this.ddlScope_SelectionChanged);
				ComboBox ddlScope = this._ddlScope;
				if (ddlScope != null)
				{
					ddlScope.SelectionChanged -= value2;
				}
				this._ddlScope = value;
				ddlScope = this._ddlScope;
				if (ddlScope != null)
				{
					ddlScope.SelectionChanged += value2;
				}
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000107FF File Offset: 0x0000E9FF
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00010808 File Offset: 0x0000EA08
		internal virtual Slider sldDir
		{
			[CompilerGenerated]
			get
			{
				return this._sldDir;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedPropertyChangedEventHandler<double> value2 = new RoutedPropertyChangedEventHandler<double>(this.sldDir_ValueChanged);
				Slider sldDir = this._sldDir;
				if (sldDir != null)
				{
					sldDir.ValueChanged -= value2;
				}
				this._sldDir = value;
				sldDir = this._sldDir;
				if (sldDir != null)
				{
					sldDir.ValueChanged += value2;
				}
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0001084B File Offset: 0x0000EA4B
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00010854 File Offset: 0x0000EA54
		internal virtual Button btnDone
		{
			[CompilerGenerated]
			get
			{
				return this._btnDone;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.btnDone_Click);
				Button btnDone = this._btnDone;
				if (btnDone != null)
				{
					btnDone.Click -= value2;
				}
				this._btnDone = value;
				btnDone = this._btnDone;
				if (btnDone != null)
				{
					btnDone.Click += value2;
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00010897 File Offset: 0x0000EA97
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x000108A0 File Offset: 0x0000EAA0
		internal virtual Button btnCancel
		{
			[CompilerGenerated]
			get
			{
				return this._btnCancel;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				RoutedEventHandler value2 = new RoutedEventHandler(this.btnCancel_Click);
				Button btnCancel = this._btnCancel;
				if (btnCancel != null)
				{
					btnCancel.Click -= value2;
				}
				this._btnCancel = value;
				btnCancel = this._btnCancel;
				if (btnCancel != null)
				{
					btnCancel.Click += value2;
				}
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x000108E3 File Offset: 0x0000EAE3
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x000108EB File Offset: 0x0000EAEB
		internal virtual ScaleTransform ToolScale { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060002D7 RID: 727 RVA: 0x000108F4 File Offset: 0x0000EAF4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Athena Desktop;component/controls/tools/markertoolicon.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00010924 File Offset: 0x0000EB24
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.MarkerIconHolder = (Canvas)target;
				return;
			}
			if (connectionId == 2)
			{
				this.txtText = (TextBox)target;
				return;
			}
			if (connectionId == 3)
			{
				this.ddlType = (ComboBox)target;
				return;
			}
			if (connectionId == 4)
			{
				this.ddlColor = (ComboBox)target;
				return;
			}
			if (connectionId == 5)
			{
				this.ddlScope = (ComboBox)target;
				return;
			}
			if (connectionId == 6)
			{
				this.sldDir = (Slider)target;
				return;
			}
			if (connectionId == 7)
			{
				this.btnDone = (Button)target;
				return;
			}
			if (connectionId == 8)
			{
				this.btnCancel = (Button)target;
				return;
			}
			if (connectionId == 9)
			{
				this.ToolScale = (ScaleTransform)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000134 RID: 308
		private Marker Marker;

		// Token: 0x04000135 RID: 309
		private Marker MarkerIcon;

		// Token: 0x04000141 RID: 321
		private bool _contentLoaded;

		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x0600077C RID: 1916
		public delegate void MarkerToolSavedEventHandler(MarkerToolIcon Tool, Marker MarkerIcon);

		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x06000780 RID: 1920
		public delegate void MarkerToolCancelledEventHandler(MarkerToolIcon Tool);

		// Token: 0x02000074 RID: 116
		// (Invoke) Token: 0x06000784 RID: 1924
		public delegate void MarkerToolSizeChangedEventHandler(MarkerToolIcon Tool);
	}
}
