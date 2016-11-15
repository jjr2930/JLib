using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JLib
{
    [AddComponentMenu("JTween/TweenLocalRotation")]
    public class TweenLocalRotation : Tween
    {
        [SerializeField]
        Vector3 from;
        
        [SerializeField]
        Vector3 to;


        protected override void OnOnEnable()
        {
            transform.rotation = Quaternion.Euler(from);
        }

        protected override void OnTweenUpdate()
        {
            Vector3 targetRotation = Vector3.Lerp(from, to, normalTime * curveValue);
            transform.rotation = Quaternion.Euler( targetRotation );
        }
    }
}
