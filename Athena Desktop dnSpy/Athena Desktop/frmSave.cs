using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x0200002B RID: 43
	[DesignerGenerated]
	public partial class frmSave : Form
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		public frmSave()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000C555 File Offset: 0x0000A755
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000C55D File Offset: 0x0000A75D
		internal virtual Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000C566 File Offset: 0x0000A766
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000C56E File Offset: 0x0000A76E
		internal virtual Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000C577 File Offset: 0x0000A777
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000C580 File Offset: 0x0000A780
		internal virtual Button btnSaveFile
		{
			[CompilerGenerated]
			get
			{
				return this._btnSaveFile;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.btnImageFile_Click);
				Button btnSaveFile = this._btnSaveFile;
				if (btnSaveFile != null)
				{
					btnSaveFile.Click -= value2;
				}
				this._btnSaveFile = value;
				btnSaveFile = this._btnSaveFile;
				if (btnSaveFile != null)
				{
					btnSaveFile.Click += value2;
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000C5C3 File Offset: 0x0000A7C3
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000C5CB File Offset: 0x0000A7CB
		internal virtual TextBox txtFileName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000C5DC File Offset: 0x0000A7DC
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000C5E5 File Offset: 0x0000A7E5
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000C5ED File Offset: 0x0000A7ED
		internal virtual SaveFileDialog dlgDestFile { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000C5F6 File Offset: 0x0000A7F6
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000C5FE File Offset: 0x0000A7FE
		internal virtual CheckBox chkSaveGroupsUnits { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000C607 File Offset: 0x0000A807
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000C60F File Offset: 0x0000A80F
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000201 RID: 513 RVA: 0x0000C618 File Offset: 0x0000A818
		// (set) Token: 0x06000202 RID: 514 RVA: 0x0000C620 File Offset: 0x0000A820
		internal virtual CheckBox chkSaveInk { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000C629 File Offset: 0x0000A829
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000C631 File Offset: 0x0000A831
		internal virtual CheckBox chkIncludeMarkers { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000205 RID: 517 RVA: 0x0000C63C File Offset: 0x0000A83C
		private void btnImageFile_Click(object sender, EventArgs e)
		{
			string text = string.Empty;
			try
			{
				if (this.dlgDestFile.ShowDialog() == DialogResult.OK)
				{
					text = this.dlgDestFile.FileName;
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
