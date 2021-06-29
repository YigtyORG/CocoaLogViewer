/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.SearchFilters
{
	internal sealed class LogLevelSearchFilter : ISearchFilter
	{
		internal static readonly LogLevelSearchFilter _inst = new();

		public string Key => "level";

		private LogLevelSearchFilter() { }

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is TokenNode node) {
				string text = node.Token.GetText();
				return model.Level             .Equals(text, StringComparison.OrdinalIgnoreCase)
					|| model.GetLogLevel().Text.Equals(text, StringComparison.OrdinalIgnoreCase);
			} else {
				return false;
			}
		}
	}
}
