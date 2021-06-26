/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Concurrent;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public readonly struct SearchTextToken : IEquatable<SearchTextToken>
	{
		private static readonly ConcurrentDictionary<string, string> _cache = new();

		public SearchTextLexer Lexer  { get; init; }
		public int             Index  { get; init; }
		public int             Length { get; init; }
		public TokenType       Type   { get; init; }

		public string GetText()
		{
			return _cache.GetOrAdd(this.Lexer.GetText(this.Index, this.Length).ToString(), text => {
				var sb = StringBuilderCache<SearchTextToken>.Get();

				for (int i = 0; i < text.Length; ++i) {
					char ch = text[i];
					if (ch == '\\') {
						++i;
						if (i < text.Length) {
							ch = text[i];
							sb.Append(ch switch {
								't' => '\t',
								'v' => '\v',
								'r' => '\r',
								'n' => '\n',
								_ => ch
							});
						}
					} else {
						sb.Append(ch);
					}
				}

				return sb.ToString();
			});
		}

		public override bool Equals(object? obj)
		{
			if (obj is SearchTextToken other) {
				return this.Equals(other);
			}
			return false;
		}

		public bool Equals(SearchTextToken other)
		{
			return this.Lexer  == other.Lexer
				&& this.Index  == other.Index
				&& this.Length == other.Length
				&& this.Type   == other.Type;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(this.Length, this.Index, this.Length, this.Type);
		}
	}

	public enum TokenType
	{
		Text,
		Keyword,
		Symbol
	}
}
