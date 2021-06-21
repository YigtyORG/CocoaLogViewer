/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Transformers.Configuration.Properties;

namespace Covid19Radar.LogViewer.Transformers.Configuration
{
	public class TransformersConfigurationTool : CocoaLogViewerModule
	{
		private TransformerSwitcher? _switcher;

		public override string? DisplayName => Resources.TransformersConfigurationTool_DisplayName;

		protected override void InitializeCore(ModuleInitializationContext context)
		{
			_switcher = new(context);
		}

		public override IEnumerable<IPlugin>? GetChildPlugins()
		{
			if (_switcher is not null) {
				yield return _switcher;
			}
		}
	}
}
