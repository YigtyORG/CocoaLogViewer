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
	internal sealed class DownloadTekFileTransformer : TransformerBase
	{
		private const string Prefix = "Download TEK file. url: ";

		internal static DownloadTekFileTransformer Instance { get; } = new();

		private DownloadTekFileTransformer() { }

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			if (string.IsNullOrEmpty(message)) {
				return string.Empty;
			}

			if (message.StartsWith(Prefix)) {
				return "Downloaded the temporary exposure key (TEK) file from \"" + message.Substring(Prefix.Length) + "\".";
			} else {
				return next(message);
			}
		}
	}
}
