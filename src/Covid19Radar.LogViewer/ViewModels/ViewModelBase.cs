/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.ComponentModel;
using System.Threading;

namespace Covid19Radar.LogViewer.ViewModels
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected void RaisePropertyChanged<T>(ref T location, T value, string name) where T: class?
		{
			var oldValue = location;
			while (Interlocked.CompareExchange(ref location, value, oldValue) != oldValue) {
				Thread.Yield();
				oldValue = location;
			}
			this.OnPropertyChanged(new(name));
		}

		protected bool TryRaisePropertyChanged<T>(ref T location, T value, T oldValue, string name) where T : class?
		{
			if (Interlocked.CompareExchange(ref location, value, oldValue) == oldValue) {
				this.OnPropertyChanged(new(name));
				return true;
			} else {
				return false;
			}
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			this.PropertyChanged?.Invoke(this, e);
		}
	}
}
