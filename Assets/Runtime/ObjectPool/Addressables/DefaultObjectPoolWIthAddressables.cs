using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement;

namespace JLib.ObjectPool.Addressables
{
    /// <summary>
    /// Object pool using addressables
    /// </summary>
    public class DefaultObjectPoolWIthAddressables : PoolBase<DefaultKey, AssetReference, DefaultPoolObject, DefaultObjectPoolWIthAddressables>
    {
        protected override void DoPoped(DefaultPoolObject popedObject)
        {
            base.DoPoped(popedObject);
            popedObject.OnPoped();
        }

        protected override void DoReturned(DefaultPoolObject returnedObject)
        {
            base.DoReturned(returnedObject);
            returnedObject.OnReturned();
        }
        
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
