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
        [SerializeField, HideInInspector] Blackboard blackboard;
        [SerializeField] State rootState = null;

        [SerializeField, HideInInspector] List<State> states = new List<State>();
        [SerializeField, HideInInspector] List<Transition> transitions = new List<Transition>();
        [SerializeField, HideInInspector] bool isBlackboardFolded = false;
        [SerializeField, HideInInspector] List<TransitionEvent> transitionEvents = new List<TransitionEvent>();

        public State CurrentState
        {
            get; 
            set;
        }

        public int StateCount
        {
            get => states.Count; 
        }

        public int TransitionCount
        { 
            get => transitions.Count; 
        }
        public int TransitionEventCount
        {
            get => transitionEvents.Count;
        }

        public Blackboard Blackboard
        {
            get => blackboard; 
            set => blackboard = value;
        }
        public bool IsBlackboardFolded 
        { 
            get => isBlackboardFolded; 
            set => isBlackboardFolded = value; 
        }

        public void SetRootState(State state) 
        {
            rootState = state;
        }

        public State GetRootState() 
        {
            return rootState;
        }

        public void AddState(State newState)
        {
            states.Add(newState);
        }

        public void RemoveState(State oldState)
        {
            if (oldState == rootState)
            {
                rootState = null;
            }

            states.Remove(oldState);
        }

        public State GetState(int index)
        {
            Assert.IsTrue(0 <= index && index < states.Count, $"out of range index : {index}, length : {states.Count}");

            return states[index];
        }

        public void AddTransition(Transition newTransition) 
        {
            transitions.Add(newTransition);
        }
        
        public void RemoveTransition(Transition oldTransition)
        {
            transitions.Remove(oldTransition);
        }

        public Transition GetTransition(int index)
        {
            return transitions[index];
        }

        public void AddTransitionEvent(TransitionEvent newEvent)
        {
            transitionEvents.Add(newEvent);
        }

        public void RemoveTransitionEvent(TransitionEvent oldEvent)
        {
            transitionEvents.Remove(oldEvent);
        }

        public TransitionEvent GetTransitionEvent(int index)
        {
            return transitionEvents[index];
        }

        public void OnEntered() 
        {
            CurrentState.OnEntered();
        }
        public void OnUpdate()
        {
            CurrentState.OnUpdate();
        }
        public void OnExit() 
        {
            CurrentState.OnExit();
        }
    }
}
