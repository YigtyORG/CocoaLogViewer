using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
						timestamp.Text = value.GetDateTimeAsString();
						var logLevel      = value.GetLogLevel();
						level.Text = logLevel.Text;
						level.Background = logLevel.BackColor;
						location.Document = value.GetLocationAsFlowDocument();
						message.Text = value.TransformedMessage;
					}
				}
			}
		}

		public LogDataView()
		{
			this.InitializeComponent();
		}

		private void details_Click(object sender, RoutedEventArgs e)
		{
			if (_log_data is not null) {
				MessageBox.Show(
					_log_data.CreateDetails(),
					"詳細情報",
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
					"クリップボードにログ情報をコピーしました。",
					"コピー",
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
