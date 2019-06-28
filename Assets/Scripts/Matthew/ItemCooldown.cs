using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Scriptables.UnityEvents;

/// <summary>
/// This is one of those 'hot stuff' scripts which relies on `StateMachineExitTalker` attached to the Animator to actually function
/// <seealso cref="StateMachineExitTalker" /> 
/// </summary>
public class ItemCooldown : MonoBehaviour
{

	public Animator animator;
	public GameObjectUnityEvent onCooldownComplete;
	public ItemContainerTalker targetContainer;
	private StateMachineExitTalker talker;
	
	/// <summary>
	/// Invoked when the `StateMachineExitTalker` emits its event. 
	/// <seealso cref="StateMachineExitTalker.onStateExit" /> 
	/// </summary>
	public void CooldownComplete() {
		// Debug.Log("CooldownComplete Invoked");
		onCooldownComplete.Invoke(targetContainer.gameObject);
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable() {   
		talker = animator.GetBehaviour<StateMachineExitTalker>();
		talker.onStateExit += CooldownComplete;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable() {
		talker.onStateExit -= CooldownComplete;
	}
}
