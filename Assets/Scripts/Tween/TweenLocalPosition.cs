using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    [AddComponentMenu("JTween/TweenLocalPosition")]
    public class TweenLocalPosition : Tween<Vector3>
    {
        protected override void OnOnEnable()
        {
            transform.localPosition = from;
        }

        protected override void OnTweenUpdate()
        {
            transform.localPosition = Lerp();
        }

        public override Vector3 Lerp()
        {
            return Vector3.Lerp(realFrom,realTo,curveValue);
        }
    }
}
