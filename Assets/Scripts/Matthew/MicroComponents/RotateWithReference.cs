using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;

public class RotateWithReference : MonoBehaviour {

	public Transform target;
	Quaternion originalRotation;
	public Vector3Variable rotation;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	public void OnEnable(){
		if( target == null ) {
			target = transform;
		}
		originalRotation = target.localRotation;
		OnChangeRotation();
		rotation.ValueChanged += OnChangeRotation;
	}

	public virtual void OnChangeRotation() {
		target.localEulerAngles = rotation.Value;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		target.localRotation = originalRotation;
	}
}
