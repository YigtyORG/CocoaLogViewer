/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer
{
	public static class StringExtensions
	{
		public static bool TryToDateTime(this string s, [MaybeNullWhen(false)][NotNullWhen(true)] out DateTime result)
		{
			return DateTime.TryParseExact(s, "yyyyMMddHHmmssfffffff",                                        null, DateTimeStyles.None, out result)
				|| DateTime.TryParseExact(s, "yyyy/MM/dd HH:mm:ss",                                          null, DateTimeStyles.None, out result)
				|| DateTime.TryParseExact(s, "yyyy/MM/dd HH:mm:ss.fff",                                      null, DateTimeStyles.None, out result)
				|| DateTime.TryParseExact(s, "yyyy/MM/dd HH:mm:ss.fffffff",                                  null, DateTimeStyles.None, out result)
				|| DateTime.TryParseExact(s, LanguageData.Current.LogDataModel_DateTime_Format_WithWordWrap, null, DateTimeStyles.None, out result)
				|| DateTime.TryParseExact(s, LanguageData.Current.LogDataModel_DateTime_Format_WithNoWrap,   null, DateTimeStyles.None, out result)
				|| DateTime.TryParse(s, out result);
		}
	}
}
