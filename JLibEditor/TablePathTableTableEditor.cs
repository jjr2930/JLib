using System.Collections.Generic;
using System.IO;
using System.Text;

using UnityEngine;
using UnityEditor;



namespace JLibEditor
{
    [System.Serializable]
    public class Data
    {
        public string name;
        public string path;
    }

    [System.Serializable]
    public class TablePathList
    {
        public List<Data> tablesPath = new List<Data>();
    }

    //must be inherited BaseTableEditor
    public class TablePathTableEditor : EditorWindow
    {
        const string tableParent = "Tables";
        const string tablePath = "TablePath";
        
        [MenuItem( "Tools/TableEditor/Edit TablePath" )]
        public static void DrawWindow()
        {
            TablePathTableEditor window = (TablePathTableEditor)EditorWindow.GetWindow(typeof(TablePathTableEditor));
            window.Initialize();
            window.minSize = new Vector2(500, 500);
        }
        static Vector2 scrollPosition = Vector2.zero;
        public TablePathList pathList = null;
        public List<Data> list2 = null;
        void OnGUI()
        {
            DrawFirstFow();
            DrawBody();
            DrawButton();
        }

        public void Initialize()
        {
            if (pathList == null)
            {
                //load table if have
                string pathTablepath = @"./Assets/Resources/" + tableParent + "/" + tablePath + ".txt";
                TextAsset table = AssetDatabase.LoadAssetAtPath<TextAsset>(pathTablepath);
                if (null == table)
                {
                    //create directory and file

                    if (!Directory.Exists(@"./Assets/Resources/" + tableParent))
                    {
                        Directory.CreateDirectory(@"./Assets/Resources/" + tableParent);
                    }
                    FileStream stream = new FileStream(@"./Assets/Resources/" + tableParent + "/" + tablePath + ".txt",
                         FileMode.OpenOrCreate,
                         FileAccess.ReadWrite);
                    //clear inside
                    ClearSteam(stream);

                    //create default data
                    pathList = new TablePathList();
                    pathList.tablesPath = new List<Data>();
                    pathList.tablesPath.Add(new Data() { name = "", path = "" });
                    string json = JsonUtility.ToJson(pathList);
                    byte[] bytes = Encoding.UTF8.GetBytes(json);

                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                else
                {
                    pathList = JsonUtility.FromJson<TablePathList>(table.text);
                }
            }
        }

        public void DrawFirstFow()
        {
            EditorGUILayout.BeginVertical("GroupBox");
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("TableName");
                    EditorGUILayout.LabelField("TablePath");
                    EditorGUILayout.LabelField("", GUILayout.MinWidth(50), GUILayout.MaxWidth(50));
                }
                EditorGUILayout.EndHorizontal();

            }
            EditorGUILayout.EndVertical();
        }
        public void DrawBody()
        {
            if (null == pathList)
            {
                Initialize();
            }

            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            {
                EditorGUILayout.BeginVertical();
                {
                    for (int i = 0; i < pathList.tablesPath.Count; i++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            pathList.tablesPath[i].name = EditorGUILayout.TextField(pathList.tablesPath[i].name);
                            pathList.tablesPath[i].path = EditorGUILayout.TextField(pathList.tablesPath[i].path);
                            DrawDeleteButton(i);
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndScrollView();

        }
        public void DrawButton()
        {
            EditorGUILayout.BeginVertical();
            {
                if (GUILayout.Button("AddList"))
                {
                    pathList.tablesPath.Add(new Data() { name = "", path = "" });
                }

                if (GUILayout.Button("Save"))
                {
                    //save pathTable
                    string path = @"./Assets/Resources/" + tableParent + "/" + tablePath + ".txt";
                    File.Delete(path);
                    FileStream pathTableFile = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    ClearSteam(pathTableFile);
                    byte[] jsonByte = Encoding.UTF8.GetBytes(JsonUtility.ToJson(pathList));
                    pathTableFile.Write(jsonByte, 0, jsonByte.Length);
                    pathTableFile.Close();

                    //save table at pathTable
                    for (int i = 0; i < pathList.tablesPath.Count; i++)
                    {
                        if (!string.IsNullOrEmpty(pathList.tablesPath[i].name))
                        {
                            path = @"./Assets/Resources/" + tableParent + "/" + pathList.tablesPath[i].path + ".txt";
                            FileStream newTable = new FileStream(path,
                                                             FileMode.OpenOrCreate,
                                                             FileAccess.ReadWrite);
                            newTable.Close();
                            Debug.Log(pathList.tablesPath[i].name + "is saved");
                        }
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }

        public void DrawDeleteButton(int index)
        {
            if (GUILayout.Button("", "OL Minus", GUILayout.MinWidth(50), GUILayout.MaxWidth(50)))
            {
                pathList.tablesPath.RemoveAt(index);
            }
        }

        public void ClearSteam(FileStream stream)
        {
            byte[] emptyBytes = new byte[stream.Length];
            stream.Write(emptyBytes, 0, emptyBytes.Length);

        }

    }


}
