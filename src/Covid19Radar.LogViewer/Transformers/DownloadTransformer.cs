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
	internal sealed class DownloadTransformer : TransformerBase
	{
		internal static DownloadTransformer Instance { get; } = new();

		private DownloadTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			return message switch {
				"Start download files"  => "ファイルのダウンロードを開始しました。",
				"End to download files" => "ファイルのダウンロードを終了しました。",
				"Success to download"   => "ダウンロードが成功しました。",
				"Fail to download"      => "ダウンロードが失敗しました。",
				_ => next(message)
			};
		}
	}
}
