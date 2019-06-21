using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Refer to <see cref="SerialController.Update" /> in the Ardity package
/// </summary>
[CreateAssetMenu(menuName = "Config/Serial Message Router")]
public class SerialMessageInput : ScriptableObject {
	public StringEventDictionary responses;
	[Header("Hardcoded Responses")]
	public UnityEvent onH;
	public UnityEvent onP;
	public void OnMessageArrived(string msg) {
		if( responses.ContainsKey(msg) ) {
			Debug.Log("Activating response for Arduino button: '" + msg + "'");
			responses[msg].Invoke();
		} else {
			Debug.LogWarning( "'" + msg + "'" + " has no defaults.");
		}
	}

	public void OnConnectionEvent(bool success) {
		if (success) {
			Debug.Log("Connection established");
		} else {
			Debug.Log("Connection attempt failed or disconnection detected");
		}	
	}
}

