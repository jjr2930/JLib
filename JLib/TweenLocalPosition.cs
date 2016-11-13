using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public class TweenLocalPosition : Tween
    {
        [SerializeField]
        Vector3 from = Vector3.zero;

        [SerializeField]
        Vector3 to = Vector3.zero;

        protected override void OnTweenUpdate()
        {
            Vector3 targetPosition = Vector3.Lerp(from, to, normalTime * curveValue);
            transform.localPosition = targetPosition;
        }
    }
}
