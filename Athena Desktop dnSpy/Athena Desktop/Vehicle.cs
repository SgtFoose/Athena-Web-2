using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Athena.Objects.v2;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000028 RID: 40
	[DesignerGenerated]
	public class Vehicle : UserControl, IComponentConnector
	{
		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		// (set) Token: 0x060001AB RID: 427 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public string CrewString { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000A901 File Offset: 0x00008B01
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000A909 File Offset: 0x00008B09
		public Unit SourceUnit { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0000A912 File Offset: 0x00008B12
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000A91A File Offset: 0x00008B1A
		public Vehicle Vehicle { get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000A923 File Offset: 0x00008B23
		public Marker MarkerIcon
		{
			get
			{
				return this.Icon;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000A92B File Offset: 0x00008B2B
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x0000A933 File Offset: 0x00008B33
		public Enums.RenderMode RenderMode { get; set; }

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A93C File Offset: 0x00008B3C
		public Vehicle(ref Vehicle Vehicle)
		{
			this.CrewString = string.Empty;
			this.RenderMode = Enums.RenderMode.Elevations;
			this.InitializeComponent();
			this.Vehicle = Vehicle;
			this.Update();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A96A File Offset: 0x00008B6A
		public Vehicle()
		{
			this.CrewString = string.Empty;
			this.RenderMode = Enums.RenderMode.Elevations;
			this.InitializeComponent();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A98C File Offset: 0x00008B8C
		public void Update()
		{
			try
			{
				if (this.Vehicle != null)
				{
					this.GetUnit();
					if (this.SourceUnit != null)
					{
						this.LoadMarker();
						if (this.HasCrewChanged())
						{
							this.LoadText();
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		private void GetUnit()
		{
			if (this.Vehicle != null && this.Vehicle.CrewDictionary != null && this.Vehicle.CrewDictionary.Count != 0)
			{
				this.SourceUnit = this.Vehicle.CrewDictionary.Values.First<CrewDictionaryItem>().Unit;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000AA38 File Offset: 0x00008C38
		private void LoadMarker()
		{
			Marker marker = new Marker();
			marker.Alpha = 1.0;
			marker.Direction = this.Vehicle.Direction;
			marker.SizeX = "0.75";
			marker.Width = 0.75;
			marker.SizeY = "0.75";
			marker.Height = 0.75;
			marker.ShapeID = Enums.MarkerShape.ICON;
			marker.ColorID = this.GetMarkerColor();
			marker.TypeID = this.GetMarkerType();
			this.Icon.ParentType = Enums.MarkerIconParentType.Vehicle;
			this.Icon.Update(marker);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000AAD8 File Offset: 0x00008CD8
		private void LoadText()
		{
			this.CrewText.Text = string.Empty;
			List<string> list = new List<string>();
			checked
			{
				if (this.Vehicle != null && this.Vehicle.CrewDictionary != null && this.Vehicle.CrewDictionary.Count != 0)
				{
					string[] array = new string[]
					{
						"commander",
						"gunner",
						"driver",
						"turret",
						"cargo"
					};
					if (!new string[]
					{
						"1",
						"2",
						"3"
					}.Contains(this.Vehicle.Class))
					{
						array = new string[]
						{
							"driver",
							"commander",
							"gunner",
							"turret",
							"cargo"
						};
					}
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						Vehicle._Closure$__23-0 CS$<>8__locals1 = new Vehicle._Closure$__23-0(CS$<>8__locals1);
						CS$<>8__locals1.$VB$Local_Position = array2[i];
						try
						{
							List<Unit> list2 = (from x in this.Vehicle.CrewDictionary
							where x.Value.Position.Equals(CS$<>8__locals1.$VB$Local_Position, StringComparison.InvariantCultureIgnoreCase) & x.Value.Unit != null
							select x).OrderBy((Athena.App.W7.Vehicle._Closure$__.$I23-1 == null) ? (Athena.App.W7.Vehicle._Closure$__.$I23-1 = ((KeyValuePair<string, CrewDictionaryItem> x) => x.Value.Unit.DisplayName)) : Athena.App.W7.Vehicle._Closure$__.$I23-1).Select((Athena.App.W7.Vehicle._Closure$__.$I23-2 == null) ? (Athena.App.W7.Vehicle._Closure$__.$I23-2 = ((KeyValuePair<string, CrewDictionaryItem> x) => x.Value.Unit)) : Athena.App.W7.Vehicle._Closure$__.$I23-2).ToList<Unit>();
							if (list2 != null && list2.Count != 0)
							{
								try
								{
									foreach (Unit unit in list2)
									{
										list.Add(CS$<>8__locals1.$VB$Local_Position.Substring(0, 1).ToUpper() + ":" + unit.DisplayName);
									}
								}
								finally
								{
									List<Unit>.Enumerator enumerator;
									((IDisposable)enumerator).Dispose();
								}
							}
						}
						catch (Exception ex)
						{
						}
					}
				}
				if (list.Count != 0)
				{
					this.CrewText.Inlines.Add(list[0] + Environment.NewLine);
					if (list.Count > 1)
					{
						int num = list.Count - 1;
						for (int j = 1; j <= num; j++)
						{
							string text = list[j];
							if (j < list.Count - 1)
							{
								text += Environment.NewLine;
							}
							this.CrewText.Inlines.Add(new Run(text)
							{
								FontSize = 8.0,
								FontWeight = FontWeights.Normal
							});
						}
					}
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000AD94 File Offset: 0x00008F94
		private Enums.MarkerColor GetMarkerColor()
		{
			Enums.MarkerColor result = Enums.MarkerColor.ColorBlack;
			switch (this.SourceUnit.SideID)
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

		// Token: 0x060001BA RID: 442 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		private Enums.MarkerType GetMarkerType()
		{
			Enums.MarkerType result = Enums.MarkerType.vehicle_car;
			if (this.Vehicle != null)
			{
				string @class = this.Vehicle.Class;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(@class);
				if (num <= 856466825U)
				{
					if (num <= 822911587U)
					{
						if (num != 806133968U)
						{
							if (num == 822911587U)
							{
								if (Operators.CompareString(@class, "4", false) == 0)
								{
									result = Enums.MarkerType.vehicle_helicopter;
								}
							}
						}
						else if (Operators.CompareString(@class, "5", false) == 0)
						{
							result = Enums.MarkerType.vehicle_plane;
						}
					}
					else if (num != 839689206U)
					{
						if (num == 856466825U)
						{
							if (Operators.CompareString(@class, "6", false) == 0)
							{
								result = Enums.MarkerType.vehicle_plane;
							}
						}
					}
					else if (Operators.CompareString(@class, "7", false) == 0)
					{
						result = Enums.MarkerType.vehicle_ship;
					}
				}
				else if (num <= 906799682U)
				{
					if (num != 873244444U)
					{
						if (num == 906799682U)
						{
							if (Operators.CompareString(@class, "3", false) == 0)
							{
								result = Enums.MarkerType.vehicle_tank;
							}
						}
					}
					else if (Operators.CompareString(@class, "1", false) == 0)
					{
						result = Enums.MarkerType.vehicle_car;
					}
				}
				else if (num != 923577301U)
				{
					if (num == 1024243015U)
					{
						if (Operators.CompareString(@class, "8", false) == 0)
						{
							result = Enums.MarkerType.vehicle_ship;
						}
					}
				}
				else if (Operators.CompareString(@class, "2", false) == 0)
				{
					result = Enums.MarkerType.vehicle_apc;
				}
			}
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000AF5C File Offset: 0x0000915C
		private bool HasCrewChanged()
		{
			bool result = false;
			string text = string.Empty;
			if (this.Vehicle.CrewDictionary != null && this.Vehicle.CrewDictionary.Count != 0)
			{
				try
				{
					List<CrewDictionaryItem> list = this.Vehicle.CrewDictionary.Where((Athena.App.W7.Vehicle._Closure$__.$I26-0 == null) ? (Athena.App.W7.Vehicle._Closure$__.$I26-0 = ((KeyValuePair<string, CrewDictionaryItem> x) => x.Value.Unit != null)) : Athena.App.W7.Vehicle._Closure$__.$I26-0).OrderBy((Athena.App.W7.Vehicle._Closure$__.$I26-1 == null) ? (Athena.App.W7.Vehicle._Closure$__.$I26-1 = ((KeyValuePair<string, CrewDictionaryItem> x) => x.Value.Position)) : Athena.App.W7.Vehicle._Closure$__.$I26-1).ThenBy((Athena.App.W7.Vehicle._Closure$__.$I26-2 == null) ? (Athena.App.W7.Vehicle._Closure$__.$I26-2 = ((KeyValuePair<string, CrewDictionaryItem> x) => x.Value.Unit.DisplayName)) : Athena.App.W7.Vehicle._Closure$__.$I26-2).Select((Athena.App.W7.Vehicle._Closure$__.$I26-3 == null) ? (Athena.App.W7.Vehicle._Closure$__.$I26-3 = ((KeyValuePair<string, CrewDictionaryItem> x) => x.Value)) : Athena.App.W7.Vehicle._Closure$__.$I26-3).ToList<CrewDictionaryItem>();
					if (list != null && list.Count != 0)
					{
						try
						{
							foreach (CrewDictionaryItem crewDictionaryItem in list)
							{
								text = text + crewDictionaryItem.Position + crewDictionaryItem.Unit.DisplayName;
							}
						}
						finally
						{
							List<CrewDictionaryItem>.Enumerator enumerator;
							((IDisposable)enumerator).Dispose();
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
			if (!string.Equals(this.CrewString, text, StringComparison.InvariantCultureIgnoreCase))
			{
				this.CrewString = text;
				result = true;
			}
			return result;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B0FC File Offset: 0x000092FC
		public void HandleRenderModeChange(Enums.RenderMode Mode)
		{
			this.RenderMode = Mode;
			switch (Mode)
			{
			case Enums.RenderMode.Elevations:
				this.CrewText.Background = new SolidColorBrush(Colors.Transparent)
				{
					Opacity = 1.0
				};
				return;
			case Enums.RenderMode.HeightMapColor:
				this.CrewText.Background = new SolidColorBrush(Colors.Transparent)
				{
					Opacity = 1.0
				};
				return;
			case Enums.RenderMode.HeightMapGray:
				this.CrewText.Background = new SolidColorBrush(Colors.White)
				{
					Opacity = 0.5
				};
				return;
			case Enums.RenderMode.Image:
				this.CrewText.Background = new SolidColorBrush(Colors.Transparent)
				{
					Opacity = 1.0
				};
				return;
			default:
				return;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000B1BE File Offset: 0x000093BE
		// (set) Token: 0x060001BE RID: 446 RVA: 0x0000B1C6 File Offset: 0x000093C6
		internal virtual Grid VehicleGrid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000B1CF File Offset: 0x000093CF
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000B1D7 File Offset: 0x000093D7
		internal virtual Marker Icon { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000B1E0 File Offset: 0x000093E0
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000B1E8 File Offset: 0x000093E8
		internal virtual TextBlock CrewText { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060001C3 RID: 451 RVA: 0x0000B1F4 File Offset: 0x000093F4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Athena Desktop;component/controls/general/vehicle.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000A8BB File Offset: 0x00008ABB
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000B223 File Offset: 0x00009423
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.VehicleGrid = (Grid)target;
				return;
			}
			if (connectionId == 2)
			{
				this.Icon = (Marker)target;
				return;
			}
			if (connectionId == 3)
			{
				this.CrewText = (TextBlock)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000CF RID: 207
		private bool _contentLoaded;
	}
}
