/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer.ViewModels
{
	public class ControllerViewModel : ViewModelBase
	{
		private LogFileView? _log_file_view;

		public LogFileView? LogFileView
		{
			get => _log_file_view;
			set => this.RaisePropertyChanged(ref _log_file_view, value, nameof(this.LogFileView));
		}

		public bool RefreshButtonEnabled => !(_log_file_view?.ViewModel.Refreshing ?? true);

		public DelegateCommand Refresh             { get; }
		public DelegateCommand ClickCopy           { get; }
		public DelegateCommand ClickCopyAsMarkdown { get; }

		public ControllerViewModel()
		{
			this.Refresh             = new(this.RefreshCore);
			this.ClickCopy           = new(this.ClickCopyCore);
			this.ClickCopyAsMarkdown = new(this.ClickCopyAsMarkdownCore);
		}

		private async ValueTask RefreshCore(object? ignored)
		{
			if (_log_file_view is not null && !await _log_file_view.ViewModel.RefreshAsync()) {
				await Dialogs.ShowMessageAsync(
					LanguageData.Current.ControllerView_Refresh_Failed,
					LanguageData.Current.ControllerView_Refresh,
					_log_file_view,
					MessageBoxImage.Warning
				);
			}
		}

		private ValueTask ClickCopyCore(object? ignored)
		{
			if (_log_file_view is not null) {
				var sb = StringBuilderCache<ControllerViewModel>.Get();
				ForAllLogData(sb, _log_file_view.listView, (sb, ldm) => ldm.CreateDetails(sb));
				return CopyToClipboardAsync(sb, false, _log_file_view);
			}
			return default;
		}

		private ValueTask ClickCopyAsMarkdownCore(object? ignored)
		{
			if (_log_file_view is not null) {
				var sb = StringBuilderCache<ControllerViewModel>.Get();
				LogDataModel.CreateMarkdownHeader(sb);
				ForAllLogData(sb, _log_file_view.listView, (sb, ldm) => ldm.CreateDetailsAsMarkdown(sb));
				LogDataModel.CreateMarkdownFooter(sb);
				return CopyToClipboardAsync(sb, true, _log_file_view);
			}
			return default;
		}

		private static void ForAllLogData(StringBuilder sb, ListView listView, Action<StringBuilder, LogDataModel> action)
		{
			var items = listView.SelectedItems;
			int count = items.Count;
			for (int i = 0; i < count; ++i) {
				if (items[i]        is LogDataView      ldv  &&
					ldv.DataContext is LogDataViewModel ldvm &&
					ldvm.LogData    is not null and var ldm) {
					action(sb, ldm);
					sb.AppendLine();
				}
			}
		}

		private static ValueTask CopyToClipboardAsync(StringBuilder data, bool copyAsMarkdown, DependencyObject depObj)
		{
			Clipboard.SetText(data.ToString());
			return Dialogs.ShowMessageAsync(
				_ =>             LanguageData.Current.ControllerView_Copy_MessageBox,
				copyAsMarkdown ? LanguageData.Current.ControllerView_CopyAsMarkdown
				               : LanguageData.Current.ControllerView_Copy,
				depObj
			);
		}
	}
}
