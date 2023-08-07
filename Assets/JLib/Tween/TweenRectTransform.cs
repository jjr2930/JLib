using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Tween
{
    public abstract class TweenRectTransform<T>: Tween<T> , ITween
    {
        [SerializeField]
        protected RectTransform rectTransform;

        public virtual void Reset()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}
