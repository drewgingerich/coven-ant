using System.Collections;
using UnityEngine;

/// <summary>
/// Makes the gameobject a glower, not a shower 😉
/// </summary>
public class GlowOnCommand : MonoBehaviour
{
	[Header("Config")]
	public Color startingColor;
	public Color endingColor;
	/// <summary>
	/// How long should it take to change colors in one direction
	/// </summary>
	[Tooltip("How long should it take to change colors in one direction?")]
	public float glowDuration;
	public float glowStickyness;
	public AnimationCurve glowAnimation;
	[Header("Behavior")]
	public bool glowOnStart;
	private float currentGlowPosition = 0.0f;
	/// <summary>
	/// Flag for if the glow routine is running
	/// </summary>
	private bool glowing = false;
	/// <summary>
	/// Flag for if the Update() function is fully paused.
	/// </summary>
	private bool paused = true;
	private Material material;
	public void StartGlowing() {
		if(!glowing){
			StartCoroutine(GlowRoutine());
		} else {
			Debug.LogWarning("Already glowing.");
		}
	}

	/// <summary>
	/// Immediately pauses the glow
	/// </summary>
	public void PauseGlowing() {

	}

	/// <summary>
	/// Gently causes the glow to return to the startingColor
	/// </summary>
	public void GentlyStopGlowing() {

	}

	/// <summary>
	/// Immediately pauses the glow and returns it to the startingColor
	/// </summary>
	public void HardStopGlowing() {

	}

	IEnumerator GlowRoutine() {
		glowing = true;
		paused = false;
		while(!paused) {
			for(float time = 0.0f; time < glowDuration; time += Time.deltaTime) {
				yield return null;
			}
		}
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {
		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if( sprite ) {
			material = sprite.material;
		} else {
			MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
			if( meshRenderer ) {
				material = meshRenderer.material;
			} else {
				Debug.LogError("Object does not contain a GlowOnCommand compatible Renderer!");
			}
		}
	}

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start() {
		if(glowOnStart) {
			StartGlowing();
		}
	}

}
