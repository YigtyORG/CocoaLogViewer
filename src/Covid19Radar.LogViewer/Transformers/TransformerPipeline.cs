/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections;
using System.Collections.Generic;

namespace Covid19Radar.LogViewer.Transformers
{
	public class TransformerPipeline : TransformerBase, IEnumerable<TransformDelegate>, IEnumerable<ITransformer>
	{
		private readonly List<TransformDelegate> _delegates;

		public TransformerPipeline()
		{
			_delegates = new();
		}

		public TransformerPipeline Add(ITransformer transformer)
		{
			if (transformer is null) {
				throw new ArgumentNullException(nameof(transformer));
			}
			if (transformer is TransformDelegateWrapper wrapper) {
				this.AddCore(wrapper.Delegate);
			} else {
				this.AddCore(transformer.Transform);
			}
			return this;
		}

		public TransformerPipeline Add(TransformDelegate transformDelegate)
		{
			if (transformDelegate is null) {
				throw new ArgumentNullException(nameof(transformDelegate));
			}
			this.AddCore(transformDelegate);
			return this;
		}

		public TransformerPipeline Add<TTransformer>()
			where TTransformer: ITransformer, new()
		{
			this.AddCore(new TTransformer().Transform);
			return this;
		}

		private void AddCore(TransformDelegate transformDelegate)
		{
			lock (_delegates) {
				_delegates.Add(transformDelegate);
			}
		}

		public virtual Func<string?, string> Build(Func<string?, string> final)
		{
			TransformDelegate[] funcs;
			lock (_delegates) {
				funcs = _delegates.ToArray();
			}
			var next = final;
			for (int i = funcs.Length - 1; i >= 0; --i) {
				next = new Next(funcs[i], next).Invoke;
			}
			return next;
		}

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			if (string.IsNullOrEmpty(message)) {
				return string.Empty;
			}

			return this.Build(next)(message);
		}

		public List<TransformDelegate>.Enumerator GetEnumerator()
		{
			return _delegates.GetEnumerator();
		}

		IEnumerator<TransformDelegate> IEnumerable<TransformDelegate>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		IEnumerator<ITransformer> IEnumerable<ITransformer>.GetEnumerator()
		{
			foreach (var item in this) {
				yield return item.ToTransformer();
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private readonly struct Next
		{
			private readonly TransformDelegate     _func;
			private readonly Func<string?, string> _next;

			internal Next(TransformDelegate f, Func<string?, string> n)
			{
				_func = f;
				_next = n;
			}

			internal string Invoke(string? msg)
			{
				return _func(msg, _next);
			}
		}
	}
}
