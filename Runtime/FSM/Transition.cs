using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    
    public class Transition : ScriptableObject
    {
        [Serializable]
        public class Condition
        {
            [SerializeField] BlackboardValueBool boolValue;
            [SerializeField] BlackboardValueInt intValue;
            [SerializeField] BlackboardValueFloat floatValue;
            [SerializeField] BlackboardValueString stringValue;
        }

        [SerializeField] State from;
        [SerializeField] State to;

        //public bool OnUpdate()
        //{

        //}
    }
}
