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
    private bool cascadeSpawning = false;
    public bool refillContainerOnUse;
    public GameObjectGameEvent containerEmptiedEvent;
    public float durationBetweenEachItemSpawnAtStart;
    private List<ItemContainerTalker> containers = new List<ItemContainerTalker>();
    
    public void PopulateSpecificContainer(ItemContainerTalker container) {
        if( container.hasItem ) {
            container.DestroyItem();
        }
        int randomItem = Random.Range(0, libraryOfItems.Value.Count);
        Debug.Log("For container '" + container.name + "', Selecting item #" + randomItem + ": " + libraryOfItems.Value[randomItem].name );
        
        GameObject newItemGameObject = Instantiate(
            libraryOfItems.Value[randomItem],
            container.transform.position,
            container.transform.rotation,
            container.transform
        );

        CharacterCreatorItem newItem = newItemGameObject.GetComponent<CharacterCreatorItem>();
        if(newItem == null) {
            Debug.LogError("Item " + newItem.name + " did not contain a CharacterCreatorItem");
            return;
        }
        container.AddItem(newItem);
        container.descriptionViewer.UpdateDescriptionFromItem(newItem.gameObject);
        onItemSpawned.Raise(newItem.gameObject);
    }

    public void ItemUsed(ItemContainerTalker container) {
        if( refillContainerOnUse ) {
            PopulateSpecificContainer(container);
        }
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
        cascadeSpawning = true;
        foreach (ItemContainerTalker container in containers) {
            PopulateSpecificContainer(container);
            yield return new WaitForSeconds(durationBetweenEachItemSpawnAtStart);
        }
        cascadeSpawning = false;
    }
}
