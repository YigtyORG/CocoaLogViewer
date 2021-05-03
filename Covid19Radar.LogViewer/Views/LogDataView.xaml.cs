using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.Transformers;

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
						message .Text     = value.TransformedMessage;
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
			MessageBox.Show(
				$"プラットフォーム: {_log_data?.Platform} (バージョン: {_log_data?.PlatformVersion})\r\n" +
				$"機器: {_log_data?.DeviceModel} (種類: {_log_data?.DeviceType})\r\n" +
				$"アプリのバージョン: {_log_data?.Version} (ビルド番号: {_log_data?.BuildNumber})",
				"詳細情報",
				MessageBoxButton.OK,
				MessageBoxImage.Information
			);
		}

		private void copy_Click(object sender, RoutedEventArgs e)
		{
			if (_log_data is not null) {
				Clipboard.SetText(
					$"日時：{_log_data.GetDateTimeAsString()}\r\n" +
					$"ログレベル：{_log_data.GetLogLevel().Text}\r\n" +
					$"場所：{_log_data.GetLocation()}\r\n" +
					$"翻訳された内容：{_log_data.TransformedMessage}\r\n" +
					$"元の内容：{_log_data.OriginalMessage}"
				);
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
