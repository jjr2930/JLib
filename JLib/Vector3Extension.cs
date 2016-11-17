using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JLib
{
    public static class Vector3Extension
    {
        public static void Add(this Vector3 v1, Vector3 v2)
        {
            v1.x += v2.x;
            v1.y += v2.y;
            v1.z += v2.z;
        }

        public static void Subtract(this Vector3 v1, Vector3 v2)
        {
            v1.x -= v2.x;
            v1.y -= v2.y;
            v1.z -= v2.z;
        }
        public static void Multiply(this Vector3 v1, float factor)
        {
            v1.x *= factor;
            v1.y *= factor;
            v1.z *= factor;
        }
        public static void Divide(this Vector3 v1, float factor)
        {
            v1.x /= factor;
            v1.y /= factor;
            v1.z /= factor;
        }
    }
}
