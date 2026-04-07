namespace Athena.App.W7
{
	// Token: 0x0200002B RID: 43
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class frmSave : global::System.Windows.Forms.Form
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0000BF08 File Offset: 0x0000A108
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

		// Token: 0x060001F0 RID: 496 RVA: 0x0000BF48 File Offset: 0x0000A148
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Athena.App.W7.frmSave));
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.btnSaveFile = new global::System.Windows.Forms.Button();
			this.txtFileName = new global::System.Windows.Forms.TextBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.dlgDestFile = new global::System.Windows.Forms.SaveFileDialog();
			this.chkSaveGroupsUnits = new global::System.Windows.Forms.CheckBox();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.chkSaveInk = new global::System.Windows.Forms.CheckBox();
			this.chkIncludeMarkers = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.btnCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(394, 125);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new global::System.Drawing.Point(313, 125);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnSaveFile.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnSaveFile.Location = new global::System.Drawing.Point(391, 10);
			this.btnSaveFile.Name = "btnSaveFile";
			this.btnSaveFile.Size = new global::System.Drawing.Size(75, 23);
			this.btnSaveFile.TabIndex = 3;
			this.btnSaveFile.Text = "Browse";
			this.btnSaveFile.UseVisualStyleBackColor = true;
			this.txtFileName.AccessibleDescription = "";
			this.txtFileName.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.txtFileName.Location = new global::System.Drawing.Point(106, 12);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new global::System.Drawing.Size(279, 20);
			this.txtFileName.TabIndex = 2;
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(12, 15);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(52, 13);
			this.Label1.TabIndex = 6;
			this.Label1.Text = "Filename:";
			this.dlgDestFile.Filter = "Athena Save|*.athena";
			this.dlgDestFile.Title = "Specify the filename...";
			this.chkSaveGroupsUnits.AutoSize = true;
			this.chkSaveGroupsUnits.Checked = true;
			this.chkSaveGroupsUnits.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.chkSaveGroupsUnits.Location = new global::System.Drawing.Point(106, 49);
			this.chkSaveGroupsUnits.Name = "chkSaveGroupsUnits";
			this.chkSaveGroupsUnits.Size = new global::System.Drawing.Size(192, 17);
			this.chkSaveGroupsUnits.TabIndex = 8;
			this.chkSaveGroupsUnits.Text = "Include Groups, Units and Vehicles";
			this.chkSaveGroupsUnits.UseVisualStyleBackColor = true;
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(12, 49);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(46, 13);
			this.Label2.TabIndex = 9;
			this.Label2.Text = "Options:";
			this.chkSaveInk.AutoSize = true;
			this.chkSaveInk.Checked = true;
			this.chkSaveInk.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.chkSaveInk.Location = new global::System.Drawing.Point(106, 95);
			this.chkSaveInk.Name = "chkSaveInk";
			this.chkSaveInk.Size = new global::System.Drawing.Size(79, 17);
			this.chkSaveInk.TabIndex = 10;
			this.chkSaveInk.Text = "Include Ink";
			this.chkSaveInk.UseVisualStyleBackColor = true;
			this.chkIncludeMarkers.AutoSize = true;
			this.chkIncludeMarkers.Checked = true;
			this.chkIncludeMarkers.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.chkIncludeMarkers.Location = new global::System.Drawing.Point(106, 72);
			this.chkIncludeMarkers.Name = "chkIncludeMarkers";
			this.chkIncludeMarkers.Size = new global::System.Drawing.Size(102, 17);
			this.chkIncludeMarkers.TabIndex = 12;
			this.chkIncludeMarkers.Text = "Include Markers";
			this.chkIncludeMarkers.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(484, 167);
			base.Controls.Add(this.chkIncludeMarkers);
			base.Controls.Add(this.chkSaveInk);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.chkSaveGroupsUnits);
			base.Controls.Add(this.btnCancel);
			base.Controls.Add(this.btnOK);
			base.Controls.Add(this.btnSaveFile);
			base.Controls.Add(this.txtFileName);
			base.Controls.Add(this.Label1);
			this.DoubleBuffered = true;
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmSave";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Save Frame";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000E2 RID: 226
		private global::System.ComponentModel.IContainer components;
	}
}
