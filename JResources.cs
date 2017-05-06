using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace JLib
{
    /// <summary> 
    /// 굳이 이클래스는 비쥬얼디버깅을 할게 없다. 직접 코드를 봐야한다
    /// </summary>
    public static class JResources 
    {
        static BaseResourcesLoader resourcesLoader = null;
        public static void Initialize()
        {
            LoadLoader();
        }

        public static UnityEngine.Object Load(string path)
        {
            if(null == resourcesLoader)
            {
                LoadLoader();
            }

            return resourcesLoader.Load(path);
        }

        public static T Load<T>(string path) where T : UnityEngine.Object
        {
            return Load(path) as T;

        }

        static void LoadLoader()
        {
            switch(Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    resourcesLoader = EditorResourcesLoader.Instance;
                    break;

                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    resourcesLoader = BundleResourcesLoader.Instance;
                    break;

                default:
                    Debug.LogErrorFormat("JResources.LoadLoader=> {0} is not supported", Application.platform);
                    break;
            }
        }
    }
}
