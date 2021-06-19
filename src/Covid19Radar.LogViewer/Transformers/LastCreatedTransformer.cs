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
	internal sealed class LastCreatedTransformer : TransformerBase
	{
		private const string Prefix = "lastCreated: ";

		internal static LastCreatedTransformer Instance { get; } = new();

		private LastCreatedTransformer() { }

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			if (string.IsNullOrEmpty(message)) {
				return string.Empty;
			}

			if (message.StartsWith(Prefix) &&
				long.TryParse(message.Substring(Prefix.Length), out long milliseconds)) {
				var dto = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
				return $"最終作成日時：{dto:yyyy\'年\'MM\'月\'dd\'日\' HH\'時\'mm\'分\'ss.fffffff\'秒\'}";
			} else {
				return next(message);
			}
		}
	}
}
