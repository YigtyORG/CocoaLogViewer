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
	internal sealed class DetailsSearchFilter : ISearchFilter
	{
		internal static readonly DetailsSearchFilter _inst = new();

		public string Key => "details";

		private DetailsSearchFilter() { }

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is TokenNode node) {
				string text = node.Token.GetText();
				return model.CreateDetails()          .Contains(text)
					|| model.CreateDetailsAsMarkdown().Contains(text);
			} else {
				return false;
			}
		}
	}
}
