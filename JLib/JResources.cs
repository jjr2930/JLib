using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace JLib
{
    public class JResources : Singletone<JResources>
    {
        BaseResourcesLoader resourcesLoader = null;
        public static UnityEngine.Object Load(string path)
        {
            if(null == Instance.resourcesLoader)
            {
                Instance.LoadLoader();
            }

            return Instance.resourcesLoader.Load(path);
        }

        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            return Load(path) as T;

        }

        void LoadLoader()
        {
            switch(App.Platform)
            {
                case JPlatformType.WindowsEditor:
                case JPlatformType.WindowsWebPlayer:
                case JPlatformType.OSXEditor:
                case JPlatformType.OSXPlayer:
                    resourcesLoader = EditorResourcesLoader.Instance;
                    break;

                case JPlatformType.Android:
                case JPlatformType.IPhonePlayer:
                    resourcesLoader = BundleResourcesLoader.Instance;
                    break;

                default:
                    Debug.LogErrorFormat("JResources.LoadLoader=> {0} is not supported", App.Platform);
                    break;
            }
        }
    }
}
