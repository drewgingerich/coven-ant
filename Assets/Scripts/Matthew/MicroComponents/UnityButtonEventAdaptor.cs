using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Code adapted (heh) from https://answers.unity.com/questions/820599/simulate-button-presses-through-code-unity-46-gui.html
/// </summary>
[RequireComponent(typeof(Button))]
public class UnityButtonEventAdaptor : MonoBehaviour
{
	private Button button;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		button = GetComponent<Button>();
	}

	public void Hover() {
		var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerEnterHandler);
	}

	public void UnHover() {
		var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerExitHandler);
	}

	public void Submit() {
		var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.submitHandler);
	}

	public void PressDown() {
		var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerDownHandler);
	}

	public void PressUp() {
		var pointer = new PointerEventData(EventSystem.current); // pointer event for Execute
		ExecuteEvents.Execute(button.gameObject, pointer, ExecuteEvents.pointerUpHandler);
	}
}