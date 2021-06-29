/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Extensibility.Features;

namespace Covid19Radar.LogViewer.Launcher.Extensibility
{
	internal sealed class PluginMenuItem : ToolStripMenuItem
	{
		private readonly FormMain          _mwnd;
		private readonly ILauncherFeature? _feature;

		internal PluginMenuItem(FormMain mwnd, IPlugin plugin) : base(
			plugin.DisplayName                     ??
			plugin.ToString()                      ??
			plugin.GetType().AssemblyQualifiedName ??
			string.Empty)
		{
			_mwnd = mwnd;

			if (plugin is CocoaLogViewerModule module) {
				if (module.Logo is not null and var image) {
					this.Image = image;
				}
				this.ToolTipText = module.GetLocalizedDescription() ?? string.Empty;
			}

			if (plugin is ILauncherFeature feature) {
				_feature     = feature;
				this.Checked = feature.IsChecked;
				this.Enabled = true;
			} else {
				this.Enabled = this.DropDownItems.Count > 0;
			}
		}

		protected sealed override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			if (_feature is not null and var feature) {
				feature.RunCommand(_mwnd);
				this.Checked = feature.IsChecked;
			}
		}
	}
}
