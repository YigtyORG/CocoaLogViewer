/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Concurrent;
using System.Windows.Media;

namespace Covid19Radar.LogViewer.Models
{
	public sealed class LogLevel
	{
		private static readonly ConcurrentDictionary<string, LogLevel> _unknowns = new();

		public static LogLevel Unknown { get; } = new("不明", new(Color.FromRgb(0xA0, 0xA0, 0xA0)));
		public static LogLevel Verbose { get; } = new("詳細", new(Color.FromRgb(0xCC, 0xCC, 0xCC)));
		public static LogLevel Debug   { get; } = new("検査", new(Color.FromRgb(0xC0, 0xC0, 0xFF)));
		public static LogLevel Info    { get; } = new("情報", new(Color.FromRgb(0xFF, 0xFF, 0xFF)));
		public static LogLevel Warning { get; } = new("警告", new(Color.FromRgb(0xE0, 0xFF, 0x80)));
		public static LogLevel Error   { get; } = new("失敗", new(Color.FromRgb(0xFF, 0xC0, 0xC0)));
		public static LogLevel Remarks { get; } = new("注釈", new(Color.FromRgb(0xFF, 0x80, 0xFF)));

		public string          Text      { get; }
		public SolidColorBrush BackColor { get; }

		private LogLevel(string text, SolidColorBrush brush)
		{
			this.Text      = text;
			this.BackColor = brush;
		}

		public static LogLevel Parse(string text)
		{
			return text switch {
				nameof(Unknown) => Unknown,
				nameof(Verbose) => Verbose,
				nameof(Debug)   => Debug,
				nameof(Info)    => Info,
				nameof(Warning) => Warning,
				nameof(Error)   => Error,
				nameof(Remarks) => Remarks,
				_ => _unknowns.GetOrAdd(text, text => new($"{Unknown.Text} ({text})", Unknown.BackColor))
			};
		}
	}
}
