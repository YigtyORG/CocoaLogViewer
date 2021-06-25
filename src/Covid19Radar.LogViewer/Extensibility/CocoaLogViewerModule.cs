/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Drawing;

namespace Covid19Radar.LogViewer.Extensibility
{
	public abstract class CocoaLogViewerModule : IPlugin
	{
		public virtual string? DisplayName => this.GetType().Assembly.FullName;
		public virtual Image?  Logo        => null;

		// 必ずメニューに表示する。（モジュールの Visible は無視される）
		bool IPlugin.Visible => true;

		public void Initialize(ModuleInitializationContext context)
		{
			if (context is null) {
				throw new ArgumentNullException(nameof(context));
			}
			this.InitializeCore(context);
		}

		public virtual string? GetLocalizedDescription()
		{
			return null;
		}

		public virtual IEnumerable<IPlugin>? GetChildPlugins()
		{
			return null;
		}

		protected abstract void InitializeCore(ModuleInitializationContext context);
	}
}
