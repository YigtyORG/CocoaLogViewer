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
	internal sealed class UserDataTransformer : TransformerBase
	{
		private const string UserDataExists   = "User data exists";
		private const string NoUserDataExists = "No user data exists";
		private const string Prefix           = "existsUserData: ";

		internal static UserDataTransformer Instance { get; } = new();

		private UserDataTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			if (message == UserDataExists) {
				return "利用者情報は存在します。";
			} else if (message == NoUserDataExists) {
				return "利用者情報は存在しません。";
			} else if ((message?.StartsWith(Prefix) ?? false) &&
				bool.TryParse(message.Substring(Prefix.Length), out bool exists)) {
				return $"利用者情報の存否：{(exists ? "存在する。" : "存在しない。")}";
			} else {
				return next(message);
			}
		}
	}
}
