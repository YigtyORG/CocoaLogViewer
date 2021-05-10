/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace Covid19Radar.LogViewer.Transformers
{
	public interface ITransformer
	{
		public string? Transform(string? message, Func<string?, string?> next);
	}

	public abstract class TransformerBase : ITransformer
	{
		public string? Transform(string? message, Func<string?, string?> next)
		{
			if (next is null) {
				throw new ArgumentNullException(nameof(next));
			}
			return this.TransformCore(message, next);
		}

		protected abstract string? TransformCore(string? message, Func<string?, string?> next);
	}

	public delegate string? TransformDelegate(string? message, Func<string?, string?> next);

	internal sealed class TransformDelegateWrapper : ITransformer
	{
		internal TransformDelegate Delegate { get; }

		internal TransformDelegateWrapper(TransformDelegate transformDelegate)
		{
			this.Delegate = transformDelegate;
		}

		public string? Transform(string? message, Func<string?, string?> next)
		{
			return this.Delegate(message, next);
		}
	}
}
