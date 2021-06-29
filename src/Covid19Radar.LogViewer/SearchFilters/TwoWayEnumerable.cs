/****
 * CocoaLogViewer
 * Copyright (C) 2020-2021 Yigty.ORG; all rights reserved.
 * Copyright (C) 2020-2021 Takym.
 *
 * distributed under the MIT License.
****/

using System.Collections;
using System.Collections.Generic;

namespace Covid19Radar.LogViewer.SearchFilters
{
	public readonly struct TwoWayEnumerable : IEnumerable<SearchTextToken>
	{
		private readonly IEnumerable<SearchTextToken>? _enumerable;

		public TwoWayEnumerable(IEnumerable<SearchTextToken> enumerable)
		{
			_enumerable = enumerable;
		}

		public Enumerator GetEnumerator()
		{
			if (_enumerable is null) {
				return default;
			}
			return new(_enumerable.GetEnumerator());
		}

		IEnumerator<SearchTextToken> IEnumerable<SearchTextToken>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		public struct Enumerator : IEnumerator<SearchTextToken>
		{
			private readonly IEnumerator<SearchTextToken>? _enumerator;
			private readonly Stack<SearchTextToken>?       _next_stack;
			private readonly Stack<SearchTextToken>?       _prev_stack;
			private          SearchTextToken               _current;
			public           SearchTextToken               Current => _current;

			object IEnumerator.Current => this.Current;

			public Enumerator(IEnumerator<SearchTextToken> enumerator)
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
