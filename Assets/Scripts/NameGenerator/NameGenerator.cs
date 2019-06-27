using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class NameGenerator : ScriptableObject
{
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> titles;

    public string GenerateName()
    {
        var nameIndex = Random.Range(0, names.Count - 1);
        var titleIndex = Random.Range(0, names.Count - 1);
        return string.Format("{0} the {1}", names[nameIndex], titles[titleIndex]);
    }
}
