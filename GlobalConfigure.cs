using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    [Serializable]
    public class GlobalConfigure : MonoSingle<GlobalConfigure>
    {
        public float DEFAULT_RADIUS;
        public float DEFAULT_GLOSS;
        public float DEFAULT_ELASTICITY;
        public float DEFAULT_WEIGHT;
    }
}
