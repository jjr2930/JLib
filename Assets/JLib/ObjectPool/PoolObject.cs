using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    [Serializable]
    public class PoolObject<KeyT> : MonoBehaviour
    {
        public KeyT key;
        public virtual void OnPoped() { }
        public virtual void OnReturned() { }
    }
}
