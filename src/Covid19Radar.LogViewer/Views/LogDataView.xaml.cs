/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.ViewModels;

namespace Covid19Radar.LogViewer.Views
{
	public partial class LogDataView : UserControl
	{
		public LogDataView(LogDataViewModel viewModel)
		{
			this.InitializeComponent();
			this.DataContext = viewModel;
			viewModel.PropertyChanged += this.ViewModel_PropertyChanged;
			details.Content = LanguageData.Current.LogDataView_Details;
			copy   .Content = LanguageData.Current.LogDataView_Copy;
		}

		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			base.OnMouseWheel(e);
			e.Handled = false;
		}

		private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (sender is LogDataViewModel vm && e.PropertyName == nameof(vm.Location)) {
				location.Document = vm.Location;
			}
		}
	}
}
