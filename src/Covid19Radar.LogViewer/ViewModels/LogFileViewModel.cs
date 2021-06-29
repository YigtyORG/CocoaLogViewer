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
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.SearchFilters;
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
						Refresh();

						async void Refresh()
						{
							await this.RefreshAsync();
						}
					}
				}
			}
		}

		public ObservableCollection<LogDataView> LogRows    { get; }
		public bool                              Refreshing { get; set; }

		public LogFileViewModel(LogFileView view)
		{
			_add_item    = this.AddItem;
			_view        = view ?? throw new ArgumentNullException(nameof(view));
			this.LogRows = new();
		}

		public async ValueTask<bool> RefreshAsync(string? searchText = null)
		{
			if (this.Refreshing) {
				return false;
			}
			bool result = true;
			this.Refreshing = true;
			await _view.Dispatcher.InvokeAsync(this.LogRows.Clear);
			if (_log_file is not null) {
				result = await this.AddItemsAsync(_log_file.Logs, searchText);
			}
			this.Refreshing = false;
			return result;
		}

		private async ValueTask<bool> AddItemsAsync(IReadOnlyList<LogDataModel> logs, string? searchText)
		{
			try {
				int count  = logs.Count;
				var filter = CreateSearchFilter(searchText);
				for (int i = 0; i < count; ++i) {
					var log = logs[i];
					if (filter is null || filter.Match(log)) {
						_view.Dispatcher.Invoke(_add_item, log);
					}
					await Task.Yield();
				}
				await Dialogs.ShowMessageAsync(
					LanguageData.Current.LogFileView_MessageBox_Succeeded,
					LanguageData.Current.LogFileView_MessageBox_Title,
					_view
				);
				return true;
			} catch (Exception e) {
				Debug.Fail(e.Message, e.ToString());
				await Dialogs.ShowMessageAsync(
					mwnd => {
						mwnd?.PrintException(e);
						return LanguageData.Current.LogFileView_MessageBox_Failed;
					},
					LanguageData.Current.LogFileView_MessageBox_Title,
					_view,
					MessageBoxImage.Error
				);
				return false;
			}
		}

		private static SearchFilterNode? CreateSearchFilter(string? text)
		{
			if (string.IsNullOrEmpty(text)) {
				return null;
			}
			using (var parser = new SearchTextParser(new(text))) {
				return parser.Parse();
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
