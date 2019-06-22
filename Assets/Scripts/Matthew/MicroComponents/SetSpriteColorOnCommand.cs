using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SetSpriteColorOnCommand : MonoBehaviour {
    public Color colorToActivate;
    public bool activateOnStart = false;
    private Color originalColor;
    private SpriteRenderer sprite;
    private bool colorApplied = false;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
        colorApplied = false;
        if(activateOnStart) {
            ActivateColor();
        }
    }

    public void ActivateColor() {
        if(!colorApplied) {
            originalColor = sprite.color;
        }
        sprite.color = colorToActivate;
        colorApplied = true;
    }

    public void DeActivateColor() {
        sprite.color = originalColor;
        colorApplied = false;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable() {
        DeActivateColor();
    }

}
