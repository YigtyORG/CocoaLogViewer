/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer.Extensibility.Features
{
	public interface ILauncherFeature : IPlugin
	{
		public bool IsChecked { get; }

		public void RunCommand(ILauncherWindow parent);
	}
}
