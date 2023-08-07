
using System;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    /// <summary>
    /// 오브젝트를 몇 개 생성할지에 대한 정보가 있다.
    /// </summary>
    /// <typeparam name="KeyT">키에 대한 타입 (ex : enum, string)</typeparam>
    /// <typeparam name="ValueT">가르킬 오브젝트의 타입 (ex gameobject, assetreference)</typeparam>
    [Serializable]
    public class PoolData<KeyT, ValueT>
    {
        /// <summary>
        /// 이 것의 키값
        /// </summary>
        public KeyT key;
        /// <summary>
        /// 어떤 것을 생성할지
        /// </summary>
        public ValueT value;
        /// <summary>
        /// 몇 개를 생성할지
        /// </summary>
        public int generationCount;
    }

    /// <summary>
    /// 프로그래머가 원하는 풀을 만들 수 있도록, 제네릭 클래스로 만들었다.
    /// JLib.ObjectPool.DefaultObjectPool과 
    /// JLib.ObjectPool.Addressables.DefaultObjectPool를 참조하여 원하는 것을 구현하라
    /// </summary>
    /// <typeparam name="KeyT">풀 오브젝트를 저장하는데 필요한 키</typeparam>
    /// <typeparam name="ValueT">풀 데이터에서 가르키는 풀 오브젝트의 타입</typeparam>
    /// <typeparam name="GeneratedT">Instantiate로 생성했을 때, 컨트롤할 컴포넌트의 타입 이것은 PoolObject를 상속받은 것만 사용할 수 있다.</typeparam>
    /// <typeparam name="ConcretePoolT">Monosingle에 사용될 타입이다. PoolBase클래스를 상속받은 자식 클래스의 타입을 이곳에 넣어준다.</typeparam>
    public abstract class PoolBase<KeyT, ValueT, GeneratedT, ConcretePoolT> : MonoSingle<ConcretePoolT>
        where GeneratedT : PoolObject<KeyT>
        where ConcretePoolT : PoolBase<KeyT, ValueT, GeneratedT, ConcretePoolT>
    {
        /// <summary>
        /// 이 멤버 변수를 인스펙터에서 설정할 수 있으며, 그를 통해 풀에서 미리 생성해놓을 오브젝트들을 얼마나 생성할지 정할 수 있다.
        /// </summary>
        [SerializeField]
        List<PoolData<KeyT, ValueT>> poolConfig = new List<PoolData<KeyT, ValueT>>();

        /// <summary>
        /// 실제로 시작시에 생성된 오브젝트들을 가지고 있는 풀, 키값으로 관리를 하고 queue를 통해 쉽게 넣고 빼고 할 수 있다.
        /// </summary>
        Dictionary<KeyT, Queue<GeneratedT>> pool = new Dictionary<KeyT, Queue<GeneratedT>>();
       

        private void Awake()
        {
            //config정보를 이용해서 풀을 구성한다.
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

        /// <summary>
        /// 풀에서 오브젝트 하나를 가져온다.
        /// </summary>
        /// <param name="key">가져올 오브젝트의 키</param>
        /// <returns>찾은 풀 오브젝트</returns>
        /// <exception cref="InvalidOperationException">키가 없을시에 이 애러가 throw된다.</exception>
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
                //오브젝트가 풀에서 나올 때의 행동을 해준다.
                DoPoped(one);
                return one;
            }
            else
            {
                var one = found.Dequeue();
                //오브젝트가 풀에서 나올 때의 행동을 해준다.
                DoPoped(one);
                return one;
            }
        }

        /// <summary>
        /// 풀에서 가져온 오브젝트를 풀로 되돌려 보낼때 사용한다.
        /// </summary>
        /// <param name="key">해당 오브젝트에 맞는 키</param>
        /// <param name="value">되돌려보낼 오브젝트</param>
        public void ReturnOne(KeyT key, GeneratedT value)
        {
            Queue<GeneratedT> found = null;
            if (false == pool.TryGetValue(key, out found))
            {
                found = new Queue<GeneratedT>(128);
                pool.Add(key, found);
            }

            //오브젝트가 풀에 들어갈 때의 행동을 해준다.
            DoReturned(value);
            
            found.Enqueue(value);
        }

        /// <summary>
        /// config에서 key를 이용하여 정보를 가져온다.
        /// </summary>
        /// <param name="key">가져오고 싶은 정보의 key</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">정보를 찾지 못하면 이 예외가 리턴된다.</exception>
        private ValueT GetObjectFromPoolConfig(KeyT key)
        {
            for (int i = 0; i < poolConfig.Count; i++)
            {
                if (poolConfig[i].key.Equals(key))
                    return poolConfig[i].value;
            }

            throw new InvalidOperationException($"there is no key{key}");
        }
        
        /// <summary>
        /// config에 있는 Object의 정보를 통해 메모리에 인스턴스화 한다.
        /// </summary>
        /// <param name="poolObject"></param>
        /// <returns></returns>
        protected abstract GeneratedT CreateInstance(ValueT poolObject);
        /// <summary>
        /// CreateInstance에서 인스턴스화 된 친구를 다시 인스턴스할 때 쓰이는 함수이다.
        /// 이렇게 사용하는 이유는 메모리에 올라간 인스턴스를 복제하는 것이 prefab이나 assetreference를 이용하는 것보다 빠르기 대문이다.
        /// </summary>
        /// <param name="instance">인스턴스화 된 오브젝트</param>
        /// <returns>instance파라미터를 이용해 새로 인스턴스화된 오브젝트</returns>
        protected abstract GeneratedT InstantiateUsingInstance(GeneratedT instance);
        /// <summary>
        /// 하나의 오브젝트를 풀에서 빼낼때 할 행위를 정의한다.
        /// </summary>
        /// <param name="popedObject">이번에 빠질 오브젝트</param>
        protected virtual void DoPoped(GeneratedT popedObject) { }
        /// <summary>
        /// 하나의 오브젝트를 다시 풀로 넣을 때 할 행위를 정의한다.
        /// </summary>
        /// <param name="returnedObject">풀로 되돌려 보낼 오브젝트</param>
        protected virtual void DoReturned(GeneratedT returnedObject) { }
    }
}
