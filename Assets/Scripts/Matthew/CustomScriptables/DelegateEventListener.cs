using System;

namespace Scriptables.GameEvents
{
	public class DelegateEventListener<T> : IHasEventRaised<T> {
		public Action<T> ListenToEventRaised;
		public void OnEventRaised(T value) {
			ListenToEventRaised(value);
		}
	}
}