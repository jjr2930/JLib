using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib;


public class TweenShake : Tween<Vector3>
{
    [SerializeField]
    Vector3 deltaLimit = Vector3.zero;

    Vector3 originPosition = Vector3.zero;
    Vector3 originLocalPosition = Vector3.zero;
    
    System.Action updateDelegate = null;
    bool isIncrease = false;
    // Use this for initialization
    protected override void OnAwake()
    {
        base.OnAwake();
        updateDelegate = DecreaseShake;
    }

    protected override void OnOnEnable()
    {
        originPosition = this.transform.position;
        originLocalPosition = this.transform.localPosition;
    }

    protected override void OnTweenUpdate()
    {
        updateDelegate();
    }
    
    void IncreaseShake()
    {
        Vector3 delta;
        delta.x = Random.Range( -deltaLimit.x , deltaLimit.x ) * ( 1 - curveValue );
        delta.y = Random.Range( -deltaLimit.y , deltaLimit.y ) * ( 1 - curveValue );
        delta.z = Random.Range( -deltaLimit.z , deltaLimit.z ) * ( 1 - curveValue );
        
        this.transform.localPosition = originLocalPosition + delta;

        Debug.Log(1-curveValue);
    }

    void DecreaseShake()
    {
        Vector3 delta;
        delta.x = Random.Range( -deltaLimit.x , deltaLimit.x ) * curveValue;
        delta.y = Random.Range( -deltaLimit.y , deltaLimit.y ) * curveValue;
        delta.z = Random.Range( -deltaLimit.z , deltaLimit.z ) * curveValue;

        this.transform.localPosition = originLocalPosition + delta;

        Debug.Log(curveValue);
    }

    public override Vector3 Lerp()
    {
        return Vector3.zero;
    }

    protected override void OnOnDisable()
    {
        base.OnOnDisable();
        this.transform.position = originPosition;
    }

}
