/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Windows.Controls;
using Covid19Radar.LogViewer.ViewModels;

namespace Covid19Radar.LogViewer.Views
{
	public partial class LogFileView : UserControl
	{
		public LogFileViewModel ViewModel { get; }

		public LogFileView()
		{
			this.InitializeComponent();
			this.DataContext = this.ViewModel = new(this);
		}

		private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (listView.SelectedItem is LogDataView ldv) {
				ldv.Focus();
			}
		}
	}
}
