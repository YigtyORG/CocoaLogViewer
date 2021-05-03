using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Models
{
	public class LogFileModel
	{
		public IReadOnlyList<LogDataModel> Logs { get; }

		public LogFileModel(Stream stream, ITransformer transformer)
			: this(stream, transformer is null ? ThrowArgNull() : transformer.Transform) { }

		public LogFileModel(Stream stream, Func<string?, string?> transformer)
		{
			if (stream is null) {
				throw new ArgumentNullException(nameof(stream));
			}
			if (transformer is null) {
				ThrowArgNull();
			}

			var logs = new List<LogDataModel>();
			using (var sr = new StreamReader(stream, true)) {
				while (sr.ReadLine() is not null and string line) {
					var row = ParseCsv(line);
					if (row.Count == 12) {
						if (row[0] != "output_date") {
							string msg = row[02];
							logs.Add(new(
								row[00], row[01], msg, transformer(msg) ?? msg,
								row[03], row[04], row[05], row[06],
								row[07], row[08], row[09], row[10],
								row[11]
							));
						}
					} else {
						logs.Add(new(
							"無効なログ", nameof(LogLevel.Remarks), row.Aggregate((a, b) => $"{a}, {b}"), "書式が誤っているため読み込めませんでした。",
							string.Empty, string.Empty, string.Empty, string.Empty,
							string.Empty, string.Empty, string.Empty, string.Empty,
							string.Empty
						));
					}
				}
			}
			this.Logs = logs;
		}

		[DoesNotReturn()]
		private static Func<string?, string?> ThrowArgNull()
		{
			throw new ArgumentNullException("transformer");
		}

		private static List<string> ParseCsv(string line)
		{
			var  result = new List<string>();
			var  sb     = new StringBuilder();
			int  i      = 0;
			bool dq     = false;
			while (i < line.Length) {
				char ch = line[i];
				switch (ch) {
				case '\"':
					++i;
					if (i < line.Length && line[i] == '\"') {
						++i;
						sb.Append('\"');
					} else {
						dq = !dq;
					}
					break;
				case ',':
					++i;
					if (dq) {
						sb.Append(',');
					} else {
						result.Add(sb.ToString());
						sb.Clear();
					}
					break;
				case ' ': case '\0': case '\t': case '\v': case '\r': case '\n':
					++i;
					if (dq) {
						sb.Append(ch);
					}
					break;
				default:
					++i;
					sb.Append(ch);
					break;
				}
			}
			if (sb.Length > 0) {
				result.Add(sb.ToString());
			}
			return result;
		}
	}
}
