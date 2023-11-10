using JLib.ObjectPool.Sample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JLib.ObjectPool.Addressables.Sample
{

    public class Spawner : MonoBehaviour
    {
        [SerializeField] float spawnDelay = 0.1f;

        private void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            while(true)
            {
                yield return new WaitForSeconds(spawnDelay);
                var newObject = TestObjectPoolWithAddressables.Instance.PopOne(TestPoolKey.Square);
                newObject.transform.position = Vector3Utility.RandomPositionInCircle(Vector3.zero, 10f);
                
                yield return new WaitForSeconds (spawnDelay);
                newObject = TestObjectPoolWithAddressables.Instance.PopOne (TestPoolKey.Circle);
                newObject.transform.position = Vector3Utility.RandomPositionInCircle(Vector3.zero, 10f);
            }
        }
    }
}