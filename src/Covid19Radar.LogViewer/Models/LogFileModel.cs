/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Models
{
	public class LogFileModel
	{
		public IReadOnlyList<LogDataModel> Logs { get; }

		public LogFileModel(Stream stream, ITransformer transformer, bool allowEscape)
			: this(stream, transformer.ToFunc(), allowEscape) { }

		public LogFileModel(Stream stream, Func<string?, string> transformer, bool allowEscape)
		{
			if (stream is null) {
				throw new ArgumentNullException(nameof(stream));
			}
			if (transformer is null) {
				throw new ArgumentNullException(nameof(transformer));
			}

			var logs = new List<LogDataModel>();
			using (var sr = new StreamReader(stream, true)) {
				while (sr.ReadLine() is not null and string line) {
					var row = ParseCsv(line, allowEscape);
					if (row.Count == 12) {
						if (row[0] != "output_date") {
							string msg = row[02];
							logs.Add(new(
								row[00], row[01], msg, transformer(msg) ?? msg,
								row[03], row[04], row[05], row[06],
								row[07], row[08], row[09], row[10],
								row[11], PrivacyValidator.Check(msg)
							));
						}
					} else {
						logs.Add(new(
							LanguageData.Current.LogFileModel_InvalidLog_Short,
							nameof(LogLevel.Remarks), row.Aggregate((a, b) => $"{a}, {b}"),
							LanguageData.Current.LogFileModel_InvalidLog_Long,
							string.Empty, string.Empty, string.Empty, string.Empty,
							string.Empty, string.Empty, string.Empty, string.Empty,
							string.Empty, false
						));
					}
				}
			}
			this.Logs = logs;
		}

		private static List<string> ParseCsv(string line, bool allowEscape)
		{
			var  result = new List<string>();
			var  sb     = StringBuilderCache<LogFileModel>.Get();
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
				case '\\' when allowEscape:
					++i;
					if (i < line.Length) {
						ch = line[i];
						++i;
						sb.Append(ch switch {
							't' => '\t',
							'v' => '\v',
							'r' => '\r',
							'n' => '\n',
							_ => ch
						});
					} else {
						sb.Append('\\');
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
