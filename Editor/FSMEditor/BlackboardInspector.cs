using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JLib.FSM.Editor
{

    [CustomEditor(typeof(Blackboard))]
    public class BlackboardInspector : UnityEditor.Editor
    {
        Blackboard Script { get => target as Blackboard; }

        Dictionary<BlackboardValue, UnityEditor.Editor> cachedEditorByValue
            = new Dictionary<BlackboardValue, UnityEditor.Editor>();

        public override void OnInspectorGUI()
        {
            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                using (var verticalScope = new EditorGUILayout.VerticalScope())
                {
                    for (int i = 0; i < Script.Count; ++i)
                    {
                        using (var horizontalScope = new EditorGUILayout.HorizontalScope("AvatarMappingBox"))
                        {
                            UnityEditor.Editor editor = null;
                            var blackboardValue = Script.GetValue(i);
                            if (false == cachedEditorByValue.TryGetValue(blackboardValue, out editor))
                            {
                                cachedEditorByValue[blackboardValue] = null;
                            }

                            using (var verticalScope2 = new EditorGUILayout.VerticalScope())
                            {
                                CreateCachedEditor(blackboardValue, GetEditorTypeByValueType(blackboardValue.GetType()), ref editor);
                                cachedEditorByValue[blackboardValue] = editor;
                                editor.OnInspectorGUI();
                            }

                            if (GUILayout.Button("-"))
                            {
                                bool result = EditorUtility.DisplayDialog("Warnning", "Are you sure?", "OK", "NO");
                                if (result)
                                {
                                    RemoveElement(i);
                                    return;
                                }
                            }
                        }
                        EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
                    }
                }

                if (GUILayout.Button("Add Integer"))
                {
                    AddValue<BlackboardValueInt>();
                }

                if (GUILayout.Button("Add Float"))
                {
                    AddValue<BlackboardValueFloat>();
                }

                if (GUILayout.Button("Add Bool"))
                {
                    AddValue<BlackboardValueBool>();
                }

                if (GUILayout.Button("Add String"))
                {
                    AddValue<BlackboardValueString>();
                }

                serializedObject.ApplyModifiedProperties();
                serializedObject.Update();

                if (changeScope.changed)
                {
                    EditorUtility.SetDirty(target);
                }
            }
        }
        Type GetEditorTypeByValueType(Type valueType)
        {
            if (valueType == typeof(BlackboardValueBool))
                return typeof(BlackboardValueBoolInspector);

            else if (valueType == typeof(BlackboardValueInt))
                return typeof(BlackboardValueIntInspector);

            else if (valueType == typeof(BlackboardValueFloat))
                return typeof(BlackboardValueFloatInspector);

            else if (valueType == typeof(BlackboardValueString))
                return typeof(BlackboardValueStringInspector);

            return null;
        }
        void AddValue<T>() where T : BlackboardValue
        {
            var newValue = CreateInstance<T>();
            newValue.name = "new " + typeof(T).Name;
            Script.AddValue(newValue);
            AssetDatabase.AddObjectToAsset(newValue, Script);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void RemoveElement(int index)
        {
            var oldValue = Script.GetValue(index);
            Script.RemoveValue(oldValue);
            AssetDatabase.RemoveObjectFromAsset(oldValue);
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}