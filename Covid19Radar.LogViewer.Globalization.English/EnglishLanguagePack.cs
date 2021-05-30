/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Globalization;
using Covid19Radar.LogViewer.Extensibility;

namespace Covid19Radar.LogViewer.Globalization
{
	public sealed class EnglishLanguagePack : CocoaLogViewerModule
	{
		protected override void InitializeCore(ModuleInitializationContext context)
		{
#if DEBUG
			var cinfo = CultureInfo.GetCultureInfo("en");
			CultureInfo.DefaultThreadCurrentCulture   = cinfo;
			CultureInfo.DefaultThreadCurrentUICulture = cinfo;
			CultureInfo.CurrentCulture                = cinfo;
			CultureInfo.CurrentUICulture              = cinfo;
#endif
			LanguageData.Current = English._inst;
		}
	}
}