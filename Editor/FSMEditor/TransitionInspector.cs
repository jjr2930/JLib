using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Pool;
using System.Buffers;
using UnityEngine.Assertions;
using System;

namespace JLib.FSM.Editor
{
    [CustomEditor(typeof(Transition))]
    public class TransitionInspector : UnityEditor.Editor
    {
        Transition Script
        {
            get => target as Transition;
        }


        public override void OnInspectorGUI()
        {
            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                var names = StateNames;
                var arrayNames = names.ToArray();
                using (var horizontal = new EditorGUILayout.HorizontalScope())
                {
                    DrawStateSelection(ref Script.from, arrayNames);
                    DrawTransitionEvent();
                    DrawStateSelection(ref Script.to, arrayNames);
                }

                ListPool<string>.Release(names);

                if (changeScope.changed)
                    EditorUtility.SetDirty(target);
            }
        }

        private void DrawTransitionEvent()
        {
            var names = TransitionEventNames;
            int index = 0;
            if (null == Script.transitionEvent)
            { 
                index = 0;
            }
            else
            {
                index = FindTransitionEventIndexByName(Script.transitionEvent, names);
                if(-1 == index)
                {
                    Debug.Log($"Transition Event : {Script.transitionEvent.name} is not found");
                    index = 0;
                }
            }

            if (TransitionEventNames.Count == 0)
            {
                EditorGUILayout.LabelField("There is no transition events..");
            }
            else
            {
                index = EditorGUILayout.Popup(index, TransitionEventNames.ToArray());
                Script.transitionEvent = Script.StateMachine.GetTransitionEvent(index);
            }
        }

        int FindTransitionEventIndexByName(TransitionEvent transitionEvent, List<string> names)
        {
            for (int i = 0; i < names.Count; ++i)
            {
                if (names[i] == transitionEvent.name)
                    return i;
            }

            return -1;
        }

        public List<string> TransitionEventNames
        {
            get
            {
                List<string> result = new List<string>(16);
                for (int i = 0; i < Script.StateMachine.TransitionEventCount; i++)
                {
                    result.Add(Script.StateMachine.GetTransitionEvent(i).name);
                }

                return result;
            }
        }

        private void DrawStateSelection(ref State state, string[] names)
        {
            int index = 0;
            if (null == state)
            {
                index = 0;
            }
            else
            {
                index = GetStateIndex(state, names);
                if(index < 0)
                {
                    index = 0;
                    Debug.LogError("can not found state in statemachine : " + state.name);
                }
            }

            if (0 < Script.StateMachine.StateCount)
            {
                index = EditorGUILayout.Popup(index, names);
                state = Script.StateMachine.GetState(index);
            }
        }

        int GetStateIndex(State state,  string[] stateNames)
        {
            Assert.IsNotNull(state);

            int count = stateNames.Length;
            for (int i = 0; i < count; i++)
            {
                if (state.name == stateNames[i])
                    return i;
            }

            return -1;
        }

        List<string> StateNames
        {
            get
            {
                int count = Script.StateMachine.StateCount;
                var result = new List<string>();

                for (int i = 1; i <= count; ++i)
                {
                    result.Add(Script.StateMachine.GetState(i - 1).name);
                }

                return result;
            }
        }
    }
}

