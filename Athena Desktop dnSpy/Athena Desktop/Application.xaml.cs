using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;

namespace Athena.App.W7
{
	// Token: 0x02000035 RID: 53
	public partial class Application : Application
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00012453 File Offset: 0x00010653
		internal AssemblyInfo Info
		{
			[DebuggerHidden]
			get
			{
				return new AssemblyInfo(Assembly.GetExecutingAssembly());
			}
		}
	}
}
