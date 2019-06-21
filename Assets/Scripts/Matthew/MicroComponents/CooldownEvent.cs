using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scriptables.Variables;

public class CooldownEvent : MonoBehaviour {
	public FloatVariable cooldown;
	public bool ready = true;
	public UnityEvent result;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		if( !ready ) StartCoroutine(DoCooldown());
	}

	public void Invoke() {
		if( ready ) {
			result.Invoke();
			ready = false;
			StartCoroutine(DoCooldown());
		} else {
			#if DEVELOPER || UNITY_EDITOR
			Debug.Log("Ignoring event from " + gameObject.name + " due to cooldown.");
			#endif
		}
	}

	IEnumerator DoCooldown() {
		ready = false;
		yield return new WaitForSeconds(cooldown.Value);
		ready = true;
	}
}
