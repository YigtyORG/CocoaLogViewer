/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Globalization;
using System.IO;
using Covid19Radar.LogViewer.Globalization;
using Covid19Radar.LogViewer.Tests.Extensibility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19Radar.LogViewer.Tests.Globalization
{
	[TestClass()]
	public class _01EnglishLanguagePackTests
	{
		[TestInitialize()]
		public void Initialize()
		{
			// ダイアログを抑制する。
			File.Create(Path.Combine(AppContext.BaseDirectory, "c19r.lv.en.suppress-notify-when-japanese")).Close();
		}

		[TestMethod()]
		public void EnglishTest()
		{
			var mock = new ModuleInitializationContextMock();
			var pack = new EnglishLanguagePack();

			Assert.IsNull(mock.TransformerPipeline);

			// 言語パックを初期化。
			pack.Initialize(mock);

			// カルチャは英語に再設定される。
			string langcode = CultureInfo.CurrentCulture.Name;
			Assert.IsTrue(langcode == "en" || langcode.StartsWith("en-"));

			// 現在の言語情報は英語に再設定される。
			Assert.AreEqual   (typeof(Japanese),     LanguageData.Default.GetType());
			Assert.AreEqual   (typeof(English),      LanguageData.Current.GetType());
			Assert.AreNotEqual(LanguageData.Default, LanguageData.Current);

			// 変換パイプラインは設定される。
			Assert.IsNotNull(mock.TransformerPipeline);
		}
	}
}
