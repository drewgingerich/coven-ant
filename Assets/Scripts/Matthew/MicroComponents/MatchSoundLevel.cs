using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;
public class MatchSoundLevel : MonoBehaviour {
	
	public AudioSource target;
	public FloatVariable value;
	float originalValue;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		originalValue = target.volume;
		target.volume = value.Value;
		value.ValueChanged += () => {
			target.volume = value.Value;
		};
	}
	
	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		target.volume = originalValue;
	}
}