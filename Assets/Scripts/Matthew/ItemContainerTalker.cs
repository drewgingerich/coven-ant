using UnityEngine;
using Scriptables.GameEvents;

public class ItemContainerTalker : MonoBehaviour {
    [SerializeField]
    private GameEvent invalidSelection;
    /**
     * <summary>
     * Read only. Use AddItem, DestroyItem, and ActivateItem
     * </summary>
     */
    public bool hasItem;
    private CharacterCreatorItem item;
    public void ActivateItem() {
        // transform.BroadcastMessage("Apply");
        if(hasItem) {
            item.OnApply.Invoke();
            Destroy(item);
            SendMessageUpwards("ItemUsed", this, SendMessageOptions.RequireReceiver);
            hasItem = false;
            item = null;
        } else {
            if( invalidSelection )
                invalidSelection.Raise();
            Debug.LogWarning("No CharacterCreatorItem inside container");
        }
    }
    public void AddItem( CharacterCreatorItem newItem ) {
        hasItem = true;
        item = newItem;
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
}
