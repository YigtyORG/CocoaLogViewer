/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Text.RegularExpressions;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.SearchFilters
{
	internal sealed class RegularExpressionSearchFilter : ISearchFilter
	{
		internal static readonly RegularExpressionSearchFilter _inst1 = new(
			"regexs",
			(model, pattern, expected) =>
				MatchCore(model, model.OriginalMessage,    pattern, expected) ||
				MatchCore(model, model.TransformedMessage, pattern, expected)
		);

		internal static readonly RegularExpressionSearchFilter _inst2 = new(
			"regexo",
			(model, pattern, expected) =>
				MatchCore(model, model.OriginalMessage, pattern, expected)
		);

		internal static readonly RegularExpressionSearchFilter _inst3 = new(
			"regext",
			(model, pattern, expected) =>
				MatchCore(model, model.TransformedMessage, pattern, expected)
		);

		private readonly Func<LogDataModel, string, SearchFilterNode[], bool> _match;

		public string Key { get; }

		private RegularExpressionSearchFilter(string key, Func<LogDataModel, string, SearchFilterNode[], bool> match)
		{
			_match   = match;
			this.Key = key;
		}

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is SearchFilterNodeList nodeList && nodeList.Nodes.Count >= 2 &&
				nodeList.Nodes[0] is TokenNode patternNode) {

				string pattern  = patternNode.Token.GetText();
				var    expected = new SearchFilterNode[nodeList.Nodes.Count - 1];

				for (int i = 0; i < expected.Length; ++i) {
					expected[i] = nodeList.Nodes[i + 1];
				}

				return _match(model, pattern, expected);
			} else {
				return false;
			}
		}

		private static bool MatchCore(LogDataModel model, string input, string pattern, SearchFilterNode[] expected)
		{
			string[] vs = Regex.Split(input, pattern);
			if (vs.Length != expected.Length) {
				return false;
			}
			for (int i = 0; i < vs.Length; ++i) {
				string msg = vs[i];
				if (!expected[i].Match(model with { OriginalMessage = msg, TransformedMessage = msg })) {
					return false;
				}
			}
			return true;
		}
	}
}
