/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Extensibility.Features;
using Covid19Radar.LogViewer.Transformers.Configuration.Properties;
using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer.Transformers.Configuration
{
	public sealed class TransformerSwitcher : ILauncherFeature
	{
		private readonly ModuleInitializationContext _context;
		private          TransformerPipeline?        _default;
		public           string?                     DisplayName => Resources.TransformerSwitcher_DisplayName;
		public           bool                        IsChecked   => _context.TransformerPipeline == EmptyTransformerPipeline.Instance;

		public TransformerSwitcher(ModuleInitializationContext moduleInitializationContext)
		{
			if (moduleInitializationContext is null) {
				throw new ArgumentNullException(nameof(moduleInitializationContext));
			}
			_context = moduleInitializationContext;
		}

		public void RunCommand(ILauncherWindow parent)
		{
			if (_context.TransformerPipeline == EmptyTransformerPipeline.Instance) {
				_context.TransformerPipeline = _default;
			} else {
				_default                     = _context.TransformerPipeline;
				_context.TransformerPipeline = EmptyTransformerPipeline.Instance;
			}
		}
	}
}
