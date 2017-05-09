using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib;
using System;

namespace JLib
{
    [AddComponentMenu( "JTween/TweenRotation" )]
    public class TweenRotation : Tween<Vector3>
    {
        protected override void OnOnEnable()
        {
            transform.rotation = Quaternion.Euler( realFrom );
        }

        protected override void OnTweenUpdate()
        {
            transform.rotation = Quaternion.Euler( Lerp() );
        }

        public override Vector3 Lerp()
        {
            return Vector3.Lerp( realFrom , realTo , curveValue );
        }
    }
}