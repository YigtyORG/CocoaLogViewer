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

		public DelegateCommand(Func<object?, ValueTask> action)
		{
			if (action is null) {
				throw new ArgumentNullException(nameof(action));
			}
			this.Action = action;
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public async void Execute(object? parameter)
		{
			await this.Action(parameter);
		}

#pragma warning disable CS0067
		event EventHandler? ICommand.CanExecuteChanged { add { } remove{ } }
#pragma warning restore CS0067
	}
}
