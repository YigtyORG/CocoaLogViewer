/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Concurrent;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public static class SearchFilterRegistry
	{
		private static readonly ConcurrentDictionary<string, ISearchFilter> _filters = new();

		public static ISearchFilter? Get(string key)
		{
			if (_filters.TryGetValue(key, out var result)) {
				return result;
			} else {
				return null;
			}
		}

		public static bool Register(ISearchFilter searchFilter)
		{
			if (searchFilter is null) {
				throw new ArgumentNullException(nameof(searchFilter));
			}
			return _filters.TryAdd(searchFilter.Key, searchFilter);
		}

		public static bool Unregister(ISearchFilter searchFilter)
		{
			if (searchFilter is null) {
				throw new ArgumentNullException(nameof(searchFilter));
			}
			return _filters.TryRemove(new(searchFilter.Key, searchFilter));
		}
	}
}
