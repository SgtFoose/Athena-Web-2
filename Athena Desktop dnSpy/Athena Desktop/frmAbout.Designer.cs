namespace Athena.App.W7
{
	// Token: 0x02000031 RID: 49
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class frmAbout : global::System.Windows.Forms.Form
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00010B44 File Offset: 0x0000ED44
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

		// Token: 0x060002E4 RID: 740 RVA: 0x00010B84 File Offset: 0x0000ED84
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Athena.App.W7.frmAbout));
			this.dlgPickupDir = new global::System.Windows.Forms.FolderBrowserDialog();
			this.tipPickupDir = new global::System.Windows.Forms.ToolTip(this.components);
			this.btnOK = new global::System.Windows.Forms.Button();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.HeaderImage = new global::System.Windows.Forms.PictureBox();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.Panel1 = new global::System.Windows.Forms.Panel();
			this.Label8 = new global::System.Windows.Forms.Label();
			this.Label6 = new global::System.Windows.Forms.Label();
			this.Label9 = new global::System.Windows.Forms.Label();
			this.Label7 = new global::System.Windows.Forms.Label();
			this.Label5 = new global::System.Windows.Forms.Label();
			this.Label4 = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.HeaderImage).BeginInit();
			this.Panel1.SuspendLayout();
			base.SuspendLayout();
			this.dlgPickupDir.Description = "Specify the folder where the @Athena mod places its files...";
			this.dlgPickupDir.RootFolder = global::System.Environment.SpecialFolder.MyComputer;
			this.btnOK.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new global::System.Drawing.Point(397, 526);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new global::System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(12, 169);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(201, 13);
			this.Label1.TabIndex = 4;
			this.Label1.Text = "Athena Desktop Version 1.0.0.26+ BETA";
			this.HeaderImage.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.HeaderImage.Image = global::Athena.App.W7.My.Resources.Resources.Welcome_600_200;
			this.HeaderImage.ImageLocation = "";
			this.HeaderImage.Location = new global::System.Drawing.Point(13, 13);
			this.HeaderImage.Name = "HeaderImage";
			this.HeaderImage.Size = new global::System.Drawing.Size(459, 153);
			this.HeaderImage.SizeMode = global::System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.HeaderImage.TabIndex = 5;
			this.HeaderImage.TabStop = false;
			this.Label2.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(58, 501);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(354, 13);
			this.Label2.TabIndex = 6;
			this.Label2.Text = "For News, Updates and Support, please visit http://www.athenamod.com";
			this.Label2.TextAlign = global::System.Drawing.ContentAlignment.TopCenter;
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(12, 182);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(142, 13);
			this.Label3.TabIndex = 7;
			this.Label3.Text = "Created By: Sean Kruis (bus)";
			this.Panel1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.Panel1.AutoScroll = true;
			this.Panel1.BackColor = global::System.Drawing.Color.White;
			this.Panel1.Controls.Add(this.Label8);
			this.Panel1.Controls.Add(this.Label6);
			this.Panel1.Controls.Add(this.Label9);
			this.Panel1.Controls.Add(this.Label7);
			this.Panel1.Controls.Add(this.Label5);
			this.Panel1.Controls.Add(this.Label4);
			this.Panel1.Location = new global::System.Drawing.Point(15, 199);
			this.Panel1.Name = "Panel1";
			this.Panel1.Padding = new global::System.Windows.Forms.Padding(5);
			this.Panel1.Size = new global::System.Drawing.Size(457, 299);
			this.Panel1.TabIndex = 8;
			this.Label8.AutoSize = true;
			this.Label8.Location = new global::System.Drawing.Point(9, 233);
			this.Label8.Name = "Label8";
			this.Label8.Size = new global::System.Drawing.Size(423, 91);
			this.Label8.TabIndex = 5;
			this.Label8.Text = componentResourceManager.GetString("Label8.Text");
			this.Label6.AutoSize = true;
			this.Label6.Location = new global::System.Drawing.Point(9, 81);
			this.Label6.Name = "Label6";
			this.Label6.Size = new global::System.Drawing.Size(430, 130);
			this.Label6.TabIndex = 3;
			this.Label6.Text = componentResourceManager.GetString("Label6.Text");
			this.Label9.AutoSize = true;
			this.Label9.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label9.Location = new global::System.Drawing.Point(9, 220);
			this.Label9.Name = "Label9";
			this.Label9.Size = new global::System.Drawing.Size(180, 13);
			this.Label9.TabIndex = 4;
			this.Label9.Text = "What does Athena do for you?";
			this.Label7.AutoSize = true;
			this.Label7.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label7.Location = new global::System.Drawing.Point(9, 68);
			this.Label7.Name = "Label7";
			this.Label7.Size = new global::System.Drawing.Size(112, 13);
			this.Label7.TabIndex = 2;
			this.Label7.Text = "How does it work?";
			this.Label5.AutoSize = true;
			this.Label5.Location = new global::System.Drawing.Point(9, 22);
			this.Label5.Name = "Label5";
			this.Label5.Size = new global::System.Drawing.Size(409, 39);
			this.Label5.TabIndex = 1;
			this.Label5.Text = componentResourceManager.GetString("Label5.Text");
			this.Label4.AutoSize = true;
			this.Label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label4.Location = new global::System.Drawing.Point(9, 9);
			this.Label4.Name = "Label4";
			this.Label4.Size = new global::System.Drawing.Size(101, 13);
			this.Label4.TabIndex = 0;
			this.Label4.Text = "What is Athena?";
			base.AcceptButton = this.btnOK;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(484, 561);
			base.Controls.Add(this.Panel1);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.HeaderImage);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.btnOK);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "frmAbout";
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			((global::System.ComponentModel.ISupportInitialize)this.HeaderImage).EndInit();
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000144 RID: 324
		private global::System.ComponentModel.IContainer components;
	}
}
