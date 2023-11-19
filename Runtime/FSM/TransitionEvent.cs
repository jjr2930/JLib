using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    public class TransitionEvent : ScriptableObject
    {
        [SerializeField] bool isLocked;

        public bool IsLocked 
        { 
            get => isLocked; 
            set => isLocked = value; 
        }
    }
}
