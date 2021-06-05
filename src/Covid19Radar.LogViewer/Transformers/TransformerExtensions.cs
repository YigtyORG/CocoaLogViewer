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
	public static class TransformerExtensions
	{
		public static string Transform(this ITransformer transformer, string? message)
		{
			if (transformer is null) {
				throw new ArgumentNullException(nameof(transformer));
			}
			return transformer.Transform(message, message => message) ?? message ?? string.Empty;
		}

		public static ITransformer ToTransformer(this TransformDelegate transformDelegate)
		{
			if (transformDelegate is null) {
				throw new ArgumentNullException(nameof(transformDelegate));
			}
			return new TransformDelegateWrapper(transformDelegate);
		}

		public static FuncTransformer ToTransformer(this Func<string?, string?> func)
		{
			if (func is null) {
				throw new ArgumentNullException(nameof(func));
			}
			return new(func);
		}

		public static TransformerPipeline ConfigureDefaults(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline
				.AddControlCharTransformer()
				.AddCallTransformer()
				.AddTekItemTransformer()
				.AddUserDataTransformer()
				.AddTransitionTransformer();
		}

		public static TransformerPipeline AddControlCharTransformer(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Add(ControlCharTransformer.Instance);
		}

		public static TransformerPipeline AddCallTransformer(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Add(CallTransformer.Instance);
		}

		public static TransformerPipeline AddTekItemTransformer(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Add(TekItemTransformer.Instance);
		}

		public static TransformerPipeline AddUserDataTransformer(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Add(UserDataTransformer.Instance);
		}

		public static TransformerPipeline AddTransitionTransformer(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Add(TransitionTransformer.Instance);
		}

		public readonly struct FuncTransformer : ITransformer
		{
			private readonly Func<string?, string?>? _func;

			public FuncTransformer(Func<string?, string?>? func)
			{
				_func = func;
			}

			public string? Transform(string? message, Func<string?, string?> next)
			{
				return _func?.Invoke(message) ?? next(message);
			}
		}
	}
}
