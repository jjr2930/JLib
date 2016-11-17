using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace JLib
{
    public abstract class App : MonoSingle<App>
    {
        public static Vector3 Gravity
        {
            get
            {
                return Instance.gravity;
            }

            set
            {
                Instance.gravity = value;
                Physics.gravity = value;
            }
        }
        Vector3 gravity = Vector3.zero;
        void Awake()
        {
            gravity = Physics.gravity;

            TableLoader.Initialize();
            GlobalEventQueue.Initialize();
            JResources.Initialize();
            ProcessOtherInitialize();
            GlobalEventQueue.RegisterListener(DefaultEvent.ChangeScene, ListenSceneChange);
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

        public static bool IsUseAssetBundle()
        {
            if(Instance._runtimePlatfom == JPlatformType.Android
                || Instance._runtimePlatfom == JPlatformType.IPhonePlayer)
            {
                return true;
            }

            return false;
        }
        JPlatformType _runtimePlatfom = JPlatformType.None;


        public abstract void ListenSceneChange(object param); 
        public abstract void ProcessOtherInitialize();       
    }
}
