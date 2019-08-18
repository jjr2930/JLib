using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JLib
{
    [AddComponentMenu("JTween/TweenLocalRotation")]
    public class TweenLocalRotation : Tween<Vector3>
    {
        protected override void OnOnEnable()
        {
            transform.localRotation = Quaternion.Euler( realFrom );
        }

        protected override void OnTweenUpdate()
        {

            transform.rotation = Quaternion.Euler( Lerp() );
        }

        public override Vector3 Lerp()
        {
            return Vector3.Lerp(realFrom,realTo,curveValue);
        }

    }
}
