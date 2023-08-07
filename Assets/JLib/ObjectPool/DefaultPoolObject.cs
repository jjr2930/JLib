using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool
{
    /// <summary>
    /// JLib.ObjectPool.DefaultObjectPool,
    /// JLib.ObjectPoo.Addressable.DefaultObjectPool
    /// 에서 사용할 오브젝트풀의 컴포넌트 앞의 두개의 클래스는 DefaultPoolObject클래스를 사용한다.
    /// </summary>

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
