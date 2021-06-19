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
	public static class PrivacyValidator
	{
		public static bool Check(string originalMessage)
		{
			if (string.IsNullOrEmpty(originalMessage)) {
				return false;
			}

			if (originalMessage.StartsWith("Exposure count: ")) {
				return true;
			}

			var msg = originalMessage.AsSpan();
			while (true) {
				if (msg.StartsWith("http")) {
					msg = msg.Slice(4);
					if (msg.Length > 0) {
						int i = msg[0] == 's' ? 1 : 0;
						if (msg.Length > (i + 3) &&
							msg[i + 0] == ':' &&
							msg[i + 1] == '/' &&
							msg[i + 2] == '/') {
							return true;
						}
					}
				} else if (msg.StartsWith(nameof(Exception))) {
					return true;
				}
				if (msg.Length > 0) {
					msg = msg.Slice(1);
				} else {
					return false;
				}
			}
		}
	}
}
