using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using System.Runtime.InteropServices;
using UnityEngine.AI;

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

        bool isStatesFolded;
        bool isTransitionsFolded;
        bool isTransitionEventFolded;
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

            //using (var verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                DrawBlackbaord();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                DrawStates();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                DrawTransitionEvents();
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

                DrawTransitions();
            }
        }

        private void DrawTransitionEvents()
        {
            colorStack.Push(GUI.color);
            GUI.color = Color.red;
            using (var verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUI.color = colorStack.Pop();

                EditorGUILayout.Space(sectionSpaceHeight);
                EditorGUILayout.LabelField("Transition Events", BigCenterWhiteLabel, centerLabelHeight);
                EditorGUILayout.Space(sectionSpaceHeight);

                var buttonString = (isTransitionEventFolded) ? "Fold" : "Unfold";
                if (GUILayout.Button(buttonString))
                {
                    isTransitionEventFolded = !isTransitionEventFolded;
                }

                if (isTransitionEventFolded)
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

                EditorGUILayout.Space(sectionSpaceHeight);
                EditorGUILayout.LabelField("Transitions", BigCenterWhiteLabel, centerLabelHeight);
                EditorGUILayout.Space(sectionSpaceHeight);

                var buttonString = (isTransitionsFolded) ? "Fold" : "Unfold";
                if (GUILayout.Button(buttonString))
                {
                    isTransitionsFolded = !isTransitionsFolded;
                }

                if (isTransitionsFolded)
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
                EditorGUILayout.Space(sectionSpaceHeight);
                GUI.color = colorStack.Pop();

                EditorGUILayout.LabelField("States", BigCenterWhiteLabel, centerLabelHeight); ;
                EditorGUILayout.Space(sectionSpaceHeight);

                var buttonString = (isStatesFolded) ? "Fold" : "Unfold";
                if (GUILayout.Button(buttonString))
                {
                    isStatesFolded = !isStatesFolded;
                }

                if (isStatesFolded)
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
                                if(Script.CurrentState == state)
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

        private void DrawBlackbaord()
        {
            colorStack.Push(GUI.color);
            GUI.color = Color.red;
            using (var verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                GUI.color = colorStack.Pop();
                EditorGUILayout.Space(sectionSpaceHeight);
                EditorGUILayout.LabelField("Blackboard", BigCenterWhiteLabel, centerLabelHeight); ;
                EditorGUILayout.Space(sectionSpaceHeight);

                using (var blackboardHorizontalScope = new EditorGUILayout.HorizontalScope())
                {
                    Script.Blackboard = EditorGUILayout.ObjectField(Script.Blackboard, typeof(Blackboard), false, null) as Blackboard;
                    var buttonText = (Script.IsBlackboardFolded) ? "Unfold" : "Fold";
                    if (GUILayout.Button(buttonText, GUILayout.Width(100)))
                    {
                        Script.IsBlackboardFolded = !Script.IsBlackboardFolded;
                    }
                }
                if (Script.Blackboard != null)
                {
                    if (!Script.IsBlackboardFolded)
                    {
                        CreateCachedEditor(Script.Blackboard, typeof(BlackboardInspector), ref blackboardEditor);
                        if (null != blackboardEditor)
                        {
                            using (var blackboardVerticalScope = new EditorGUILayout.VerticalScope())
                            {
                                blackboardEditor.DrawHeader();
                                blackboardEditor.OnInspectorGUI();
                            }
                        }
                    }
                }

                EditorGUILayout.Space(sectionSpaceHeight);
            }
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
