using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Views
{
	public partial class LogFileView : UserControl
	{
		private static readonly TransformerPipeline            _transformer;
		private        readonly Action<ListView, LogDataModel> _add_items;
		private                 LogFileModel?                  _log_file;

		public LogFileModel? LogFile
		{
			get => _log_file;
			set
			{
				if (_log_file is null) {
					_log_file = value;
					if (value is not null) {
						this.AddItems(value.Logs);
					}
				}
			}
		}

		static LogFileView()
		{
			_transformer = new();
			_transformer.ConfigureDefaults();
		}

		public LogFileView()
		{
			_add_items = AddItem;
			this.InitializeComponent();
		}

		private async void AddItems(IReadOnlyList<LogDataModel> logs)
		{
			try {
				int count = logs.Count;
				for (int i = 0; i < count; ++i) {
					this.Dispatcher.Invoke(_add_items, listView, logs[i]);
					await Task.Delay(1);
				}
				MessageBox.Show("ログファイルの読み込みが完了しました。", "ログファイルを開く", MessageBoxButton.OK, MessageBoxImage.Information);
			} catch (Exception e) {
				MessageBox.Show(e.Message, "エラーが発生しました。", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private static void AddItem(ListView listView, LogDataModel log)
		{
			listView.Items.Add(new LogDataView(_transformer) { LogData = log });
		}
	}
}
