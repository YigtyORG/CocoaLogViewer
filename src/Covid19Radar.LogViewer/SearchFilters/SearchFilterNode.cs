/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public abstract class SearchFilterNode
	{
		public bool Match(LogDataModel model)
		{
			if (model is null) {
				throw new ArgumentNullException(nameof(model));
			}
			return this.MatchCore(model);
		}

		public NegationNode Invert()
		{
			return new(this);
		}

		protected abstract bool MatchCore(LogDataModel model);
	}

	public sealed class TokenNode : SearchFilterNode
	{
		public SearchTextToken Token { get; }

		public TokenNode(SearchTextToken token)
		{
			this.Token = token;
		}

		protected override bool MatchCore(LogDataModel model)
		{
			string text = this.Token.GetText();
			return model.Timestamp         .Contains(text)
				|| model.Level             .Contains(text)
				|| model.OriginalMessage   .Contains(text)
				|| model.TransformedMessage.Contains(text)
				|| model.Method            .Contains(text)
				|| model.FilePath          .Contains(text)
				|| model.LineNumber        .Contains(text)
				|| model.Platform          .Contains(text)
				|| model.PlatformVersion   .Contains(text)
				|| model.DeviceModel       .Contains(text)
				|| model.DeviceType        .Contains(text)
				|| model.Version           .Contains(text)
				|| model.BuildNumber       .Contains(text);
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

		protected override bool MatchCore(LogDataModel model)
		{
			if (this.Key is TokenNode node) {
				return SearchFilterRegistry.Get(node.Token.GetText())?.Match(this.Value, model) ?? false;
			} else {
				return false;
			}
		}
	}

	public sealed class NegationNode : SearchFilterNode
	{
		public SearchFilterNode Node { get; }

		public NegationNode(SearchFilterNode node)
		{
			this.Node = node ?? throw new ArgumentNullException(nameof(node));
		}

		protected override bool MatchCore(LogDataModel model)
		{
			return !this.Node.Match(model);
		}
	}

	public class SearchFilterNodeList : SearchFilterNode
	{
		public List<SearchFilterNode> Nodes { get; }

		public SearchFilterNodeList()
		{
			this.Nodes = new();
		}

		protected override bool MatchCore(LogDataModel model)
		{
			int    count = this.Nodes.Count;
			double match = 0;
			for (int i = 0; i < count; ++i) {
				if (this.Nodes[i].Match(model)) {
					++match;
				}
			}
			return (match / count) >= 0.5D;
		}
	}

	public sealed class ConjunctionNode : SearchFilterNodeList
	{
		protected override bool MatchCore(LogDataModel model)
		{
			int count = this.Nodes.Count;
			for (int i = 0; i < count; ++i) {
				if (!this.Nodes[i].Match(model)) {
					return false;
				}
			}
			return true;
		}
	}

	public abstract class DisjunctionNode : SearchFilterNodeList { }

	public sealed class InclusiveDisjunctionNode : DisjunctionNode
	{
		protected override bool MatchCore(LogDataModel model)
		{
			int count = this.Nodes.Count;
			for (int i = 0; i < count; ++i) {
				if (this.Nodes[i].Match(model)) {
					return true;
				}
			}
			return false;
		}
	}

	public sealed class ExclusiveDisjunctionNode : DisjunctionNode
	{
		protected override bool MatchCore(LogDataModel model)
		{
			int  count  = this.Nodes.Count;
			bool result = false;
			for (int i = 0; i < count; ++i) {
				result ^= this.Nodes[i].Match(model);
			}
			return result;
		}
	}
}
