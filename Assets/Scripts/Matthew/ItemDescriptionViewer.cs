using UnityEngine;
using UnityEngine.UI;
using Scriptables.GameEvents;

[RequireComponent(typeof(Text))]
public class ItemDescriptionViewer : MonoBehaviour
{
	private Text text;

	public void UpdateDescriptionFromItem(GameObject item)
	{
		CharacterCreatorItem characterItem = item.GetComponentInChildren<CharacterCreatorItem>();
		if(characterItem) {
			text.text = characterItem.itemName;
		} else {
			Debug.Log("Item was not a Character Creator Item!");
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		text = GetComponent<Text>();
	}
}
