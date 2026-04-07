using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x0200002A RID: 42
	[DesignerGenerated]
	public partial class frmLoad : Form
	{
		// Token: 0x060001D6 RID: 470 RVA: 0x0000B754 File Offset: 0x00009954
		public frmLoad()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000BDA7 File Offset: 0x00009FA7
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000BDAF File Offset: 0x00009FAF
		internal virtual Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		internal virtual Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000BDC9 File Offset: 0x00009FC9
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000BDD4 File Offset: 0x00009FD4
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

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000BE17 File Offset: 0x0000A017
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000BE1F File Offset: 0x0000A01F
		internal virtual TextBox txtFileName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000BE28 File Offset: 0x0000A028
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000BE30 File Offset: 0x0000A030
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000BE39 File Offset: 0x0000A039
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000BE41 File Offset: 0x0000A041
		internal virtual CheckBox chkLoadGameGroupsUnits { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000BE4A File Offset: 0x0000A04A
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000BE52 File Offset: 0x0000A052
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000BE5B File Offset: 0x0000A05B
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000BE63 File Offset: 0x0000A063
		internal virtual CheckBox chkSaveInk { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000BE6C File Offset: 0x0000A06C
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000BE74 File Offset: 0x0000A074
		internal virtual OpenFileDialog dlgOpenFile { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000BE7D File Offset: 0x0000A07D
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000BE85 File Offset: 0x0000A085
		internal virtual CheckBox chkLoadGameMarkers { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060001ED RID: 493 RVA: 0x0000BE90 File Offset: 0x0000A090
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
