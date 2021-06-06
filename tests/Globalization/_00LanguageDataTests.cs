/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19Radar.LogViewer.Tests.Globalization
{
	[TestClass()]
	public class _00LanguageDataTests
	{
		[TestMethod()]
		public void LanguageDataTest()
		{
			// 言語情報の既定値は日本語。
			Assert.AreEqual(typeof(Japanese),     LanguageData.Default.GetType());
			Assert.AreEqual(typeof(Japanese),     LanguageData.Current.GetType());
			Assert.AreEqual(LanguageData.Default, LanguageData.Current);
		}
	}
}
