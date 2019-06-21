using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables.UnityEvents
{
    [Serializable]
    public class ListStringUnityEvent : UnityEvent<List<string>> { }
}