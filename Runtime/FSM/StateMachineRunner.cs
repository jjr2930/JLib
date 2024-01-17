using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using UnityEngine;

namespace JLib.FSM
{
    public class StateMachineRunner : MonoBehaviour
    {
        [SerializeField] StateMachine stateMachine;

        public StateMachine StateMachine 
        { 
            get => stateMachine; 
            set => stateMachine = value; 
        }

        private void Start()
        {
            //copy value to runtimeValue
            stateMachine.Owner = this;
            stateMachine.OnEntered();            
        }

        private void Update()
        {
            stateMachine.OnUpdate();
        }

        private void OnDestroy() 
        {
            stateMachine.OnExit();
        }

        
        public void PushEvent(string eventName)
        {
            var transitionCount = stateMachine.TransitionCount;
            for (int i = 0; i < transitionCount; ++i)
            {
                var transition = stateMachine.GetTransition(i);
                if (stateMachine.CurrentState == transition.from
                    && transition.transitionEvent.name == eventName)
                {
                    stateMachine.CurrentState.OnExit(this);
                    stateMachine.CurrentState = transition.to;
                    stateMachine.CurrentState.OnEntered(this);
                }
            }
        }

        public void SetStateMachineValue<T>(string name, T value)
        {
            stateMachine.SetValue<T>(name, value);
        }

        public T GetStateMachineValue<T>(string name)
        {
            return stateMachine.GetValue<T>(name);
        }

        public int GetEventIndex(string eventName)
        {
            var eventCount = stateMachine.TransitionEventCount;
            for (int i = 0; i < eventCount; i++)
            {
                if(stateMachine.GetTransitionEvent(i).name == eventName)
                {
                    return i;
                }
            }

            //TODO should be throw excpetion?
            return -1;
        }
    }
}
