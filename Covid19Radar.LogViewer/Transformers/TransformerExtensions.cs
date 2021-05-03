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

		public static TransformerPipeline ConfigureDefaults(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline
				.UseCall()
				.UseTekItem()
				.UseUserData()
				.UseTransition();
		}

		public static TransformerPipeline UseCall(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Use(CallTransformer.Instance);
		}

		public static TransformerPipeline UseTekItem(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Use(TekItemTransformer.Instance);
		}

		public static TransformerPipeline UseUserData(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Use(UserDataTransformer.Instance);
		}

		public static TransformerPipeline UseTransition(this TransformerPipeline pipeline)
		{
			if (pipeline is null) {
				throw new ArgumentNullException(nameof(pipeline));
			}
			return pipeline.Use(TransitionTransformer.Instance);
		}
	}
}
