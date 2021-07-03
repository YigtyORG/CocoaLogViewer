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
using Covid19Radar.LogViewer.Extensibility.Features;
using Covid19Radar.LogViewer.Globalization.EnglishTransformers;
using Covid19Radar.LogViewer.Transformers;
using Covid19Radar.LogViewer.Views;

namespace Covid19Radar.LogViewer.Globalization
{
	public sealed class EnglishLanguagePack : CocoaLogViewerModule, ILauncherFeature
	{
		private const    string  FILENAME_OF_DISABLER   = "c19r.lv.en.disabled";
		private const    string  FILENAME_OF_SUPPRESSOR = "c19r.lv.en.suppress-notify-when-japanese";
		private          bool    _disabled;
		public  override string? DisplayName => "The English Language Pack";
		public           bool    IsChecked   => !_disabled;

		public override string? GetLocalizedDescription()
		{
			return "This extension provides the English UI.";
		}

		protected override void InitializeCore(ModuleInitializationContext context)
		{
			// Step 0: check if a disabler exists.
			if (_disabled = File.Exists(Path.Combine(AppContext.BaseDirectory, FILENAME_OF_DISABLER))) {
				return;
			}

			// Step 1: set the current culture to English.
			SetCultureInfo();

			// Step 2: modify the language data.
			LanguageData.Current = English._inst;

			// Step 3: build a transformer pipeline.
			BuildTransformerPipeline(context);
		}

		public void RunCommand(ILauncherWindow parent)
		{
			_disabled = !_disabled;

			string disabler = Path.Combine(AppContext.BaseDirectory, FILENAME_OF_DISABLER);
			try {
				if (_disabled) {
					File.Create(disabler).Close();
				} else {
					File.Delete(disabler);
				}
			} catch (Exception e) {
				// Ignore I/O error on release build, log on debug build.
				Debug.Fail(e.Message, e.ToString());
			}

			var asm = Assembly.GetExecutingAssembly();
			MessageBox.Show(
				parent,
				$"The English UI will be {(_disabled ? "disabled" : "re-enabled")} on the next startup.",
				VersionInfo.GetCaption(asm),
				MessageBoxButtons.OK,
				MessageBoxIcon.Information
			);
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
			string suppressor = Path.Combine(AppContext.BaseDirectory, FILENAME_OF_SUPPRESSOR);
			if (!File.Exists(suppressor)) {
				var asm = Assembly.GetExecutingAssembly();
				MessageBox.Show(
					$"英語版で起動します。日本語版で起動する場合は拡張機能ファイル「{asm.GetName().Name}.dll」を削除するか、機能メニューから無効化してください。",
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
				.Add(CallTransformer                .Instance)
				.Add(TekItemTransformer             .Instance)
				.Add(LastCreatedTransformer         .Instance)
				.Add(DownloadTekFileTransformer     .Instance)
				.Add(DownloadTransformer            .Instance)
				.Add(ExposureNotificationTransformer.Instance)
				.Add(UserDataTransformer            .Instance)
				.Add(TransitionTransformer          .Instance);
		}
	}
}
