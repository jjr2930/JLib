using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using JLib;
using System;

namespace JLib
{
    [AddComponentMenu("JTween/TweenFontColor")]
    public class TweenFontColor : Tween<Color>
    {
        Text txt = null;

        // Use this for initialization
        protected override void OnAwake()
        {
            txt = GetComponent<Text>();
        }

        protected override void OnOnEnable()
        {
            txt.color = realFrom;
        }

        protected override void OnTweenUpdate()
        {
            txt.color = Lerp();
        }

        public override Color Lerp()
        {
            return Color.Lerp( realFrom , realTo , curveValue );
        }

    }
}