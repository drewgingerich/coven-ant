using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scriptables.Variables;
public class DoIfTrue : MonoBehaviour {
	public BoolVariable check;
	public UnityEvent result;

	public void Invoke() {
		if( check.Value ) {
			result.Invoke();
		}
	}
	
}
