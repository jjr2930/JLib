using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JLib
{
    public static class V3Extension
    {
        public static void Add(ref Vector3 v1, Vector3 v2)
        {
            v1.x += v2.x;
            v1.y += v2.y;
            v1.z += v2.z;
        }

        public static void Subtract(ref Vector3 v1, Vector3 v2)
        {
            v1.x -= v2.x;
            v1.y -= v2.y;
            v1.z -= v2.z;
        }
        public static void Multiply(ref Vector3 v1, float factor)
        {
            v1.x *= factor;
            v1.y *= factor;
            v1.z *= factor;
        }
        public static void Divide(ref Vector3 v1, float factor)
        {
            v1.x /= factor;
            v1.y /= factor;
            v1.z /= factor;
        }

        public static void SetZero(ref Vector3 v1)
        {
            v1.x = 0f;
            v1.y = 0f;
            v1.z = 0f;
        }

        public static void SetCross(ref Vector3 v1, ref Vector3 v2, ref Vector3 result)
        {
            result.x = v1.y * v2.z - v1.z * v2.y;
            result.y = v1.z * v2.x - v1.x * v2.z;
            result.z = v1.x * v2.y - v1.y * v2.x;
        }
    }
}
