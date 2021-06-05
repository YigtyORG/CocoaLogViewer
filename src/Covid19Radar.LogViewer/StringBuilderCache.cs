/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Text;

namespace Covid19Radar.LogViewer
{
	internal static class StringBuilderCache<T>
	{
		[ThreadStatic()]
		private static StringBuilder? _sb;

		internal static StringBuilder Get()
		{
			if (_sb is null) {
				_sb = new();
			} else {
				_sb.Clear();
			}
			return _sb;
		}
	}
}
