using UnityEngine;
using Scriptables.Variables;

public class EnableGameObjectOnBool : MonoBehaviour {
	public GameObject target;
	public BoolVariable enabler;
	private bool originalState;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		originalState = target.activeSelf;
		target.SetActive(enabler.Value);
		enabler.ValueChanged += delegate() {
			target.SetActive(enabler.Value);
		};
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		target.SetActive(originalState);
	}
}
