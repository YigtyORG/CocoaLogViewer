/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.SearchFilters.Properties;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public class MoreSearchFilters : CocoaLogViewerModule
	{
		public override string? DisplayName => Resources.MoreSearchFilters_DisplayName;

		protected override void InitializeCore(ModuleInitializationContext context)
		{
		}

		public override IEnumerable<IPlugin>? GetChildPlugins()
		{
			yield break;
		}
	}
}
