using System;

namespace Covid19Radar.LogViewer.Transformers
{
	internal sealed class CallTransformer : TransformerBase
	{
		private const string Start = "処理を開始しました。";
		private const string End   = "処理を終了しました。";

		public static CallTransformer Instance { get; } = new();

		private CallTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			return message switch {
				nameof(Start) => Start,
				nameof(End)   => End,
				_ => next(message)
			};
		}
	}
}
