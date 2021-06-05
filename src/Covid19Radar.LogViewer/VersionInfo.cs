/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
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
			if (_caption is null) {
				_caption = GetCaption(Assembly.GetExecutingAssembly());
			}
			return _caption;
		}

		public static string GetCaption(Assembly asm)
		{
			return GetCaption(
				asm.GetCustomAttribute<AssemblyProductAttribute>()?.Product,
				asm.GetName().Version,
				asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion,
				asm.GetCustomAttribute<AssemblyConfigurationAttribute>()?.Configuration
			);
		}

		public static string GetCaption(string? name, Version? version, string? codeName, string? buildConfiguration)
		{
			string  unknown = LanguageData.Current.VersionInfo_Unknown;
			string  v       = version?.ToString(4) ?? "?.?.?.?";
			string  cn      = codeName             ?? unknown;
			string  bc      = buildConfiguration   ?? unknown;

			return bc switch {
				Debug   => $"{name} - {LanguageData.Current.VersionInfo_Debug} [v{v}, cn:{cn}]",
				Release => $"{name} [v{v}, cn:{cn}]",
				_       => $"{name} [v{v}, cn:{cn}, bc:{bc}]"
			};
		}

		public static string GetCopyright()
		{
			if (_copyright is null) {
				_copyright = GetCopyright(Assembly.GetExecutingAssembly());
			}
			return _copyright;
		}

		public static string GetCopyright(Assembly asm)
		{
			return asm.GetCustomAttribute<AssemblyCopyrightAttribute>()?.Copyright ?? LanguageData.Current.VersionInfo_Unknown;
		}
	}
}
