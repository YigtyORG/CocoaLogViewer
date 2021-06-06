/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Toolchains.CsProj;

#if RELEASE
using BenchmarkDotNet.Running;
#endif

namespace Covid19Radar.LogViewer.Tests
{
	[TestClass()]
	[MemoryDiagnoser()]
	[DisassemblyDiagnoser()]
	public class Benchmarks
	{
		// https://github.com/dotnet/BenchmarkDotNet/issues/1535#issuecomment-694093258
		private static readonly IConfig _config = DefaultConfig.Instance.AddJob(new Job()
			.Freeze()
			.WithToolchain(CsProjCoreToolchain.From(new("net5.0-windows", null, null)))
		);

		[TestMethod()]
		public void Run()
		{
#if RELEASE
			BenchmarkSwitcher.FromTypes(new[] { this.GetType() }).RunAllJoined(_config);
#else
			Console.WriteLine(nameof(Benchmarks) + " are only supported in the release build.");
#endif
		}

		[Benchmark()]
		public void VersionInfo()
		{
			var tester = new VersionInfoTests();
			tester.GetCaptionTest1();
			tester.GetCaptionTest2();
			tester.GetCopyrightTest();
		}
	}
}
