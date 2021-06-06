/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if DEBUG
using Covid19Radar.LogViewer.Launcher.Extensibility;
#endif

namespace Covid19Radar.LogViewer.Tests.Extensibility
{
	[TestClass()]
	public class ModuleInitializationContextTests
	{
		[TestMethod()]
		public void MockTest()
		{
			var mock = new ModuleInitializationContextMock();

			Assert.IsNull (mock.Arguments);
			Assert.IsNull (mock.TransformerPipeline);
			Assert.IsNull (mock.LogFilesToOpen);
			Assert.IsFalse(mock.AllowEscape);
			Assert.IsFalse(mock.DisallowExtensions);

			Assert.ThrowsException<NotImplementedException>(mock.ParseArguments);
		}

		[TestMethod()]
		public void LauncherInternalTest()
		{
#if DEBUG
			var context = new ModuleInitializationContextInternal(Array.Empty<string>());

			Assert.IsNotNull(context.Arguments);
			Assert.AreEqual (Array.Empty<string>(), context.Arguments);
			Assert.AreEqual (0,                     context.Arguments.Length);
			Assert.IsNull   (context.TransformerPipeline);
			Assert.IsNull   (context.LogFilesToOpen);
			Assert.IsFalse  (context.AllowEscape);
			Assert.IsFalse  (context.DisallowExtensions);

			context.ParseArguments();
			Assert.IsNotNull(context.LogFilesToOpen);
#else
			Console.WriteLine(nameof(LauncherInternalTest) + " is only supported in the debug build.");
#endif
		}

		[TestMethod()]
		public void ParseArgumentsTest()
		{
#if DEBUG
			ModuleInitializationContextInternal context;
			
			context = new(Array.Empty<string>());
			context.ParseArguments();
			Assert.IsNotNull(context.LogFilesToOpen);
			Assert.IsFalse  (context.AllowEscape);
			Assert.IsFalse  (context.DisallowExtensions);

			context.Arguments = new[] { "-e", "--disallow-extensions" };
			context.LogFilesToOpen.Add(string.Empty);
			context.LogFilesToOpen.Add("123");
			Assert.AreEqual(2, context.LogFilesToOpen.Count);

			context.ParseArguments();
			Assert.IsNotNull(context.LogFilesToOpen);
			Assert.AreEqual (0, context.LogFilesToOpen.Count);
			Assert.IsTrue   (context.AllowEscape);
			Assert.IsTrue   (context.DisallowExtensions);

			context = new(new[] { "--allow-escape", "/AllowEscape" });
			context.ParseArguments();
			Assert.IsNotNull(context.LogFilesToOpen);
			Assert.IsTrue   (context.AllowEscape);
			Assert.IsFalse  (context.DisallowExtensions);

			context = new(new[] { "/AllowEscape" });
			context.ParseArguments();
			Assert.IsNotNull(context.LogFilesToOpen);
			Assert.IsTrue   (context.AllowEscape);
			Assert.IsFalse  (context.DisallowExtensions);

			context.Arguments = new[] { "/DisallowExtensions" };
			context.ParseArguments();
			Assert.IsNotNull(context.LogFilesToOpen);
			Assert.IsTrue   (context.AllowEscape);
			Assert.IsTrue   (context.DisallowExtensions);

			context = new(new[] { "--disallow-extensions", "/?" });
			context.ParseArguments();
			Assert.IsNotNull(context.LogFilesToOpen);
			Assert.IsFalse  (context.AllowEscape);
			Assert.IsTrue   (context.DisallowExtensions);
#else
			Console.WriteLine(nameof(ParseArgumentsTest) + " is only supported in the debug build.");
#endif
		}
	}
}
