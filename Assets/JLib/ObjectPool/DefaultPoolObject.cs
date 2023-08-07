using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    public class DefaultPoolObject : PoolObject<DefaultKey>
    {
        public override void OnPoped()
        {
            base.OnPoped();
            gameObject.SetActive(true);
        }

        public override void OnReturned()
        {
            base.OnReturned();
            gameObject.SetActive(false);
        }
    }
}
