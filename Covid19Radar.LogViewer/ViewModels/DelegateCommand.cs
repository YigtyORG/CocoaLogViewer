/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Covid19Radar.LogViewer.ViewModels
{
	public sealed class DelegateCommand : ICommand
	{
		public Func<object?, ValueTask> Action { get; }

		public event EventHandler? CanExecuteChanged;

		public DelegateCommand(Func<object?, ValueTask>? action)
		{
			this.Action = action ?? (_ => default);
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public async void Execute(object? parameter)
		{
			await this.Action(parameter);
		}
	}
}
