using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// int bool float 등과 같은 기본 자료형을 사용하기 위한 파라미터이다.
/// 유니티의 스트럭쳐 Quaternion, Vector3도 만들어주자.
/// </summary>
namespace JLib
{
    [Serializable]
    public class IntParameter
    {
        public int value;
    }

    [Serializable]
    public class BoolParameter
    {
        public bool value;
    }

    [Serializable]
    public class FloatParameter
    {
        public float value;
    }

    [Serializable]
    public class QuaternionParameter
    {
        public Quaternion value;
    }

    [Serializable]
    public class Vector3Parameter
    {
        public Vector3 value;
    }

}
