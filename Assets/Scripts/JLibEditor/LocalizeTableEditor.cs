using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using JLib;
using System;

namespace JLibEditor
{
    public class LocalizeTableEditor : BaseTableEditor<LocalizeDataList,LocalizeData>
    {
        [MenuItem( "Tools/TableEditor/LocalizeTable" )]
        static void ShowWindow()
        {
            var window = GetWindow<LocalizeTableEditor>();
            window.Initialize();

        }
        
        protected override void PreInitialze()
        {
            tablePath = "Assets/Resources/Tables/LocalizeTable.txt";
            COLUMN_COUNT = 4;   // key, korean, english, - button
        }

        protected override void OnGUI_ComlumnName(int column)
        {
            switch( column )
            {
                case 0:
                    EditorGUILayout.LabelField( "Key" );
                    break;

                case 1:
                    EditorGUILayout.LabelField( "Korean" );
                    break;

                case 2:
                    EditorGUILayout.LabelField( "English" );
                    break;
            }
        }

        protected override void OnGUI_Body_Element(  int column, LocalizeData data )
        {
            switch(column)
            {
                case 0:
                    data.key = EditorGUILayout.TextField( data.key,
                        GUILayout.MinHeight(HEIGHT_SIZE),
                        GUILayout.MaxHeight(HEIGHT_SIZE) );
                    break;

                case 1:
                    data.list[ 0 ] = EditorGUILayout.TextField( data.list[ 0 ],
                        GUILayout.MinHeight( HEIGHT_SIZE ),
                        GUILayout.MaxHeight( HEIGHT_SIZE ) );
                    break;

                case 2:
                    data.list[ 1 ] = EditorGUILayout.TextField( data.list[ 1 ],
                        GUILayout.MinHeight( HEIGHT_SIZE ),
                        GUILayout.MaxHeight( HEIGHT_SIZE ) );
                    break;              
                    
            }
        }

        protected override string ToJson()
        {
            LocalizeDataList obj = ( LocalizeDataList )table;
            return JsonUtility.ToJson( obj );
        }

        protected override void ToObject(string json)
        {
            LocalizeDataList obj = JsonUtility.FromJson<LocalizeDataList>( json );
            table = obj;
        }

    }
}