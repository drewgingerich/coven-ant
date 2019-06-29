 using UnityEngine;
 public delegate void StateEvent();
 public class StateMachineExitTalker : StateMachineBehaviour
 {
		public event StateEvent onStateExit;
		public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
			onStateExit.Invoke();
		}
 }