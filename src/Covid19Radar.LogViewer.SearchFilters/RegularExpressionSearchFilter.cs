/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Linq;
using System.Text.RegularExpressions;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.SearchFilters
{
	internal sealed class RegularExpressionSearchFilter : ISearchFilter
	{
		internal static readonly RegularExpressionSearchFilter _inst = new();

		public string Key => "regexs";

		private RegularExpressionSearchFilter() { }

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is SearchFilterNodeList nodeList && nodeList.Nodes.Count >= 2 &&
				nodeList.Nodes[0] is TokenNode patternNode) {

				string   pattern  = patternNode.Token.GetText();
				string[] expected = new string[nodeList.Nodes.Count - 1];

				for (int i = 0; i < expected.Length; ++i) {
					expected[i] = nodeList.Nodes[i + 1] is TokenNode node ? node.Token.GetText() : string.Empty;
				}

				return expected.SequenceEqual(Regex.Split(model.OriginalMessage,    pattern))
					|| expected.SequenceEqual(Regex.Split(model.TransformedMessage, pattern));
			} else {
				return false;
			}
		}
	}
}
