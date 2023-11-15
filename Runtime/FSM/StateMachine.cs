using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace JLib.FSM
{
    [CreateAssetMenu(menuName = "FSM/New State Machine")]
    public class StateMachine : ScriptableObject
    {
        //[SerializeField] List<StateMachine> stateMachines = new List<StateMachine>();
        [SerializeField] List<State> states = new List<State>();
        [SerializeField] State rootState = null;

        public int StateCount { get => states.Count; }
        //public int StateMachineCount { get => stateMachines.Count; }

        public void SetRootState(State state) 
        {
            rootState = state;
        }

        public State GetRootState() 
        {
            return rootState;
        }

        public void AddState(State state)
        {

        }

        public void RemoveState(State state)
        {

        }

        public State GetState(int index)
        {
            Assert.IsFalse(0 <= index && index < states.Count, $"out of range index : {index}, length : {states.Count}");

            return states[index];
        }


        public void OnEntered() { }
        public void OnUpdate() { }
        public void OnExit() { }
    }
}
