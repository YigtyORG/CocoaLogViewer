/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19Radar.LogViewer.Tests.Extensibility
{
	[TestClass()]
	public class CocoaLogViewerModuleTests
	{
		[TestMethod()]
		public void DisplayNameTest()
		{
			var mock = new CocoaLogViewerModuleMock();
			Assert.IsTrue(mock.DisplayName?.Contains("Covid19Radar.LogViewer.Tests"));
			Assert.AreEqual(mock.DisplayName, typeof(VersionInfoTests).Assembly.FullName);
		}

		[TestMethod()]
		public void InitializeTest()
		{
			var mock = new CocoaLogViewerModuleMock();
			mock.Initialize(new ModuleInitializationContextMock());
			Assert.ThrowsException<ArgumentNullException>(() => mock.Initialize(null!));
			Assert.ThrowsException<ArgumentNullException>(() => mock.Initialize(default!));
		}

		[TestMethod()]
		public void GetLocalizedDescriptionTest()
		{
			var mock = new CocoaLogViewerModuleMock();
			Assert.IsNull(mock.GetLocalizedDescription());
		}
	}
}
