/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Launcher
{
	public partial class FormReceiver : Form
	{
		private bool _receiving;

		public FormReceiver()
		{
			this.InitializeComponent();
		}

		private void FormReceiver_Load(object sender, EventArgs e)
		{
			this            .Text = LanguageData.Current.FormReceiver_Title;
			labelDescription.Text = LanguageData.Current.FormReceiver_Description;

			_receiving = true;
		}

		private void FormReceiver_FormClosing(object sender, FormClosingEventArgs e)
		{
			_receiving = false;
		}
	}
}
