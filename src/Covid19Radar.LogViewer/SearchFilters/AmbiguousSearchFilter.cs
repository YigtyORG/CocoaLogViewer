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
	internal sealed class AmbiguousSearchFilter : ISearchFilter
	{
		internal static readonly AmbiguousSearchFilter _inst = new();

		public string Key => "$";

		private AmbiguousSearchFilter() { }

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is TokenNode node) {
				string text = node.Token.GetText();
				return node.Match(model)
					|| model.Timestamp                .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.Level                    .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.OriginalMessage          .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.TransformedMessage       .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.Method                   .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.FilePath                 .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.LineNumber               .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.Platform                 .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.PlatformVersion          .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.DeviceModel              .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.DeviceType               .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.Version                  .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.BuildNumber              .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.GetDateTimeAsString()    .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.GetLogLevel().Text       .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.GetLocation()            .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.CreateDetails()          .Contains(text, StringComparison.CurrentCultureIgnoreCase)
					|| model.CreateDetailsAsMarkdown().Contains(text, StringComparison.CurrentCultureIgnoreCase);
			} else {
				return value.Match(model);
			}
		}
	}
}
