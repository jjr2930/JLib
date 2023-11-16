using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    public class TransitionEvent : ScriptableObject
    {
        [SerializeField] int value;
        [SerializeField] bool isLocked;

        public int Value 
        {
            get => value; 
            set => this.value = value; 
        }

        public bool IsLocked 
        { 
            get => isLocked; 
            set => isLocked = value; 
        }
    }
}
