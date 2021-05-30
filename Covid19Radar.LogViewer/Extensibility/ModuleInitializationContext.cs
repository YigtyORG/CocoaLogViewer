/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;
using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Extensibility
{
	public abstract class ModuleInitializationContext
	{
		public string[]?            Arguments           { get; set; }
		public TransformerPipeline? TransformerPipeline { get; set; }
		public IList<string>?       LogFilesToOpen      { get; set; }
		public bool                 AllowEscape         { get; set; }
		public bool                 DisallowExtensions  { get; set; }

		public abstract void ParseArguments();
	}
}
