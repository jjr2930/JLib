using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace JLib.FSM.Editor
{
    [CustomEditor(typeof(StateMachine))]
    public class StateMachineInspector : UnityEditor.Editor
    {
        public StateMachine Script
        {
            get => target as StateMachine; 
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            int stateCount = Script.StateCount;

            if(GUILayout.Button("Add State"))
            {
                var mousePosition = Event.current.mousePosition;
                StateSelectionWindow.OpenWindow((int)mousePosition.x, (int)mousePosition.y, 400, 500,
                    (selectedType) =>
                    {
                        Debug.Log("Add state code here");
                    });
            }
        }
    }
}
