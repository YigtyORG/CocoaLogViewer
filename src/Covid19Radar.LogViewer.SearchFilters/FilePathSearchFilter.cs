/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections.Generic;
using System.Linq;
using Covid19Radar.LogViewer.Models;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

namespace Covid19Radar.LogViewer.SearchFilters
{
	internal sealed class FilePathSearchFilter : ISearchFilter
	{
		internal static readonly FilePathSearchFilter _inst = new();
		private         readonly PatternBuilder       _pattern_builder;

		public string Key => "glob";

		private FilePathSearchFilter()
		{
			_pattern_builder = new();
		}

		public bool Match(SearchFilterNode value, LogDataModel model)
		{
			if (value is TokenNode node) {
				var pattern = _pattern_builder.Build(node.Token.GetText());
				var context = new MatcherContext(
					GetPatterns(pattern),
					Enumerable.Empty<IPattern>(),
					new InMemoryDirectoryInfo("/", GetFiles(model.FilePath)),
					StringComparison.OrdinalIgnoreCase
				);
				return context.Execute().HasMatches;

				static IEnumerable<IPattern> GetPatterns(IPattern pattern)
				{
					yield return pattern;
				}

				static IEnumerable<string> GetFiles(string filePath)
				{
					yield return filePath;
				}
			} else {
				return false;
			}
		}
	}
}
