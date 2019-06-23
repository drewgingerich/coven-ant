using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables.GameEvents;

[CreateAssetMenu(menuName = "Config/Serial Button Handler")]
public class SerialButtonHandler : ScriptableObject {
    public StringToUnityEventDictionary buttonToEvent;
    
    public void OnButtonEvent(string message) {
        string[] tokenizedMsg = message.Split(' ');
        if( buttonToEvent.ContainsKey(tokenizedMsg[1]) ) {
            buttonToEvent[tokenizedMsg[1]].Invoke();
        } else {
            Debug.LogError("No event for message '" + message + "'");
        }
    }
}
