/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public static class SearchFilterRegistry
	{
		private static readonly ConcurrentDictionary<string, ISearchFilter> _filters;

		static SearchFilterRegistry()
		{
			_filters = new(EqualityComparer._inst);
			Register(AmbiguousSearchFilter                  ._inst);
			Register(DetailsSearchFilter                    ._inst);
			Register(LogLevelSearchFilter                   ._inst);
			Register(DateTimeSearchFilters.From             ._inst);
			Register(DateTimeSearchFilters.To               ._inst);
			Register(MessageSearchFilters .EqualsWith       ._inst1);
			Register(MessageSearchFilters .EqualsWith       ._inst2);
			Register(MessageSearchFilters .StartsWith       ._inst1);
			Register(MessageSearchFilters .StartsWith       ._inst2);
			Register(MessageSearchFilters .EndsWith         ._inst1);
			Register(MessageSearchFilters .EndsWith         ._inst2);
			Register(MessageSearchFilters .RegularExpression._inst);
			Register(LocationSearchFilters.Method           ._inst1);
			Register(LocationSearchFilters.Method           ._inst2);
			Register(LocationSearchFilters.FilePath         ._inst1);
			Register(LocationSearchFilters.FilePath         ._inst2);
			Register(LocationSearchFilters.LineNumber       ._inst);
			Register(LocationSearchFilters.Ambiguous        ._inst1);
			Register(LocationSearchFilters.Ambiguous        ._inst2);
		}

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

		private sealed class EqualityComparer : IEqualityComparer<string>
		{
			internal static readonly EqualityComparer _inst = new();

			private EqualityComparer() { }

			public bool Equals(string? x, string? y)
			{
				if (x == y) {
					return true;
				}
				if (x is null || y is null) {
					return false;
				}
				return x.ToLower() == y.ToLower();
			}

			public int GetHashCode([DisallowNull()] string obj)
			{
				return (obj ?? string.Empty).ToLower().GetHashCode();
			}
		}
	}
}
