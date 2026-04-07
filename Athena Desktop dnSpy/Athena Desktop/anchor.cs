using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using Athena.Objects.v2;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000026 RID: 38
	[DesignerGenerated]
	public class anchor : UserControl, IComponentConnector
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00009AA0 File Offset: 0x00007CA0
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00009AA8 File Offset: 0x00007CA8
		public MapAnchor MapAnchor { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00009AB1 File Offset: 0x00007CB1
		public double OffsetVertical
		{
			get
			{
				return 30.0;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00009ABC File Offset: 0x00007CBC
		public double OffsetHorizontal
		{
			get
			{
				return 100.0;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00009AC7 File Offset: 0x00007CC7
		public anchor(ref MapAnchor MapAnchor, Enums.RenderMode RenderMode)
		{
			this.InitializeComponent();
			this.MapAnchor = MapAnchor;
			this.SetText();
			this.SetColor(RenderMode);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00009AEA File Offset: 0x00007CEA
		public void SetText()
		{
			if (this.MapAnchor != null)
			{
				this.AnchorName.Text = this.MapAnchor.Name;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00009B0C File Offset: 0x00007D0C
		public void SetColor(Enums.RenderMode RenderMode)
		{
			switch (RenderMode)
			{
			case Enums.RenderMode.Elevations:
			case Enums.RenderMode.HeightMapColor:
			case Enums.RenderMode.Image:
				this.outerRing.Stroke = new SolidColorBrush(Colors.Black);
				this.innerRing.Stroke = new SolidColorBrush(Colors.Black);
				this.AnchorName.Foreground = new SolidColorBrush(Colors.Black);
				return;
			case Enums.RenderMode.HeightMapGray:
				this.outerRing.Stroke = new SolidColorBrush(Colors.SaddleBrown);
				this.innerRing.Stroke = new SolidColorBrush(Colors.SaddleBrown);
				this.AnchorName.Foreground = new SolidColorBrush(Colors.SaddleBrown);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00009BB3 File Offset: 0x00007DB3
		public void HandleRenderModeChange(Enums.RenderMode RenderMode)
		{
			this.SetColor(RenderMode);
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00009BBC File Offset: 0x00007DBC
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00009BC4 File Offset: 0x00007DC4
		internal virtual Ellipse outerRing { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00009BCD File Offset: 0x00007DCD
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00009BD5 File Offset: 0x00007DD5
		internal virtual Ellipse innerRing { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00009BDE File Offset: 0x00007DDE
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00009BE6 File Offset: 0x00007DE6
		internal virtual TextBlock AnchorName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000182 RID: 386 RVA: 0x00009BF0 File Offset: 0x00007DF0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Athena Desktop;component/controls/general/anchor.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009C1F File Offset: 0x00007E1F
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.outerRing = (Ellipse)target;
				return;
			}
			if (connectionId == 2)
			{
				this.innerRing = (Ellipse)target;
				return;
			}
			if (connectionId == 3)
			{
				this.AnchorName = (TextBlock)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x040000BB RID: 187
		private bool _contentLoaded;
	}
}
