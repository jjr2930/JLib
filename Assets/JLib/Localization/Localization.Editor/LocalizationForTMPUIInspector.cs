using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;

namespace JLib.Localization.Editor
{
    [CustomEditor(typeof(LocalizationForTMPUI))]
    public class LocalizationForTMPUIInspector : UnityEditor.Editor
    {
        static LocalizationTextTable table = null;
        static string filter = "";
        private void OnEnable()
        {
            if (null == table)
                table = LocalizationTextEditorUtility.FindLocalizationTextTable();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            LocalizationForTMPUI script = target as LocalizationForTMPUI;

            using (var horizontalScope = new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("Key");
                string keyString = string.IsNullOrEmpty(script.Key) ? "None" : script.Key;
                if (GUILayout.Button(keyString))
                {
                    var popupWindow = new LocalizationKeyPopupWindow();
                    popupWindow.Caller = script;
                    popupWindow.OnKeySelected +=
                        (key) =>
                        {
                            script.SetKeyOnly(key);
                            script.SetTextOnly(key);
                            UnityEditor.EditorUtility.SetDirty(target);
                        };
                    var windowSize = popupWindow.GetWindowSize();

                    Rect popupRect = new Rect();
                    
                    popupRect.width = EditorGUIUtility.currentViewWidth;
                    popupRect.x = popupRect.width * 0.5f;
                    popupRect.y += EditorGUIUtility.singleLineHeight * 4;
                    PopupWindow.Show(popupRect, popupWindow);
                }
            }
        }
    }

    public class LocalizationKeyPopupWindow : PopupWindowContent
    {
        const float WINDOW_HEIGHT = 300;
        const float WINDOW_WIDTH = 400;

        static string filter = "";
        static Vector2 scroll = new Vector2();
        static LocalizationTextTable table = null;
        public Action<string> OnKeySelected { get; set; }
        public LocalizationForTMPUI Caller { get; set; }
        public override Vector2 GetWindowSize()
        {
            return new Vector2(WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        public override void OnOpen()
        {
            base.OnOpen();
            if (null == table)
            {
                var allPaths = AssetDatabase.GetAllAssetPaths();
                for (int i = 0; i < allPaths.Length; i++)
                {
                    if (false == allPaths[i].ToUpper().Contains("LOCALIZATION"))
                        continue;

                    var loadedAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(allPaths[i]);
                    if (loadedAsset is LocalizationTextTable)
                    {
                        table = loadedAsset as LocalizationTextTable;
                        Debug.Log("table successfully referenced");
                        break;
                    }
                }
            }
        }

        public override void OnGUI(Rect rect)
        {
            Rect stackedRect = new Rect(rect);
            stackedRect.height = EditorGUIUtility.singleLineHeight;
            filter = EditorGUILayout.TextField("Filter", filter);

            stackedRect.y += EditorGUIUtility.singleLineHeight;
            Rect scrollViewPosition = new Rect(stackedRect);
            Rect scrollViewRect = new Rect(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            scrollViewPosition.height = WINDOW_HEIGHT;
            scrollViewRect.height = WINDOW_HEIGHT - EditorGUIUtility.singleLineHeight * 2;

            using (var scrollScope = new EditorGUILayout.ScrollViewScope(scroll))
            {
                var keyList = table.GetKeyList();
                if (null == keyList)
                    return;

                for (int i = 0; i < keyList.Length; i++)
                {
                    if (!keyList[i].ToUpper().Contains(filter.ToUpper()))
                        continue;

                    stackedRect.y += EditorGUIUtility.singleLineHeight;
                    if (GUILayout.Button(keyList[i]))
                    {
                        OnKeySelected(keyList[i]);
                        editorWindow.Close();
                        return;
                    }
                }

                scroll = scrollScope.scrollPosition;
            }

            EditorGUILayout.LabelField("");
            if (GUILayout.Button("Go to localization text table"))
            {
                UnityEditor.Selection.activeObject = table;
                Localization.Editor.Selection.PushSelection(Caller);
                editorWindow.Close();
            }
        }
    }

}
