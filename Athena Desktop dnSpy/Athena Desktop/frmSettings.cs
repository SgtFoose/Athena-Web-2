using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000032 RID: 50
	[DesignerGenerated]
	public partial class frmSettings : Form
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00011B28 File Offset: 0x0000FD28
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00011B30 File Offset: 0x0000FD30
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000306 RID: 774 RVA: 0x00011B39 File Offset: 0x0000FD39
		// (set) Token: 0x06000307 RID: 775 RVA: 0x00011B41 File Offset: 0x0000FD41
		internal virtual TextBox txtARMAIP { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00011B4A File Offset: 0x0000FD4A
		// (set) Token: 0x06000309 RID: 777 RVA: 0x00011B52 File Offset: 0x0000FD52
		internal virtual Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00011B5B File Offset: 0x0000FD5B
		// (set) Token: 0x0600030B RID: 779 RVA: 0x00011B63 File Offset: 0x0000FD63
		internal virtual Button btnCancel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600030C RID: 780 RVA: 0x00011B6C File Offset: 0x0000FD6C
		// (set) Token: 0x0600030D RID: 781 RVA: 0x00011B74 File Offset: 0x0000FD74
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00011B7D File Offset: 0x0000FD7D
		// (set) Token: 0x0600030F RID: 783 RVA: 0x00011B85 File Offset: 0x0000FD85
		internal virtual TextBox txtARMAPort { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000310 RID: 784 RVA: 0x00011B8E File Offset: 0x0000FD8E
		// (set) Token: 0x06000311 RID: 785 RVA: 0x00011B96 File Offset: 0x0000FD96
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00011B9F File Offset: 0x0000FD9F
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00011BA7 File Offset: 0x0000FDA7
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		// (set) Token: 0x06000315 RID: 789 RVA: 0x00011BB8 File Offset: 0x0000FDB8
		internal virtual Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00011BC1 File Offset: 0x0000FDC1
		// (set) Token: 0x06000317 RID: 791 RVA: 0x00011BC9 File Offset: 0x0000FDC9
		internal virtual CheckBox chkUseTouchForInk { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000318 RID: 792 RVA: 0x00011BD4 File Offset: 0x0000FDD4
		public frmSettings()
		{
			this.InitializeComponent();
			if (MySettings.Default["ARMAPCIP"] != null && !string.IsNullOrEmpty(MySettings.Default["ARMAPCIP"].ToString().Trim()))
			{
				this.txtARMAIP.Text = MySettings.Default.ARMAPCIP.Trim();
			}
			if (MySettings.Default["ARMAPCPort"] != null)
			{
				this.txtARMAPort.Text = MySettings.Default.ARMAPCPort.ToString();
			}
			if (MySettings.Default["UseTouchForInk"] != null)
			{
				this.chkUseTouchForInk.Checked = MySettings.Default.UseTouchForInk;
			}
		}
	}
}
