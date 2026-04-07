namespace Athena.App.W7
{
	// Token: 0x02000032 RID: 50
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class frmSettings : global::System.Windows.Forms.Form
	{
		// Token: 0x06000302 RID: 770 RVA: 0x000114C4 File Offset: 0x0000F6C4
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

		// Token: 0x06000303 RID: 771 RVA: 0x00011504 File Offset: 0x0000F704
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Athena.App.W7.frmSettings));
			this.Label1 = new global::System.Windows.Forms.Label();
			this.txtARMAIP = new global::System.Windows.Forms.TextBox();
			this.txtARMAPort = new global::System.Windows.Forms.TextBox();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.Label4 = new global::System.Windows.Forms.Label();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.Label7 = new global::System.Windows.Forms.Label();
			this.chkUseTouchForInk = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(12, 30);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(71, 13);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "ARMA PC IP:";
			this.txtARMAIP.AccessibleDescription = "";
			this.txtARMAIP.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtARMAIP.Location = new global::System.Drawing.Point(125, 27);
			this.txtARMAIP.Name = "txtARMAIP";
			this.txtARMAIP.Size = new global::System.Drawing.Size(341, 20);
			this.txtARMAIP.TabIndex = 1;
			this.txtARMAPort.AccessibleDescription = "";
			this.txtARMAPort.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtARMAPort.Location = new global::System.Drawing.Point(125, 87);
			this.txtARMAPort.Name = "txtARMAPort";
			this.txtARMAPort.Size = new global::System.Drawing.Size(62, 20);
			this.txtARMAPort.TabIndex = 7;
			this.btnOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new global::System.Drawing.Point(310, 240);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(391, 240);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(12, 90);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(80, 13);
			this.Label3.TabIndex = 6;
			this.Label3.Text = "ARMA PC Port:";
			this.Label4.AutoSize = true;
			this.Label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 7f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label4.Location = new global::System.Drawing.Point(125, 110);
			this.Label4.Name = "Label4";
			this.Label4.Size = new global::System.Drawing.Size(112, 13);
			this.Label4.TabIndex = 8;
			this.Label4.Text = "Note: Default is 28800";
			this.Label2.AutoSize = true;
			this.Label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 7f, global::System.Drawing.FontStyle.Italic, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label2.Location = new global::System.Drawing.Point(125, 50);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(317, 13);
			this.Label2.TabIndex = 5;
			this.Label2.Text = "Note: If playing on the same PC as the Athena App, use 'localhost'";
			this.Label7.AutoSize = true;
			this.Label7.Location = new global::System.Drawing.Point(12, 149);
			this.Label7.Name = "Label7";
			this.Label7.Size = new global::System.Drawing.Size(99, 13);
			this.Label7.TabIndex = 12;
			this.Label7.Text = "Use Touch For Ink:";
			this.chkUseTouchForInk.AutoSize = true;
			this.chkUseTouchForInk.Location = new global::System.Drawing.Point(125, 149);
			this.chkUseTouchForInk.Name = "chkUseTouchForInk";
			this.chkUseTouchForInk.Size = new global::System.Drawing.Size(15, 14);
			this.chkUseTouchForInk.TabIndex = 13;
			this.chkUseTouchForInk.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(484, 275);
			base.Controls.Add(this.chkUseTouchForInk);
			base.Controls.Add(this.Label7);
			base.Controls.Add(this.Label4);
			base.Controls.Add(this.txtARMAPort);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.txtARMAIP);
			base.Controls.Add(this.Label1);
			this.DoubleBuffered = true;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSettings";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Settings";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000153 RID: 339
		private global::System.ComponentModel.IContainer components;
	}
}
