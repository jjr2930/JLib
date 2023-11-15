using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace JLib.FSM.Editor
{
    public class StateSelectionWindow : UnityEditor.EditorWindow
    {
        public static Type selectedType;
        public static List<Type> stateTypes;

        Vector2 scrollPosition;
        Action<Type> onSelected;
        public static void OpenWindow(int x, int y, int width, int height, Action<Type> callback)
        {
            StateSelectionWindow window = ScriptableObject.CreateInstance<StateSelectionWindow>();
            window.onSelected = callback;
            window.position = new Rect(x, y, width, height);
            window.ShowPopup();
        }


        private void OnEnable()
        {
            stateTypes.Clear();

            Type typeOfState = typeof(State);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                var types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    if(type.IsSubclassOf(typeOfState))
                    {
                        stateTypes.Add(type);
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
                    for (int i = 0; i < stateTypes.Count; i++)
                    {
                        if (GUILayout.Button(stateTypes[i].Name))
                        {
                            selectedType = stateTypes[i];
                            onSelected?.Invoke(selectedType);
                            Close();
                        }
                    }
                }
                scrollPosition = scrollViewScope.scrollPosition;
            }
        }
    }
}