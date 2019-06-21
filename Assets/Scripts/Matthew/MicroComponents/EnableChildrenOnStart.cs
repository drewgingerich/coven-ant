using UnityEngine;

public class EnableChildrenOnStart : MonoBehaviour {
	void Start () {
		foreach( Transform thisChild in transform ) {
			thisChild.gameObject.SetActive(true);
		}
	}
}
