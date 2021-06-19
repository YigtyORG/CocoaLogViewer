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
	internal sealed class DownloadTransformer : TransformerBase
	{
		internal static DownloadTransformer Instance { get; } = new();

		private DownloadTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			return message switch {
				"Start download files"  => "To download files starts.",
				"End to download files" => "To download files ends.",
				"Success to download"   => "Succeeded to download.",
				"Fail to download"      => "Failed to download.",
				_ => next(message)
			};
		}
	}
}
