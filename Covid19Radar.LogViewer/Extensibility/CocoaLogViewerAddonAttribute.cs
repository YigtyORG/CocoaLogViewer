/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Covid19Radar.LogViewer.Properties;

namespace Covid19Radar.LogViewer.Extensibility
{
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class CocoaLogViewerAddonAttribute : Attribute
	{
		public Type ModuleType { get; }

		public CocoaLogViewerAddonAttribute(Type moduleType)
		{
			if (moduleType is null) {
				throw new ArgumentNullException(nameof(moduleType));
			}
			if (!moduleType.IsAssignableTo(typeof(CocoaLogViewerModule))) {
				throw new ArgumentException(
					string.Format(
						Resources.CocoaLogViewerAddonAttribute_ArgumentException,
						moduleType.AssemblyQualifiedName,
						typeof(CocoaLogViewerModule).AssemblyQualifiedName
					),
					nameof(moduleType)
				);
			}
			this.ModuleType = moduleType;
		}
	}
}
