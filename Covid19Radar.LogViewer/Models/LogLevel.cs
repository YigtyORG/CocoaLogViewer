/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Concurrent;
using System.Windows.Media;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Models
{
	public sealed class LogLevel
	{
		private static readonly ConcurrentDictionary<string, LogLevel> _unknowns = new();

		public static LogLevel Unknown { get; } = new(nameof(Unknown), new(Color.FromRgb(0xA0, 0xA0, 0xA0)));
		public static LogLevel Verbose { get; } = new(nameof(Verbose), new(Color.FromRgb(0xCC, 0xCC, 0xCC)));
		public static LogLevel Debug   { get; } = new(nameof(Debug),   new(Color.FromRgb(0xC0, 0xC0, 0xFF)));
		public static LogLevel Info    { get; } = new(nameof(Info),    new(Color.FromRgb(0xFF, 0xFF, 0xFF)));
		public static LogLevel Warning { get; } = new(nameof(Warning), new(Color.FromRgb(0xE0, 0xFF, 0x80)));
		public static LogLevel Error   { get; } = new(nameof(Error),   new(Color.FromRgb(0xFF, 0xC0, 0xC0)));
		public static LogLevel Remarks { get; } = new(nameof(Remarks), new(Color.FromRgb(0xFF, 0x80, 0xFF)));

		public string          RawText   { get; }
		public SolidColorBrush BackColor { get; }

		public string Text
		{
			get
			{
				return this.RawText switch {
					nameof(Unknown) => LanguageData.Current.LogLevel_Unknown,
					nameof(Verbose) => LanguageData.Current.LogLevel_Verbose,
					nameof(Debug)   => LanguageData.Current.LogLevel_Debug,
					nameof(Info)    => LanguageData.Current.LogLevel_Info,
					nameof(Warning) => LanguageData.Current.LogLevel_Warning,
					nameof(Error)   => LanguageData.Current.LogLevel_Error,
					nameof(Remarks) => LanguageData.Current.LogLevel_Remarks,
					_ => $"{LanguageData.Current.LogLevel_Unknown} ({this.RawText})"
				};
			}
		}

		private LogLevel(string rawtext, SolidColorBrush brush)
		{
			this.RawText   = rawtext;
			this.BackColor = brush;
		}

		public static LogLevel Parse(string text)
		{
			return text.Trim() switch {
				nameof(Unknown) => Unknown,
				nameof(Verbose) => Verbose,
				nameof(Debug)   => Debug,
				nameof(Info)    => Info,
				nameof(Warning) => Warning,
				nameof(Error)   => Error,
				nameof(Remarks) => Remarks,
				_ => _unknowns.GetOrAdd(text, text => new(text, Unknown.BackColor))
			};
		}
	}
}
