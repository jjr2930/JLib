using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JLib.FSM.Editor
{
    public class StateMachineValueTemplateInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            using (var verticalScope = new EditorGUILayout.VerticalScope())
            {
                EditorGUILayout.LabelField(target.GetType().Name.Replace("StateMachineValue", ""));
                using (var horizontalScope = new EditorGUILayout.HorizontalScope())
                {
                    using (var changeScope = new EditorGUI.ChangeCheckScope())
                    {
                        target.name = EditorGUILayout.TextField(target.name);
                        if (changeScope.changed)
                        {
                            EditorUtility.SetDirty(target);
                            AssetDatabase.SaveAssets();
                            AssetDatabase.Refresh();
                        }
                    }
                    var valueProperty = serializedObject.FindProperty("value");
                    EditorGUILayout.PropertyField(valueProperty,new GUIContent(""));
                    serializedObject.ApplyModifiedProperties();
                    serializedObject.Update();
                }
            }
        }
    }
}