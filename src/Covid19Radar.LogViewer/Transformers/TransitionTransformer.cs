/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;

namespace Covid19Radar.LogViewer.Transformers
{
	internal sealed class TransitionTransformer : TransformerBase
	{
		private const string Failed                    = "Failed transition.";
		private const string Prefix                    = "Transition to ";
		private const string HomePage                  = "ホーム";
		private const string TutorialPage1             = "このアプリでできること";
		private const string ReAgreeTermsOfServicePage = "利用規約の改定";
		private const string ReAgreePrivacyPolicyPage  = "プライバシーポリシーの改定";

		internal static TransitionTransformer Instance { get; } = new();

		private TransitionTransformer() { }

		protected override string TransformCore(string? message, Func<string?, string> next)
		{
			if (string.IsNullOrEmpty(message)) {
				return string.Empty;
			}

			if (message == Failed) {
				return "ページの遷移に失敗しました。";
			} else if (message.StartsWith(Prefix)) {
				string page = message.Substring(Prefix.Length);
				page = page switch {
					nameof(HomePage)                  => HomePage,
					nameof(TutorialPage1)             => TutorialPage1,
					nameof(ReAgreeTermsOfServicePage) => ReAgreeTermsOfServicePage,
					nameof(ReAgreePrivacyPolicyPage)  => ReAgreePrivacyPolicyPage,
					_ => page
				};
				return $"ページ「{page}」に遷移します。";
			} else {
				return next(message);
			}
		}
	}
}
