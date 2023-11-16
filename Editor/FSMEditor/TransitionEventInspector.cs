using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JLib.FSM.Editor
{
    [CustomEditor(typeof(TransitionEvent))]
    public class TransitionEventInspector : UnityEditor.Editor
    {
        TransitionEvent Script 
        {
            get => target as TransitionEvent; 
        }

        public override void OnInspectorGUI()
        {
            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                using (var horizontal = new EditorGUILayout.HorizontalScope())
                {
                    GUI.enabled = false == Script.IsLocked;
                    {
                        Script.name = EditorGUILayout.TextField(Script.name);
                        Script.Value = EditorGUILayout.IntField(Script.Value);
                    }
                    GUI.enabled = true;
                    Script.IsLocked = EditorGUILayout.Toggle(Script.IsLocked, GUILayout.Width(50));
                   
                }

                if(changeScope.changed)
                {
                    EditorUtility.SetDirty(target);
                }
            }
        }
    }
}
