using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

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

        UnityEditor.Editor blackboardEditor;

        public StateMachine Script
        {
            get => target as StateMachine;
        }

        private void OnEnable()
        {
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
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawBlackbaord();
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            DrawStates();            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            DrawTransitions();
        }

        private void DrawTransitions()
        {
            using (var verticalScope = new EditorGUILayout.VerticalScope())
            {
                var count = Script.TransitionCount;
                for (int i = 0; i < count; i++)
                {
                    var transition = Script.GetTransition(i);
                    var cachedTransitionEditor = cachedTransitionEditorByObject[transition];
                    CreateCachedEditor(transition, null, ref cachedTransitionEditor);
                    cachedTransitionEditorByObject[transition] = cachedTransitionEditor;

                    cachedTransitionEditor.DrawHeader();
                    cachedTransitionEditor.OnInspectorGUI();
                    if(GUILayout.Button("-"))
                    {
                        var result = EditorUtility.DisplayDialog("Warnning", "Are sure?", "Yes", "No");
                        if(result)
                        {
                            RemoveTransition(i);
                            return;
                        }
                    }
                }
            }

            if(GUILayout.Button("Add Transition"))
            {
                AddTransition();
            }
        }

        void AddTransition()
        {
            var newTransition = CreateInstance<Transition>();
            newTransition.name = "new Transition";
            Script.AddTransition(newTransition);
            cachedTransitionEditorByObject[newTransition] = null;

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
            int stateCount = Script.StateCount;
            using (var scrollScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
            {
                using (var verticalScope = new EditorGUILayout.VerticalScope())
                {
                    for (int i = 0; i < stateCount; i++)
                    {
                        var state = Script.GetState(i);                        
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
        void AddState(Type State)
        {
            var newState = ScriptableObject.CreateInstance(State) as State;
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
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }
}
