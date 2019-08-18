using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib
{
    [AddComponentMenu("JTween/TweenPosition")]
    public class TweenPosition : Tween<Vector3>
    {

        protected override void OnOnEnable()
        {
            transform.position = realFrom;
        }

        protected override void OnTweenUpdate()
        {
            transform.position = Lerp();
        }

        public override Vector3 Lerp()
        {
            return Vector3.Lerp(realFrom,realTo,curveValue);
        }


    }
}
