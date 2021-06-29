/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Launcher.Extensibility;
using Covid19Radar.LogViewer.Views;
using MBOX = System.Windows.Forms.MessageBox;

namespace Covid19Radar.LogViewer.Launcher
{
	public partial class FormMain : Form, ILauncherWindow
	{
		private readonly IEnumerable<CocoaLogViewerModule> _modules;
		private readonly ModuleInitializationContext       _context;
		private          App?                              _app;
		private          FormReceiver?                     _receiver;
		private          FormSender?                       _sender;

		public FormMain(IEnumerable<CocoaLogViewerModule> modules, ModuleInitializationContext context)
		{
			_modules = modules ?? throw new ArgumentNullException(nameof(modules));
			_context = context ?? throw new ArgumentNullException(nameof(context));
			this.InitializeComponent();
		}

		private async void FormMain_Load(object sender, EventArgs e)
		{
			foreach (var module in _modules) {
				PluginLoader.Load(module, this, menuFeatures);
			}

			this                     .Text = LanguageData.Current.MainWindow_Title;
			btnOpen                  .Text = LanguageData.Current.FormMain_ButtonOpen;
			menuFeatures             .Text = LanguageData.Current.FormMain_FeaturesMenu;
			menuFeatures_showReceiver.Text = LanguageData.Current.FormMain_FeaturesMenu_ShowReceiver;
			menuFeatures_showSender  .Text = LanguageData.Current.FormMain_FeaturesMenu_ShowSender;
			cboxAllowEscape          .Text = LanguageData.Current.FormMain_CheckBoxAllowEscape;
			labelVersion             .Text = VersionInfo.GetCaption() + "\r\n" + VersionInfo.GetCopyright();

			_app = new App();
			_app.OpenWindow   = false;
			_app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
			_app.InitializeComponent();

			cboxAllowEscape.Checked = _context.AllowEscape;

			if (_context.LogFilesToOpen is not null) {
				var files = _context.LogFilesToOpen;
				int count = files.Count;
				for (int i = 0; i < count; ++i) {
					await this.OpenFileAsync(files[i]);
				}
			}
		}

		private void menuFeatures_showReceiver_Click(object sender, EventArgs e)
		{
			if (_receiver is null || _receiver.IsDisposed) {
				_receiver = new(this);
				_receiver.ShowReceiver();
			}
		}

		private void menuFeatures_showSender_Click(object sender, EventArgs e)
		{
			if (_sender is null || _sender.IsDisposed) {
				_sender = new();
				_sender.Show(this);
			}
		}

		private async void btnOpen_Click(object sender, EventArgs e)
		{
			var mwnd = this.CreateMainWindow();
			if (await mwnd.ShowOpenFileDialogAsync(cboxAllowEscape.Checked)) {
				this.ShowMainWindow(mwnd);
			}
		}

		private void viewers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (viewers.SelectedItem is MainWindow mwnd) {
				mwnd.Activate();
			}
		}

		private void Mwnd_Closing(object sender, CancelEventArgs e)
		{
			viewers.Items.Remove(sender);
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && viewers.Items.Count > 0) {
				var dr = MBOX.Show(
					this,
					LanguageData.Current.FormMain_FormClosing,
					this.Text,
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question
				);
				if (dr == DialogResult.No) {
					e.Cancel = true;
				}
			}
		}

		private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_app is not null) {
				_app.Shutdown();
			}
		}

		internal async ValueTask OpenFileAsync(string filename, bool? allowEscape = null)
		{
			var mwnd = this.CreateMainWindow();
			if (await mwnd.OpenFile(filename, allowEscape ?? cboxAllowEscape.Checked)) {
				this.ShowMainWindow(mwnd);
			}
		}

		private MainWindow CreateMainWindow()
		{
			MainWindow mwnd;
			if (_context.TransformerPipeline is not null and var transformer) {
				mwnd = new(transformer);
			} else {
				mwnd = new();
			}
			ElementHost.EnableModelessKeyboardInterop(mwnd);
			mwnd.Closing += this.Mwnd_Closing;
			return mwnd;
		}

		private void ShowMainWindow(MainWindow mwnd)
		{
			mwnd.Show();
			viewers.Items.Add(mwnd);
		}
	}
}
