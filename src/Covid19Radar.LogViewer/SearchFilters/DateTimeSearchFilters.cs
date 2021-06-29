/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.SearchFilters
{
	internal static class DateTimeSearchFilters
	{
		internal sealed class From : ISearchFilter
		{
			internal static readonly From _inst = new();

			public string Key => "from";

			private From() { }

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node &&
					node.Token.GetText().TryToDateTime(out var left) &&
					model.Timestamp     .TryToDateTime(out var right)) {
					return left <= right;
				} else {
					return false;
				}
			}
		}

		internal sealed class To : ISearchFilter
		{
			internal static readonly To _inst = new();

			public string Key => "to";

			private To() { }

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node &&
					node.Token.GetText().TryToDateTime(out var left) &&
					model.Timestamp.TryToDateTime(out var right)) {
					return left >= right;
				} else {
					return false;
				}
			}
		}
	}
}
