using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace JLib.FSM
{
    [CreateAssetMenu(menuName = "FSM/New State Machine")]
    public sealed class StateMachine : ScriptableObject
    {
        [SerializeField] State rootState = null;

        [SerializeField, HideInInspector] List<StateMachineValue> stateMachineValues = new List<StateMachineValue>();
        [SerializeField, HideInInspector] List<State> states = new List<State>();
        [SerializeField, HideInInspector] List<Transition> transitions = new List<Transition>();
        [SerializeField, HideInInspector] List<TransitionEvent> transitionEvents = new List<TransitionEvent>();
        [SerializeField, HideInInspector] bool isValuesFolded = false;
        [SerializeField, HideInInspector] bool isStatesFolded = false;
        [SerializeField, HideInInspector] bool isTransitionEventFolded = false;
        [SerializeField, HideInInspector] bool isTransitionFolded = false;


        Dictionary<int, StateMachineValue> valueByHash = new Dictionary<int, StateMachineValue>();
        Dictionary<State, Dictionary<string, State>> transitionMap
            = new Dictionary<State, Dictionary<string, State>>();

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

        public int ValuesCount
        {
            get => stateMachineValues.Count;
        }

        public StateMachineRunner Owner
        {
            get;
            set;
        }

        public bool IsValuesFolded 
        { 
            get => isValuesFolded; 
            set => isValuesFolded = value; 
        }
        public bool IsStatesFolded 
        { 
            get => isStatesFolded; 
            set => isStatesFolded = value; 
        }

        public bool IsTransitionEventFolded 
        {
            get => isTransitionEventFolded; 
            set => isTransitionEventFolded = value;
        }
        public bool IsTransitionFolded
        {
            get => isTransitionFolded;
            set => isTransitionFolded = value;
        }

        public StateMachine GetClone()
        {
            Dictionary<State, State> copiedStateMap = new Dictionary<State, State>(16);
            var copiedMachine = Instantiate(this);
            //copy states
            for (int i = 0; i < copiedMachine.states.Count; ++i)
            {
                bool isRoot = copiedMachine.rootState == copiedMachine.states[i];
                State copiedState = Instantiate(copiedMachine.states[i]);
                RemoveClonePostfix(copiedState);
                if (isRoot)
                {
                    copiedMachine.rootState = copiedState;
                }

                copiedMachine.states[i] = copiedState;
                copiedStateMap[this.states[i]] = copiedState;
            }
            //copy values
            for (int i = 0;i < copiedMachine.stateMachineValues.Count; ++i)
            {
                var copiedValue = Instantiate(copiedMachine.stateMachineValues[i]);
                RemoveClonePostfix(copiedValue);
                copiedMachine.stateMachineValues[i] = copiedValue;
            }
            //copy transitions
            for(int i = 0; i< copiedMachine.transitions.Count; ++i)
            {
                var copiedTransition = Instantiate(copiedMachine.transitions[i]);
                RemoveClonePostfix(copiedTransition);
                copiedTransition.from = copiedStateMap[this.transitions[i].from];
                copiedTransition.to = copiedStateMap[this.transitions[i].to];
                copiedTransition.StateMachine = copiedMachine;
                copiedMachine.transitions[i] = copiedTransition;
            }
            //copy event
            for(int i =0; i< copiedMachine.transitionEvents.Count; ++i)
            {
                var copiedTransitionEvent = Instantiate(copiedMachine.transitionEvents[i]);
                RemoveClonePostfix(copiedTransitionEvent);
                copiedMachine.transitionEvents[i] = copiedTransitionEvent;
                
            }

            return copiedMachine;
        }


        #region UNITY EVENT METHODS
        private void OnEnable()
        {
            valueByHash.Clear();
            foreach (var value in stateMachineValues)
            {
                valueByHash[value.name.GetHashCode()] = value;
            }

            foreach (var item in transitionMap)
            {
                item.Value.Clear();
            }
        }
        #endregion

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

        public void SetValue<T>(string name, T value)
        {
            foreach (var stateMachineValue in this.stateMachineValues) 
            { 
                if(stateMachineValue.name == name)
                {
                    var convertedValue = stateMachineValue as ISettable<T>;

                    if (null == convertedValue)
                        throw new InvalidOperationException($"value({name}) is not isettable");
                    
                    convertedValue.SetValue(value);
                    return;
                }
            }

            throw new InvalidOperationException($"Can not found value name : {name}");
        }

        public T GetValue<T>(string name)
        {
            foreach (var stateMachineValue in this.stateMachineValues)
            {
                if (stateMachineValue.name == name)
                {
                    return (stateMachineValue as ISettable<T>).GetValue();
                }
            }

            throw new InvalidOperationException($"Can not found value name : {name}");
        }

        public void AddValue<T>(T value) where T : StateMachineValue
        {
            stateMachineValues.Add(value);
        }

        public void RemoveValue<T>(T value) where T : StateMachineValue
        {
            stateMachineValues.Remove(value);
        }

        public void RemoveValueByIndex(int index)
        {
            stateMachineValues.RemoveAt(index);
        }

        public StateMachineValue GetValueByIndex(int index)
        {
            return stateMachineValues[index];
        }

        public void OnEntered() 
        {
            CurrentState = rootState;
            CurrentState.OnEntered(Owner);
        }
        public void OnUpdate()
        {
            CurrentState.OnUpdate(Owner);
        }
        public void OnExit() 
        {
            CurrentState.OnExit(Owner);
        }

        public void RemoveClonePostfix(UnityEngine.Object obj)
        {
            obj.name = obj.name.Replace("(Clone)", "");
        }
    }
}
