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
	internal sealed class TekItemTransformer : TransformerBase
	{
		private const string Prefix = "tekItem.Created: ";

		internal static TekItemTransformer Instance { get; } = new();

		private TekItemTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			if ((message?.StartsWith(Prefix) ?? false) &&
				long.TryParse(message.Substring(Prefix.Length), out long milliseconds)) {
				var dto = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
				return $"The temporary exposure key (TEK) was created on {dto:MMMM dd, yyyy tthh:mm:ss.fffffff}.";
			} else {
				return next(message);
			}
		}
	}
}
