/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.ComponentModel;
using System.Windows.Forms;

namespace Covid19Radar.LogViewer.Views
{
	public interface ILauncherWindow : IBindableComponent, IContainerControl, IDropTarget, ISynchronizeInvoke, IWin32Window
	{
	}
}
