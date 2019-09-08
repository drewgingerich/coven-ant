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
        // Mac has an extra blank char that needs to die
        string key = tokenizedMsg[1].Substring(0, 1);
        if( buttonToEvent.ContainsKey(key) ) {
            buttonToEvent[key].Invoke();
        } else {
            Debug.LogError("No event for message '" + message + "'");
        }
    }
}
