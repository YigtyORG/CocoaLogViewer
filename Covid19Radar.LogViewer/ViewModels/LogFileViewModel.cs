/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer.ViewModels
{
	public class LogFileViewModel : ViewModelBase
	{
		private readonly Action<LogDataModel> _add_item;
		private readonly LogFileView          _view;
		private          LogFileModel?        _log_file;

		public LogFileModel? LogFile
		{
			get => _log_file;
			set
			{
				if (value is not null) {
					if (this.TryRaisePropertyChanged(ref _log_file, value, null, nameof(this.LogFile))) {
						this.LogRows.Clear();
						this.AddItems(value.Logs);
					}
				}
			}
		}

		public ObservableCollection<LogDataView> LogRows { get; }

		public LogFileViewModel(LogFileView view)
		{
			_add_item = this.AddItem;
			_view     = view ?? throw new ArgumentNullException(nameof(view));
			this.LogRows = new();
		}

		private async void AddItems(IReadOnlyList<LogDataModel> logs)
		{
			try {
				int count = logs.Count;
				for (int i = 0; i < count; ++i) {
					_view.Dispatcher.Invoke(_add_item, logs[i]);
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
			return ShowMessageBoxCore(msg, _view, 0, 2);

			static async ValueTask ShowMessageBoxCore(Func<MainWindow?, string> msg, DependencyObject obj, int i, int max)
			{
				if (obj is MainWindow mwnd) {
					await obj.Dispatcher.InvokeAsync(() => MessageBox.Show(
						mwnd,
						msg(mwnd),
						LanguageData.Current.LogFileView_MessageBox_Title,
						MessageBoxButton.OK,
						MessageBoxImage.Information
					));
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

		private void AddItem(LogDataModel log)
		{
			var vm = new LogDataViewModel();
			this.LogRows.Add(new LogDataView(vm));
			vm.LogData = log;
		}
	}
}
