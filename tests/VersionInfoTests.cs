/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19Radar.LogViewer.Tests
{
	[TestClass()]
	public class VersionInfoTests
	{
		[TestMethod()]
		public void GetCaptionTest1()
		{
			string caption;

			caption = VersionInfo.GetCaption("CocoaLogViewer", new(0, 0, 0, 0), "c19r.lv00a0", "初期バージョンの題名");
			Assert.AreEqual("CocoaLogViewer [v0.0.0.0, cn:c19r.lv00a0, bc:初期バージョンの題名]", caption);

			caption = VersionInfo.GetCaption("Name", new(1, 2, 3, 4), "Hello", "World");
			Assert.AreEqual("Name [v1.2.3.4, cn:Hello, bc:World]", caption);

			caption = VersionInfo.GetCaption("AppName", new(1, 0, 0, 0), "abc123", VersionInfo.Debug);
			Assert.AreEqual("AppName - デバッグ版 [v1.0.0.0, cn:abc123]", caption);

			caption = VersionInfo.GetCaption("AppName", new(1, 0, 0, 0), "abc123", VersionInfo.Release);
			Assert.AreEqual("AppName [v1.0.0.0, cn:abc123]", caption);

			caption = VersionInfo.GetCaption("AppName", new(1, 0, 0, 0), "abc123", null);
			Assert.AreEqual("AppName [v1.0.0.0, cn:abc123, bc:<不明>]", caption);

			caption = VersionInfo.GetCaption("AppName", null, "abc123", null);
			Assert.AreEqual("AppName [v?.?.?.?, cn:abc123, bc:<不明>]", caption);

			caption = VersionInfo.GetCaption("AppName", new(1, 0, 0, 0), "", null);
			Assert.AreEqual("AppName [v1.0.0.0, cn:, bc:<不明>]", caption);

			caption = VersionInfo.GetCaption("AppName", new(int.MaxValue, 0x7FFFFFFF, 0, 0), "", null);
			Assert.AreEqual("AppName [v2147483647.2147483647.0.0, cn:, bc:<不明>]", caption);

			caption = VersionInfo.GetCaption("AppName", new(1, 0, 0, 0), null, null);
			Assert.AreEqual("AppName [v1.0.0.0, cn:<不明>, bc:<不明>]", caption);

			caption = VersionInfo.GetCaption("1234", new(99, 0, 0, 5), null, "Debug");
			Assert.AreEqual("1234 - デバッグ版 [v99.0.0.5, cn:<不明>]", caption);

			caption = VersionInfo.GetCaption(null, new(99, 0, 0, 5), null, "Debug");
			Assert.AreEqual(" - デバッグ版 [v99.0.0.5, cn:<不明>]", caption);

			caption = VersionInfo.GetCaption(null, null, null, "Release");
			Assert.AreEqual(" [v?.?.?.?, cn:<不明>]", caption);
		}

		[TestMethod()]
		public void GetCaptionTest2()
		{
			// 同一のアセンブリに対して必ず同じ結果を出力しなければならない。

			Assert.AreEqual(
				VersionInfo.GetCaption(),
				VersionInfo.GetCaption(typeof(VersionInfo).Assembly)
			);

			Assert.AreNotEqual(
				VersionInfo.GetCaption(),
				VersionInfo.GetCaption(typeof(VersionInfoTests).Assembly)
			);

			Assert.AreEqual(
				VersionInfo.GetCaption(Assembly.GetExecutingAssembly()),
				VersionInfo.GetCaption(typeof(VersionInfoTests).Assembly)
			);

			Assert.AreNotEqual(
				VersionInfo.GetCaption(Assembly.GetExecutingAssembly()),
				VersionInfo.GetCaption(typeof(VersionInfo).Assembly)
			);
		}

		[TestMethod()]
		public void GetCopyrightTest()
		{
			Assert.AreEqual(
				"Copyright (C) 2020-2021 Yigty.ORG; all rights reserved. Copyright (C) 2020-2021 Takym.",
				VersionInfo.GetCopyright()
			);

			Assert.AreEqual(
				"Copyright (C) 2020-2021 Yigty.ORG; all rights reserved. Copyright (C) 2020-2021 Takym.",
				VersionInfo.GetCopyright(typeof(VersionInfo).Assembly)
			);
		}
	}
}
