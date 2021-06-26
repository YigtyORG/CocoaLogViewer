/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public class SearchTextParser
	{
		private readonly SearchTextLexer _lexer;

		public SearchTextParser(SearchTextLexer lexer)
		{
			_lexer = lexer ?? throw new ArgumentNullException(nameof(lexer));
		}

		public void Parse()
		{
			var x = TwoWayEnumerable.Create(_lexer.Scan(), e => e?.GetEnumerator());

			// TODO: 構文解析処理
		}
	}
}
