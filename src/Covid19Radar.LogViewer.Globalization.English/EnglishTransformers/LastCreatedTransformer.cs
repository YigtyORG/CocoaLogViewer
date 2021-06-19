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
	internal sealed class LastCreatedTransformer : TransformerBase
	{
		private const string Prefix = "lastCreated: ";

		internal static LastCreatedTransformer Instance { get; } = new();

		private LastCreatedTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			if (message is null) {
				return null;
			}

			if (message.StartsWith(Prefix) &&
				long.TryParse(message.Substring(Prefix.Length), out long milliseconds)) {
				var dto = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
				return $"Last created on {dto:MMMM dd, yyyy tthh:mm:ss.fffffff}.";
			} else {
				return next(message);
			}
		}
	}
}
