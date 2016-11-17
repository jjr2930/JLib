using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace JLib
{
    public class TableLoader : Singletone<TableLoader>
    {
        Dictionary<string, string> tablePathTable = new Dictionary<string, string>();
        public Dictionary<string, string> TablePath
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

        public TableLoader()
        {
            LoadTable();
        }

        public void LoadTable()
        {
            LoadTablePathTable();
            LoadLocalizeTable();
            LoadResourcesTable();
        }

        void LoadResourcesTable()
        {
            string resourcesTablePath = Instance.tablePathTable["ResourcesTable"];
            TextAsset jsonTable = JResources.Load<TextAsset>(resourcesTablePath);
            ResourcesTable.LoadFromJson(jsonTable.text);
        }

        void LoadTablePathTable()
        {
            TextAsset txt =  JResources.Load<TextAsset>("Tables/TablePathTable");
            string json = txt.text;
            TablePathTable tpt = JsonUtility.FromJson<TablePathTable>(json);
            for( int i = 0 ; i < tpt.pathTable.Count ; i++ )
            {
                tablePathTable.Add( tpt.pathTable[ i ].name , tpt.pathTable[ i ].path );
            }
        }

        void LoadLocalizeTable()
        {
            string localTablePath = tablePathTable["LocalizeTable"];
            string localJson = JResources.Load<TextAsset>(localTablePath).text;
            using (LocalizeTableTemp tempTable = JsonUtility.FromJson<LocalizeTableTemp>(localJson))
            {
                string location = Application.systemLanguage.ToString();
                for (int i = 0; i < tempTable.data.Count; i++)
                {
                    if (tempTable.data[i].local == location)
                    {
                        LocalizeTable.AddLocalString(tempTable.data[i].key, tempTable.data[i].value);
                    }
                }
            }
        }
    }
}
