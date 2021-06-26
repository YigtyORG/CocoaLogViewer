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
using System.Runtime.CompilerServices;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public sealed class SearchTextParser : IDisposable
	{
		private readonly TwoWayEnumerable<SearchTextToken, IEnumerable<SearchTextToken>, IEnumerator<SearchTextToken>>.Enumerator _tokens;

		private SearchFilterNode? _result;

		public SearchTextParser(SearchTextLexer lexer)
		{
			if (lexer is null) {
				throw new ArgumentNullException(nameof(lexer));
			}
			_tokens = TwoWayEnumerable.Create(lexer.Scan(), e => e?.GetEnumerator()).GetEnumerator();
		}

		public SearchFilterNode? Parse()
		{
			if (_result is null) {
				_result = this.ParseCore();
			}
			return _result;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private SearchFilterNode? ParseCore()
		{
			return this.ParseLevel4();
		}

		private SearchFilterNode? ParseLevel4()
		{
			var left = this.ParseLevel3();
			if (left is not null) {
				_ = this.TryPeek(out var token) && token.Type == TokenType.Symbol &&
					token.GetText() is "," or ";" && _tokens.MoveNext();
				return CreateNode(left, this.ParseLevel4());
			}
			return left;

			static SearchFilterNode? CreateNode(SearchFilterNode left, SearchFilterNode? right)
			{
				if (right is null) {
					return left;
				} else if (right is SearchFilterNodeList nodeList) {
					nodeList.Nodes.Add(left);
					return nodeList;
				} else {
					var result = new SearchFilterNodeList();
					result.Nodes.Add(left);
					result.Nodes.Add(right);
					return result;
				}
			}
		}

		private SearchFilterNode? ParseLevel3()
		{
			var left = this.ParseLevel2();
			if (left is not null && this.TryPeek(out var token) && token.Type is TokenType.Keyword or TokenType.Symbol) {
				string text = token.GetText().ToUpper();
				if (text is "OR" or "IOR" or "|" or "||" or "+" && _tokens.MoveNext()) {
					var right = this.ParseLevel3();
					if (right is null) {
						return left;
					} else if (right is InclusiveDisjunctionNode disjunctionNode) {
						disjunctionNode.Nodes.Add(left);
						return disjunctionNode;
					} else {
						var result = new InclusiveDisjunctionNode();
						result.Nodes.Add(left);
						result.Nodes.Add(right);
						return result;
					}
				} else if (text is "XOR" or "^" && _tokens.MoveNext()) {
					var right = this.ParseLevel3();
					if (right is null) {
						return left;
					} else if (right is ExclusiveDisjunctionNode disjunctionNode) {
						disjunctionNode.Nodes.Add(left);
						return disjunctionNode;
					} else {
						var result = new ExclusiveDisjunctionNode();
						result.Nodes.Add(left);
						result.Nodes.Add(right);
						return result;
					}
				}
			}
			return left;
		}

		private SearchFilterNode? ParseLevel2()
		{
			var left = this.ParseLevel1();
			if (left is not null && this.TryPeek(out var token) && token.Type is TokenType.Keyword or TokenType.Symbol &&
				token.GetText().ToUpper() is "AND" or "&" or "&&" or "*" && _tokens.MoveNext()) {
				var right = this.ParseLevel2();
				if (right is null) {
					return left;
				} else if (right is ConjunctionNode conjunctionNode) {
					conjunctionNode.Nodes.Add(left);
					return conjunctionNode;
				} else {
					var result = new ConjunctionNode();
					result.Nodes.Add(left);
					result.Nodes.Add(right);
					return result;
				}
			}
			return left;
		}

		private SearchFilterNode? ParseLevel1()
		{
			var node = this.ParseLevel0();
			if (node is TokenNode tokenNode && tokenNode.Token.Type == TokenType.Keyword) {
				switch (tokenNode.Token.GetText().ToUpper()) {
				case "INCLUDE":
					return this.ParseLevel1();
				case "EXCLUDE":
				case "NOT":
				case "-":
				case "!":
					return this.ParseLevel1()?.Invert();
				}
			}
			return node;
		}

		private SearchFilterNode? ParseLevel0()
		{
			var key = this.ParseToken();
			if (key is not null && this.TryPeek(out var token) && token.Type == TokenType.Symbol &&
				token.GetText() is "." or ":" or "=" && _tokens.MoveNext()) {
				var value = this.ParseLevel0();
				if (value is not null) {
					return new RequirementNode(key, value);
				}
			}
			return key;
		}

		private SearchFilterNode? ParseToken()
		{
			if (this.TryPeek(out var token)) {
				if (token.Type == TokenType.Symbol && token.GetText() == "(") {
					if (_tokens.MoveNext()) {
						var node = this.ParseCore();
						if (this.TryPeek(out token) && token.Type == TokenType.Symbol && token.GetText() == ")") {
							_tokens.MoveNext();
						}
						return node;
					}
				} else {
					return new TokenNode(token);
				}
			}
			return null;
		}

		private bool TryPeek([MaybeNullWhen(false)][NotNullWhen(true)] out SearchTextToken result)
		{
			if (_tokens.MoveNext()) {
				result = _tokens.Current;
				return _tokens.MovePrev();
			} else {
				result = default;
				return false;
			}
		}

		public void Dispose()
		{
			_tokens.Dispose();
		}
	}
}
