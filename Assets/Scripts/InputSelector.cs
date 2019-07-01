using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputSelector : MonoBehaviour
{
    public UnityEvent onHackSubmit;
    InputField m_InputField;

    bool m_CanSubmit = false;

    // Update is called once per frame
    void Update()
    {
        if (m_CanSubmit && Input.GetButtonUp("Submit"))
        {
            onHackSubmit.Invoke();
        }
    }

    void OnEnable()
    {
        m_InputField = GetComponent<InputField>();
        StartCoroutine(InputDelay());
    }

    public void SubmitNow() {
        onHackSubmit.Invoke();
    }

    // HACK: needed to have the "Finish" button display and be selected so that
    // the user could just hit enter / space to click it, but for some reason
    // Unity wouldn't let me. So I added an InputField, which Unity does allow selecting,
    // and put the finish event on that. But that was being triggered immediately for
    // some reason, so I had to add an input delay so there would be no chance the
    // button press to show the Finish button would accidentally also trigger the
    // scene transition. This was way too difficult for such a simple task. ;-P
    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(1f);
        m_CanSubmit = true;
    }
}
