/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Extensibility
{
	public abstract class ModuleInitializationContext
	{
		public string[]?            Arguments           { get; set; }
		public TransformerPipeline? TransformerPipeline { get; set; }
	}
}
