using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace JLib.Editor
{
    [CustomEditor(typeof(SubScene))]    
    public class SubSceneEditor : UnityEditor.Editor
    {
        public SubScene Script
        {
            get => target as SubScene;
        }
        public override void OnInspectorGUI()
        {
            using(var changeScope = new EditorGUI.ChangeCheckScope())
            {
                base.OnInspectorGUI();
                if (changeScope.changed)
                {
                    if(null != Script.Scene)
                    {
                        Script.ScenePath = AssetDatabase.GetAssetPath(Script.Scene);
                        Script.SceneName = Script.Scene.name;
                    }
                }
            }
            if(GUILayout.Button("Load"))
            {
                var path = AssetDatabase.GetAssetPath(Script.Scene);
                var loadedScene = EditorSceneManager.OpenScene(path, OpenSceneMode.Additive);
                  
                if(Script.ShouldActiveScene)
                {
                    EditorSceneManager.SetActiveScene(loadedScene);
                }
            }

            if(GUILayout.Button("Close"))
            {
                int sceneCount = EditorSceneManager.sceneCount;
                for(int i = 0; i < sceneCount; i++)
                {
                    var foundScene = EditorSceneManager.GetSceneAt(i);
                    if(foundScene.name == Script.Scene.name)
                    {
                        EditorSceneManager.CloseScene(foundScene, true);
                    }
                }
            }
        }
    }
}
