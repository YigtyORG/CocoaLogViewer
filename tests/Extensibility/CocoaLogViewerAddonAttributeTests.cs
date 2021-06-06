/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Covid19Radar.LogViewer.Extensibility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19Radar.LogViewer.Tests.Extensibility
{
	[TestClass()]
	public class CocoaLogViewerAddonAttributeTests
	{
		[TestMethod()]
		public void ConstructorTest()
		{
			var type = typeof(CocoaLogViewerModuleMock);
			var attr = new CocoaLogViewerAddonAttribute(type);
			Assert.AreEqual(type, attr.ModuleType);

			type = typeof(CocoaLogViewerModule);
			attr = new CocoaLogViewerAddonAttribute(type);
			Assert.AreEqual(type, attr.ModuleType);

			Assert.ThrowsException<ArgumentNullException>(() => new CocoaLogViewerAddonAttribute(null!));
			Assert.ThrowsException<ArgumentNullException>(() => new CocoaLogViewerAddonAttribute(default!));

			Assert.ThrowsException<ArgumentException>(() => new CocoaLogViewerAddonAttribute(this.GetType()));
			Assert.ThrowsException<ArgumentException>(() => new CocoaLogViewerAddonAttribute(typeof(object)));
			Assert.ThrowsException<ArgumentException>(() => new CocoaLogViewerAddonAttribute(typeof(string)));
			Assert.ThrowsException<ArgumentException>(() => new CocoaLogViewerAddonAttribute(typeof(int)));
			Assert.ThrowsException<ArgumentException>(() => new CocoaLogViewerAddonAttribute(typeof(Console)));
		}
	}
}
