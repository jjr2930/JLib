using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.ObjectPool.Sample
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] float spawnDelay = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Spawn());
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnDelay);
                var newObject = TestObjectPool.Instance.PopOne(TestPoolKey.Square);
                newObject.transform.position = Vector3Utility.RandomPositionInCircle(Vector3.zero, 10f);

                yield return new WaitForSeconds(spawnDelay);
                newObject = TestObjectPool.Instance.PopOne(TestPoolKey.Circle);
                newObject.transform.position = Vector3Utility.RandomPositionInCircle(Vector3.zero, 10f);
            }
        }
    }
}