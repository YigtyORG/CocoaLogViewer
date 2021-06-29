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
	internal static class MessageSearchFilters
	{
		internal sealed class EqualsWith : ISearchFilter
		{
			internal static readonly EqualsWith _inst1 = new("equals_with");
			internal static readonly EqualsWith _inst2 = new("equals");

			public string Key { get; }

			private EqualsWith(string key)
			{
				this.Key = key;
			}

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node) {
					string text = node.Token.GetText();
					return model.OriginalMessage    == text
						|| model.TransformedMessage == text;
				} else {
					return false;
				}
			}
		}

		internal sealed class StartsWith : ISearchFilter
		{
			internal static readonly StartsWith _inst1 = new("starts_with");
			internal static readonly StartsWith _inst2 = new("starts");

			public string Key { get; }

			private StartsWith(string key)
			{
				this.Key = key;
			}

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node) {
					string text = node.Token.GetText();
					return model.OriginalMessage   .StartsWith(text)
						|| model.TransformedMessage.StartsWith(text);
				} else {
					return false;
				}
			}
		}
		
		internal sealed class EndsWith : ISearchFilter
		{
			internal static readonly EndsWith _inst1 = new("ends_with");
			internal static readonly EndsWith _inst2 = new("ends");

			public string Key { get; }

			private EndsWith(string key)
			{
				this.Key = key;
			}

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node) {
					string text = node.Token.GetText();
					return model.OriginalMessage   .EndsWith(text)
						|| model.TransformedMessage.EndsWith(text);
				} else {
					return false;
				}
			}
		}

		internal sealed class RegularExpression : ISearchFilter
		{
			internal static readonly RegularExpression _inst = new();

			public string Key => "regex";

			private RegularExpression() { }

			public bool Match(SearchFilterNode value, LogDataModel model)
			{
				if (value is TokenNode node) {
					string text = node.Token.GetText();
					return Regex.IsMatch(model.OriginalMessage,    text)
						|| Regex.IsMatch(model.TransformedMessage, text);
				} else {
					return false;
				}
			}
		}
	}
}
