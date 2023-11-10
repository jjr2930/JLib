using UnityEngine;

namespace JLib.ObjectPool.Sample
{
    public static class Vector3Utility
    {
        public static Vector3 RandomPositionInCircle(Vector3 startPosition, float radius)
        {
            float randomAngle = Random.Range(0f, 360f);
            float randomRadius = Random.Range(0f, radius);

            Vector3 randomPosition = new Vector3();
            randomPosition.x = randomRadius * Mathf.Sin(randomAngle);
            randomPosition.y = randomRadius * Mathf.Cos(randomAngle);
            randomPosition.z = 0f;
            randomPosition += startPosition;
            return randomPosition;
        }
    }
}