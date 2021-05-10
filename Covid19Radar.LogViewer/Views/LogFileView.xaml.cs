/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.Views
{
	public partial class LogFileView : UserControl
	{
		private readonly Action<ListView, LogDataModel> _add_items;
		private          LogFileModel?                  _log_file;

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
				await this.ShowMessageBox();
			} catch (Exception e) {
				MessageBox.Show(e.Message, "エラーが発生しました。", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private ValueTask ShowMessageBox()
		{
			return ShowMessageBoxCore(this.Parent, 0 , 2);
			static async ValueTask ShowMessageBoxCore(DependencyObject obj, int i, int max)
			{
				if (obj is MainWindow mwnd) {
					obj.Dispatcher.Invoke(() => {
						MessageBox.Show(
							mwnd,
							$"動作情報ファイル「{mwnd.Title}」の読み込みが完了しました。",
							"動作情報ファイルを開く",
							MessageBoxButton.OK,
							MessageBoxImage.Information
						);
					});
				} else if (i < max && obj is FrameworkElement elem) {
					await Task.Delay(1);
					await ShowMessageBoxCore(elem.Parent, ++i, max);
				} else {
					MessageBox.Show(
						"動作情報ファイルの読み込みが完了しました。",
						"動作情報ファイルを開く",
						MessageBoxButton.OK,
						MessageBoxImage.Information
					);
				}
			}
		}

		private static void AddItem(ListView listView, LogDataModel log)
		{
			listView.Items.Add(new LogDataView() { LogData = log });
		}
	}
}
