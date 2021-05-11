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
using Covid19Radar.LogViewer.Globalization;
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
					await Task.Yield();
				}
				await this.ShowMessageBox(LanguageData.Current.LogFileView_MessageBox_Succeeded);
			} catch (Exception e) {
				await this.ShowMessageBox(mwnd => {
					mwnd?.PrintException(e);
					return LanguageData.Current.LogFileView_MessageBox_Failed;
				});
			}
		}

		private ValueTask ShowMessageBox(Func<MainWindow?, string> msg)
		{
			return ShowMessageBoxCore(msg, this.Parent, 0 , 2);
			static async ValueTask ShowMessageBoxCore(Func<MainWindow?, string> msg, DependencyObject obj, int i, int max)
			{
				if (obj is MainWindow mwnd) {
					await obj.Dispatcher.InvokeAsync(() => {
						MessageBox.Show(
							mwnd,
							msg(mwnd),
							LanguageData.Current.LogFileView_MessageBox_Title,
							MessageBoxButton.OK,
							MessageBoxImage.Information
						);
					});
				} else if (i < max && obj is FrameworkElement elem) {
					await Task.Delay(1);
					await ShowMessageBoxCore(msg, elem.Parent, ++i, max);
				} else {
					MessageBox.Show(
						msg(null),
						LanguageData.Current.LogFileView_MessageBox_Title,
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
