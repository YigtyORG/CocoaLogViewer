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
	internal sealed class ControlCharTransformer : TransformerBase
	{
		internal static ControlCharTransformer Instance { get; } = new();

		private ControlCharTransformer() { }

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			var sb  = StringBuilderCache<ControlCharTransformer>.Get();
			var msg = message.AsSpan();
			for (int i = 0; i < msg.Length; ++i) {
				char ch = msg[i];
				sb.Append(ch switch {
					'\t' => '\u2409',
					'\v' => '\u240B',
					'\r' => '\u240D',
					'\n' => '\u240A',
					_ => ch
				});
			}
			return next(sb.ToString());
		}
	}
}
