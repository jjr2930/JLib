
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    [Serializable]
    public class PoolData<KeyT, ValueT>
    {
        public KeyT key;
        public ValueT value;
        public int generationCount;
    }

    public abstract class PoolBase<KeyT,ValueT,GeneratedT,ConcreteT> : MonoSingle<ConcreteT>
        where GeneratedT : PoolObject<KeyT>
        where ConcreteT : PoolBase<KeyT, ValueT, GeneratedT, ConcreteT>
    {
        [SerializeField]
        List<PoolData<KeyT, ValueT>> poolConfig = new List<PoolData<KeyT, ValueT>>();

        Dictionary<KeyT, Queue<GeneratedT>> pool = new Dictionary<KeyT, Queue<GeneratedT>>();
       

        private void Awake()
        {
            foreach (var item in poolConfig)
            {
                if (item.generationCount == 0)
                {
                    Debug.Log($"some item({item.key.ToString()}) count is 0, please check it");
                    continue;
                }

                var poolQueue = new Queue<GeneratedT>(item.generationCount);
                var originInstance = CreateInstance(item.value);

                for (int i = 0; i < item.generationCount - 1; i++)
                {
                    var instance = InstantiateUsingInstance(originInstance);
                    instance.OnReturned();
                    poolQueue.Enqueue(instance);    
                }

                pool.Add(item.key, poolQueue);
            }            
        }

        public GeneratedT PopOne(KeyT key)
        {
            Queue<GeneratedT> found = null;
            if (false == pool.TryGetValue(key, out found))
                throw new InvalidOperationException($"invalid key : {key}");

            //if not enough, make one.
            if(0 == found.Count)
            {
                Debug.Log("no anymore object in pool, create one");
                var one = CreateInstance(GetObjectFromPoolConfig(key));
                DoPoped(one);
                return one;
            }
            else
            {
                var one = found.Dequeue();
                DoPoped(one);
                return one;
            }
        }

        public void ReturnOne(KeyT key, GeneratedT value)
        {
            Queue<GeneratedT> found = null;
            if (false == pool.TryGetValue(key, out found))
            {
                found = new Queue<GeneratedT>(128);
                pool.Add(key, found);
            }

            DoReturned(value);
            found.Enqueue(value);
        }

        private ValueT GetObjectFromPoolConfig(KeyT key)
        {
            for (int i = 0; i < poolConfig.Count; i++)
            {
                if (poolConfig[i].key.Equals(key))
                    return poolConfig[i].value;
            }

            throw new InvalidOperationException($"there is no key{key}");
        }

        protected abstract GeneratedT CreateInstance(ValueT poolObject);
        protected abstract GeneratedT InstantiateUsingInstance(GeneratedT instance);
        protected virtual void DoPoped(GeneratedT popedObject) { }
        protected virtual void DoReturned(GeneratedT returnedObject) { }
    }
}
