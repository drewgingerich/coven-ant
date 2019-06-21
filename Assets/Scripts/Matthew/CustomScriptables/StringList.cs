using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;

[CreateAssetMenu(menuName = "Variables/StringList")]
public class StringList : GenericVariable<List<string>> {
	/// <summary>
	/// Appends video clip to list of video clips
	/// </summary>
	/// <param name="newString">`string`</param>
	public void Add(string newString) {
		Value.Add(newString);
		OnValueChanged();
	}
}