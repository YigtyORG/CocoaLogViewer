/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Covid19Radar.LogViewer.Extensibility;
using Covid19Radar.LogViewer.Globalization.EnglishTransformers;
using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Globalization
{
	public sealed class EnglishLanguagePack : CocoaLogViewerModule
	{
		public override string? DisplayName => "The English Language Pack";

		public override string? GetLocalizedDescription()
		{
			return "This extension provides the English UI.";
		}

		protected override void InitializeCore(ModuleInitializationContext context)
		{
			// Step 1: set the current culture to English.
			SetCultureInfo();

			// Step 2: modify the language data.
			LanguageData.Current = English._inst;

			// Step 3: build a transformer pipeline.
			BuildTransformerPipeline(context);
		}

		private static void SetCultureInfo()
		{
			string langcode = CultureInfo.CurrentCulture.Name;
			if (langcode == "en" || langcode.StartsWith("en-")) {
				return;
			}
			if (langcode == "ja" || langcode.StartsWith("ja-")) {
				NotifyWhenJapanese();
			}
			var cinfo = CultureInfo.GetCultureInfo("en");
			CultureInfo.DefaultThreadCurrentCulture   = cinfo;
			CultureInfo.DefaultThreadCurrentUICulture = cinfo;
			CultureInfo.CurrentCulture                = cinfo;
			CultureInfo.CurrentUICulture              = cinfo;
		}

		private static void NotifyWhenJapanese()
		{
			string suppressor = Path.Combine(AppContext.BaseDirectory, "c19r.lv.en.suppress-notify-when-japanese");
			if (!File.Exists(suppressor)) {
				var asm = Assembly.GetExecutingAssembly();
				MessageBox.Show(
					$"英語版で起動します。日本語版で起動する場合は「{asm.GetName().Name}.dll」を削除してください。",
					VersionInfo.GetCaption(asm),
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
				);
				try {
					File.Create(suppressor).Close();
				} catch (Exception e) {
					// Ignore I/O error on release build, log on debug build.
					Debug.Fail(e.Message, e.ToString());
				}
			}
		}

		private static void BuildTransformerPipeline(ModuleInitializationContext context)
		{
			context.TransformerPipeline = new TransformerPipeline()
				.AddControlCharTransformer()
				.Add(CallTransformer       .Instance)
				.Add(TekItemTransformer    .Instance)
				.Add(LastCreatedTransformer.Instance)
				.Add(UserDataTransformer   .Instance)
				.Add(TransitionTransformer .Instance);
		}
	}
}
