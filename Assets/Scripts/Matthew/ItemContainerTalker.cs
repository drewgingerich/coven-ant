using UnityEngine;
using Scriptables.GameEvents;

[RequireComponent(typeof(Selectable))]
public class ItemContainerTalker : MonoBehaviour {
    [SerializeField]
    private GameEvent invalidSelection;
    /**
     * <summary>
     * Read only. Use AddItem, DestroyItem, and ActivateItem
     * </summary>
     */
    public bool hasItem;
    public SpriteRenderer poof;
    public ItemDescriptionViewer descriptionViewer;
    private CharacterCreatorItem item;
    private Selectable selectable;
    public void ActivateItem() {
        // transform.BroadcastMessage("Apply");
        if(hasItem) {
            poof.color = item.gameObject.GetComponentInChildren<SpriteRenderer>().color;
            item.OnApply.Invoke();
            if(item == null) {
                hasItem = false;
            }
            SendMessageUpwards("ItemUsed", this, SendMessageOptions.RequireReceiver);
        } else {
            if( invalidSelection )
                invalidSelection.Raise();
            Debug.LogWarning("No CharacterCreatorItem inside container");
        }
    }
    public void AddItem( CharacterCreatorItem newItem ) {
        Debug.Log("Item " + newItem.itemName + " was refreshed!");
        hasItem = true;
        item = newItem;
        if( selectable.isHovered ) {
            StartHoverItem();
        }
    }

    public void StartHoverItem() {
        if(hasItem) {
            item.OnHover.Invoke();
        }
    }

    public void StopHoverItem() {
        if(hasItem) {
            item.OnStopHover.Invoke();
        }
    }

    public void DestroyItem() {
        if(hasItem) {
            // item.OnDiscard.Invoke();
            Destroy(item);
            hasItem = false;
        } else {
            Debug.LogWarning("Couldn't destroy non-existant item");
        }
    }

    // private bool UpdateItem() {
    //     if( item != null && item.isActiveAndEnabled ) {
    //         return true;
    //     } else {
    //         item = transform.GetComponentInChildren<ApplyEffect>();
    //         return item != null && item.isActiveAndEnabled;
    //     }
    // }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        selectable = GetComponent<Selectable>();
    }
}
