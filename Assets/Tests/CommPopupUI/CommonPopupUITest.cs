using JLib.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonPopupUITest : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        CommonPopupUI.Instance.Show("this is title",
            "this is message",
            "okok", "cancelcancel",
            () => { Debug.Log("Ok clicked"); },
            () => { Debug.Log("Cancel clicked"); });
    }

}
