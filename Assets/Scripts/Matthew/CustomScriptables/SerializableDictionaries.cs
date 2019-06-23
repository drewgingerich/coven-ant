using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Scriptables.GameEvents;

[Serializable]
public class StringToUnityEventDictionary : SerializableDictionary<string, UnityEvent> {}
[Serializable]
public class StringToStringUnityEventDictionary : SerializableDictionary<string, StringUnityEvent> {}