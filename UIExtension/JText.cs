using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
public class JText : Text {
    [SerializeField]
    UnityEvent valueChanged = null;
    public override string text
    {
        get
        {
            return base.text;
        }

        set
        {
            base.text = value;
            valueChanged.Invoke();
        }
    }
}
