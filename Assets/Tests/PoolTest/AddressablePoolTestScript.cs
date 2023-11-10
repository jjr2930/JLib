using JLib.ObjectPool.Addressables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressablePoolTestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var one1 = DefaultObjectPoolWIthAddressables.Instance.PopOne(JLib.ObjectPool.DefaultKey.SampleKey1);
        DefaultObjectPoolWIthAddressables.Instance.ReturnOne(one1.key, one1);
    }
}
