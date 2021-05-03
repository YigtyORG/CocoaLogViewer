using System;

namespace Covid19Radar.LogViewer.Transformers
{
	internal sealed class TekItemTransformer : TransformerBase
	{
		private const string Prefix = "tekItem.Created: ";

		public static TekItemTransformer Instance { get; } = new();

		private TekItemTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			if ((message?.StartsWith(Prefix) ?? false) &&
				long.TryParse(message.Substring(Prefix.Length), out long milliseconds)) {
				var dto = DateTimeOffset.FromUnixTimeMilliseconds(milliseconds);
				return $"一時接触キー(TEK)が作成されました。(作成日時：{dto:yyyy\'年\'MM\'月\'dd\'日\' HH\'時\'mm\'分\'ss.fffffff\'秒\'})";
			} else {
				return next(message);
			}
		}
	}
}
