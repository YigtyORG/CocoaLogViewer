/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Windows;
using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer
{
	public partial class App : Application
	{
		public bool OpenWindow { get; set; } = true;

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			if (this.OpenWindow) {
				this.MainWindow = new MainWindow();
				this.MainWindow.Show();
			}
		}
	}
}
