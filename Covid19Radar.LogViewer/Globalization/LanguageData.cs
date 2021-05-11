/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Threading;

namespace Covid19Radar.LogViewer.Globalization
{
	public abstract class LanguageData
	{
		private static readonly LanguageData _default;
		private static          LanguageData _current;

		static LanguageData()
		{
			_default = Japanese._inst;
			_current = _default;
		}

		public static LanguageData Default => _default;

		public static LanguageData Current
		{
			get => _current;
			set
			{
				value ??= _default;
				var current = _current;
				while (Interlocked.CompareExchange(ref _current, value, current) != current) {
					Thread.Yield();
					current = _current;
				}
			}
		}
	}
}
