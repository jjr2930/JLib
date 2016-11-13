using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JLib
{
    public abstract class BaseResourcesLoader : MonoSingle<BaseResourcesLoader>
    {
        Dictionary<string, UnityEngine.Object> loadedObjectList = new Dictionary<string, UnityEngine.Object>();
        public UnityEngine.Object Load(string path)
        {
            UnityEngine.Object foundedObj = null;
            if(!Instance.loadedObjectList.TryGetValue(path, out foundedObj))
            {
                foundedObj = Instance.OnLoad(path);
                Instance.loadedObjectList.Add(path, foundedObj);
            }

            return foundedObj;
        }

        public T Load<T>(string path) where T : UnityEngine.Object
        {
            UnityEngine.Object loadedObj = Load(path);
            return loadedObj as T;
        }

        public abstract UnityEngine.Object OnLoad(string path);
    }
}
