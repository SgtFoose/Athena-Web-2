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
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000030 RID: 48
	[DesignerGenerated]
	public class Note : UserControl, IComponentConnector
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x000109D4 File Offset: 0x0000EBD4
		public Note()
		{
			base.PreviewMouseDown += this.Note_MouseDown;
			base.MouseEnter += this.Note_MouseEnter;
			base.MouseLeave += this.Note_MouseLeave;
			base.PreviewMouseMove += this.Note_MouseMove;
			this.InitializeComponent();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void txtEntry_KeyDown(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00010A37 File Offset: 0x0000EC37
		private void Note_MouseDown(object sender, MouseButtonEventArgs e)
		{
			this.txtEntry.Background = Brushes.White;
			this.txtEntry.BorderBrush = Brushes.Black;
			this.txtEntry.Focus();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00010A65 File Offset: 0x0000EC65
		private void Note_MouseEnter(object sender, MouseEventArgs e)
		{
			this.txtEntry.Background = Brushes.White;
			this.txtEntry.BorderBrush = Brushes.Black;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00010A87 File Offset: 0x0000EC87
		private void Note_MouseLeave(object sender, MouseEventArgs e)
		{
			this.txtEntry.Background = Brushes.Transparent;
			this.txtEntry.BorderBrush = Brushes.Transparent;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00010A35 File Offset: 0x0000EC35
		private void Note_MouseMove(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00010AA9 File Offset: 0x0000ECA9
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		internal virtual TextBox txtEntry
		{
			[CompilerGenerated]
			get
			{
				return this._txtEntry;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				KeyEventHandler value2 = new KeyEventHandler(this.txtEntry_KeyDown);
				TextBox txtEntry = this._txtEntry;
				if (txtEntry != null)
				{
					txtEntry.KeyDown -= value2;
				}
				this._txtEntry = value;
				txtEntry = this._txtEntry;
				if (txtEntry != null)
				{
					txtEntry.KeyDown += value2;
				}
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00010AF8 File Offset: 0x0000ECF8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (!this._contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/Athena Desktop;component/controls/tools/note.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00010B27 File Offset: 0x0000ED27
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void System_Windows_Markup_IComponentConnector_Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.txtEntry = (TextBox)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000143 RID: 323
		private bool _contentLoaded;
	}
}
