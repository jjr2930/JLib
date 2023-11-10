using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib.ObjectPool;
namespace JLib.ObjectPool.Addressables.Sample
{

    public class TestPoolObjectWithAddressables : DefaultPoolObject<TestPoolKey>
    {
        public override void OnPoped()
        {
            base.OnPoped();
            StartCoroutine(ReturnToPool());
        }

        private void Update()
        {
            transform.Rotate(Vector3.forward, 90f * Time.deltaTime);
        }

        IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(5f);
            TestObjectPoolWithAddressables.Instance.ReturnOne(this.key, this);
        }
    }

}