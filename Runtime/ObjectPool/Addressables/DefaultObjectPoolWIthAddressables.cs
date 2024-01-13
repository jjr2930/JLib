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
    public class DefaultObjectPoolWithAddressables<KeyT, ConcretePoolT> : PoolBase<KeyT, AssetReference, DefaultPoolObject<KeyT>, ConcretePoolT>
        where ConcretePoolT : PoolBase<KeyT, AssetReference, DefaultPoolObject<KeyT>, ConcretePoolT>
    {
        protected override void DoPoped(DefaultPoolObject<KeyT> popedObject)
        {
            base.DoPoped(popedObject);
            popedObject.OnPoped();
        }

        protected override void DoReturned(DefaultPoolObject<KeyT> returnedObject)
        {
            base.DoReturned(returnedObject);
            returnedObject.OnReturned();
        }
        
        protected override DefaultPoolObject<KeyT> CreateInstance(AssetReference poolObject)
        {
            var one = poolObject.InstantiateAsync().WaitForCompletion();
            var defaultPoolObject = one.GetComponent<DefaultPoolObject<KeyT>>();
            if (null == defaultPoolObject)
                throw new InvalidOperationException($"{poolObject.ToString()} is null");

            return defaultPoolObject;
        }

        protected override DefaultPoolObject<KeyT> InstantiateUsingInstance(DefaultPoolObject<KeyT> instance)
        {
            return Instantiate(instance);
        }
    }
}
