/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.Views
{
	public partial class LogDataView : UserControl
	{
		private LogDataModel? _log_data;

		public LogDataModel? LogData
		{
			get => _log_data;
			set
			{
				if (_log_data != value) {
					_log_data = value;
					if (value is not null) {
						timestamp.Text    = value.GetDateTimeAsString();
						var logLevel      = value.GetLogLevel();
						level.Text        = logLevel.Text;
						level.Background  = logLevel.BackColor;
						location.Document = value.GetLocationAsFlowDocument();
						message.Text      = value.TransformedMessage;
					}
				}
			}
		}

		public LogDataView()
		{
			this.InitializeComponent();
			details.Content = LanguageData.Current.LogDataView_Details;
			copy   .Content = LanguageData.Current.LogDataView_Copy;
		}

		private void details_Click(object sender, RoutedEventArgs e)
		{
			if (_log_data is not null) {
				MessageBox.Show(
					_log_data.CreateDetails(),
					LanguageData.Current.LogDataView_DetailedInformation,
					MessageBoxButton.OK,
					MessageBoxImage.Information
				);
			}
		}

		private void copy_Click(object sender, RoutedEventArgs e)
		{
			if (_log_data is not null) {
				Clipboard.SetText(_log_data.CreateDetails());
				MessageBox.Show(
					LanguageData.Current.LogDataView_Copy_MessageBox,
					LanguageData.Current.LogDataView_Copy,
					MessageBoxButton.OK,
					MessageBoxImage.Information
				);
			}
		}

		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			base.OnMouseWheel(e);
			e.Handled = false;
		}
	}
}
