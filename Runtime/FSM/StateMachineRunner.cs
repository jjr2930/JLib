using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using UnityEngine;

namespace JLib.FSM
{
    public class StateMachineRunner : MonoBehaviour
    {
        [SerializeField] StateMachine rootStateMachine;
        [SerializeField] StateMachine runtimeStateMachine;

        private void Start()
        {
            runtimeStateMachine = Instantiate(rootStateMachine);
            runtimeStateMachine.CurrentState = runtimeStateMachine.GetRootState();
            runtimeStateMachine.OnEntered();            
        }

        private void Update()
        {
            runtimeStateMachine.OnUpdate();
        }

        private void OnDestroy() 
        {
            runtimeStateMachine.OnExit();
        }

        public void PushEvent(int eventNumber)
        {
            var transitionCount = runtimeStateMachine.TransitionCount;
            for (int i = 0; i < transitionCount; ++i)
            {
                var transition = runtimeStateMachine.GetTransition(i);
                if(runtimeStateMachine.CurrentState == transition.from
                    && transition.transitionEvent.Value == eventNumber)
                {
                    runtimeStateMachine.CurrentState.OnExit();
                    runtimeStateMachine.CurrentState = transition.to;
                    runtimeStateMachine.CurrentState.OnEntered();
                }
            }
        }

        public void PushEvent(string eventName)
        {
            var transitionCount = runtimeStateMachine.TransitionCount;
            for (int i = 0; i < transitionCount; ++i)
            {
                var transition = runtimeStateMachine.GetTransition(i);
                if (runtimeStateMachine.CurrentState == transition.from
                    && transition.transitionEvent.name == eventName)
                {
                    runtimeStateMachine.CurrentState.OnExit();
                    runtimeStateMachine.CurrentState = transition.to;
                    runtimeStateMachine.CurrentState.OnEntered();
                }
            }
        }

        public void SetBlackboardValue<T>(string name, T value)
        {
            runtimeStateMachine.Blackboard.SetValue<T>(name, value);
        }

        public T GetBlackboardValue<T>(string name)
        {
            return runtimeStateMachine.Blackboard.GetValue<T>(name);
        }
    }
}
