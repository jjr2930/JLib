using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    /// <summary>
    /// 풀 오브젝트의 기본 클래스
    /// </summary>
    /// <typeparam name="KeyT"></typeparam>
    [Serializable]
    public class PoolObject<KeyT> : MonoBehaviour
    {
        /// <summary>
        /// 풀에서 관리될 오브젝트의 키
        /// </summary>
        public KeyT key;
        /// <summary>
        /// 처음 만들어졌을 때 할 행위
        /// </summary>
        public virtual void OnCreated() { }
        /// <summary>
        /// 풀에서 나올 때 할 행위
        /// </summary>
        public virtual void OnPoped() { }
        /// <summary>
        /// 풀로 다시 들어갈 때 할 행위
        /// </summary>
        public virtual void OnReturned() { }
    }
}
