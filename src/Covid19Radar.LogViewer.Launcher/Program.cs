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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Launcher.Extensibility;

namespace Covid19Radar.LogViewer.Launcher
{
	internal static class Program
	{
		[STAThread()]
		private static int Main(string[] args)
		{
			try {
				AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
				Application.ThreadException                += Application_ThreadException;

				Run(args);
				return 0;
			} catch (Exception e) {
				HandleException(e);
				return e.HResult;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Run(string[] args)
		{
			var context = new ModuleInitializationContextInternal(args);
			var modules = ModuleLoader.LoadModules(context);
			ShowWindow(modules, context);
		}

		private static void ShowWindow(IEnumerable<CocoaLogViewerModule> modules, ModuleInitializationContext context)
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain(modules, context));
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			if (e.ExceptionObject is Exception exception) {
				HandleException(exception);
			} else {
				HandleException(new Exception((sender, e).ToString()));
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			HandleException(e.Exception);
		}

		private static void HandleException(Exception exception)
		{
			Debug.Fail(exception.Message, exception.ToString());
			MessageBox.Show(exception.Message, LanguageData.Current.MainWindow_OFD_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

			using (var fs = new FileStream(Path.Combine(AppContext.BaseDirectory, "error_log.md"), FileMode.Append, FileAccess.Write, FileShare.None))
			using (var sw = new StreamWriter(fs, Encoding.UTF8)) {
				var    now   = DateTime.Now;
				int    pid   = Environment.ProcessId;
				string dt    = now.ToString("yyyy/MM/dd HH:mm:ss.fffffff");
				string id    = now.ToString("yyyyMMddHHmmssfffffff") + "__" + pid;
				sw.WriteLine();
				sw.WriteLine("<a id=\"{0}\"></a>", id);
				sw.WriteLine("# **[Date/Time: {0}, PID: {1}](#{2})**", dt, pid, id);
				sw.WriteLine("```log");
				sw.WriteLine(exception);
				sw.WriteLine("```");
				sw.WriteLine("----------------");
				sw.WriteLine();
			}
		}

#if DEBUG
		private static class DebugEnvironment
		{
			[STAThread()]
			[Conditional("DEBUG")]
			private static void Main(string[] args)
			{
				Run(args);
			}
		}
#endif
	}
}
