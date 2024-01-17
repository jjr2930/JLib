using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    public class PoolObject : MonoBehaviour
    {
        /// <summary>
        /// ó�� ��������� �� �� ����
        /// </summary>
        public virtual void OnCreated() { }
        /// <summary>
        /// Ǯ���� ���� �� �� ����
        /// </summary>
        public virtual void OnPoped() { }
        /// <summary>
        /// Ǯ�� �ٽ� �� �� �� ����
        /// </summary>
        public virtual void OnReturned() { }
    }

    /// <summary>
    /// Ǯ ������Ʈ�� �⺻ Ŭ����
    /// </summary>
    /// <typeparam name="KeyT"></typeparam>
    [Serializable]
    public class PoolObject<KeyT> : PoolObject
    {
        /// <summary>
        /// Ǯ���� ������ ������Ʈ�� Ű
        /// </summary>
        public KeyT key;
    }
}
