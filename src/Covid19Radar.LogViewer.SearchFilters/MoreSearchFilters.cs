/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Extensibility.Providers;
using Covid19Radar.LogViewer.SearchFilters.Properties;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public class MoreSearchFilters : CocoaLogViewerModule, ISearchFilterProvider
	{
		public override string? DisplayName => Resources.MoreSearchFilters_DisplayName;

		protected override void InitializeCore(ModuleInitializationContext context)
		{
			// do nothing
		}

		public IEnumerable<ISearchFilter> GetSearchFilters()
		{
			yield return RegularExpressionSearchFilter._inst1;
			yield return RegularExpressionSearchFilter._inst2;
			yield return RegularExpressionSearchFilter._inst3;
			yield return FilePathSearchFilter         ._inst;
			yield return PrivacySearchFilter          ._inst1;
			yield return PrivacySearchFilter          ._inst2;
		}

		private static void Main()
		{
			// do nothing
		}
	}
}
