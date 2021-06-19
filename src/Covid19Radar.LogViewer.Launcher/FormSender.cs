/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Launcher
{
	public partial class FormSender : Form
	{
		public FormSender()
		{
			this.InitializeComponent();
		}

		private void FormSender_Load(object sender, EventArgs e)
		{
			this        .Text   = LanguageData.Current.FormSender_Title;
			labelAddress.Text   = LanguageData.Current.FormSender_Label_Address;
			labelPort   .Text   = LanguageData.Current.FormSender_Label_Port;
			labelFile   .Text   = LanguageData.Current.FormSender_Label_File;
			btnCancel   .Text   = LanguageData.Current.FormSender_Button_Cancel;
			btnSend     .Text   = LanguageData.Current.FormSender_Button_Send;
			ofd         .Title  = LanguageData.Current.MainWindow_OFD_Title;
			ofd         .Filter = LanguageData.Current.MainWindow_OFD_Filter();
		}

		private async void btnSend_Click(object sender, EventArgs e)
		{
			using (var client = new TcpClient(tboxAddress.Text, ((int)(nudPort.Value)))) {
				var ns = client.GetStream();
				await using (ns.ConfigureAwait(false)) {
					var fs = new FileStream(tboxFile.Text, FileMode.Create, FileAccess.Write, FileShare.None);
					await using (fs.ConfigureAwait(false)) {
						await fs.CopyToAsync(ns);
					}
				}
			}
			this.Invoke(new Action(this.Close));
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOpenFile_Click(object sender, EventArgs e)
		{
			if (ofd.ShowDialog() == DialogResult.OK) {
				tboxFile.Text = ofd.FileName;
			}
		}
	}
}
