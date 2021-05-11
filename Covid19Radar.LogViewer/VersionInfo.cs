/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Reflection;

namespace Covid19Radar.LogViewer
{
	public static class VersionInfo
	{
		public const string Debug   = nameof(Debug);
		public const string Release = nameof(Release);

		private static string? _caption;
		private static string? _copyright;

		public static string GetCaption()
		{
			if (_caption is null) {
				var     asm  = Assembly.GetExecutingAssembly();
				string? name = asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
				string  v    = asm.GetName().Version?.ToString(4) ?? "?.?.?.?";
				string  cn   = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? "unknown";
				string  bc   = asm.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration?? "<Unknown>";
				switch (bc) {
				case Debug:
					_caption = $"{name} - DEBUG [v{v}, cn:{cn}]";
					break;
				case Release:
					_caption = $"{name} [v{v}, cn:{cn}]";
					break;
				default:
					_caption = $"{name} [v{v}, cn:{cn}, bc:{bc}]";
					break;
				}
			}
			return _caption;
		}

		public static string GetCopyright()
		{
			if (_copyright is null) {
				_copyright = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? "Unknown.";
			}
			return _copyright;
		}
	}
}
