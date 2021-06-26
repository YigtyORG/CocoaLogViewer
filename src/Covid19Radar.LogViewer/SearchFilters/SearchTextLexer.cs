/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public class SearchTextLexer
	{
		private readonly string _src;

		public SearchTextLexer(string text)
		{
			_src = text ?? string.Empty;
		}

		public virtual IEnumerable<SearchTextToken> Scan()
		{
			int pos = 0;
			while (TryRead(out char ch)) {
				switch (ch) {
				case '\0':
					yield break;
				case '\b':
					--pos;
					break;
				case (>= '\u0001' and <= '\u001F') or '\u007F':
					break;
				case '\"' or '\'':
					char tail  = ch;
					int  index = pos;
					while (TryPeek(out ch) && ch != tail) {
						++pos;
					}
					++pos;
					yield return new() {
						Lexer  = this,
						Index  = index,
						Length = pos - index,
						Type   = TokenType.Text
					};
					break;
				case '(':
				case ')' or '.' or ':' or '=' or '-' or '!' or '*' or '+' or '^' or ',' or ';':
					yield return new() {
						Lexer  = this,
						Index  = pos,
						Length = 1,
						Type   = TokenType.Symbol
					};
					break;
				case '&' or '|':
					if (TryPeek(out char ch2) && ch == ch2) {
						yield return new() {
							Lexer  = this,
							Index  = pos,
							Length = 2,
							Type   = TokenType.Symbol
						};
						++pos;
					} else {
						goto case '(';
					}
					break;
				default:
					index = pos;
					while (TryPeek(out ch) && Continue(ch)) {
						++pos;
					}
					yield return new() {
						Lexer  = this,
						Index  = index - 1,
						Length = pos - index + 1,
						Type   = TokenType.Keyword
					};
					break;
				}
			}

			bool TryPeek([MaybeNullWhen(false)][NotNullWhen(true)] out char result)
			{
				if (0 <= pos && pos < _src.Length) {
					result = _src[pos];
					return true;
				} else {
					result = char.MinValue;
					return false;
				}
			}

			bool TryRead([MaybeNullWhen(false)][NotNullWhen(true)] out char result)
			{
				if (TryPeek(out result)) {
					++pos;
					return true;
				}
				return false;
			}

			static bool Continue(char c)
			{
				return c is not (
					(>= '\0' and <= '\u001F') or '\u007F'
					or
					'\"' or '\''
					or
					'(' or ')' or '.' or ':' or '=' or '-' or '!' or '*' or '+' or '^' or ',' or ';'
					or
					'&' or '|'
				);
			}
		}

		public ReadOnlySpan<char> GetText(int index, int length)
		{
			return _src.AsSpan(index, length);
		}
	}
}
