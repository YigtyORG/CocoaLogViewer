/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace Covid19Radar.LogViewer.Transformers
{
	internal sealed class CallTransformer : TransformerBase
	{
		private const string Start = "処理を開始しました。";
		private const string End   = "処理を終了しました。";

		internal static CallTransformer Instance { get; } = new();

		private CallTransformer() { }

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			return message switch {
				nameof(Start) => Start,
				nameof(End)   => End,
				_ => next(message)
			};
		}
	}
}
