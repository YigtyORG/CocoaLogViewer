/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Runtime.CompilerServices;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public readonly struct SearchTextToken
	{
		public SearchTextLexer Lexer  { get; init; }
		public int             Index  { get; init; }
		public int             Length { get; init; }
		public TokenType       Type   { get; init; }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string GetText()
		{
			return this.Lexer.GetText(this.Index, this.Length).ToString();
		}
	}

	public enum TokenType
	{
		Text,
		Keyword,
		Symbol
	}
}
