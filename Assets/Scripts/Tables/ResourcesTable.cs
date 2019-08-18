using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{

    [Serializable]
    public class ResourcesData
    {
        public string name;
        public int index;
        public string standalonePath;
        public string bundlePath;
    }

    [Serializable]
    public class ResourcesDataList
    {
        public List<ResourcesData> table = new List<ResourcesData>();
    }

    public class ResourcesTable: MonoSingle<ResourcesTable>
    {
        ResourcesDataList table = null;

        public static void LoadFromJson(string json)
        {
            Instance.table = JsonUtility.FromJson<ResourcesDataList>(json);
        }
        public static string GetResourcePath(string resourcesName)
        {
            ResourcesData foundedData = null;
            for( int i = 0 ; i < Instance.table.table.Count ; i++ )
            {
                if(resourcesName == Instance.table.table[i].name)
                {
                    foundedData = Instance.table.table[i];
                    if(App.IsUseAssetBundle())
                    {
                        return foundedData.standalonePath;
                    }
                    else
                    {
                        return foundedData.bundlePath;
                    }
                }
            }
            Debug.LogErrorFormat( "ResourcesTable.GetResourcesPath=>{0} is not founded" , resourcesName );
            return null;
        }
    }
}
