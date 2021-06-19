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
				return "The exposure notification status is: " + message.Substring(StatusPrefix.Length);
			} else if (message.StartsWith(CountPrefix)) {
				string sCount = message.Substring(CountPrefix.Length);
				if (int.TryParse(sCount, out int nCount)) {
					if (nCount == 1) {
						return "The user did exposure one time.";
					} else {
						return "The user did exposure " + nCount + " times.";
					}
				} else {
					return "The user did exposure " + sCount + " time(s).";
				}
			} else if (message == Failed) {
				return "Failed to get an exposure notification status.";
			} else {
				return next(message);
			}
		}
	}
}
