using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic.Logging;

namespace Athena.App.W7.My
{
	// Token: 0x02000005 RID: 5
	[StandardModule]
	[HideModuleName]
	internal sealed class MyWpfExtension
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020FB File Offset: 0x000002FB
		internal static Application Application
		{
			get
			{
				return (Application)System.Windows.Application.Current;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002107 File Offset: 0x00000307
		internal static Computer Computer
		{
			get
			{
				return MyWpfExtension.s_Computer.GetInstance;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002113 File Offset: 0x00000313
		internal static User User
		{
			get
			{
				return MyWpfExtension.s_User.GetInstance;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000211F File Offset: 0x0000031F
		internal static Log Log
		{
			get
			{
				return MyWpfExtension.s_Log.GetInstance;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000212B File Offset: 0x0000032B
		internal static MyWpfExtension.MyWindows Windows
		{
			[DebuggerHidden]
			get
			{
				return MyWpfExtension.s_Windows.GetInstance;
			}
		}

		// Token: 0x04000003 RID: 3
		private static MyProject.ThreadSafeObjectProvider<Computer> s_Computer = new MyProject.ThreadSafeObjectProvider<Computer>();

		// Token: 0x04000004 RID: 4
		private static MyProject.ThreadSafeObjectProvider<User> s_User = new MyProject.ThreadSafeObjectProvider<User>();

		// Token: 0x04000005 RID: 5
		private static MyProject.ThreadSafeObjectProvider<MyWpfExtension.MyWindows> s_Windows = new MyProject.ThreadSafeObjectProvider<MyWpfExtension.MyWindows>();

		// Token: 0x04000006 RID: 6
		private static MyProject.ThreadSafeObjectProvider<Log> s_Log = new MyProject.ThreadSafeObjectProvider<Log>();

		// Token: 0x0200003C RID: 60
		[EditorBrowsable(EditorBrowsableState.Never)]
		[MyGroupCollection("System.Windows.Window", "Create__Instance__", "Dispose__Instance__", "My.MyWpfExtenstionModule.Windows")]
		internal sealed class MyWindows
		{
			// Token: 0x060006B4 RID: 1716 RVA: 0x00029800 File Offset: 0x00027A00
			[DebuggerHidden]
			private static T Create__Instance__<T>(T Instance) where T : Window, new()
			{
				T result;
				if (Instance == null)
				{
					if (MyWpfExtension.MyWindows.s_WindowBeingCreated != null)
					{
						if (MyWpfExtension.MyWindows.s_WindowBeingCreated.ContainsKey(typeof(!!0)))
						{
							throw new InvalidOperationException("The window cannot be accessed via My.Windows from the Window constructor.");
						}
					}
					else
					{
						MyWpfExtension.MyWindows.s_WindowBeingCreated = new Hashtable();
					}
					MyWpfExtension.MyWindows.s_WindowBeingCreated.Add(typeof(!!0), null);
					result = Activator.CreateInstance<T>();
				}
				else
				{
					result = Instance;
				}
				return result;
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00029867 File Offset: 0x00027A67
			[DebuggerHidden]
			private void Dispose__Instance__<T>(ref T instance) where T : Window
			{
				instance = default(!!0);
			}

			// Token: 0x060006B6 RID: 1718 RVA: 0x00005FA9 File Offset: 0x000041A9
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MyWindows()
			{
			}

			// Token: 0x060006B7 RID: 1719 RVA: 0x00029870 File Offset: 0x00027A70
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override bool Equals(object o)
			{
				return base.Equals(RuntimeHelpers.GetObjectValue(o));
			}

			// Token: 0x060006B8 RID: 1720 RVA: 0x0002987E File Offset: 0x00027A7E
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x060006B9 RID: 1721 RVA: 0x00029886 File Offset: 0x00027A86
			[EditorBrowsable(EditorBrowsableState.Never)]
			internal new Type GetType()
			{
				return typeof(MyWpfExtension.MyWindows);
			}

			// Token: 0x060006BA RID: 1722 RVA: 0x00029892 File Offset: 0x00027A92
			[EditorBrowsable(EditorBrowsableState.Never)]
			public override string ToString()
			{
				return base.ToString();
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x060006BB RID: 1723 RVA: 0x0002989A File Offset: 0x00027A9A
			// (set) Token: 0x060006BD RID: 1725 RVA: 0x000298CC File Offset: 0x00027ACC
			public HotKeys HotKeys
			{
				get
				{
					this.m_HotKeys = MyWpfExtension.MyWindows.Create__Instance__<HotKeys>(this.m_HotKeys);
					return this.m_HotKeys;
				}
				set
				{
					if (value != this.m_HotKeys)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<HotKeys>(ref this.m_HotKeys);
					}
				}
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x060006BC RID: 1724 RVA: 0x000298B3 File Offset: 0x00027AB3
			// (set) Token: 0x060006BE RID: 1726 RVA: 0x000298F1 File Offset: 0x00027AF1
			public MainWindow MainWindow
			{
				get
				{
					this.m_MainWindow = MyWpfExtension.MyWindows.Create__Instance__<MainWindow>(this.m_MainWindow);
					return this.m_MainWindow;
				}
				set
				{
					if (value != this.m_MainWindow)
					{
						if (value != null)
						{
							throw new ArgumentException("Property can only be set to Nothing");
						}
						this.Dispose__Instance__<MainWindow>(ref this.m_MainWindow);
					}
				}
			}

			// Token: 0x0400028A RID: 650
			[ThreadStatic]
			private static Hashtable s_WindowBeingCreated;

			// Token: 0x0400028B RID: 651
			[EditorBrowsable(EditorBrowsableState.Never)]
			public HotKeys m_HotKeys;

			// Token: 0x0400028C RID: 652
			[EditorBrowsable(EditorBrowsableState.Never)]
			public MainWindow m_MainWindow;
		}
	}
}
