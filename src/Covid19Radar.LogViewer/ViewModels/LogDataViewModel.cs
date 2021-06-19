/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Models;

namespace Covid19Radar.LogViewer.ViewModels
{
	public class LogDataViewModel : ViewModelBase
	{
		private static readonly SolidColorBrush _does_not_contain_privacy = new(Color.FromRgb(0xFF, 0xFF, 0xFF));
		private static readonly SolidColorBrush _maybe_contains_privacy   = new(Color.FromRgb(0xFF, 0xEE, 0xEE));

		private LogDataModel?    _log_data;
		private string?          _timestamp;
		private string?          _level;
		private SolidColorBrush? _level_back;
		private FlowDocument?    _location;
		private string?          _message;
		private SolidColorBrush? _message_back;

		public LogDataModel? LogData
		{
			get => _log_data;
			set
			{
				if (value is not null) {
					this.RaisePropertyChanged(ref _log_data, value, nameof(this.LogData));
					var loglevel           = value.GetLogLevel();
					this.Timestamp         = value.GetDateTimeAsString();
					this.LogLevel          = loglevel.Text;
					this.LogLevelBackColor = loglevel.BackColor;
					this.Location          = value.GetLocationAsFlowDocument();
					this.Message           = value.TransformedMessage;
					this.MessageBackColor  = value.MaybeContainsPrivacy ? _maybe_contains_privacy : _does_not_contain_privacy;
				}
			}
		}

		public string? Timestamp
		{
			get => _timestamp;
			set => this.RaisePropertyChanged(ref _timestamp, value, nameof(this.Timestamp));
		}

		public string? LogLevel
		{
			get => _level;
			set => this.RaisePropertyChanged(ref _level, value, nameof(this.LogLevel));
		}

		public SolidColorBrush? LogLevelBackColor
		{
			get => _level_back;
			set => this.RaisePropertyChanged(ref _level_back, value, nameof(this.LogLevelBackColor));
		}

		public FlowDocument? Location
		{
			get => _location;
			set => this.RaisePropertyChanged(ref _location, value, nameof(this.Location));
		}

		public string? Message
		{
			get => _message;
			set => this.RaisePropertyChanged(ref _message, value, nameof(this.Message));
		}

		public SolidColorBrush? MessageBackColor
		{
			get => _message_back;
			set => this.RaisePropertyChanged(ref _message_back, value, nameof(this.MessageBackColor));
		}

		public DelegateCommand ClickDetails { get; }
		public DelegateCommand ClickCopy    { get; }

		public LogDataViewModel()
		{
			this.ClickDetails = new(this.ClickDetailsCore);
			this.ClickCopy    = new(this.ClickCopyCore);
		}

		private ValueTask ClickDetailsCore(object? ignored)
		{
			if (_log_data is not null) {
				MessageBox.Show(
					_log_data.CreateDetails(),
					LanguageData.Current.LogDataView_DetailedInformation,
					MessageBoxButton.OK,
					MessageBoxImage.Information
				);
			}
			return default;
		}

		private ValueTask ClickCopyCore(object? ignored)
		{
			if (_log_data is not null) {
				Clipboard.SetText(_log_data.CreateDetails());
				MessageBox.Show(
					LanguageData.Current.LogDataView_Copy_MessageBox,
					LanguageData.Current.LogDataView_Copy,
					MessageBoxButton.OK,
					MessageBoxImage.Information
				);
			}
			return default;
		}
	}
}
