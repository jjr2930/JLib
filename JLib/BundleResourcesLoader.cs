﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JLib
{
    public class BundleResourcesLoader : BaseResourcesLoader
    {
        /// <summary>
        /// key : path, bundle name
        /// </summary>
        Dictionary<string, string> bundleNameList = new Dictionary<string, string>();

        public override UnityEngine.Object OnLoad(string path)
        {
            string foundedBundleName = null;
            if(bundleNameList.TryGetValue(path,out foundedBundleName))
            {
                AssetBundle loadedBundle = AssetBundle.LoadFromFile(foundedBundleName);
                if(null == loadedBundle)
                {
                    Debug.LogErrorFormat("can not founded Object({0}) int assetbundle({1})", path, foundedBundleName);
                    return null;
                }
                UnityEngine.Object loadedObject = loadedBundle.LoadAsset(path);
                return loadedObject;
            }

            Debug.LogErrorFormat("BundleResourcesLoader.OnLoad=> bundleName is not founded about {0}", path);
            return null;
        }
    }
}
