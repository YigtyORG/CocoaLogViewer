/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
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
				var context = new ModuleInitializationContextInternal();
				context.Arguments = args;
				ModuleLoader.LoadModules(context);
				ShowWindow(context);
				return 0;
			} catch (Exception e) {
				MessageBox.Show(e.Message, LanguageData.Current.MainWindow_OFD_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return e.HResult;
			}
		}

		private static void ShowWindow(ModuleInitializationContext context)
		{
			Application.ThreadException += Application_ThreadException;
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormMain(context));
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			MessageBox.Show(e.Exception.Message, LanguageData.Current.MainWindow_OFD_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
