using UnityEngine;
using UnityEngine.Events;

public class CharacterCreatorItem : MonoBehaviour {
    public UnityEvent OnApply;
		public UnityEvent OnHover;
		public UnityEvent OnStopHover;
		public UnityEvent OnDiscard;

		/// <summary>
		/// This function is called when the MonoBehaviour will be destroyed.
		/// </summary>
		void OnDestroy() {
				OnDiscard.Invoke();
		}
}
