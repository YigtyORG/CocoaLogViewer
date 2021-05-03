using System;
using System.Collections.Generic;

namespace Covid19Radar.LogViewer.Transformers
{
	public class TransformerPipeline : TransformerBase
	{
		private readonly List<TransformDelegate> _delegates;

		public TransformerPipeline()
		{
			_delegates = new();
		}

		public TransformerPipeline Use(ITransformer transformer)
		{
			if (transformer is null) {
				throw new ArgumentNullException(nameof(transformer));
			}
			this.UseCore(transformer.Transform);
			return this;
		}

		public TransformerPipeline Use(TransformDelegate transformDelegate)
		{
			if (transformDelegate is null) {
				throw new ArgumentNullException(nameof(transformDelegate));
			}
			this.UseCore(transformDelegate);
			return this;
		}

		public TransformerPipeline Use<TTransformer>()
			where TTransformer: ITransformer, new()
		{
			this.UseCore(new TTransformer().Transform);
			return this;
		}

		private void UseCore(TransformDelegate transformDelegate)
		{
			lock (_delegates) {
				_delegates.Add(transformDelegate);
			}
		}

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			TransformDelegate[] funcs;
			lock (_delegates) {
				funcs = _delegates.ToArray();
			}
			for (int i = funcs.Length - 1; i >= 0; --i) {
				next = new Next(funcs[i], next).Invoke;
			}
			return next(message);
		}

		public delegate string? TransformDelegate(string? message, Func<string?, string?> next);

		private readonly struct Next
		{
			private readonly TransformDelegate      _func;
			private readonly Func<string?, string?> _next;

			internal Next(TransformDelegate f, Func<string?, string?> n)
			{
				_func = f;
				_next = n;
			}

			internal string? Invoke(string? msg)
			{
				return _func(msg, _next);
			}
		}
	}
}
