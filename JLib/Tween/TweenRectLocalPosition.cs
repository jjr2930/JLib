using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    [AddComponentMenu("JTween/TweenRectLocalPosition")]
    [RequireComponent(typeof(RectTransform))]
    public class TweenRectLocalPosition : Tween<Vector3>
    {
        bool isValidate = true;
        protected override void OnAwake()
        {
            if(null == rectTransform)
            {
                isValidate = false;
            }
        }

        protected override void OnOnEnable()
        {
            if (!isValidate)
            {
                Debug.LogError("TweenRectLocalPosition is not validation component , please addcomponent to gameobject, which have RectTransfrom");
                Debug.Break();                
            }
            duringTime = 0;
            startTime = JTime.Time;
            rectTransform.anchoredPosition = realFrom;
        }

        protected override void OnTweenUpdate()
        {
            rectTransform.anchoredPosition = Lerp();
        }

        public override Vector3 Lerp()
        {
            return Vector3.Lerp(realFrom,realTo,curveValue);
        }
    }
}
