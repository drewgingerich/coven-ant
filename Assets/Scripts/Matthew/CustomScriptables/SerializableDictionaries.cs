using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class StringEventDictionary : SerializableDictionary<string, UnityEvent> {}