using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
namespace JLib
{
    public class App : MonoSingle<App>
    {
        public static bool IsUseAssetBundle()
        {
            if( Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.IPhonePlayer )
            {
                return true;
            }

            return false;
        }

        [SerializeField]
        UnityEvent appStartMethod = null;

#if UNITY_EDITOR
        [RuntimeInitializeOnLoadMethod]
        static void LoadInScene()
        {
            var app = GameObject.FindObjectOfType<App>();
            if( null == app )
            {
                App.Initialize();
            }
        }


        static bool IsLoadedUI = false;
#endif

        //#endregion 
        void Awake()
        {
            TableLoader.Initialize();
            GlobalEventQueue.Initialize();
            JResources.Initialize();
            EffectManager.Initialize();
            OnAwake();
            GlobalEventQueue.RegisterListener( DefaultEvent.LoadScene , ListenSceneChange );
            GlobalEventQueue.RegisterListener( DefaultEvent.AddScene , ListenAddScene );
            GlobalEventQueue.RegisterListener( DefaultEvent.UnloadScene , ListenUnloadScene );
            SceneManager.sceneLoaded += LoadedCompleteMethod;
            //gravity = Physics.gravity;


            if( null != appStartMethod )
            {
                appStartMethod.Invoke();
            }
        }

        public void ListenSceneChange( object param )
        {
            string sceneName = param as string;
            if( !string.IsNullOrEmpty( sceneName ) )
            {
                SceneManager.LoadScene( sceneName );
            }
        }

        public void ListenAddScene( object sceneName )
        {
            string name = sceneName as string;
            if( !string.IsNullOrEmpty( name ) )
            {
                SceneManager.LoadScene( name , LoadSceneMode.Additive );
            }
        }

        public void ListenUnloadScene( object param )
        {
            string sceneName = param as string;
            if( !string.IsNullOrEmpty( sceneName ) )
            {
                SceneManager.UnloadSceneAsync( sceneName );
            }
        }

        public void LoadedCompleteMethod( Scene scene , LoadSceneMode mode )
        {
            GlobalEventQueue.EnQueueEvent( DefaultEvent.CompleteLoadScene , scene.name );
        }

        public virtual void OnAwake() { }

        void OnDistroy()
        {
            GlobalEventQueue.RemoveListener( DefaultEvent.LoadScene , ListenSceneChange );
            GlobalEventQueue.RemoveListener( DefaultEvent.AddScene , ListenAddScene );
            GlobalEventQueue.RemoveListener( DefaultEvent.UnloadScene , ListenUnloadScene );

        }
    }

}
