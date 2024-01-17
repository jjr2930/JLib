using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    [Serializable]
    public class StateMachineValue : ScriptableObject
    {
        public virtual void CopyValueToRuntimeValue() { }
    }
}
