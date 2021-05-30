/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Reflection;
using Covid19Radar.LogViewer.Globalization;

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
			return GetCaption(Assembly.GetExecutingAssembly());
		}

		public static string GetCaption(Assembly asm)
		{
			if (_caption is null) {
				string  unknown = LanguageData.Current.VersionInfo_Unknown;
				string? name    = asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product;
				string  v       = asm.GetName().Version?.ToString(4) ?? "?.?.?.?";
				string  cn      = asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? unknown;
				string  bc      = asm.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration ?? unknown;
				switch (bc) {
				case Debug:
					_caption = $"{name} - {LanguageData.Current.VersionInfo_Debug} [v{v}, cn:{cn}]";
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
			return GetCopyright(Assembly.GetExecutingAssembly());
		}

		public static string GetCopyright(Assembly asm)
		{
			if (_copyright is null) {
				_copyright = asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? LanguageData.Current.VersionInfo_Unknown;
			}
			return _copyright;
		}
	}
}
