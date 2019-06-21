using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;

public class RotateCameraWithReference : RotateWithReference {

	public float originalSize;
	public FloatVariable perpendicularSize;

	
	new void OnEnable(){
		base.OnEnable();
		perpendicularSize.ValueChanged += OnChangeRotation;
	}

	public override void OnChangeRotation() {
		target.localEulerAngles = rotation.Value;
		float hemisphereDeg = rotation.Value.z % 180;
		float ratio = Mathf.Abs( hemisphereDeg - 90 ) / 90;
		// Debug.Log("Ratio: " + ratio);
		target.GetComponent<Camera>().orthographicSize = Mathf.Lerp( perpendicularSize.Value, originalSize,  ratio);
	}
}