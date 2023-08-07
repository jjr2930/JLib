using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib.ObjectPool;

public class PoolTestScript : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            DefaultPoolObject p1 = DefaultObjectPool.Instance.PopOne(DefaultKey.SampleKey1);
            DefaultObjectPool.Instance.ReturnOne(p1.key, p1);
        }        
        

        for (int i = 0; i < 1; i++)
        {
            DefaultPoolObject p1 = DefaultObjectPool.Instance.PopOne(DefaultKey.SampleKey2);
        }

        for (int i = 0; i < 23; i++)
        {
            DefaultPoolObject p1 = DefaultObjectPool.Instance.PopOne(DefaultKey.SampleKey4);
        }
    }
}
