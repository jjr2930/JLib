using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JLib.FSM.Editor
{
    [CustomEditor(typeof(StateMachineRunner))]
    public class StateMachineRunnerInspector : UnityEditor.Editor
    {
        StateMachineRunner Script 
        {
            get => target as StateMachineRunner; 
        }

        UnityEditor.Editor cachedEditor;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CreateCachedEditor(Script.StateMachine, null, ref cachedEditor);

            cachedEditor.DrawHeader();
            cachedEditor.OnInspectorGUI();
        }
    }
}
