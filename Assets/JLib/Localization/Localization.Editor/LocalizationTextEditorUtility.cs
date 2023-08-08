using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JLib.Localization.Editor
{
    public static class LocalizationTextEditorUtility
    {
        public static LocalizationTextTable FindLocalizationTextTable()
        {
            LocalizationTextTable result = null;
            var guids = AssetDatabase.FindAssets("table");
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                result = AssetDatabase.LoadAssetAtPath<LocalizationTextTable>(path);
                if (null != result)
                    break;
            }

            return result;
        }
    }
}