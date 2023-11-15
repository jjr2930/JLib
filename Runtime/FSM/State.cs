using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    public class State : ScriptableObject
    {
        public virtual void OnEntered() { }
        public virtual void OnUpdate() { }
        public virtual void OnExit() { }
    }
}
