using JLib.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace JLib.ObjectPool.Sample
{
    public class TestPoolObject : DefaultPoolObject<TestPoolKey>
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
            TestObjectPool.Instance.ReturnOne(this.key, this);
        }
    }
}