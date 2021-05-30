/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Launcher
{
	public partial class FormMain : Form
	{
		private readonly ModuleInitializationContext _context;

		public FormMain(ModuleInitializationContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			this.InitializeComponent();
			this           .Text = LanguageData.Current.MainWindow_Title;
			btnOpen        .Text = LanguageData.Current.FormMain_ButtonOpen;
			cboxAllowEscape.Text = LanguageData.Current.FormMain_CheckBoxAllowEscape;
		}

		private async void FormMain_Load(object sender, EventArgs e)
		{
			labelVersion.Text = VersionInfo.GetCaption();

			var app = new App();
			app.OpenWindow = false;
			app.InitializeComponent();

			if (_context.Arguments is not null and var args && args.Length >= 1) {
				var mwnd = this.CreateMainWindow();
				cboxAllowEscape.Checked = args.Contains("--allow-escape");
				if (await mwnd.OpenFile(args[0], cboxAllowEscape.Checked)) {
					this.ShowMainWindow(mwnd);
				}
			}
		}

		private async void btnOpen_Click(object sender, EventArgs e)
		{
			var mwnd = this.CreateMainWindow();
			if (await mwnd.ShowOpenFileDialogAsync(cboxAllowEscape.Checked)) {
				this.ShowMainWindow(mwnd);
			}
		}

		private void Mwnd_Closing(object sender, CancelEventArgs e)
		{
			viewers.Items.Remove(sender);
		}

		private void viewers_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (viewers.SelectedItem is MainWindow mwnd) {
				mwnd.Activate();
			}
		}

		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing && viewers.Items.Count > 0) {
				var dr = MessageBox.Show(
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

		private MainWindow CreateMainWindow()
		{
			MainWindow mwnd;
			if (_context.TransformerPipeline is not null and var transformer) {
				mwnd = new(transformer);
			} else {
				mwnd = new();
			}
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
