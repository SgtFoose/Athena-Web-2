using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Athena.App.W7.My.Resources;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000031 RID: 49
	[DesignerGenerated]
	public partial class frmAbout : Form
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x000113C8 File Offset: 0x0000F5C8
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x000113D0 File Offset: 0x0000F5D0
		internal virtual FolderBrowserDialog dlgPickupDir { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000113D9 File Offset: 0x0000F5D9
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x000113E1 File Offset: 0x0000F5E1
		internal virtual ToolTip tipPickupDir { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000113EA File Offset: 0x0000F5EA
		// (set) Token: 0x060002EA RID: 746 RVA: 0x000113F2 File Offset: 0x0000F5F2
		internal virtual Button btnOK { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000113FB File Offset: 0x0000F5FB
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00011403 File Offset: 0x0000F603
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0001140C File Offset: 0x0000F60C
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00011414 File Offset: 0x0000F614
		internal virtual PictureBox HeaderImage { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0001141D File Offset: 0x0000F61D
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00011425 File Offset: 0x0000F625
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0001142E File Offset: 0x0000F62E
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00011436 File Offset: 0x0000F636
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001143F File Offset: 0x0000F63F
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00011447 File Offset: 0x0000F647
		internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00011450 File Offset: 0x0000F650
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00011458 File Offset: 0x0000F658
		internal virtual Label Label8 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00011461 File Offset: 0x0000F661
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00011469 File Offset: 0x0000F669
		internal virtual Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00011472 File Offset: 0x0000F672
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0001147A File Offset: 0x0000F67A
		internal virtual Label Label9 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00011483 File Offset: 0x0000F683
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0001148B File Offset: 0x0000F68B
		internal virtual Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00011494 File Offset: 0x0000F694
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0001149C File Offset: 0x0000F69C
		internal virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000114A5 File Offset: 0x0000F6A5
		// (set) Token: 0x06000300 RID: 768 RVA: 0x000114AD File Offset: 0x0000F6AD
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000301 RID: 769 RVA: 0x000114B6 File Offset: 0x0000F6B6
		public frmAbout()
		{
			this.InitializeComponent();
		}
	}
}
