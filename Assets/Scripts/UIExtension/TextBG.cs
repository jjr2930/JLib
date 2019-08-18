using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBG : MonoBehaviour {
    Image img = null;
    void Awake()
    {
        img = GetComponent<Image>();
    }	

    public void SetResize(Vector2 size)
    {
        img.rectTransform.sizeDelta = size;
    }
}
