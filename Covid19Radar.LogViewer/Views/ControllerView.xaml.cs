/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Windows.Controls;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.ViewModels;

namespace Covid19Radar.LogViewer.Views
{
	public partial class ControllerView : UserControl
	{
		private readonly ControllerViewModel _view_model;

		public LogFileView? LogFileView
		{
			get => _view_model.LogFileView;
			set => _view_model.LogFileView = value;
		}

		public ControllerView()
		{
			this.InitializeComponent();
			this.DataContext = _view_model = new ControllerViewModel();
			copy    .Content = LanguageData.Current.ControllerView_Copy;
			copyAsMd.Content = LanguageData.Current.ControllerView_CopyAsMarkdown;
		}
	}
}
