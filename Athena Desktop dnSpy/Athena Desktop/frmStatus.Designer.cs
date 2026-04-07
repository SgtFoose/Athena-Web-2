namespace Athena.App.W7
{
	// Token: 0x0200002C RID: 44
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class frmStatus : global::System.Windows.Forms.Form
	{
		// Token: 0x06000207 RID: 519 RVA: 0x0000C700 File Offset: 0x0000A900
		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.components != null)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000C740 File Offset: 0x0000A940
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Athena.App.W7.frmStatus));
			this.TabControl1 = new global::System.Windows.Forms.TabControl();
			this.TabPage1 = new global::System.Windows.Forms.TabPage();
			this.Label5 = new global::System.Windows.Forms.Label();
			this.txtRelayMessageOut = new global::System.Windows.Forms.TextBox();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.txtRelayMessageIn = new global::System.Windows.Forms.TextBox();
			this.lblRelayOtherQueue = new global::System.Windows.Forms.Label();
			this.Label4 = new global::System.Windows.Forms.Label();
			this.lblRelayFrameQueue = new global::System.Windows.Forms.Label();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.lblRelayStatus = new global::System.Windows.Forms.Label();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.TabPage2 = new global::System.Windows.Forms.TabPage();
			this.TabControl1.SuspendLayout();
			this.TabPage1.SuspendLayout();
			base.SuspendLayout();
			this.TabControl1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.TabControl1.Controls.Add(this.TabPage1);
			this.TabControl1.Controls.Add(this.TabPage2);
			this.TabControl1.Location = new global::System.Drawing.Point(12, 12);
			this.TabControl1.Name = "TabControl1";
			this.TabControl1.SelectedIndex = 0;
			this.TabControl1.Size = new global::System.Drawing.Size(664, 349);
			this.TabControl1.TabIndex = 0;
			this.TabPage1.Controls.Add(this.Label5);
			this.TabPage1.Controls.Add(this.txtRelayMessageOut);
			this.TabPage1.Controls.Add(this.Label3);
			this.TabPage1.Controls.Add(this.txtRelayMessageIn);
			this.TabPage1.Controls.Add(this.lblRelayOtherQueue);
			this.TabPage1.Controls.Add(this.Label4);
			this.TabPage1.Controls.Add(this.lblRelayFrameQueue);
			this.TabPage1.Controls.Add(this.Label2);
			this.TabPage1.Controls.Add(this.lblRelayStatus);
			this.TabPage1.Controls.Add(this.Label1);
			this.TabPage1.Location = new global::System.Drawing.Point(4, 22);
			this.TabPage1.Name = "TabPage1";
			this.TabPage1.Padding = new global::System.Windows.Forms.Padding(3);
			this.TabPage1.Size = new global::System.Drawing.Size(656, 323);
			this.TabPage1.TabIndex = 0;
			this.TabPage1.Text = "Relay";
			this.TabPage1.UseVisualStyleBackColor = true;
			this.Label5.AutoSize = true;
			this.Label5.Location = new global::System.Drawing.Point(350, 74);
			this.Label5.Name = "Label5";
			this.Label5.Size = new global::System.Drawing.Size(96, 13);
			this.Label5.TabIndex = 9;
			this.Label5.Text = "Last Message Out:";
			this.txtRelayMessageOut.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtRelayMessageOut.Location = new global::System.Drawing.Point(350, 90);
			this.txtRelayMessageOut.Multiline = true;
			this.txtRelayMessageOut.Name = "txtRelayMessageOut";
			this.txtRelayMessageOut.ReadOnly = true;
			this.txtRelayMessageOut.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtRelayMessageOut.Size = new global::System.Drawing.Size(300, 227);
			this.txtRelayMessageOut.TabIndex = 8;
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(6, 74);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(88, 13);
			this.Label3.TabIndex = 7;
			this.Label3.Text = "Last Message In:";
			this.txtRelayMessageIn.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left);
			this.txtRelayMessageIn.Location = new global::System.Drawing.Point(6, 90);
			this.txtRelayMessageIn.Multiline = true;
			this.txtRelayMessageIn.Name = "txtRelayMessageIn";
			this.txtRelayMessageIn.ReadOnly = true;
			this.txtRelayMessageIn.ScrollBars = global::System.Windows.Forms.ScrollBars.Vertical;
			this.txtRelayMessageIn.Size = new global::System.Drawing.Size(300, 227);
			this.txtRelayMessageIn.TabIndex = 6;
			this.lblRelayOtherQueue.AutoSize = true;
			this.lblRelayOtherQueue.Location = new global::System.Drawing.Point(86, 29);
			this.lblRelayOtherQueue.Name = "lblRelayOtherQueue";
			this.lblRelayOtherQueue.Size = new global::System.Drawing.Size(58, 13);
			this.lblRelayOtherQueue.TabIndex = 5;
			this.lblRelayOtherQueue.Text = "<Pending>";
			this.Label4.AutoSize = true;
			this.Label4.Location = new global::System.Drawing.Point(6, 29);
			this.Label4.Name = "Label4";
			this.Label4.Size = new global::System.Drawing.Size(71, 13);
			this.Label4.TabIndex = 4;
			this.Label4.Text = "Other Queue:";
			this.lblRelayFrameQueue.AutoSize = true;
			this.lblRelayFrameQueue.Location = new global::System.Drawing.Point(86, 7);
			this.lblRelayFrameQueue.Name = "lblRelayFrameQueue";
			this.lblRelayFrameQueue.Size = new global::System.Drawing.Size(58, 13);
			this.lblRelayFrameQueue.TabIndex = 3;
			this.lblRelayFrameQueue.Text = "<Pending>";
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(6, 7);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(74, 13);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "Frame Queue:";
			this.lblRelayStatus.AutoSize = true;
			this.lblRelayStatus.Location = new global::System.Drawing.Point(86, 51);
			this.lblRelayStatus.Name = "lblRelayStatus";
			this.lblRelayStatus.Size = new global::System.Drawing.Size(58, 13);
			this.lblRelayStatus.TabIndex = 1;
			this.lblRelayStatus.Text = "<Pending>";
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(6, 51);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(40, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Status:";
			this.TabPage2.Location = new global::System.Drawing.Point(4, 22);
			this.TabPage2.Name = "TabPage2";
			this.TabPage2.Padding = new global::System.Windows.Forms.Padding(3);
			this.TabPage2.Size = new global::System.Drawing.Size(656, 323);
			this.TabPage2.TabIndex = 1;
			this.TabPage2.Text = "ACS";
			this.TabPage2.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(688, 373);
			base.Controls.Add(this.TabControl1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "frmStatus";
			this.Text = "Connection Status";
			this.TabControl1.ResumeLayout(false);
			this.TabPage1.ResumeLayout(false);
			this.TabPage1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040000ED RID: 237
		private global::System.ComponentModel.IContainer components;
	}
}
