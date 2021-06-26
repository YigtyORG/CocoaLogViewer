/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public abstract class SearchFilterNode
	{
		public NegationNode Invert()
		{
			return new(this);
		}
	}

	public sealed class TokenNode : SearchFilterNode
	{
		public SearchTextToken Token { get; }

		public TokenNode(SearchTextToken token)
		{
			this.Token = token;
		}
	}

	public sealed class RequirementNode : SearchFilterNode
	{
		public SearchFilterNode Key   { get; }
		public SearchFilterNode Value { get; }

		public RequirementNode(SearchFilterNode key, SearchFilterNode value)
		{
			this.Key   = key   ?? throw new ArgumentNullException(nameof(key));
			this.Value = value ?? throw new ArgumentNullException(nameof(value));
		}
	}

	public sealed class NegationNode : SearchFilterNode
	{
		public SearchFilterNode Node { get; }

		public NegationNode(SearchFilterNode node)
		{
			this.Node = node ?? throw new ArgumentNullException(nameof(node));
		}
	}

	public class SearchFilterNodeList : SearchFilterNode
	{
		public List<SearchFilterNode> Nodes { get; }

		public SearchFilterNodeList()
		{
			this.Nodes = new();
		}
	}

	public sealed class ConjunctionNode : SearchFilterNodeList { }

	public abstract class DisjunctionNode : SearchFilterNodeList { }

	public sealed class InclusiveDisjunctionNode : DisjunctionNode { }

	public sealed class ExclusiveDisjunctionNode : DisjunctionNode { }
}
