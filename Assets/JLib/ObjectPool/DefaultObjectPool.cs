using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    /// <summary>
    /// Addressables를 사용하지 않는 오브젝트 풀
    /// </summary>
    public class DefaultObjectPool : PoolBase<DefaultKey, GameObject, DefaultPoolObject, DefaultObjectPool>
    {
        protected override DefaultPoolObject CreateInstance(GameObject poolObject)
        {
            var one = Instantiate(poolObject);
            var defaultPoolObject = one.GetComponent<DefaultPoolObject>();
            if (null == defaultPoolObject)
                throw new InvalidOperationException($"{one.name} has no DefaultPoolObject");

            return defaultPoolObject;
        }

        protected override DefaultPoolObject InstantiateUsingInstance(DefaultPoolObject instance)
        {
            return Instantiate(instance);
        }

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
    }
}
