/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;
using Covid19Radar.LogViewer.SearchFilters;

namespace Covid19Radar.LogViewer.Extensibility.Providers
{
	public interface ISearchFilterProvider : IProvider
	{
		public IEnumerable<ISearchFilter> GetSearchFilters();
	}
}
