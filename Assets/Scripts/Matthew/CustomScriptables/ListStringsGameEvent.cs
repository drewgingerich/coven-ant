using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scriptables.GameEvents
{
    [CreateAssetMenu(menuName = "Events/List<string> Game Event")]
    [Serializable]
    public class ListStringsGameEvent : GenericGameEvent<List<string>>
    {
    }
}