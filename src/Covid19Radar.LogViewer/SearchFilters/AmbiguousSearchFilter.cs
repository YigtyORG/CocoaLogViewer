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
	public sealed class AmbiguousSearchFilter : ISearchFilter
	{
		public string Key => "$";

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is TokenNode node) {
				string text = node.Token.GetText();
				return node.Match(model)
					|| model.LineNumber               .Contains(text)
					|| model.Platform                 .Contains(text)
					|| model.PlatformVersion          .Contains(text)
					|| model.DeviceModel              .Contains(text)
					|| model.DeviceType               .Contains(text)
					|| model.Version                  .Contains(text)
					|| model.BuildNumber              .Contains(text)
					|| model.GetDateTimeAsString()    .Contains(text)
					|| model.GetLogLevel().Text       .Contains(text)
					|| model.GetLocation()            .Contains(text)
					|| model.CreateDetails()          .Contains(text)
					|| model.CreateDetailsAsMarkdown().Contains(text);
			} else {
				return value.Match(model);
			}
		}
	}
}
