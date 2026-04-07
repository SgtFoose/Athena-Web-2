using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x0200002C RID: 44
	[DesignerGenerated]
	public partial class frmStatus : Form
	{
		// Token: 0x06000206 RID: 518 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public frmStatus()
		{
			base.Closing += this.frmStatus_Closing;
			this.FrameQueueCount = -1;
			this.OtherQueueCount = -1;
			this.Direction = string.Empty;
			this.MessageIn = string.Empty;
			this.MessageOut = string.Empty;
			this.InitializeComponent();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		// (set) Token: 0x0600020A RID: 522 RVA: 0x0000CEF4 File Offset: 0x0000B0F4
		internal virtual TabControl TabControl1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000CEFD File Offset: 0x0000B0FD
		// (set) Token: 0x0600020C RID: 524 RVA: 0x0000CF05 File Offset: 0x0000B105
		internal virtual TabPage TabPage1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000CF0E File Offset: 0x0000B10E
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000CF16 File Offset: 0x0000B116
		internal virtual TabPage TabPage2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000CF1F File Offset: 0x0000B11F
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000CF27 File Offset: 0x0000B127
		internal virtual Label lblRelayStatus { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000CF30 File Offset: 0x0000B130
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000CF38 File Offset: 0x0000B138
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000CF41 File Offset: 0x0000B141
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000CF49 File Offset: 0x0000B149
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000CF52 File Offset: 0x0000B152
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000CF5A File Offset: 0x0000B15A
		internal virtual Label lblRelayOtherQueue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000CF63 File Offset: 0x0000B163
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000CF6B File Offset: 0x0000B16B
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000CF74 File Offset: 0x0000B174
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000CF7C File Offset: 0x0000B17C
		internal virtual Label lblRelayFrameQueue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000CF85 File Offset: 0x0000B185
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000CF8D File Offset: 0x0000B18D
		internal virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000CF96 File Offset: 0x0000B196
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000CF9E File Offset: 0x0000B19E
		internal virtual TextBox txtRelayMessageOut { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000CFA7 File Offset: 0x0000B1A7
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000CFAF File Offset: 0x0000B1AF
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000CFB8 File Offset: 0x0000B1B8
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000CFC0 File Offset: 0x0000B1C0
		internal virtual TextBox txtRelayMessageIn { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000CFD1 File Offset: 0x0000B1D1
		public int FrameQueueCount { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000CFDA File Offset: 0x0000B1DA
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000CFE2 File Offset: 0x0000B1E2
		public int OtherQueueCount { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000CFF3 File Offset: 0x0000B1F3
		public string Direction { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000D004 File Offset: 0x0000B204
		public string MessageIn { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000D00D File Offset: 0x0000B20D
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000D015 File Offset: 0x0000B215
		public string MessageOut { get; set; }

		// Token: 0x0600022D RID: 557 RVA: 0x0000D020 File Offset: 0x0000B220
		public void UpdateQueueCounts(int FrameQueue, int OtherQueue)
		{
			this.FrameQueueCount = FrameQueue;
			this.OtherQueueCount = OtherQueue;
			if (base.Visible)
			{
				base.Invoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.lblRelayFrameQueue.Text = FrameQueue.ToString();
					this.lblRelayOtherQueue.Text = OtherQueue.ToString();
				}));
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D07C File Offset: 0x0000B27C
		public void UpdateStatus(string Direction)
		{
			this.Direction = Direction;
			if (base.Visible)
			{
				base.Invoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.lblRelayStatus.Text = Direction;
				}));
			}
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		public void UpdateMessageIn(string Message)
		{
			this.MessageIn = Message;
			if (base.Visible)
			{
				base.Invoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtRelayMessageIn.Text = Message;
				}));
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D10C File Offset: 0x0000B30C
		public void UpdateMessageOut(string Message)
		{
			this.MessageOut = Message;
			if (base.Visible)
			{
				base.Invoke(new VB$AnonymousDelegate_0(delegate()
				{
					this.txtRelayMessageOut.Text = Message;
				}));
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D154 File Offset: 0x0000B354
		private void frmStatus_Closing(object sender, CancelEventArgs e)
		{
			base.Hide();
			e.Cancel = true;
		}
	}
}
