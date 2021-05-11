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

namespace Covid19Radar.LogViewer.Launcher
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			this.InitializeComponent();
		}

		private void FormMain_Load(object sender, EventArgs e)
		{
			labelVersion.Text = VersionInfo.GetCaption();
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
				var dr = MessageBox.Show(this,
					"全ての COCOA 動作情報ファイルウィンドウを閉じます。宜しいですか？",
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
