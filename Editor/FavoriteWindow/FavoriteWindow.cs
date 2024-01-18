using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace JLib.Editor
{
    
    public class FavoriteWindow : EditorWindow
    {
        [Serializable]
        public class PathCollection
        {
            public List<string> guids = new List<string>();
            public int Count { get => guids.Count; }
        }

        [MenuItem("Tools/Favorite Window")]
        public static void OpenWindow()
        {
            GetWindow<FavoriteWindow>();
        }

        const string FAVORITE_KEY = "FAVORITE";
        static string favoritesJson;
        PathCollection pathCollection;

        public void OnEnable()
        {
            favoritesJson = EditorUserSettings.GetConfigValue(FAVORITE_KEY);
            if(null != favoritesJson)
            {
                pathCollection = JsonUtility.FromJson<PathCollection>(favoritesJson);
            }
            else
            {
                pathCollection = new PathCollection();
            }
        }

        public void OnGUI()
        {
            using (var changeScope = new EditorGUI.ChangeCheckScope())
            {
                using(var verticalScope = new EditorGUILayout.VerticalScope())
                {
                    for(int i =0; i<pathCollection.guids.Count; i++)
                    {
                        var guid = pathCollection.guids[i]; 
                        using (var hozitontalScope = new EditorGUILayout.HorizontalScope())
                        {
                            var path = AssetDatabase.GUIDToAssetPath(pathCollection.guids[i]);
                            using (var changeScope2 = new EditorGUI.ChangeCheckScope())
                            {
                                var obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
                                obj = EditorGUILayout.ObjectField(obj, typeof(UnityEngine.Object), false);

                                if (changeScope2.changed)
                                {
                                    var changedObjectPath = AssetDatabase.GetAssetPath(obj);
                                    pathCollection.guids[i] = AssetDatabase.GUIDFromAssetPath(changedObjectPath).ToString();
                                }
                            }

                            if(GUILayout.Button("Select"))
                            {
                                Selection.activeObject = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));
                            }    

                            if(GUILayout.Button("Open"))
                            {
                                AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object)));
                            }

                        }
                    }

                    if(GUILayout.Button("+"))
                    {
                        pathCollection.guids.Add("");
                    }
                }

                if(changeScope.changed)
                {
                    var json = JsonUtility.ToJson(pathCollection);
                    EditorUserSettings.SetConfigValue(FAVORITE_KEY, json);
                }
            }
        }
    }
}
