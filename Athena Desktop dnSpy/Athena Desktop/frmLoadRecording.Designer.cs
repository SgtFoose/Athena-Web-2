namespace Athena.App.W7
{
	// Token: 0x02000029 RID: 41
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class frmLoadRecording : global::System.Windows.Forms.Form
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x0000B270 File Offset: 0x00009470
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

		// Token: 0x060001C8 RID: 456 RVA: 0x0000B2B0 File Offset: 0x000094B0
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Athena.App.W7.frmLoadRecording));
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.btnLoadFile = new global::System.Windows.Forms.Button();
			this.txtFileName = new global::System.Windows.Forms.TextBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.dlgOpenFile = new global::System.Windows.Forms.OpenFileDialog();
			base.SuspendLayout();
			this.btnCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(391, 66);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new global::System.Drawing.Point(310, 66);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnLoadFile.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnLoadFile.Location = new global::System.Drawing.Point(391, 10);
			this.btnLoadFile.Name = "btnLoadFile";
			this.btnLoadFile.Size = new global::System.Drawing.Size(75, 23);
			this.btnLoadFile.TabIndex = 3;
			this.btnLoadFile.Text = "Browse";
			this.btnLoadFile.UseVisualStyleBackColor = true;
			this.txtFileName.AccessibleDescription = "";
			this.txtFileName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtFileName.Location = new global::System.Drawing.Point(77, 12);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new global::System.Drawing.Size(308, 20);
			this.txtFileName.TabIndex = 2;
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(12, 15);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(59, 13);
			this.Label1.TabIndex = 6;
			this.Label1.Text = "Recording:";
			this.dlgOpenFile.Filter = "Compressed Zip File|*.zip";
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(484, 101);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnLoadFile);
			base.Controls.Add(this.txtFileName);
			base.Controls.Add(this.Label1);
			this.DoubleBuffered = true;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmLoadRecording";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Load Recording";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000D0 RID: 208
		private global::System.ComponentModel.IContainer components;
	}
}
