using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public class BundlePathData
    {
        public string key;
        public string bundlePath;
    }

    public class BundlePathList
    {
        public List<BundlePathData> bundleList = new List<BundlePathData>();
    }

    public class BundleResourcesTable : MonoSingle<BundleResourcesTable>
    {
        const string TABLE_PATH = "Tables/BundleTable";
        /// <summary>
        /// key : name, value : path
        /// </summary>
        Dictionary<string, string> bundlePathes = new Dictionary<string, string>();

        public void Awake()
        {
            TextAsset bundleList = JResources.Load<TextAsset>(TABLE_PATH);
            BundlePathList tempList = JsonUtility.FromJson<BundlePathList>(bundleList.text);
            for( int i = 0 ; i < tempList.bundleList.Count ; i++ )
            {
                bundlePathes.Add(tempList.bundleList[i].key,tempList.bundleList[i].bundlePath);
            }
        }
        public static string GetBundlePath( string key )
        {
            string foundedPath = null;
            if( !Instance.bundlePathes.TryGetValue( key , out foundedPath ) )
            {
                Debug.LogErrorFormat( "BundleResourcesTable.GetBundlePath=> not founded key : {0}" , key );
            }
            return foundedPath;
        }

        public static void AddPath( string key , string value )
        {
            Instance.bundlePathes.Add( key , value );
        }
    }
}
