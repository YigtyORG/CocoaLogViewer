/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Windows;
using Covid19Radar.LogViewer.Globalization;
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

		public DelegateCommand ClickCopy { get; }

		public ControllerViewModel()
		{
			this.ClickCopy = new(_ => {
				this.ClickCopyCore();
				return default;
			});
		}

		private void ClickCopyCore()
		{
			if (_log_file_view is not null) {
				var sb    = StringBuilderCache<ControllerViewModel>.Get();
				var items = _log_file_view.listView.SelectedItems;
				int count = items.Count;
				for (int i = 0; i < count; ++i) {
					if (items[i]        is LogDataView      ldv  &&
						ldv.DataContext is LogDataViewModel ldvm &&
						ldvm.LogData    is not null and var ldm) {
						ldm.CreateDetails(sb);
						sb.AppendLine();
					}
				}
				Clipboard.SetText(sb.ToString());
				MessageBox.Show(
					LanguageData.Current.ControllerView_Copy_MessageBox,
					LanguageData.Current.ControllerView_Copy,
					MessageBoxButton.OK,
					MessageBoxImage.Information
				);
			}
		}
	}
}
