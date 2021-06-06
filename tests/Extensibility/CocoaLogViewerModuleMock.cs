/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Extensibility;

namespace Covid19Radar.LogViewer.Tests.Extensibility
{
	internal sealed class CocoaLogViewerModuleMock : CocoaLogViewerModule
	{
		protected override void InitializeCore(ModuleInitializationContext context)
		{
			// do nothing
		}
	}
}
