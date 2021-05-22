/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Launcher
{
	public partial class FormMain : Form
	{
		private readonly string[]? _args;

		public FormMain(string[]? args)
		{
			_args = args;
			this.InitializeComponent();
			this   .Text = LanguageData.Current.MainWindow_Title;
			btnOpen.Text = LanguageData.Current.FormMain_ButtonOpen;
		}

		private async void FormMain_Load(object sender, EventArgs e)
		{
			labelVersion.Text = VersionInfo.GetCaption();

			if (_args is not null && _args.Length == 1) {
				var mwnd = new MainWindow();
				mwnd.Closing += this.Mwnd_Closing;
				if (await mwnd.OpenFile(_args[0])) {
					mwnd.Show();
					viewers.Items.Add(mwnd);
				}
			}
		}

		private async void btnOpen_Click(object sender, EventArgs e)
		{
			var mwnd = new MainWindow();
			mwnd.Closing += this.Mwnd_Closing;
			if (await mwnd.ShowOpenFileDialogAsync()) {
				mwnd.Show();
				viewers.Items.Add(mwnd);
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
	}
}
