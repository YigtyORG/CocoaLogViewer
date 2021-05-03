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
}
