using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;

[CreateAssetMenu(menuName = "Variables/PrefabList")]
public class PrefabList : GenericVariable<List<GameObject>> {
	/// <summary>
	/// Appends audio clip to list of audio clips
	/// </summary>
	/// <param name="newPrefab">`UnityEngine.AudioClip`</param>
	public void Add(GameObject newPrefab) {
		Value.Add(newPrefab);
		OnValueChanged();
	}
}