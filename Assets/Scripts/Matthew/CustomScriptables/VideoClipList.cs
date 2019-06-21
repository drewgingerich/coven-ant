using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;
using UnityEngine.Video;

[CreateAssetMenu(menuName = "Variables/VideoClipList")]
public class VideoClipList : GenericVariable<List<VideoClip>> {
	/// <summary>
	/// Appends video clip to list of video clips
	/// </summary>
	/// <param name="videoClip">`VideoClip`</param>
	public void Add(VideoClip videoClip) {
		Value.Add(videoClip);
		OnValueChanged();
	}
}