using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Covid19Radar.LogViewer.Models
{
	public class LogFileModel
	{
		public IReadOnlyList<LogDataModel> Logs { get; }

		public LogFileModel(Stream stream)
		{
			var logs = new List<LogDataModel>();
			using (var sr = new StreamReader(stream, true)) {
				while (sr.ReadLine() is not null and string line) {
					var row = ParseCsv(line);
					if (row.Count == 12) {
						if (row[0] != "output_date") {
							logs.Add(new(
								row[00], row[01], row[02], row[03],
								row[04], row[05], row[06], row[07],
								row[08], row[09], row[10], row[11]
							));
						}
					} else {
						logs.Add(new(
							"無効なログ", nameof(LogLevel.Remarks), row.Aggregate((a, b) => $"{a}, {b}"),
							string.Empty, string.Empty, string.Empty, string.Empty,
							string.Empty, string.Empty, string.Empty, string.Empty,
							string.Empty
						));
					}
				}
			}
			this.Logs = logs;
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
