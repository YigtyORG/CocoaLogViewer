/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections.Generic;
using System.IO;
using Covid19Radar.LogViewer.Extensibility;

namespace Covid19Radar.LogViewer.Launcher.Extensibility
{
	internal sealed class ModuleInitializationContextInternal : ModuleInitializationContext
	{
		internal ModuleInitializationContextInternal(string[] args)
		{
			this.Arguments = args;
		}

		public override void ParseArguments()
		{
			if (this.Arguments is not null and var args) {
				if (this.LogFilesToOpen is not null && !this.LogFilesToOpen.IsReadOnly) {
					this.LogFilesToOpen.Clear();
				} else {
					this.LogFilesToOpen = new List<string>();
				}
				for (int i = 0; i < args.Length; ++i) {
					string arg = args[i];
					switch (arg) {
					case "-e":
					case "--allow-escape":
					case "/AllowEscape":
						this.AllowEscape = true;
						break;
					case "--disallow-extensions":
					case "/DisallowExtensions":
						this.DisallowExtensions = true;
						break;
					default:
						if (File.Exists(arg)) {
							this.LogFilesToOpen.Add(arg);
						}
						break;
					}
				}
			}
		}
	}
}
