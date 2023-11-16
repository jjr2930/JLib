using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{    
    public class Transition : ScriptableObject
    {
        [SerializeField] StateMachine stateMachine;

        public State from;
        public State to;
        public TransitionEvent transitionEvent;

        public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
    }
}
