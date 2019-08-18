using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;

[RequireComponent(typeof(Toggle))]
public class JToggleExtension : MonoBehaviour
{
    [SerializeField]
    Toggle toggle = null;

    [SerializeField]
    UnityEvent trueAction = null;

    [SerializeField]
    UnityEvent falseActions = null;
    public void Awake()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(CallValueChanged);
    }

    public void CallValueChanged(bool value)
    {
        if(value)
        {
            if (null != trueAction)
            {
                trueAction.Invoke();
            }
        }

        else
        {
            if (null != falseActions)
            {
                falseActions.Invoke();
            }
        }
    }
}
