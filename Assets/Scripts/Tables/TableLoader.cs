using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace JLib
{
    public class TableLoader : MonoSingle<TableLoader>
    {
        Dictionary<string, string> tablePathTable = new Dictionary<string, string>();
        public Dictionary<string , string> TablePath
        {
            get
            {
                return TablePath;
            }
        }



        public static void ReLoad()
        {
            Instance.LoadTable();
        }

        public void Awake()
        {
            LoadTable();
        }

        public void LoadTable()
        {
            //LoadTablePathTable();
            //LoadLocalizeTable();
            //LoadResourcesTable();
        }

        void LoadResourcesTable()
        {
            string resourcesTablePath = Instance.tablePathTable["ResourcesTable"];
            TextAsset jsonTable = JResources.Load<TextAsset>(resourcesTablePath);
            ResourcesTable.LoadFromJson( jsonTable.text );
        }

        void LoadTablePathTable()
        {
            TextAsset txt =  JResources.Load<TextAsset>("Tables/TablePath");
            string json = txt.text;
            TablePathTable tpt = JsonUtility.FromJson<TablePathTable>(json);
            for( int i = 0 ; i < tpt.pathTable.Count ; i++ )
            {
                tablePathTable.Add( tpt.pathTable[ i ].name , tpt.pathTable[ i ].path );
            }
        }
        

        public int MapLocalizationToIndex( SystemLanguage lang )
        {
            Enum_Local loc = (Enum_Local) Enum.Parse(typeof(Enum_Local),lang.ToString());
            return ( int )loc;
        }
    }
}
