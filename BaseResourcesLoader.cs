using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace JLib
{
    public abstract class BaseResourcesLoader
    {
        protected Dictionary<string, UnityEngine.Object> loadedObjectList = new Dictionary<string, UnityEngine.Object>();
        public UnityEngine.Object Load(string path)
        {
            UnityEngine.Object foundedObj = null;
            if(!loadedObjectList.TryGetValue(path, out foundedObj))
            {
                foundedObj = OnLoad(path);
                loadedObjectList.Add(path, foundedObj);
            }

            return foundedObj;
        }

        public T2 Load<T2>(string path) where T2 : UnityEngine.Object
        {
            UnityEngine.Object loadedObj = Load(path);
            return loadedObj as T2;
        }

        public virtual UnityEngine.Object OnLoad(string path )
        {
            Debug.Log("I do not want call this");
            return null;
        }
    }
}
