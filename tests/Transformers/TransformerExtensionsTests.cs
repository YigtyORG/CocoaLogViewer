/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Linq;
using Covid19Radar.LogViewer.Transformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Covid19Radar.LogViewer.Tests.Transformers
{
	[TestClass()]
	public class TransformerExtensionsTests
	{
		[TestMethod()]
		public void ConfigureDefaultsTests()
		{
			Assert.ThrowsException<ArgumentNullException>(() => TransformerExtensions.ConfigureDefaults(null!));
			Assert.ThrowsException<ArgumentNullException>(() => TransformerExtensions.ConfigureDefaults(default!));

			var pipeline = new TransformerPipeline();
			Assert.AreEqual(0, Enumerable.Count<TransformDelegate>(pipeline));
			Assert.AreEqual(0, Enumerable.Count<ITransformer>     (pipeline));
			Assert.IsFalse(Enumerable.Any<TransformDelegate>(pipeline));
			Assert.IsFalse(Enumerable.Any<ITransformer>     (pipeline));

			pipeline.ConfigureDefaults();
			Assert.IsTrue(Enumerable.Any<TransformDelegate>(pipeline));
			Assert.IsTrue(Enumerable.Any<ITransformer>     (pipeline));

#pragma warning disable CA1827 // Any() を使用できる場合は、Count() または LongCount() を使用しない
			Assert.IsTrue(Enumerable.Count<TransformDelegate>(pipeline) > 0);
			Assert.IsTrue(Enumerable.Count<ITransformer>     (pipeline) > 0);
#pragma warning restore CA1827 // Any() を使用できる場合は、Count() または LongCount() を使用しない
		}
	}
}
