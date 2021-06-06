/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Covid19Radar.LogViewer.Extensibility;

namespace Covid19Radar.LogViewer.Tests.Extensibility
{
	internal sealed class ModuleInitializationContextMock : ModuleInitializationContext
	{
		public override void ParseArguments()
		{
			throw new NotImplementedException();
		}
	}
}
