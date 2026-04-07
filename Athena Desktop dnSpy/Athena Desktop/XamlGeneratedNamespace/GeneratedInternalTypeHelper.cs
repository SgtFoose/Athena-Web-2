using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Markup;

namespace Athena.App.W7.XamlGeneratedNamespace
{
	// Token: 0x02000039 RID: 57
	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class GeneratedInternalTypeHelper : InternalTypeHelper
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x0002971F File Offset: 0x0002791F
		protected override object CreateInstance(Type type, CultureInfo culture)
		{
			return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, null, culture);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0002972F File Offset: 0x0002792F
		protected override object GetPropertyValue(PropertyInfo propertyInfo, object target, CultureInfo culture)
		{
			return propertyInfo.GetValue(RuntimeHelpers.GetObjectValue(target), BindingFlags.Default, null, null, culture);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00029741 File Offset: 0x00027941
		protected override void SetPropertyValue(PropertyInfo propertyInfo, object target, object value, CultureInfo culture)
		{
			propertyInfo.SetValue(RuntimeHelpers.GetObjectValue(target), RuntimeHelpers.GetObjectValue(value), BindingFlags.Default, null, null, culture);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0002975C File Offset: 0x0002795C
		protected override Delegate CreateDelegate(Type delegateType, object target, string handler)
		{
			return (Delegate)target.GetType().InvokeMember("_CreateDelegate", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, RuntimeHelpers.GetObjectValue(target), new object[]
			{
				delegateType,
				handler
			}, null);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00029799 File Offset: 0x00027999
		protected override void AddEventHandler(EventInfo eventInfo, object target, Delegate handler)
		{
			eventInfo.AddEventHandler(RuntimeHelpers.GetObjectValue(target), handler);
		}
	}
}
