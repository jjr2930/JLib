using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    /// <summary>
    /// Addressables를 사용하지 않는 오브젝트 풀
    /// </summary>
    public class DefaultObjectPool<KeyT> : PoolBase<KeyT, GameObject, DefaultPoolObject<KeyT>, DefaultObjectPool<KeyT>>
    {
        protected override DefaultPoolObject<KeyT> CreateInstance(GameObject poolObject)
        {
            var one = Instantiate(poolObject);
            var defaultPoolObject = one.GetComponent<DefaultPoolObject<KeyT>>();
            if (null == defaultPoolObject)
                throw new InvalidOperationException($"{one.name} has no DefaultPoolObject");

            return defaultPoolObject;
        }

        protected override DefaultPoolObject<KeyT> InstantiateUsingInstance(DefaultPoolObject<KeyT>  instance)
        {
            return Instantiate(instance);
        }

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
    }
}
