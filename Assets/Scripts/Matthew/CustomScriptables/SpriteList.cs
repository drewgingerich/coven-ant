using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;

[CreateAssetMenu(menuName = "Variables/SpriteList")]
public class SpriteList : GenericVariable<List<Sprite>> {
	/// <summary>
	/// Appends sprite to List of sprites
	/// </summary>
	/// <param name="sprite">`Sprite`</param>
	public void Add(Sprite sprite) {
		Value.Add(sprite);
		OnValueChanged();
	}

	/// <summary>
	/// Appends sprite to List of sprites
	/// </summary>
	/// <param name="texture">`Texture2D` to generate sprite from</param>
	public void Add(Texture2D texture) {
		Sprite newSprite = Sprite.Create(
			texture,
			new Rect(0,0,texture.width, texture.height),
			new Vector2(texture.width/2, texture.height/2)
		);
		Value.Add(newSprite);
		OnValueChanged();
	}
}