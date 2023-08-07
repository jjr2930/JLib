using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;

namespace Localization.Editor
{
    [CustomEditor(typeof(LocalizationTextTable))]
    public class LocalizationTextTableInspector : UnityEditor.Editor
    {
        const string DEFAULT_GOOGLE_SHEET_URI = "https://docs.google.com/spreadsheets/d/e/2PACX-1vTvX4m634AedrgrGT_goaWj3aPbfjqhGNPKtb4ZEt4yhQpIbNOUWlmnDjT88mdwD0XuWw_-Efz2Hkh9/pub?output=tsv";
        const string GOOGLE_SHEET_URI_KEY = "GOOGLE_SHEET_URI_KEY";
        LocalizationTextTable Script { get { return target as LocalizationTextTable; } }
        ReorderableList list;
        static string filter = "";
        static string googleSheetURI = "";
        void OnEnable()
        {
            list = new ReorderableList(Script.Data, typeof(LocalizationTableScheme));
            list.drawHeaderCallback =
                (rect) =>
                {
                    var baseRect = rect;
                    baseRect.x += 20;
                    baseRect.width -= 20;
                    baseRect.width = baseRect.width / 5f;

                    EditorGUI.LabelField(baseRect, "Key");
                    baseRect.x += baseRect.width;

                    EditorGUI.LabelField(baseRect, "English");
                    baseRect.x += baseRect.width;

                    EditorGUI.LabelField(baseRect, "Korean");
                    baseRect.x += baseRect.width;

                    EditorGUI.LabelField(baseRect, "Chinese");
                    baseRect.x += baseRect.width;

                    EditorGUI.LabelField(baseRect, "Japanese");
                    baseRect.x += baseRect.width;
                };
            list.drawElementCallback =
                (rect, index, isActive, isFocused) =>
                {
                    if (Script.Data.Count == 0)
                        return;

                    var stackedRect = rect;
                    //stackedRect.width -= 20;
                    stackedRect.width /= 5;
                    //Script.Data[index]..stringValue = EditorGUI.TextField(stackedRect, keyProperty.stringValue);
                    Script.Data[index].key = EditorGUI.TextField(stackedRect, Script.Data[index].key);

                    stackedRect.x += stackedRect.width;
                    Script.Data[index].english = EditorGUI.TextField(stackedRect, Script.Data[index].english);

                    stackedRect.x += stackedRect.width;
                    Script.Data[index].korean = EditorGUI.TextField(stackedRect, Script.Data[index].korean);

                    stackedRect.x += stackedRect.width;
                    Script.Data[index].chinese = EditorGUI.TextField(stackedRect, Script.Data[index].chinese);
                    
                    stackedRect.x += stackedRect.width;
                    Script.Data[index].japanese = EditorGUI.TextField(stackedRect, Script.Data[index].japanese);
                };

            list.onRemoveCallback =
                (list) =>
                {
                    if (EditorUtility.DisplayDialog("Remove?", "Really?", "ok", "no"))
                    {
                        int oldIndex = list.index;
                        Script.Data.RemoveAt(oldIndex); 
                    }
                };

            list.onAddCallback =
                (list) =>
                {
                    Script.Data.Add(new LocalizationTableScheme() { key = "" });
                };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.ApplyModifiedProperties();
            list.DoLayoutList();
            if (Selection.HasAny())
            {
                var oiginColor = GUI.backgroundColor;
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Back"))
                {
                    var objectToBack = Selection.PopSelection();
                    UnityEditor.Selection.activeObject = objectToBack;
                }
                GUI.backgroundColor = oiginColor;
            }

            string sheetURL = EditorPrefs.GetString(GOOGLE_SHEET_URI_KEY, DEFAULT_GOOGLE_SHEET_URI);
            using (var horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                using (var changeSopce = new EditorGUI.ChangeCheckScope())
                {
                    sheetURL = EditorGUILayout.TextField(sheetURL);
                    if (changeSopce.changed)
                        EditorPrefs.SetString(GOOGLE_SHEET_URI_KEY, sheetURL);
                }

                if(GUILayout.Button("Default", GUILayout.Width(100)))
                    EditorPrefs.SetString(GOOGLE_SHEET_URI_KEY, DEFAULT_GOOGLE_SHEET_URI);
            }

            if (GUILayout.Button("Import TSV"))
            {
                EditorUtility.DisplayProgressBar("Wait...", "connecting..", 0.5f);

                UnityWebRequest.ClearCookieCache();
                UnityWebRequest request = new UnityWebRequest(sheetURL);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.timeout = 5;
                
                var requestOperation = request.SendWebRequest();
                requestOperation.completed +=
                    (operation) =>
                    {
                        var convertedOperation = operation as UnityWebRequestAsyncOperation;
                        if(false == string.IsNullOrEmpty(convertedOperation.webRequest.error))
                        {
                            Debug.LogError($"Error : {convertedOperation.webRequest.error}");
                            EditorUtility.ClearProgressBar();
                            return;
                        }


                        EditorUtility.DisplayProgressBar("Wait...", "connecting..", 0.75f);
                        var csv = convertedOperation.webRequest.downloadHandler.text;
                        
                        if (string.IsNullOrEmpty(csv))
                        {
                            Debug.LogError("구글 시트가 비워져 있어요");
                            return;
                        }
                        Script.ImportFromTSV(csv);
                        EditorUtility.SetDirty(target);
                        EditorUtility.ClearProgressBar();
                        Repaint();
                    };
            }

            if(GUILayout.Button("Export TSV"))
            {
                var path = EditorUtility.SaveFilePanel("Save", "", "LocalizationTable", "csv");
                if (string.IsNullOrEmpty(path))
                    EditorUtility.DisplayDialog("Warnning", "Please select valid path", "OK");

                Script.Export(path);
            }

            var errorInfo = Script.FindError();
            if (errorInfo.errorType == ErrorType.NoError)
                EditorGUILayout.LabelField($"There is no error");
            else
                EditorGUILayout.LabelField($"Error : {errorInfo.errorType}, line : {errorInfo.line}, column : {errorInfo.column}");
        }
    }
}
