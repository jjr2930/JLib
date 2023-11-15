using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

namespace JLib.FSM
{
    public class StateMachineRunner : MonoBehaviour
    {
        [SerializeField] StateMachine rootStateMachine;
        [SerializeField] Blackboard blackBoard;

        private void Start()
        {
            rootStateMachine.OnEntered();
        }

        private void Update()
        {
            rootStateMachine.OnUpdate();
        }

        private void OnDestroy() 
        {
            rootStateMachine.OnExit();
        }
    }
}
