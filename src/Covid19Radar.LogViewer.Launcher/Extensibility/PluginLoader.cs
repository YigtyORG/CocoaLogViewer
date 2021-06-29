/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Windows.Forms;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Extensibility.Features;
using Covid19Radar.LogViewer.Extensibility.Providers;
using Covid19Radar.LogViewer.SearchFilters;

namespace Covid19Radar.LogViewer.Launcher.Extensibility
{
	internal static class PluginLoader
	{
		internal static void Load(IPlugin plugin, FormMain mwnd, ToolStripMenuItem? parentPluginMenu)
		{
			PluginMenuItem? pluginMenu = null;
			if (plugin.Visible && parentPluginMenu is not null) {
				pluginMenu = new(mwnd, plugin);
				parentPluginMenu.DropDownItems.Add(pluginMenu);
			}

			if (plugin.GetChildPlugins() is not null and var plugins) {
				foreach (var childPlugin in plugins) {
					Load(childPlugin, mwnd, pluginMenu);
				}
			}

			if (plugin is ISearchFilterFeature searchFilterFeature) {
				SearchFilterRegistry.Register(searchFilterFeature);
			}
			if (plugin is ISearchFilterProvider searchFilterProvider) {
				foreach (var filter in searchFilterProvider.GetSearchFilters()) {
					SearchFilterRegistry.Register(filter);
				}
			}
		}
	}
}
