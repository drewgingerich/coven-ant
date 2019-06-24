using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptables.GameEvents {
    [CreateAssetMenu(menuName = "Events/Character Creator Item Game Event")]
    [Serializable]
    public class CharacterCreatorItemGameEvent : GenericGameEvent<CharacterCreatorItem> {}

    [Serializable]
    public class CharacterCreatorUnityEvent : UnityEvent<CharacterCreatorItem> { }

    public class CharacterCreatorGameEventListener : GenericGameEventListener<CharacterCreatorItem, CharacterCreatorItemGameEvent, CharacterCreatorUnityEvent>
    {
    }
}