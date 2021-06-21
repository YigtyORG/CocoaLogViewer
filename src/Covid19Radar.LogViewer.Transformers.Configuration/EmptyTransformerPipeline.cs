/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace Covid19Radar.LogViewer.Transformers.Configuration
{
	public sealed class EmptyTransformerPipeline : TransformerPipeline
	{
		public static EmptyTransformerPipeline Instance { get; } = new();

		private EmptyTransformerPipeline() { }

		public override Func<string?, string> Build(Func<string?, string> final)
		{
			return final;
		}

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			if (string.IsNullOrEmpty(message)) {
				return string.Empty;
			}
			return new(message);
		}
	}
}
