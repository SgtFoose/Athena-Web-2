using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace Athena.App.W7
{
	// Token: 0x02000034 RID: 52
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.1.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal sealed partial class MySettings : ApplicationSettingsBase
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00012355 File Offset: 0x00010555
		public static MySettings Default
		{
			get
			{
				return MySettings.defaultInstance;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600033E RID: 830 RVA: 0x0001235C File Offset: 0x0001055C
		// (set) Token: 0x0600033F RID: 831 RVA: 0x0001236E File Offset: 0x0001056E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("localhost")]
		public string ARMAPCIP
		{
			get
			{
				return Conversions.ToString(this["ARMAPCIP"]);
			}
			set
			{
				this["ARMAPCIP"] = value;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0001237C File Offset: 0x0001057C
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0001238E File Offset: 0x0001058E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("28800")]
		public int ARMAPCPort
		{
			get
			{
				return Conversions.ToInteger(this["ARMAPCPort"]);
			}
			set
			{
				this["ARMAPCPort"] = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000342 RID: 834 RVA: 0x000123A1 File Offset: 0x000105A1
		// (set) Token: 0x06000343 RID: 835 RVA: 0x000123B3 File Offset: 0x000105B3
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string HotKeys
		{
			get
			{
				return Conversions.ToString(this["HotKeys"]);
			}
			set
			{
				this["HotKeys"] = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000123C1 File Offset: 0x000105C1
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000123D3 File Offset: 0x000105D3
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool UseTouchForInk
		{
			get
			{
				return Conversions.ToBoolean(this["UseTouchForInk"]);
			}
			set
			{
				this["UseTouchForInk"] = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000346 RID: 838 RVA: 0x000123E6 File Offset: 0x000105E6
		// (set) Token: 0x06000347 RID: 839 RVA: 0x000123F8 File Offset: 0x000105F8
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string ACSServerAddress
		{
			get
			{
				return Conversions.ToString(this["ACSServerAddress"]);
			}
			set
			{
				this["ACSServerAddress"] = value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00012406 File Offset: 0x00010606
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00012418 File Offset: 0x00010618
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("28804")]
		public int ACSServerPort
		{
			get
			{
				return Conversions.ToInteger(this["ACSServerPort"]);
			}
			set
			{
				this["ACSServerPort"] = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0001242B File Offset: 0x0001062B
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0001243D File Offset: 0x0001063D
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string ACSCallsign
		{
			get
			{
				return Conversions.ToString(this["ACSCallsign"]);
			}
			set
			{
				this["ACSCallsign"] = value;
			}
		}

		// Token: 0x04000168 RID: 360
		private static MySettings defaultInstance = (MySettings)SettingsBase.Synchronized(new MySettings());
	}
}
