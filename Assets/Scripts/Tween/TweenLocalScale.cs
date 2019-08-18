using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JLib
{
    [AddComponentMenu("JTween/TweenLocalScale")]
    public class TweenLocalScale : Tween<Vector3>
    {
        protected override void OnOnEnable()
        {
            transform.localScale = realFrom;
        }

        protected override void OnTweenUpdate()
        {
            transform.localScale = Lerp();
        }

        public override Vector3 Lerp()
        {
            return Vector3.Lerp(realFrom,realTo,curveValue);
        }

    }
}
