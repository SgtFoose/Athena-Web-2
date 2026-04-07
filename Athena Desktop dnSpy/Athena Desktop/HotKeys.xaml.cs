using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x0200002D RID: 45
	[DesignerGenerated]
	public partial class HotKeys : Window
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000D163 File Offset: 0x0000B363
		public HotKeys()
		{
			this.AllowClose = false;
			this.AthenaHotKeys = new List<AthenaHotKey>();
			this.InitializeComponent();
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000233 RID: 563 RVA: 0x0000D184 File Offset: 0x0000B384
		// (remove) Token: 0x06000234 RID: 564 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		public event HotKeys.HotkeyAdditionRequestedEventHandler HotkeyAdditionRequested;

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000235 RID: 565 RVA: 0x0000D1F4 File Offset: 0x0000B3F4
		// (remove) Token: 0x06000236 RID: 566 RVA: 0x0000D22C File Offset: 0x0000B42C
		public event HotKeys.HotkeyRemovalRequestedEventHandler HotkeyRemovalRequested;

		// Token: 0x06000237 RID: 567 RVA: 0x0000D264 File Offset: 0x0000B464
		public void LoadHotKeys()
		{
			if (this.AthenaHotKeys != null && this.AthenaHotKeys.Count != 0)
			{
				try
				{
					foreach (AthenaHotKey athenaHotKey in this.AthenaHotKeys)
					{
						string action = athenaHotKey.Action;
						uint num = <PrivateImplementationDetails>.ComputeStringHash(action);
						if (num <= 2067622972U)
						{
							if (num <= 710316662U)
							{
								if (num <= 283578168U)
								{
									if (num != 85755406U)
									{
										if (num == 283578168U)
										{
											if (Operators.CompareString(action, "pandown", false) == 0)
											{
												this.pandown.Text = athenaHotKey.HotKeyString;
											}
										}
									}
									else if (Operators.CompareString(action, "layergroups", false) == 0)
									{
										this.layergroups.Text = athenaHotKey.HotKeyString;
									}
								}
								else if (num != 429101034U)
								{
									if (num == 710316662U)
									{
										if (Operators.CompareString(action, "layerink", false) == 0)
										{
											this.layerink.Text = athenaHotKey.HotKeyString;
										}
									}
								}
								else if (Operators.CompareString(action, "layerlocations", false) == 0)
								{
									this.layerlocations.Text = athenaHotKey.HotKeyString;
								}
							}
							else if (num <= 1697318111U)
							{
								if (num != 1191862662U)
								{
									if (num == 1697318111U)
									{
										if (Operators.CompareString(action, "start", false) == 0)
										{
											this.start.Text = athenaHotKey.HotKeyString;
										}
									}
								}
								else if (Operators.CompareString(action, "zoomout", false) == 0)
								{
									this.zoomout.Text = athenaHotKey.HotKeyString;
								}
							}
							else if (num != 1909040957U)
							{
								if (num == 2067622972U)
								{
									if (Operators.CompareString(action, "track", false) == 0)
									{
										this.track.Text = athenaHotKey.HotKeyString;
									}
								}
							}
							else if (Operators.CompareString(action, "layermarkers", false) == 0)
							{
								this.layermarkers.Text = athenaHotKey.HotKeyString;
							}
						}
						else if (num <= 3777329969U)
						{
							if (num <= 3472975342U)
							{
								if (num != 2281451638U)
								{
									if (num == 3472975342U)
									{
										if (Operators.CompareString(action, "layergrid", false) == 0)
										{
											this.layergrid.Text = athenaHotKey.HotKeyString;
										}
									}
								}
								else if (Operators.CompareString(action, "panright", false) == 0)
								{
									this.panright.Text = athenaHotKey.HotKeyString;
								}
							}
							else if (num != 3652464425U)
							{
								if (num == 3777329969U)
								{
									if (Operators.CompareString(action, "panup", false) == 0)
									{
										this.panup.Text = athenaHotKey.HotKeyString;
									}
								}
							}
							else if (Operators.CompareString(action, "layerunits", false) == 0)
							{
								this.layerunits.Text = athenaHotKey.HotKeyString;
							}
						}
						else if (num <= 3899525117U)
						{
							if (num != 3889436925U)
							{
								if (num == 3899525117U)
								{
									if (Operators.CompareString(action, "panleft", false) == 0)
									{
										this.panleft.Text = athenaHotKey.HotKeyString;
									}
								}
							}
							else if (Operators.CompareString(action, "zoomin", false) == 0)
							{
								this.zoomin.Text = athenaHotKey.HotKeyString;
							}
						}
						else if (num != 3952099778U)
						{
							if (num != 4149321280U)
							{
								if (num == 4204126854U)
								{
									if (Operators.CompareString(action, "unitnext", false) == 0)
									{
										this.unitnext.Text = athenaHotKey.HotKeyString;
									}
								}
							}
							else if (Operators.CompareString(action, "recordstart", false) == 0)
							{
								this.recordstart.Text = athenaHotKey.HotKeyString;
							}
						}
						else if (Operators.CompareString(action, "unitprev", false) == 0)
						{
							this.unitprev.Text = athenaHotKey.HotKeyString;
						}
					}
				}
				finally
				{
					List<AthenaHotKey>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D6D8 File Offset: 0x0000B8D8
		public void ClearConflict(string Action)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(Action);
			if (num <= 2067622972U)
			{
				if (num <= 710316662U)
				{
					if (num <= 283578168U)
					{
						if (num != 85755406U)
						{
							if (num != 283578168U)
							{
								return;
							}
							if (Operators.CompareString(Action, "pandown", false) != 0)
							{
								return;
							}
							this.pandown.Text = string.Empty;
							return;
						}
						else
						{
							if (Operators.CompareString(Action, "layergroups", false) != 0)
							{
								return;
							}
							this.layergroups.Text = string.Empty;
							return;
						}
					}
					else if (num != 429101034U)
					{
						if (num != 710316662U)
						{
							return;
						}
						if (Operators.CompareString(Action, "layerink", false) != 0)
						{
							return;
						}
						this.layerink.Text = string.Empty;
						return;
					}
					else
					{
						if (Operators.CompareString(Action, "layerlocations", false) != 0)
						{
							return;
						}
						this.layerlocations.Text = string.Empty;
						return;
					}
				}
				else if (num <= 1697318111U)
				{
					if (num != 1191862662U)
					{
						if (num != 1697318111U)
						{
							return;
						}
						if (Operators.CompareString(Action, "start", false) != 0)
						{
							return;
						}
						this.start.Text = string.Empty;
						return;
					}
					else
					{
						if (Operators.CompareString(Action, "zoomout", false) != 0)
						{
							return;
						}
						this.zoomout.Text = string.Empty;
						return;
					}
				}
				else if (num != 1909040957U)
				{
					if (num != 2067622972U)
					{
						return;
					}
					if (Operators.CompareString(Action, "track", false) != 0)
					{
						return;
					}
					this.track.Text = string.Empty;
					return;
				}
				else
				{
					if (Operators.CompareString(Action, "layermarkers", false) != 0)
					{
						return;
					}
					this.layermarkers.Text = string.Empty;
					return;
				}
			}
			else if (num <= 3777329969U)
			{
				if (num <= 3472975342U)
				{
					if (num != 2281451638U)
					{
						if (num != 3472975342U)
						{
							return;
						}
						if (Operators.CompareString(Action, "layergrid", false) != 0)
						{
							return;
						}
						this.layergrid.Text = string.Empty;
						return;
					}
					else
					{
						if (Operators.CompareString(Action, "panright", false) != 0)
						{
							return;
						}
						this.panright.Text = string.Empty;
						return;
					}
				}
				else if (num != 3652464425U)
				{
					if (num != 3777329969U)
					{
						return;
					}
					if (Operators.CompareString(Action, "panup", false) != 0)
					{
						return;
					}
					this.panup.Text = string.Empty;
					return;
				}
				else
				{
					if (Operators.CompareString(Action, "layerunits", false) != 0)
					{
						return;
					}
					this.layerunits.Text = string.Empty;
					return;
				}
			}
			else if (num <= 3899525117U)
			{
				if (num != 3889436925U)
				{
					if (num != 3899525117U)
					{
						return;
					}
					if (Operators.CompareString(Action, "panleft", false) != 0)
					{
						return;
					}
					this.panleft.Text = string.Empty;
					return;
				}
				else
				{
					if (Operators.CompareString(Action, "zoomin", false) != 0)
					{
						return;
					}
					this.zoomin.Text = string.Empty;
					return;
				}
			}
			else if (num != 3952099778U)
			{
				if (num != 4149321280U)
				{
					if (num != 4204126854U)
					{
						return;
					}
					if (Operators.CompareString(Action, "unitnext", false) != 0)
					{
						return;
					}
					this.unitnext.Text = string.Empty;
					return;
				}
				else
				{
					if (Operators.CompareString(Action, "recordstart", false) != 0)
					{
						return;
					}
					this.recordstart.Text = string.Empty;
					return;
				}
			}
			else
			{
				if (Operators.CompareString(Action, "unitprev", false) != 0)
				{
					return;
				}
				this.unitprev.Text = string.Empty;
				return;
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000DA30 File Offset: 0x0000BC30
		private void TextKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			try
			{
				if (!(e.Key == Key.LeftShift | e.Key == Key.RightShift | e.Key == Key.LeftCtrl | e.Key == Key.RightCtrl | e.Key == Key.LeftAlt | e.Key == Key.RightAlt | e.Key == Key.System))
				{
					if (sender != null)
					{
						System.Windows.Controls.TextBox textBox = sender as System.Windows.Controls.TextBox;
						if (textBox != null)
						{
							if (e.Key == Key.Escape)
							{
								HotKeys.HotkeyRemovalRequestedEventHandler hotkeyRemovalRequestedEvent = this.HotkeyRemovalRequestedEvent;
								if (hotkeyRemovalRequestedEvent != null)
								{
									hotkeyRemovalRequestedEvent(Conversions.ToString(textBox.Tag));
								}
								textBox.Text = string.Empty;
								return;
							}
							bool shift = false;
							bool control = false;
							bool altMenu = false;
							List<string> list = new List<string>();
							if ((Keyboard.Modifiers & ModifierKeys.Shift) > ModifierKeys.None)
							{
								list.Add("SHIFT");
								shift = true;
							}
							if ((Keyboard.Modifiers & ModifierKeys.Control) > ModifierKeys.None)
							{
								list.Add("CONTROL");
								control = true;
							}
							if ((Keyboard.Modifiers & ModifierKeys.Alt) > ModifierKeys.None)
							{
								list.Add("ALT/MENU");
								altMenu = true;
							}
							string text = string.Empty;
							if (list.Count == 0)
							{
								text = Enum.GetName(typeof(Key), e.Key);
							}
							else
							{
								text = Strings.Join(list.ToArray(), " + ") + " + " + Enum.GetName(typeof(Key), e.Key);
							}
							textBox.Text = text;
							Keys keyPressed = (Keys)KeyInterop.VirtualKeyFromKey(e.Key);
							this.RaiseKeyDown(Conversions.ToString(textBox.Tag), text, keyPressed, shift, control, altMenu);
						}
					}
					e.Handled = true;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000DC04 File Offset: 0x0000BE04
		private void RaiseKeyDown(string Action, string KeyString, Keys KeyPressed, bool Shift, bool Control, bool AltMenu)
		{
			HotKeys.HotkeyAdditionRequestedEventHandler hotkeyAdditionRequestedEvent = this.HotkeyAdditionRequestedEvent;
			if (hotkeyAdditionRequestedEvent != null)
			{
				hotkeyAdditionRequestedEvent(Action, KeyString, KeyPressed, Shift, Control, AltMenu);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000DC2A File Offset: 0x0000BE2A
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			base.Hide();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000DC32 File Offset: 0x0000BE32
		private void Window_Closing(object sender, CancelEventArgs e)
		{
			if (!this.AllowClose)
			{
				e.Cancel = true;
				base.Hide();
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000DC49 File Offset: 0x0000BE49
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000DC51 File Offset: 0x0000BE51
		internal virtual System.Windows.Controls.TextBox start { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000DC5A File Offset: 0x0000BE5A
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000DC62 File Offset: 0x0000BE62
		internal virtual System.Windows.Controls.TextBox recordstart { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000DC6B File Offset: 0x0000BE6B
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000DC73 File Offset: 0x0000BE73
		internal virtual System.Windows.Controls.TextBox track { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000DC84 File Offset: 0x0000BE84
		internal virtual System.Windows.Controls.TextBox unitnext { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000DC8D File Offset: 0x0000BE8D
		// (set) Token: 0x06000246 RID: 582 RVA: 0x0000DC95 File Offset: 0x0000BE95
		internal virtual System.Windows.Controls.TextBox unitprev { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000DC9E File Offset: 0x0000BE9E
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000DCA6 File Offset: 0x0000BEA6
		internal virtual System.Windows.Controls.TextBox zoomin { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000DCAF File Offset: 0x0000BEAF
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000DCB7 File Offset: 0x0000BEB7
		internal virtual System.Windows.Controls.TextBox zoomout { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		// (set) Token: 0x0600024C RID: 588 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		internal virtual System.Windows.Controls.TextBox panleft { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000DCD1 File Offset: 0x0000BED1
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000DCD9 File Offset: 0x0000BED9
		internal virtual System.Windows.Controls.TextBox panright { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000DCE2 File Offset: 0x0000BEE2
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000DCEA File Offset: 0x0000BEEA
		internal virtual System.Windows.Controls.TextBox panup { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000DCF3 File Offset: 0x0000BEF3
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000DCFB File Offset: 0x0000BEFB
		internal virtual System.Windows.Controls.TextBox pandown { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000DD04 File Offset: 0x0000BF04
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000DD0C File Offset: 0x0000BF0C
		internal virtual System.Windows.Controls.TextBox layerlocations { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000DD15 File Offset: 0x0000BF15
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000DD1D File Offset: 0x0000BF1D
		internal virtual System.Windows.Controls.TextBox layergrid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000DD26 File Offset: 0x0000BF26
		// (set) Token: 0x06000258 RID: 600 RVA: 0x0000DD2E File Offset: 0x0000BF2E
		internal virtual System.Windows.Controls.TextBox layerink { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000DD37 File Offset: 0x0000BF37
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0000DD3F File Offset: 0x0000BF3F
		internal virtual System.Windows.Controls.TextBox layermarkers { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000DD48 File Offset: 0x0000BF48
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000DD50 File Offset: 0x0000BF50
		internal virtual System.Windows.Controls.TextBox layerunits { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000DD59 File Offset: 0x0000BF59
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000DD61 File Offset: 0x0000BF61
		internal virtual System.Windows.Controls.TextBox layergroups { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x04000100 RID: 256
		public bool AllowClose;

		// Token: 0x04000101 RID: 257
		public List<AthenaHotKey> AthenaHotKeys;

		// Token: 0x0200006E RID: 110
		// (Invoke) Token: 0x0600076C RID: 1900
		public delegate void HotkeyAdditionRequestedEventHandler(string Action, string KeyString, Keys Key, bool Shift, bool Control, bool AltMenu);

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x06000770 RID: 1904
		public delegate void HotkeyRemovalRequestedEventHandler(string Action);
	}
}
