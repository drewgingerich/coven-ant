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
	public StringToStringUnityEventDictionary responses;
	public void OnMessageArrived(string msg) {
		String[] tokenizedMsg = msg.Split(' ');
		
		if( responses.ContainsKey(tokenizedMsg[0]) ) {
			Debug.Log("Activating response for message: '" + tokenizedMsg[0] + "'");
			responses[tokenizedMsg[0]].Invoke(msg);
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

