/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Threading.Tasks;
using System.Windows;

namespace Covid19Radar.LogViewer.Views
{
	public static class Dialogs
	{
		public static ValueTask ShowMessageAsync(
			Func<MainWindow?, string> messageFactory,
			string                    title,
			DependencyObject          depObj,
			MessageBoxImage           image = MessageBoxImage.Information)
		{
			return ShowMessageAsyncCore(messageFactory, title, depObj, image, 0, 16);

			static async ValueTask ShowMessageAsyncCore(
				Func<MainWindow?, string> msg,
				string                    title,
				DependencyObject          obj,
				MessageBoxImage           image,
				int                       i,
				int                       max)
			{
				if (obj is MainWindow mwnd) {
					await obj.Dispatcher.InvokeAsync(() => MessageBox.Show(
						mwnd,
						msg(mwnd),
						title,
						MessageBoxButton.OK,
						image
					));
				} else if (i < max && obj is FrameworkElement elem) {
					await ShowMessageAsyncCore(msg, title, elem.Parent, image, ++i, max);
				} else {
					MessageBox.Show(
						msg(null),
						title,
						MessageBoxButton.OK,
						image
					);
				}
			}
		}
	}
}
