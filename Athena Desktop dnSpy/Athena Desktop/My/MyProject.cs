using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7.My
{
	// Token: 0x02000003 RID: 3
	[StandardModule]
	[HideModuleName]
	[GeneratedCode("MyTemplate", "11.0.0.0")]
	internal sealed class MyProject
	{
		// Token: 0x0200003B RID: 59
		[EditorBrowsable(EditorBrowsableState.Never)]
		[ComVisible(false)]
		internal sealed class ThreadSafeObjectProvider<T> where T : new()
		{
			// Token: 0x170001AD RID: 429
			// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000297E0 File Offset: 0x000279E0
			internal T GetInstance
			{
				[DebuggerHidden]
				get
				{
					if (MyProject.ThreadSafeObjectProvider<!0>.m_ThreadStaticValue == null)
					{
						MyProject.ThreadSafeObjectProvider<!0>.m_ThreadStaticValue = Activator.CreateInstance<T>();
					}
					return MyProject.ThreadSafeObjectProvider<!0>.m_ThreadStaticValue;
				}
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x00005FA9 File Offset: 0x000041A9
			[DebuggerHidden]
			[EditorBrowsable(EditorBrowsableState.Never)]
			public ThreadSafeObjectProvider()
			{
			}

			// Token: 0x04000289 RID: 649
			[CompilerGenerated]
			[ThreadStatic]
			private static T m_ThreadStaticValue;
		}
	}
}
