/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using Covid19Radar.LogViewer.Transformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19Radar.LogViewer.Tests.Transformers
{
	[TestClass()]
	public class PrivacyValidatorTests
	{
		[TestMethod()]
		public void CheckTest()
		{
			Assert.IsFalse(PrivacyValidator.Check(string.Empty));
			Assert.IsFalse(PrivacyValidator.Check(""));
			Assert.IsFalse(PrivacyValidator.Check(null!));

			Assert.IsTrue(PrivacyValidator.Check("Exposure count: "));
			Assert.IsTrue(PrivacyValidator.Check("http://"));
			Assert.IsTrue(PrivacyValidator.Check("https://"));
			Assert.IsTrue(PrivacyValidator.Check("Exception"));

			Assert.IsFalse(PrivacyValidator.Check("  Exposure count: "));
			Assert.IsFalse(PrivacyValidator.Check("Exposure count:"));
			Assert.IsFalse(PrivacyValidator.Check("http"));
			Assert.IsFalse(PrivacyValidator.Check("https"));
			Assert.IsFalse(PrivacyValidator.Check("exception"));

			Assert.IsTrue(PrivacyValidator.Check("Exposure count: http://"));
			Assert.IsTrue(PrivacyValidator.Check("http://  Exception"));
			Assert.IsTrue(PrivacyValidator.Check("aa http:// bb"));
			Assert.IsTrue(PrivacyValidator.Check("aa https:// bb"));
			Assert.IsTrue(PrivacyValidator.Check("aa Exception bb"));
			Assert.IsTrue(PrivacyValidator.Check("http  http://  ____"));
			Assert.IsTrue(PrivacyValidator.Check("https https:// ____"));

			Assert.IsFalse(PrivacyValidator.Check("http https"));
			Assert.IsFalse(PrivacyValidator.Check("Hello, World!!"));
			Assert.IsFalse(PrivacyValidator.Check("0123456789"));
		}
	}
}
