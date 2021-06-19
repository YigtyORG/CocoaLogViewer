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
	internal sealed class UserDataTransformer : TransformerBase
	{
		private const string UserDataExists   = "User data exists";
		private const string NoUserDataExists = "No user data exists";
		private const string Prefix           = "existsUserData: ";

		internal static UserDataTransformer Instance { get; } = new();

		private UserDataTransformer() { }

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			if (string.IsNullOrEmpty(message)) {
				return string.Empty;
			}

			if (message == UserDataExists) {
				return "The user data exists.";
			} else if (message == NoUserDataExists) {
				return "The user data does not exist.";
			} else if (message.StartsWith(Prefix) &&
				bool.TryParse(message.Substring(Prefix.Length), out bool exists)) {
				return $"Does user data exist? {(exists ? "Yes" : "No")}.";
			} else {
				return next(message);
			}
		}
	}
}
