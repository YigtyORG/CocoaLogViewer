/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;

namespace Covid19Radar.LogViewer.Extensibility
{
	public interface IPlugin
	{
		public string? DisplayName { get; }

		public IEnumerable<IPlugin>? GetChildPlugins()
		{
			return null;
		}
	}
}
