
using System;
using UnityEngine;
namespace JLib
{
    [AddComponentMenu("JTween/TweenColor")]
    public class TweenColor : Tween<Color>
    {
        new Renderer renderer = null;
        protected override void OnAwake()
        {
            renderer = GetComponent<Renderer>();
        }
        protected override void OnOnEnable()
        {
            renderer.material.color = realFrom;
        }

        protected override void OnTweenUpdate()
        {
            renderer.material.color = Lerp();
        }

        public override Color Lerp()
        {
            return Color.Lerp(realFrom, realTo, curveValue);
        }
    }
}
