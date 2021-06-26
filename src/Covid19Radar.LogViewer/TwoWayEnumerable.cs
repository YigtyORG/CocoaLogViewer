/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Covid19Radar.LogViewer
{
	public static class TwoWayEnumerable
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TwoWayEnumerable<T, TEnumerable, TEnumerator> Create<T, TEnumerable, TEnumerator>(
			TEnumerable                      enumerable,
			Func<TEnumerable?, TEnumerator?> getEnumerator
		)
			where TEnumerable: IEnumerable<T>
			where TEnumerator: IEnumerator<T>
		{
			return new(enumerable, getEnumerator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TwoWayEnumerable<T, IEnumerable<T>, IEnumerator<T>> Create<T>(
			IEnumerable<T>                         enumerable,
			Func<IEnumerable<T>?, IEnumerator<T>?> getEnumerator
		)
		{
			return new(enumerable, getEnumerator);
		}
	}

	public readonly struct TwoWayEnumerable<T, TEnumerable, TEnumerator> : IEnumerable<T>
		where TEnumerable: IEnumerable<T>
		where TEnumerator: IEnumerator<T>
	{
		private readonly TEnumerable?                      _enumerable;
		private readonly Func<TEnumerable?, TEnumerator?>? _get_enumerator;

		public TwoWayEnumerable(TEnumerable enumerable, Func<TEnumerable?, TEnumerator?> getEnumerator)
		{
			_enumerable     = enumerable;
			_get_enumerator = getEnumerator;
		}

		public Enumerator GetEnumerator()
		{
			if (_get_enumerator is null) {
				if (typeof(TEnumerator).IsAssignableFrom(typeof(IEnumerator<T>))) {
					return new((TEnumerator?)(_enumerable?.GetEnumerator()));
				}
				return default;
			}
			return new(_get_enumerator(_enumerable));
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public struct Enumerator : IEnumerator<T>
		{
			private readonly TEnumerator? _enumerator;
			private readonly Stack<T?>?   _next_stack;
			private readonly Stack<T?>?   _prev_stack;
			private          T?           _current;
			public           T            Current => _current!;

			object IEnumerator.Current => this.Current!;

			public Enumerator(TEnumerator? enumerator)
			{
				_enumerator = enumerator;
				_next_stack = new();
				_prev_stack = new();
				_current    = default;
			}

			public bool MoveNext()
			{
				if (_next_stack?.TryPop(out _current) ?? false) {
					_prev_stack?.Push(_current);
					return true;
				}
				if (_enumerator?.MoveNext() ?? false) {
					_current = _enumerator.Current;
					_prev_stack?.Push(_current);
					return true;
				}
				return false;
			}

			public bool MovePrev()
			{
				if (_prev_stack?.TryPop(out _current) ?? false) {
					_next_stack?.Push(_current);
					return true;
				}
				return false;
			}

			public void Reset()
			{
				_enumerator?.Reset();
				_next_stack?.Clear();
				_prev_stack?.Clear();
				_current = default;
			}

			public void Dispose()
			{
				_enumerator?.Dispose();
			}
		}
	}
}
