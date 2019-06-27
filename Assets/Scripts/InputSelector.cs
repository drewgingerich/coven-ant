using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class InputSelector : MonoBehaviour
{
    public UnityEvent onHackSubmit;
    InputField m_InputField;

    bool m_CanSubmit = false;

    // Use this for initialization
    void Start()
    {
        
    }

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
        //StartCoroutine(SelectLater());
    }

    IEnumerator InputDelay()
    {
        yield return new WaitForSeconds(1f);
        m_CanSubmit = true;
    }

    //IEnumerator SelectLater()
    //{
    //    yield return new WaitForSeconds(1f);
    //    m_InputField.interactable = true;
    //    m_InputField.Select();
    //    m_InputField.onEndEdit.AddListener(delegate { onHackSubmit.Invoke(); });
    //}
}
