using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;

[CreateAssetMenu(menuName = "Variables/TextureList")]
public class TextureList : GenericVariable<List<Texture2D>> {
	/// <summary>
	/// Appends texture to list of textures
	/// </summary>
	/// <param name="texture">`Sprite`</param>
	public void Add(Texture2D texture) {
		Value.Add(texture);
		OnValueChanged();
	}
}