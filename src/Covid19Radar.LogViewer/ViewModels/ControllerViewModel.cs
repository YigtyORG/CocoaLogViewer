/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Models;
using Covid19Radar.LogViewer.Views;
using Clipboard = System.Windows.Clipboard;
using ListView = System.Windows.Controls.ListView;

namespace Covid19Radar.LogViewer.ViewModels
{
	public class ControllerViewModel : ViewModelBase
	{
		private MainWindow?  _mwnd;
		private LogFileView? _log_file_view;

		public MainWindow? MainWindow
		{
			get => _mwnd;
			set => this.RaisePropertyChanged(ref _mwnd, value, nameof(this.MainWindow));
		}

		public LogFileView? LogFileView
		{
			get => _log_file_view;
			set => this.RaisePropertyChanged(ref _log_file_view, value, nameof(this.LogFileView));
		}

		public bool RefreshButtonEnabled => !(_log_file_view?.ViewModel.Refreshing ?? true);

		public DelegateCommand Refresh             { get; }
		public DelegateCommand ClickCopy           { get; }
		public DelegateCommand ClickCopyAsMarkdown { get; }
		public DelegateCommand ClickSearch         { get; }
		public DelegateCommand ClickSave           { get; }

		public ControllerViewModel()
		{
			this.Refresh             = new(this.RefreshCore);
			this.ClickCopy           = new(this.ClickCopyCore);
			this.ClickCopyAsMarkdown = new(this.ClickCopyAsMarkdownCore);
			this.ClickSearch         = new(this.ClickSearchCore);
			this.ClickSave           = new(this.ClickSaveCore);
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

		private ValueTask ClickSearchCore(object? ignored)
		{
			return default;
		}

		private ValueTask ClickSaveCore(object? ignored)
		{
			if (_mwnd is not null && _mwnd.FilePath is not null and var path) {
				using (var sfd = new SaveFileDialog() {
					Title                        = LanguageData.Current.ControllerView_Save,
					Filter                       = LanguageData.Current.MainWindow_OFD_Filter(),
					RestoreDirectory             = true,
					DereferenceLinks             = true,
					AddExtension                 = false,
					SupportMultiDottedExtensions = true,
					CheckPathExists              = false,
					CheckFileExists              = false,
					ValidateNames                = true,
					AutoUpgradeEnabled           = true,
					OverwritePrompt              = true
				}) {
					if (sfd.ShowDialog() == DialogResult.OK) {
						string? dir = Path.GetDirectoryName(path);
						if (dir is not null && !Directory.Exists(dir)) {
							Directory.CreateDirectory(dir);
						}
						File.Copy(path, sfd.FileName, true);
					}
				}
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
