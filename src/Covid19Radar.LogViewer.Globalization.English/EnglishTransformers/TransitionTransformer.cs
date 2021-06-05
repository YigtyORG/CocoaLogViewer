/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Covid19Radar.LogViewer.Transformers;

namespace Covid19Radar.LogViewer.Globalization.EnglishTransformers
{
	internal sealed class TransitionTransformer : TransformerBase
	{
		private const string Failed                    = "Failed transition.";
		private const string Prefix                    = "Transition to ";
		private const string HomePage                  = "HOME";
		private const string TutorialPage1             = "Protect yourself with the app";
		private const string ReAgreeTermsOfServicePage = "Revision of Terms of Use";
		private const string ReAgreePrivacyPolicyPage  = "Privacy Policy Revision";

		internal static TransitionTransformer Instance { get; } = new();

		private TransitionTransformer() { }

		protected override string? TransformCore(string? message, Func<string?, string?> next)
		{
			if (message == Failed) {
				return "Failed to transit to a page.";
			} else if (message?.StartsWith(Prefix) ?? false) {
				string page = message.Substring(Prefix.Length);
				page = page switch {
					nameof(HomePage)                  => HomePage,
					nameof(TutorialPage1)             => TutorialPage1,
					nameof(ReAgreeTermsOfServicePage) => ReAgreeTermsOfServicePage,
					nameof(ReAgreePrivacyPolicyPage)  => ReAgreePrivacyPolicyPage,
					_ => page
				};
				return $"Transiting to the page \"{page}\"...";
			} else {
				return next(message);
			}
		}
	}
}
