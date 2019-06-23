using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables.GameEvents
{
    [CreateAssetMenu(menuName = "Events/String Game Event")]
    [Serializable]
    public class StringGameEvent : GenericGameEvent<string>
    {
    }

    [Serializable]
    public class StringUnityEvent : UnityEvent<String> { }

    public class StringGameEventListener : GenericGameEventListener<String, StringGameEvent, StringUnityEvent>
    {
    }
}