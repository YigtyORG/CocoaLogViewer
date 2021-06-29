/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.IO.Enumeration;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.SearchFilters
{
	internal static class LocationSearchFilters
	{
		internal sealed class Method : ISearchFilter
		{
			internal static readonly Method _inst1 = new("method");
			internal static readonly Method _inst2 = new("func");

			public string Key { get; }

			private Method(string key)
			{
				this.Key = key;
			}

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node) {
					string text = node.Token.GetText();
					return model.Method == text;
				} else {
					return false;
				}
			}
		}

		internal sealed class FilePath : ISearchFilter
		{
			internal static readonly FilePath _inst1 = new("file");
			internal static readonly FilePath _inst2 = new("path");

			public string Key { get; }

			private FilePath(string key)
			{
				this.Key = key;
			}

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node) {
					return FileSystemName.MatchesWin32Expression(
						node.Token.GetText(),
						model.FilePath
					);
				} else {
					return false;
				}
			}
		}

		internal sealed class LineNumber : ISearchFilter
		{
			internal static readonly LineNumber _inst = new();

			public string Key => "line";

			private LineNumber() { }

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node &&
					long.TryParse(node.Token.GetText(), out long a) &&
					long.TryParse(model.LineNumber,     out long b)) {
					return a == b;
				} else {
					return false;
				}
			}
		}

		internal sealed class Ambiguous : ISearchFilter
		{
			internal static readonly Ambiguous _inst1 = new("location");
			internal static readonly Ambiguous _inst2 = new("loc");

			public string Key { get; }

			private Ambiguous(string key)
			{
				this.Key = key;
			}

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node) {
					string text = node.Token.GetText();
					return model.Method       .Contains(text)
						|| model.FilePath     .Contains(text)
						|| model.LineNumber   .Contains(text)
						|| model.GetLocation().Contains(text);
				} else {
					return false;
				}
			}
		}
	}
}
