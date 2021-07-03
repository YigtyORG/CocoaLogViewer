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
	internal sealed class PrivacySearchFilter : ISearchFilter
	{
		internal static readonly PrivacySearchFilter _inst1 = new("privacy");
		internal static readonly PrivacySearchFilter _inst2 = new("private");

		public string Key { get; }

		private PrivacySearchFilter(string key)
		{
			this.Key = key;
		}

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is TokenNode node && bool.TryParse(node.Token.GetText(), out bool flag) && !flag) {
				return !model.MaybeContainsPrivacy;
			} else {
				return model.MaybeContainsPrivacy;
			}
		}
	}
}
