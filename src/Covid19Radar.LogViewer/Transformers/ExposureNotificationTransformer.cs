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
	internal sealed class ExposureNotificationTransformer : TransformerBase
	{
		private const string StatusPrefix = "Exposure notification status: ";
		private const string CountPrefix  = "Exposure count: ";
		private const string Failed       = "Failed to exposure notification status.";

		internal static ExposureNotificationTransformer Instance { get; } = new();

		private ExposureNotificationTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			if (message is null) {
				return null;
			}

			if (message.StartsWith(StatusPrefix)) {
				return "接触通知の状態：" + message.Substring(StatusPrefix.Length);
			} else if (message.StartsWith(CountPrefix)) {
				return message.Substring(CountPrefix.Length) + "回接触しました。";
			} else if (message == Failed) {
				return "接触通知の状態の取得に失敗しました。";
			} else {
				return next(message);
			}
		}
	}
}
