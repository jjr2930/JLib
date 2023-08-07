using JLib.ObjectPool.Addressables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressablePoolTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var one1 = DefaultObjectPool.Instance.PopOne(JLib.ObjectPool.DefaultKey.SampleKey1);
        DefaultObjectPool.Instance.ReturnOne(one1.key, one1);
    }
}
