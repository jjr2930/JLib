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

    public class BundlPathList
    {
        List<BundlePathData> bundleList;
    }

    public class BundleResourcesTable : Singletone<BundleResourcesTable>
    {
        Dictionary<string, string> bundlePathes = new Dictionary<string, string>();

        public static string GetBundlePath(string key)
        {
            string foundedPath = null;
            if(!Instance.bundlePathes.TryGetValue(key, out foundedPath))
            {
                Debug.LogErrorFormat("BundleResourcesTable.GetBundlePath=> not founded key : {0}", key);
            }
            return foundedPath;
        }

        public static void AddPath(string key, string value)
        {
            Instance.bundlePathes.Add(key, value);
        }
    }
}
