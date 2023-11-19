using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    public class State : ScriptableObject
    {
        public virtual void OnEntered(StateMachineRunner owner) { }
        public virtual void OnUpdate(StateMachineRunner owner) { }
        public virtual void OnExit(StateMachineRunner owner) { }
    }
}
