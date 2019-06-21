using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;

[CreateAssetMenu(menuName = "Variables/AudioClipList")]
public class AudioClipList : GenericVariable<List<AudioClip>> {
	/// <summary>
	/// Appends audio clip to list of audio clips
	/// </summary>
	/// <param name="newClip">`UnityEngine.AudioClip`</param>
	public void Add(AudioClip newClip) {
		Value.Add(newClip);
		OnValueChanged();
	}
}