using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Athena.App.W7
{
	// Token: 0x02000011 RID: 17
	public class ACSRoomLayerControl
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00005C73 File Offset: 0x00003E73
		public ACSRoomLayerControl()
		{
			this.LayerPanel = new StackPanel();
			this.LayerCheckbox = new CheckBox();
			this.LayerButton = new Button();
			this.LayerTextblock = new TextBlock();
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005CA7 File Offset: 0x00003EA7
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005CAF File Offset: 0x00003EAF
		public InkCanvas LayerCanvas { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005CB8 File Offset: 0x00003EB8
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005CC0 File Offset: 0x00003EC0
		public StackPanel LayerPanel { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005CC9 File Offset: 0x00003EC9
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005CD1 File Offset: 0x00003ED1
		public CheckBox LayerCheckbox { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005CDA File Offset: 0x00003EDA
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005CE2 File Offset: 0x00003EE2
		public Button LayerButton { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005CEB File Offset: 0x00003EEB
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00005CF3 File Offset: 0x00003EF3
		public TextBlock LayerTextblock { get; set; }

		// Token: 0x060000EE RID: 238 RVA: 0x00005CFC File Offset: 0x00003EFC
		public void CreateControl(ref Guid RoomGUID, string LayerName)
		{
			this.LayerCanvas = new InkCanvas();
			this.LayerCanvas.Tag = RoomGUID.ToString();
			this.LayerCanvas.IsEnabled = false;
			this.LayerCanvas.IsHitTestVisible = false;
			this.LayerCanvas.Background = new SolidColorBrush(Colors.Transparent);
			this.LayerPanel.Orientation = Orientation.Horizontal;
			this.LayerPanel.Tag = RoomGUID.ToString();
			this.LayerPanel.Children.Add(this.LayerCheckbox);
			this.LayerCheckbox.Tag = RoomGUID.ToString();
			this.LayerCheckbox.IsChecked = new bool?(true);
			this.LayerPanel.Children.Add(this.LayerButton);
			this.LayerButton.Tag = RoomGUID.ToString();
			this.LayerButton.Margin = new Thickness(0.0);
			this.LayerButton.Padding = new Thickness(0.0);
			this.LayerButton.BorderThickness = new Thickness(0.0);
			this.LayerButton.Background = null;
			this.LayerButton.Content = this.LayerTextblock;
			this.LayerButton.Tag = RoomGUID.ToString();
			this.LayerTextblock.Text = LayerName;
			this.LayerTextblock.VerticalAlignment = VerticalAlignment.Center;
			this.LayerTextblock.Foreground = new SolidColorBrush(Colors.Black);
			this.LayerTextblock.FontSize = 12.0;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005EA8 File Offset: 0x000040A8
		public void Update(bool IsActive)
		{
			if (IsActive)
			{
				this.LayerPanel.Background = new SolidColorBrush(Colors.SlateGray);
				this.LayerTextblock.Foreground = new SolidColorBrush(Colors.White);
				this.LayerTextblock.FontSize = 14.0;
				this.LayerTextblock.FontWeight = FontWeights.SemiBold;
				return;
			}
			this.LayerPanel.Background = new SolidColorBrush(Colors.Transparent);
			this.LayerTextblock.Foreground = new SolidColorBrush(Colors.Black);
			this.LayerTextblock.FontSize = 12.0;
			this.LayerTextblock.FontWeight = FontWeights.Normal;
		}
	}
}
