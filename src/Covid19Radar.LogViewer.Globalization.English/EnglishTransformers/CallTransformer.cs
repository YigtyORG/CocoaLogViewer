/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Globalization.EnglishTransformers
{
	internal sealed class CallTransformer : TransformerBase
	{
		private const string Start = "The process starts.";
		private const string End   = "The process ends.";

		internal static CallTransformer Instance { get; } = new();

		private CallTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			return message switch {
				nameof(Start) => Start,
				nameof(End)   => End,
				_ => next(message)
			};
		}
	}
}
