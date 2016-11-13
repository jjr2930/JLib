using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace JLib
{
    public abstract class App : MonoSingle<App>
    {
        void Awake()
        {
            TableLoader.Initialize();
            GlobalEventQueue.Initialize();
            JResources.Initialize();
        }

        public static JPlatformType Platform
        {
            get
            {
                if(JPlatformType.None == Instance._runtimePlatfom)
                {
                    int enumNumber = (int)Application.platform;
                    Instance._runtimePlatfom = (JPlatformType)enumNumber;
                }

                return Instance._runtimePlatfom;
            }
        }

        JPlatformType _runtimePlatfom = JPlatformType.None;


        public abstract void ListenSceneChange(object param);        
    }
}
