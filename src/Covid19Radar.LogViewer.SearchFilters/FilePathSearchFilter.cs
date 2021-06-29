/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Models;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Covid19Radar.LogViewer.SearchFilters
{
	internal sealed class FilePathSearchFilter : ISearchFilter
	{
		internal static readonly FilePathSearchFilter _inst = new();

		public string Key => "glob";

		private FilePathSearchFilter() { }

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is TokenNode node) {
				var matcher = new Matcher();
				matcher.AddInclude(node.Token.GetText());
				return matcher.Match(new string[] { model.FilePath }).HasMatches;
			} else {
				return false;
			}
		}
	}
}
