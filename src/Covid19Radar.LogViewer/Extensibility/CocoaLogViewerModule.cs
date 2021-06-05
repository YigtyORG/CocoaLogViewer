/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace Covid19Radar.LogViewer.Extensibility
{
	public abstract class CocoaLogViewerModule
	{
		public virtual string? DisplayName => this.GetType().Assembly.FullName;

		public void Initialize(ModuleInitializationContext context)
		{
			if (context is null) {
				throw new ArgumentNullException(nameof(context));
			}
			this.InitializeCore(context);
		}

		public virtual string? GetLocalizedDescription()
		{
			return string.Empty;
		}

		protected abstract void InitializeCore(ModuleInitializationContext context);
	}
}
