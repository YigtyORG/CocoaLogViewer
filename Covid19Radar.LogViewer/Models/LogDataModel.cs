/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Globalization;
using System.Text;
using System.Windows.Documents;
using System.Windows.Media;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Models
{
	public record LogDataModel(
		string Timestamp,
		string Level,
		string OriginalMessage,
		string TransformedMessage,
		string Method,
		string FilePath,
		string LineNumber,
		string Platform,
		string PlatformVersion,
		string DeviceModel,
		string DeviceType,
		string Version,
		string BuildNumber)
	{
		[ThreadStatic()]
		private static StringBuilder? _sb;

		private static readonly SolidColorBrush _method_color    = new(Color.FromRgb(0x00, 0x80, 0xFF));
		private static readonly SolidColorBrush _file_path_color = new(Color.FromRgb(0x80, 0x40, 0x00));
		private static readonly SolidColorBrush _line_num_color  = new(Color.FromRgb(0x80, 0xFF, 0x80));

		public bool TryGetDateTime(out DateTime result)
		{
			return DateTime.TryParseExact(this.Timestamp, "yyyy/MM/dd HH:mm:ss",         null, DateTimeStyles.None, out result)
				|| DateTime.TryParseExact(this.Timestamp, "yyyy/MM/dd HH:mm:ss.fff",     null, DateTimeStyles.None, out result)
				|| DateTime.TryParseExact(this.Timestamp, "yyyy/MM/dd HH:mm:ss.fffffff", null, DateTimeStyles.None, out result)
				|| DateTime.TryParse(this.Timestamp, out result);
		}

		public string GetDateTimeAsString()
		{
			if (this.TryGetDateTime(out var dt)) {
				return dt.ToString(LanguageData.Current.LogDataModel_DateTime_Format);
			} else {
				return this.Timestamp;
			}
		}

		public LogLevel GetLogLevel()
		{
			return LogLevel.Parse(this.Level);
		}

		public string GetLocation()
		{
			return $"{this.Method} \"{this.FilePath}\"({this.LineNumber})";
		}

		public FlowDocument GetLocationAsFlowDocument()
		{
			return new(new Paragraph() {
				Inlines = {
					new Bold(new Run(this.Method) { Foreground = _method_color }),
					new Run(" \""),
					new Italic(new Run(this.FilePath) { Foreground = _file_path_color }),
					new Run("\"("),
					new Run(this.LineNumber) { Foreground = _line_num_color },
					new Run(")"),
				}
			});
		}

		public string CreateDetails()
		{
			if (_sb is null) {
				_sb = new StringBuilder();
			} else {
				_sb.Clear();
			}
			return this.CreateDetails(_sb);
		}

		public string CreateDetails(StringBuilder sb)
		{
			sb.AppendFormat(
				LanguageData.Current.LogDataModel_DateTime,
				this.GetDateTimeAsString()
			).AppendLine();
			sb.AppendFormat(
				LanguageData.Current.LogDataModel_LogLevel,
				this.GetLogLevel().Text
			).AppendLine();
			sb.AppendFormat(
				LanguageData.Current.LogDataModel_Location,
				this.GetLocation()
			).AppendLine();
			if (this.OriginalMessage == this.TransformedMessage) {
				sb.AppendFormat(
					LanguageData.Current.LogDataModel_Message,
					this.OriginalMessage
				).AppendLine();
			} else {
				sb.AppendFormat(
					LanguageData.Current.LogDataModel_Message_Transformed,
					this.TransformedMessage
				).AppendLine();
				sb.AppendFormat(
					LanguageData.Current.LogDataModel_Message_Original,
					this.OriginalMessage
				).AppendLine();
			}
			sb.AppendFormat(
				LanguageData.Current.LogDataModel_Platform,
				this.Platform,
				this.PlatformVersion
			).AppendLine();
			sb.AppendFormat(
				LanguageData.Current.LogDataModel_Device,
				this.DeviceModel,
				this.DeviceType
			).AppendLine();
			sb.AppendFormat(
				LanguageData.Current.LogDataModel_Version,
				this.Version,
				this.BuildNumber
			).AppendLine();
			return sb.ToString();
		}
	}
}
