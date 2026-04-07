using System;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Athena.Objects.v2;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000010 RID: 16
	public class ACSRoomControl
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00005370 File Offset: 0x00003570
		public ACSRoomControl()
		{
			this.RoomItem = null;
			this.GUID = Guid.Empty;
			this.PromptSeconds = 120;
			this.PromptTicks = 0;
			this.PromptTimer = null;
			this.RoomPanel = new StackPanel();
			this.Header = new TextBlock();
			this.ButtonGrid = new Grid();
			this.ButtonJoin = new Button();
			this.ButtonActive = new Button();
			this.ParticipantScroll = new ScrollViewer();
			this.ParticipantPanel = new StackPanel();
			this.PromptPanel = new StackPanel();
			this.PromptMessage = new TextBlock();
			this.PromptTimeRemaining = new TextBlock();
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060000BA RID: 186 RVA: 0x0000541C File Offset: 0x0000361C
		// (remove) Token: 0x060000BB RID: 187 RVA: 0x00005454 File Offset: 0x00003654
		public event ACSRoomControl.PromptTimerExpiredEventHandler PromptTimerExpired;

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005489 File Offset: 0x00003689
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00005491 File Offset: 0x00003691
		public RoomDictionaryItem RoomItem { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000549A File Offset: 0x0000369A
		// (set) Token: 0x060000BF RID: 191 RVA: 0x000054A2 File Offset: 0x000036A2
		public Guid GUID { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000054AB File Offset: 0x000036AB
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000054B3 File Offset: 0x000036B3
		public ACSRoomControl.CurrentMapDelegate GetCurrentMap { get; set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000054BC File Offset: 0x000036BC
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000054C4 File Offset: 0x000036C4
		public int PromptSeconds { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000054CD File Offset: 0x000036CD
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x000054D5 File Offset: 0x000036D5
		public int PromptTicks { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x000054DE File Offset: 0x000036DE
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x000054E8 File Offset: 0x000036E8
		public virtual Timer PromptTimer
		{
			[CompilerGenerated]
			get
			{
				return this._PromptTimer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ElapsedEventHandler value2 = new ElapsedEventHandler(this.HandlePromptTimerElapsed);
				Timer promptTimer = this._PromptTimer;
				if (promptTimer != null)
				{
					promptTimer.Elapsed -= value2;
				}
				this._PromptTimer = value;
				promptTimer = this._PromptTimer;
				if (promptTimer != null)
				{
					promptTimer.Elapsed += value2;
				}
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000552B File Offset: 0x0000372B
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00005533 File Offset: 0x00003733
		public StackPanel RoomPanel { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000553C File Offset: 0x0000373C
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00005544 File Offset: 0x00003744
		public TextBlock Header { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000554D File Offset: 0x0000374D
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00005555 File Offset: 0x00003755
		public Grid ButtonGrid { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000555E File Offset: 0x0000375E
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00005566 File Offset: 0x00003766
		public Button ButtonJoin { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000556F File Offset: 0x0000376F
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00005577 File Offset: 0x00003777
		public Button ButtonActive { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00005580 File Offset: 0x00003780
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00005588 File Offset: 0x00003788
		public ScrollViewer ParticipantScroll { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005591 File Offset: 0x00003791
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00005599 File Offset: 0x00003799
		public StackPanel ParticipantPanel { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x000055A2 File Offset: 0x000037A2
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x000055AA File Offset: 0x000037AA
		public StackPanel PromptPanel { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000055B3 File Offset: 0x000037B3
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000055BB File Offset: 0x000037BB
		public TextBlock PromptMessage { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000055C4 File Offset: 0x000037C4
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000055CC File Offset: 0x000037CC
		public TextBlock PromptTimeRemaining { get; set; }

		// Token: 0x060000DC RID: 220 RVA: 0x000055D8 File Offset: 0x000037D8
		public void CreateControls(ref Guid SessionGUID, ref RoomDictionaryItem Room, ref ACSRoomControl.CurrentMapDelegate CurrentMap)
		{
			this.RoomItem = Room;
			this.GUID = SessionGUID;
			this.GetCurrentMap = CurrentMap;
			this.RoomPanel.Margin = new Thickness(0.0, 0.0, 0.0, 10.0);
			this.RoomPanel.Children.Add(this.Header);
			this.Header.FontWeight = FontWeights.Bold;
			this.Header.Width = double.NaN;
			this.Header.Background = new SolidColorBrush(Colors.SlateGray);
			this.Header.Padding = new Thickness(5.0);
			this.Header.TextAlignment = TextAlignment.Center;
			this.RoomPanel.Children.Add(this.ButtonGrid);
			this.ButtonGrid.Height = 25.0;
			this.ButtonGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
			this.ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(50.0, GridUnitType.Star)
			});
			this.ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition
			{
				Width = new GridLength(50.0, GridUnitType.Star)
			});
			this.ButtonGrid.Children.Add(this.ButtonJoin);
			this.ButtonJoin.Margin = new Thickness(2.0);
			this.ButtonJoin.Background = new SolidColorBrush(Colors.LightGray);
			this.ButtonJoin.Content = new TextBlock
			{
				Text = "MONITOR",
				FontWeight = FontWeights.Bold,
				Foreground = new SolidColorBrush(Colors.SlateGray)
			};
			this.ButtonJoin.Tag = this.RoomItem.Room.RoomGUID;
			this.ButtonGrid.Children.Add(this.ButtonActive);
			Grid.SetColumn(this.ButtonActive, 1);
			this.ButtonActive.Margin = new Thickness(2.0);
			this.ButtonActive.Background = new SolidColorBrush(Colors.LightGray);
			this.ButtonActive.Content = new TextBlock
			{
				Text = "ACTIVE",
				FontWeight = FontWeights.Bold,
				Foreground = new SolidColorBrush(Colors.SlateGray)
			};
			this.ButtonActive.Tag = this.RoomItem.Room.RoomGUID;
			this.RoomPanel.Children.Add(this.ParticipantScroll);
			this.ParticipantScroll.MaxHeight = 50.0;
			this.ParticipantScroll.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
			this.ParticipantScroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
			this.ParticipantScroll.Content = this.ParticipantPanel;
			this.ParticipantPanel.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
			this.RoomPanel.Children.Add(this.PromptPanel);
			this.PromptPanel.Margin = new Thickness(5.0, 0.0, 5.0, 0.0);
			this.PromptPanel.Visibility = Visibility.Collapsed;
			LinearGradientBrush background = new LinearGradientBrush(Colors.White, Colors.LightBlue, 90.0);
			this.PromptPanel.Background = background;
			this.PromptPanel.Children.Add(this.PromptMessage);
			this.PromptMessage.TextWrapping = TextWrapping.Wrap;
			this.PromptPanel.Children.Add(this.PromptTimeRemaining);
			this.PromptTimeRemaining.HorizontalAlignment = HorizontalAlignment.Right;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000059C4 File Offset: 0x00003BC4
		public void SetPrompt(string Message, int Seconds)
		{
			try
			{
				this.PromptMessage.Dispatcher.Invoke(delegate()
				{
					this.PromptMessage.Text = Message;
					this.PromptTimeRemaining.Text = Seconds.ToString() + "s";
				});
				this.PromptTicks = 0;
				if (this.PromptTimer != null)
				{
					this.PromptTimer.Enabled = false;
					this.PromptTimer.Stop();
					this.PromptTimer = null;
				}
				if (this.PromptTimer == null)
				{
					this.PromptTimer = new Timer();
					this.PromptTimer.Interval = 1000.0;
					this.PromptTimer.AutoReset = true;
					this.PromptTimer.Enabled = true;
					this.PromptTimer.Start();
				}
				this.PromptPanel.Dispatcher.Invoke(delegate()
				{
					this.PromptPanel.Visibility = Visibility.Visible;
				});
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005ABC File Offset: 0x00003CBC
		public void HandlePromptTimerElapsed(object sender, ElapsedEventArgs e)
		{
			checked
			{
				try
				{
					bool flag = false;
					if (this.RoomItem != null)
					{
						Map map = this.GetCurrentMap();
						if (map != null && this.RoomItem.Room.WorldName.Equals(map.WorldName, StringComparison.InvariantCultureIgnoreCase))
						{
							flag = true;
						}
						if (!this.RoomItem.ParticipantDictionary.ContainsKey(this.GUID))
						{
							flag = true;
						}
					}
					if (flag)
					{
						this.PromptTimer.Enabled = false;
						this.PromptTimer.Stop();
						this.PromptPanel.Dispatcher.Invoke(delegate()
						{
							this.PromptPanel.Visibility = Visibility.Collapsed;
						});
					}
					else
					{
						this.PromptTicks++;
						if (this.PromptTicks >= this.PromptSeconds)
						{
							this.PromptTimeRemaining.Dispatcher.Invoke(delegate()
							{
								this.PromptPanel.Visibility = Visibility.Collapsed;
								this.PromptTimeRemaining.Text = "0s";
							});
							this.PromptTimer.Enabled = false;
							this.PromptTimer.Stop();
							ACSRoomControl.PromptTimerExpiredEventHandler promptTimerExpiredEvent = this.PromptTimerExpiredEvent;
							if (promptTimerExpiredEvent != null)
							{
								promptTimerExpiredEvent(this.RoomItem.Room.RoomGUID);
							}
						}
						else
						{
							this.PromptTimeRemaining.Dispatcher.Invoke(delegate()
							{
								this.PromptTimeRemaining.Text = Conversions.ToString(this.PromptSeconds - this.PromptTicks) + "s";
							});
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		// Token: 0x02000061 RID: 97
		// (Invoke) Token: 0x0600073F RID: 1855
		public delegate Map CurrentMapDelegate();

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x06000743 RID: 1859
		public delegate void PromptTimerExpiredEventHandler(Guid RoomGUID);
	}
}
