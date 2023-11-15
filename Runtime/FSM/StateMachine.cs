using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    [CreateAssetMenu(menuName = "FSM/New State Machine")]
    public class StateMachine : ScriptableObject
    {
        [SerializeField] List<StateMachine> stateMachines = new List<StateMachine>();
        [SerializeField] List<State> states;

        public void OnEntered() { }
        public void OnUpdate() { }
        public void OnExit() { }
    }
}
