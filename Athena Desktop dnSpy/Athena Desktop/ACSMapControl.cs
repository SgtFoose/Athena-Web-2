using System;
using System.Windows;
using System.Windows.Controls;

namespace Athena.App.W7
{
	// Token: 0x0200000F RID: 15
	public class ACSMapControl
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00005110 File Offset: 0x00003310
		public ACSMapControl()
		{
			this.MapName = string.Empty;
			this.MapWorld = string.Empty;
			this.MapPanel = new StackPanel();
			this.MapGrid = new Grid();
			this.MapLabel = new TextBlock();
			this.MapButton = new Button();
			this.StatusLabel = new TextBlock();
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00005170 File Offset: 0x00003370
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00005178 File Offset: 0x00003378
		public string MapName { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00005181 File Offset: 0x00003381
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00005189 File Offset: 0x00003389
		public string MapWorld { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00005192 File Offset: 0x00003392
		// (set) Token: 0x060000AF RID: 175 RVA: 0x0000519A File Offset: 0x0000339A
		public StackPanel MapPanel { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000051A3 File Offset: 0x000033A3
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000051AB File Offset: 0x000033AB
		public Grid MapGrid { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000051B4 File Offset: 0x000033B4
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000051BC File Offset: 0x000033BC
		public TextBlock MapLabel { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000051C5 File Offset: 0x000033C5
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000051CD File Offset: 0x000033CD
		public Button MapButton { get; set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000051D6 File Offset: 0x000033D6
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000051DE File Offset: 0x000033DE
		public TextBlock StatusLabel { get; set; }

		// Token: 0x060000B8 RID: 184 RVA: 0x000051E8 File Offset: 0x000033E8
		public void CreateControl(string MapWorld, string MapName)
		{
			this.MapWorld = MapWorld;
			this.MapName = MapName;
			this.MapPanel.Children.Add(this.MapGrid);
			this.MapGrid.Height = 24.0;
			this.MapGrid.Children.Add(this.MapLabel);
			this.MapLabel.Text = MapName;
			this.MapLabel.VerticalAlignment = VerticalAlignment.Center;
			this.MapGrid.Children.Add(this.MapButton);
			this.MapButton.Content = "GET";
			this.MapButton.HorizontalAlignment = HorizontalAlignment.Right;
			this.MapButton.Margin = new Thickness(2.0, 2.0, 5.0, 2.0);
			this.MapButton.Tag = MapWorld;
			this.MapButton.Width = 50.0;
			this.MapPanel.Children.Add(this.StatusLabel);
			this.StatusLabel.FontSize = 10.0;
			this.StatusLabel.FontStyle = FontStyles.Italic;
			this.StatusLabel.Margin = new Thickness(2.0, 0.0, 0.0, 0.0);
			this.StatusLabel.Text = string.Empty;
			this.StatusLabel.VerticalAlignment = VerticalAlignment.Center;
		}
	}
}
