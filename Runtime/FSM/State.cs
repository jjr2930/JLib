using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    public class State : ScriptableObject
    {
        [SerializeField] private StateMachine parentStateMachine;

        public StateMachine ParentStateMachine 
        {
            get => parentStateMachine; 
            set => parentStateMachine = value; 
        }


        public virtual void OnEntered() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}
