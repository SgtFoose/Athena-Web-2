namespace Athena.App.W7
{
	// Token: 0x0200002A RID: 42
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class frmLoad : global::System.Windows.Forms.Form
	{
		// Token: 0x060001D7 RID: 471 RVA: 0x0000B764 File Offset: 0x00009964
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

		// Token: 0x060001D8 RID: 472 RVA: 0x0000B7A4 File Offset: 0x000099A4
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Athena.App.W7.frmLoad));
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.btnOK = new global::System.Windows.Forms.Button();
			this.btnLoadFile = new global::System.Windows.Forms.Button();
			this.txtFileName = new global::System.Windows.Forms.TextBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.chkLoadGameGroupsUnits = new global::System.Windows.Forms.CheckBox();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.chkSaveInk = new global::System.Windows.Forms.CheckBox();
			this.dlgOpenFile = new global::System.Windows.Forms.OpenFileDialog();
			this.chkLoadGameMarkers = new global::System.Windows.Forms.CheckBox();
			base.SuspendLayout();
			this.btnCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new global::System.Drawing.Point(394, 134);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new global::System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 7;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new global::System.Drawing.Point(313, 134);
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
			this.chkLoadGameGroupsUnits.AutoSize = true;
			this.chkLoadGameGroupsUnits.Checked = true;
			this.chkLoadGameGroupsUnits.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.chkLoadGameGroupsUnits.Location = new global::System.Drawing.Point(106, 49);
			this.chkLoadGameGroupsUnits.Name = "chkLoadGameGroupsUnits";
			this.chkLoadGameGroupsUnits.Size = new global::System.Drawing.Size(174, 17);
			this.chkLoadGameGroupsUnits.TabIndex = 8;
			this.chkLoadGameGroupsUnits.Text = "Include Groups, Units, Vehicles";
			this.chkLoadGameGroupsUnits.UseVisualStyleBackColor = true;
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
			this.dlgOpenFile.Filter = "Athena Save|*.athena";
			this.chkLoadGameMarkers.AutoSize = true;
			this.chkLoadGameMarkers.Checked = true;
			this.chkLoadGameMarkers.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.chkLoadGameMarkers.Location = new global::System.Drawing.Point(106, 72);
			this.chkLoadGameMarkers.Name = "chkLoadGameMarkers";
			this.chkLoadGameMarkers.Size = new global::System.Drawing.Size(102, 17);
			this.chkLoadGameMarkers.TabIndex = 11;
			this.chkLoadGameMarkers.Text = "Include Markers";
			this.chkLoadGameMarkers.UseVisualStyleBackColor = true;
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.btnCancel;
			base.ClientSize = new global::System.Drawing.Size(484, 176);
			base.Controls.Add(this.chkLoadGameMarkers);
			base.Controls.Add(this.chkSaveInk);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.chkLoadGameGroupsUnits);
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
			base.Name = "frmLoad";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Load Frame";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000D7 RID: 215
		private global::System.ComponentModel.IContainer components;
	}
}
