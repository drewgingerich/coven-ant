using UnityEngine;
using UnityEngine.UI;
using Scriptables.GameEvents;
using System;

public class ItemDescriptionViewer : MonoBehaviour
{
    Text m_NameText;
    Text m_DescriptionText;

    void Start()
    {
        var texts = GetComponentsInChildren<Text>();
        m_NameText = texts[0];
        m_DescriptionText = texts[1];
        gameObject.SetActive(false);
    }

    public void UpdateDescriptionFromItem(GameObject item)
	{
		CharacterCreatorItem characterItem = item.GetComponentInChildren<CharacterCreatorItem>();

		if(characterItem) {
            m_NameText.text = characterItem.itemName.ToUpper();
            m_DescriptionText.text = characterItem.itemDescription;
		} else {
			Debug.Log("Item was not a Character Creator Item!");
		}
	}
}
