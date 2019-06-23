using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scriptables.Variables;
using Scriptables.GameEvents;

public class ItemPopulator : MonoBehaviour {
    public GameObjectGameEvent onItemSpawned;
    public PrefabList libraryOfItems;
    [Header("Item Spawning")]
    public bool populateItemsOnStart;
    public float durationBetweenEachItemSpawnAtStart;
    private List<ItemContainerTalker> containers = new List<ItemContainerTalker>();
    
    public void PopulateSpecificContainer(ItemContainerTalker container) {
        if( container.hasItem ) {
            container.DestroyItem();
        }
        int randomItem = Random.Range(0, libraryOfItems.Value.Count);
        CharacterCreatorItem newItem = Instantiate(
            libraryOfItems.Value[randomItem],
            container.transform.position,
            container.transform.rotation,
            container.transform
        ).GetComponent<CharacterCreatorItem>();
        if(newItem == null) {
            Debug.LogError("Item did not contain a CharacterCreatorItem");
        }
        container.AddItem(newItem);
        onItemSpawned.Raise(newItem.gameObject);
    }

    public void ItemUsed(ItemContainerTalker container) {

    }

    void Start() {
        if( containers.Count == 0 ) {
            transform.GetComponentsInChildren<ItemContainerTalker>(false, containers);
            if( containers.Count == 0 ) {
                Debug.LogWarning("No Item Containers in this collection");
            }
        }
        if( populateItemsOnStart ) {
            StartCoroutine(CascadeSpawnItems());
        }
    }
    
    
    IEnumerator CascadeSpawnItems() {
        foreach (ItemContainerTalker container in containers) {
            PopulateSpecificContainer(container);
            yield return new WaitForSeconds(durationBetweenEachItemSpawnAtStart);
        }
    }
}
