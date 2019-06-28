using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/EffectFromPool")]
public class EffectFromPool : ScriptableObject
{
    public List<Effect> effectPool;

    public void Apply()
    {
        var index = Random.Range(0, effectPool.Count);
        effectPool[index].Apply();
    }
}
