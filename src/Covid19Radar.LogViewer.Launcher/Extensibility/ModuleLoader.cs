/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Launcher.Extensibility
{
	internal static class ModuleLoader
	{
		private static readonly EnumerationOptions _eo = new() {
			AttributesToSkip         = FileAttributes.Hidden | FileAttributes.System,
			BufferSize               = 0,
			IgnoreInaccessible       = true,
			MatchCasing              = MatchCasing.PlatformDefault,
			MatchType                = MatchType.Win32,
			RecurseSubdirectories    = true,
			ReturnSpecialDirectories = false
		};

		internal static IEnumerable<CocoaLogViewerModule> LoadModules(ModuleInitializationContext context)
		{
			return LoadModules(context, AppContext.BaseDirectory, "*.dll");
		}

		internal static IEnumerable<CocoaLogViewerModule> LoadModules(ModuleInitializationContext context, string dir, string pattern)
		{
			context.ParseArguments();
			if (context.DisallowExtensions || !Directory.Exists(dir)) {
				yield break;
			}
			foreach (string fname in Directory.EnumerateFiles(dir, pattern, _eo)) {
				if (LoadModuleCore(fname) is not null and var mod) {
					mod.Initialize(context);
					yield return mod;
				}
			}
		}

		private static CocoaLogViewerModule? LoadModuleCore(string fname)
		{
			try {
				var asm   = Assembly.LoadFrom(fname);
				var addon = asm.GetCustomAttribute<CocoaLogViewerAddonAttribute>();
				if (addon is null) {
					return null;
				}
				return Activator.CreateInstance(addon.ModuleType) as CocoaLogViewerModule;
			} catch (Exception e) {
				MessageBox.Show(
					string.Format(LanguageData.Current.ModuleLoader_Failed_Message, fname, e.Message),
					LanguageData.Current.ModuleLoader_Failed_Title,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
				);
				Debug.Fail(e.ToString());
				return null;
			}
		}
	}
}
