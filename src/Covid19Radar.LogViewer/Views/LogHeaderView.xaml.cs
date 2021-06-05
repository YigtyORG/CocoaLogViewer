/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Windows.Controls;
using Covid19Radar.LogViewer.Globalization;

namespace Covid19Radar.LogViewer.Views
{
	public partial class LogHeaderView : UserControl
	{
		public LogHeaderView()
		{
			this.InitializeComponent();
			control  .Content = LanguageData.Current.LogHeaderView_Control;
			timestamp.Content = LanguageData.Current.LogHeaderView_Timestamp;
			level    .Content = LanguageData.Current.LogHeaderView_Level;
			location .Content = LanguageData.Current.LogHeaderView_Location;
			message  .Content = LanguageData.Current.LogHeaderView_Message;
		}
	}
}
