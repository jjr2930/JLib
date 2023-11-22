using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JLib.FSM.Editor
{
    public class ValueSelectionPopup : UnityEditor.EditorWindow
    {
        public static Type selectedType;
        public static List<Type> valueTypes = new List<Type>();
        public static ValueSelectionPopup window = null;
        Vector2 scrollPosition;
        Action<Type> onSelected;

        public static void OpenWindow(int x, int y, int width, int height, Action<Type> callback)
        {
            if (null == window)
            {
                window = ScriptableObject.CreateInstance<ValueSelectionPopup>();
            }

            window.onSelected = callback;
            window.position = new Rect(x, y, width, height);
            window.ShowPopup();
        }


        private void OnEnable()
        {
            valueTypes.Clear();

            Type typeOfValue = typeof(StateMachineValue);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if (type.IsSubclassOf(typeOfValue) 
                        && false == type.IsGenericType)
                    {
                        valueTypes.Add(type);
                    }
                }
            }
        }

        private void OnGUI()
        {
            using (var scrollViewScope = new EditorGUILayout.ScrollViewScope(scrollPosition))
            {
                using (var verticalScope = new EditorGUILayout.VerticalScope())
                {
                    for (int i = 0; i < valueTypes.Count; i++)
                    {
                        if (GUILayout.Button(valueTypes[i].Name))
                        {
                            selectedType = valueTypes[i];
                            onSelected?.Invoke(selectedType);
                            Close();
                        }
                    }
                }
                scrollPosition = scrollViewScope.scrollPosition;
            }

            if (GUILayout.Button("Close"))
            {
                Close();
            }
        }
    }
}
