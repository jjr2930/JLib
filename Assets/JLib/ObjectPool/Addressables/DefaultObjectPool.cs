using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

namespace JLib.ObjectPool.Addressables
{
    /// <summary>
    /// addressable을 이용한 오브젝트풀
    /// </summary>
    public class DefaultObjectPool : PoolBase<DefaultKey, AssetReference, DefaultPoolObject, DefaultObjectPool>
    {
        protected override DefaultPoolObject CreateInstance(AssetReference poolObject)
        {
            var one = poolObject.InstantiateAsync().WaitForCompletion();
            var defaultPoolObject = one.GetComponent<DefaultPoolObject>();
            if (null == defaultPoolObject)
                throw new InvalidOperationException($"{poolObject.ToString()} is null");

            return defaultPoolObject;
        }

        protected override DefaultPoolObject InstantiateUsingInstance(DefaultPoolObject instance)
        {
            return Instantiate(instance);
        }
    }
}
