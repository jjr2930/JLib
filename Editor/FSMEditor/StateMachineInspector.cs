using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using System.Runtime.InteropServices;
using UnityEngine.AI;
using System.Net.Http.Headers;
using JetBrains.Annotations;

namespace JLib.FSM.Editor
{
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineInspector : UnityEditor.Editor
    {
        Vector2 scrollPosition;

        Dictionary<State, UnityEditor.Editor> cachedStateEdtiorByObject
            = new Dictionary<State, UnityEditor.Editor>();

        Dictionary<Transition, UnityEditor.Editor> cachedTransitionEditorByObject
            = new Dictionary<Transition, UnityEditor.Editor>();

        Dictionary<TransitionEvent, UnityEditor.Editor> cachedTransitionEventEditorByObject
            = new Dictionary<TransitionEvent, UnityEditor.Editor>();

        Dictionary<StateMachineValue, UnityEditor.Editor> cachedValueEditorByObject
            = new Dictionary<StateMachineValue, UnityEditor.Editor>();

        GUIStyle BigCenterWhiteLabel
        {
            get
            {
                var style = new GUIStyle("WhiteLargeCenterLabel");
                style.fontSize = 50;
                style.stretchHeight = true;
                return style;
            }
        }


        GUILayoutOption centerLabelHeight = GUILayout.Height(50);
        float sectionSpaceHeight = 20f;

        UnityEditor.Editor blackboardEditor;

        Stack<Color> colorStack = null;

        public StateMachine Script
        {
            get => target as StateMachine;
        }

        private void OnEnable()
        {
            //Debug.Log("StateMachineInspector OnEnable");
            if (null == Script)
                return;

            cachedStateEdtiorByObject.Clear();
            for (int i = 0; i < Script.StateCount; i++)
            {
                var state = Script.GetState(i);
                cachedStateEdtiorByObject[state] = null;
            }

            cachedTransitionEditorByObject.Clear();
            for (int i = 0; i < Script.TransitionCount; i++)
            {
                var transition = Script.GetTransition(i);
                cachedTransitionEditorByObject[transition] = null;
            }

            cachedTransitionEventEditorByObject.Clear();
            for (int i = 0; i < Script.TransitionEventCount; i++)
            {
                var transitionEvent = Script.GetTransitionEvent(i);
                cachedTransitionEventEditorByObject[transitionEvent] = null;
            }

            colorStack = new Stack<Color>();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                DrawValues();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                DrawStates();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                DrawTransitionEvents();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                DrawTransitions();

                if(changeScope.changed)
                {
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
            }
        }

        private void DrawTransitionEvents()
        {
            colorStack.Push(GUI.color);
            GUI.color = Color.red;
            using (var verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUI.color = colorStack.Pop();

                DrawSectionTitle("Transition Events");

                var buttonString = (Script.IsTransitionEventFolded) ? "Fold" : "Unfold";
                if (GUILayout.Button(buttonString))
                {
                    Script.IsTransitionEventFolded = !Script.IsTransitionEventFolded;
                }

                if (Script.IsTransitionEventFolded)
                {
                    using (var verticalScope2 = new EditorGUILayout.VerticalScope())
                    {
                        var count = Script.TransitionEventCount;
                        for (int i = 0; i < count; i++)
                        {
                            using (var horizontal = new EditorGUILayout.HorizontalScope())
                            {
                                var transitionEvent = Script.GetTransitionEvent(i);
                                var cachedTransitionEventEditor = cachedTransitionEventEditorByObject[transitionEvent];
                                CreateCachedEditor(transitionEvent, null, ref cachedTransitionEventEditor);
                                cachedTransitionEventEditorByObject[transitionEvent] = cachedTransitionEventEditor;

                                //cachedTransitionEventEditor.DrawHeader();
                                cachedTransitionEventEditor.OnInspectorGUI();
                                if (GUILayout.Button("-"))
                                {
                                    var result = EditorUtility.DisplayDialog("Warnning", "Are sure?", "Yes", "No");
                                    if (result)
                                    {
                                        RemoveTransitionEvent(i);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (GUILayout.Button("Add Transition Event"))
                    {
                        AddTransitionEvent();
                    }
                }
                EditorGUILayout.Space(sectionSpaceHeight);
            }
        }

        void AddTransitionEvent()
        {
            var newTransitionEvent = CreateInstance<TransitionEvent>();
            newTransitionEvent.name = "new Transition Event";
            Script.AddTransitionEvent(newTransitionEvent);
            cachedTransitionEventEditorByObject[newTransitionEvent] = null;

            AddAsset(newTransitionEvent);
        }

        void RemoveTransitionEvent(int index)
        {
            var oldTransitionEvent = Script.GetTransitionEvent(index);
            Script.RemoveTransitionEvent(oldTransitionEvent);
            cachedTransitionEventEditorByObject.Remove(oldTransitionEvent);

            RemoveAsset(oldTransitionEvent);
        }

        private void DrawTransitions()
        {
            colorStack.Push(GUI.color);
            GUI.color = Color.red;

            using (var verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUI.color = colorStack.Pop();

                DrawSectionTitle("Transitions");

                var buttonString = (Script.IsTransitionFolded) ? "Fold" : "Unfold";
                if (GUILayout.Button(buttonString))
                {
                    Script.IsTransitionFolded = !Script.IsTransitionFolded;
                }

                if (Script.IsTransitionFolded)
                {
                    using (var verticalScope2 = new EditorGUILayout.VerticalScope())
                    {
                        var count = Script.TransitionCount;
                        for (int i = 0; i < count; i++)
                        {
                            using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                            {
                                var transition = Script.GetTransition(i);
                                var cachedTransitionEditor = cachedTransitionEditorByObject[transition];
                                CreateCachedEditor(transition, null, ref cachedTransitionEditor);
                                cachedTransitionEditorByObject[transition] = cachedTransitionEditor;

                                //cachedTransitionEditor.DrawHeader();
                                cachedTransitionEditor.OnInspectorGUI();
                                if (GUILayout.Button("-"))
                                {
                                    var result = EditorUtility.DisplayDialog("Warnning", "Are sure?", "Yes", "No");
                                    if (result)
                                    {
                                        RemoveTransition(i);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (GUILayout.Button("Add Transition"))
                    {
                        AddTransition();
                    }
                }

                EditorGUILayout.Space(sectionSpaceHeight);
            }
        }

        void AddTransition()
        {
            var newTransition = CreateInstance<Transition>();
            newTransition.name = "new Transition";
            Script.AddTransition(newTransition);
            cachedTransitionEditorByObject[newTransition] = null;
            newTransition.StateMachine = Script;

            AddAsset(newTransition);
        }

        void RemoveTransition(int index)
        {
            var oldTransition = Script.GetTransition(index);
            Script.RemoveTransition(oldTransition);
            cachedTransitionEditorByObject.Remove(oldTransition);

            RemoveAsset(oldTransition);
        }

        private void DrawStates()
        {
            colorStack.Push(GUI.color);
            GUI.color = Color.red;
            using (var verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUI.color = colorStack.Pop();

                DrawSectionTitle("States");

                var buttonString = (Script.IsStatesFolded) ? "Fold" : "Unfold";
                if (GUILayout.Button(buttonString))
                {
                    Script.IsStatesFolded = !Script.IsStatesFolded;
                }

                if (Script.IsStatesFolded)
                {
                    int stateCount = Script.StateCount;
                    using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
                    {
                        using (var verticalScope2 = new EditorGUILayout.VerticalScope())
                        {
                            for (int i = 0; i < stateCount; i++)
                            {
                                var state = Script.GetState(i);
                                colorStack.Push(GUI.color);
                                if (Script.CurrentState == state)
                                {
                                    GUI.color = new Color(0.75f, 0.75f, 1f, 1f);
                                }
                                else if (Script.GetRootState() == state)
                                {
                                    GUI.color = new Color(0.75f, 1f, 0.75f, 1f);
                                }

                                UnityEditor.Editor cachedStateEditor = cachedStateEdtiorByObject[state];
                                CreateCachedEditor(state, null, ref cachedStateEditor);
                                cachedStateEdtiorByObject[state] = cachedStateEditor;
                                cachedStateEditor.DrawHeader();

                                state.name = EditorGUILayout.TextField("name", state.name);
                                cachedStateEditor.DrawDefaultInspector();

                                if (GUILayout.Button("-"))
                                {
                                    RemoveState(i);
                                    return;
                                }
                                if (GUILayout.Button("To rootState"))
                                {
                                    Script.SetRootState(state);
                                }

                                GUI.color = colorStack.Pop();
                            }
                        }
                    }

                    if (GUILayout.Button("Add State"))
                    {
                        var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                        StateSelectionWindow.OpenWindow((int)mousePosition.x, (int)mousePosition.y, 400, 500,
                            (selectedType) =>
                            {
                                AddState(selectedType);
                            });
                    }
                }
                EditorGUILayout.Space(sectionSpaceHeight);
            }

        }
        void AddState(Type stateType)
        {
            var newState = ScriptableObject.CreateInstance(stateType) as State;
            newState.name = "new " + stateType.ToString();
            Script.AddState(newState);
            cachedStateEdtiorByObject.Add(newState, null);

            AddAsset(newState);
        }

        void RemoveState(int index)
        {
            var oldState = Script.GetState(index);
            Script.RemoveState(oldState);
            cachedStateEdtiorByObject.Remove(oldState);

            RemoveAsset(oldState);
        }

        private void DrawValues()
        {
            colorStack.Push(GUI.color);
            GUI.color = Color.red;
            using (var verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUI.color = colorStack.Pop();
                DrawSectionTitle("Values");

                var buttonText = (Script.IsValuesFolded) ? "Unfold" : "Fold";
                if (GUILayout.Button(buttonText))
                {
                    Script.IsValuesFolded = !Script.IsValuesFolded;
                }

                if (!Script.IsValuesFolded)
                {
                    using (var valuesVerticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        for (int i = 0; i < Script.ValuesCount; ++i)
                        {
                            using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                            {
                                TryDrawEditor(Script.GetValueByIndex(i), cachedValueEditorByObject);
                                if(GUILayout.Button("-"))
                                {
                                    if(EditorUtility.DisplayDialog("Warnning", "Are you sure?", "OK", "NO"))
                                    {
                                        Script.RemoveValueByIndex(i);
                                        return;
                                    }
                                }
                            }
                            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                        }
                    }

                    if(GUILayout.Button("Add Value"))
                    {
                        var mousePosition = GUIUtility.GUIToScreenPoint(Event.current.mousePosition);
                        ValueSelectionPopup.OpenWindow((int)mousePosition.x, (int)mousePosition.y, 400, 500,
                            (selectedType) =>
                            {
                                AddBlackboardValue(selectedType);
                            });
                    }
                }

                EditorGUILayout.Space(sectionSpaceHeight);
            }
        }

        void TryDrawEditor<T>(T key, IDictionary<T, UnityEditor.Editor> dictionary) where T : UnityEngine.Object
        {
            UnityEditor.Editor found = null;
            if (dictionary.TryGetValue(key, out found))
            {
                dictionary[key] = found;
            }
            CreateCachedEditor(key, null, ref found);
            dictionary[key] = found;

            found.OnInspectorGUI();
        }

        private void DrawSectionTitle(string titleLabel)
        {
            EditorGUILayout.Space(sectionSpaceHeight);
            EditorGUILayout.LabelField(titleLabel, BigCenterWhiteLabel, centerLabelHeight); ;
            EditorGUILayout.Space(sectionSpaceHeight);
        }

        void AddBlackboardValue(Type valueType) 
        {
            var newValue = ScriptableObject.CreateInstance(valueType) as StateMachineValue;
            newValue.name = "new " + valueType.ToString();
            Script.AddValue(newValue);
            cachedValueEditorByObject.Add(newValue, null);

            AddAsset(newValue);
        }

        void RemoveBlackboardValue(int index)
        {
            var oldValue = Script.GetValueByIndex(index);
            Script.RemoveValue(oldValue);
            AssetDatabase.RemoveObjectFromAsset(oldValue);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void AddAsset(UnityEngine.Object newAsset)
        {
            AssetDatabase.AddObjectToAsset(newAsset, Script);
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void RemoveAsset(UnityEngine.Object oldAsset)
        {
            AssetDatabase.RemoveObjectFromAsset(oldAsset);
            DestroyImmediate(oldAsset);
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
