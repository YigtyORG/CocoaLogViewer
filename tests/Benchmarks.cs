/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if RELEASE
using BenchmarkDotNet.Running;
#endif

namespace TakymLibTests
{
	[TestClass()]
	[MemoryDiagnoser()]
	[DisassemblyDiagnoser()]
	public class Benchmarks
	{
		[TestMethod()]
		public void Run()
		{
#if RELEASE
			BenchmarkSwitcher.FromTypes(new[] { this.GetType() }).RunAllJoined();
#else
			Console.WriteLine(nameof(Benchmarks) + " are only supported in the release build.");
#endif
		}
	}
}
