using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000029 RID: 41
	[DesignerGenerated]
	public partial class frmLoadRecording : Form
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000B25F File Offset: 0x0000945F
		public frmLoadRecording()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000B648 File Offset: 0x00009848
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000B650 File Offset: 0x00009850
		internal virtual Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000B659 File Offset: 0x00009859
		// (set) Token: 0x060001CC RID: 460 RVA: 0x0000B661 File Offset: 0x00009861
		internal virtual Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001CD RID: 461 RVA: 0x0000B66A File Offset: 0x0000986A
		// (set) Token: 0x060001CE RID: 462 RVA: 0x0000B674 File Offset: 0x00009874
		internal virtual Button btnLoadFile
		{
			[CompilerGenerated]
			get
			{
				return this._btnLoadFile;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.btnImageFile_Click);
				Button btnLoadFile = this._btnLoadFile;
				if (btnLoadFile != null)
				{
					btnLoadFile.Click -= value2;
				}
				this._btnLoadFile = value;
				btnLoadFile = this._btnLoadFile;
				if (btnLoadFile != null)
				{
					btnLoadFile.Click += value2;
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000B6B7 File Offset: 0x000098B7
		// (set) Token: 0x060001D0 RID: 464 RVA: 0x0000B6BF File Offset: 0x000098BF
		internal virtual TextBox txtFileName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000B6C8 File Offset: 0x000098C8
		// (set) Token: 0x060001D2 RID: 466 RVA: 0x0000B6D0 File Offset: 0x000098D0
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000B6D9 File Offset: 0x000098D9
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000B6E1 File Offset: 0x000098E1
		internal virtual OpenFileDialog dlgOpenFile { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060001D5 RID: 469 RVA: 0x0000B6EC File Offset: 0x000098EC
		private void btnImageFile_Click(object sender, EventArgs e)
		{
			string text = string.Empty;
			try
			{
				if (this.dlgOpenFile.ShowDialog() == DialogResult.OK)
				{
					text = this.dlgOpenFile.FileName;
				}
				else
				{
					text = string.Empty;
				}
			}
			catch (Exception ex)
			{
				text = string.Empty;
			}
			this.txtFileName.Text = text;
		}
	}
}
