using UnityEngine;
using System.Collections;
using System;

[AddComponentMenu("JTween/TweenRectSize")]
public class TweenRectSize : JLib.Tween<Vector2>
{
    protected override void OnOnEnable()
    {
        rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal , realFrom.x );
        rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical , realFrom.y );
    }

    protected override void OnTweenUpdate()
    {
        Vector2 nextSize = Lerp();
        rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Horizontal , nextSize.x );
        rectTransform.SetSizeWithCurrentAnchors( RectTransform.Axis.Vertical , nextSize.y );
    }

    public override Vector2 Lerp()
    {
        return Vector2.Lerp( realFrom , realTo , curveValue ); 
    }
}
