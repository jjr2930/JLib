using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public class MonoSingle<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance = null;
        public static T Instance
        {
            get
            {
                if( null == _instance )
                {
                    Initialize();
                }
                return _instance;
            }
        }

        public static void Initialize()
        {
            //find Instance all
            T[] objects = FindObjectsOfType<T>();
            switch( objects.Length )
            {
                case 0:
                    //it is in the resources folder?
                    string[] typeNameSplits = typeof(T).ToString().Split('.');
                    string typeName = typeNameSplits[typeNameSplits.Length -1];
                    var loadObjs = Resources.LoadAll<T>("Prefabs/Singleton/");
                    
                    if (loadObjs.Length != 1)
                        throw new InvalidOperationException($"{typeof(T)} is not only one");

                    if( null != loadObjs[0] )
                    {
                        _instance = Instantiate(loadObjs[0]);
                    }
                    else
                    {
                        //create new gameobject
                        Debug.LogFormat( "{0} was not founded so Instnatiate {0}" , typeof( T ).ToString() ); ;
                        string[] splits = typeof( T ).ToString().Split( '.' );
                        string name = splits[ splits.Length - 1 ];
                        GameObject go = new GameObject( name );
                        _instance = go.AddComponent<T>();
                    }
                    break;

                case 1:
                    _instance = objects[ 0 ];
                    break;

                default:
                    Debug.LogWarningFormat( "{0} is MonoSingle, so it must be unique, leave only one" , typeof( T ).ToString() );
                    for( int i = 1 ; i < objects.Length ; i++ )
                    {
                        GameObject.Destroy( objects[ i ] );
                    }
                    _instance = objects[ 0 ];
                    objects = null;
                    break;
            }
            DontDestroyOnLoad( _instance );
            Debug.LogFormat( "{0} was set DontDestroyOnLoad object" , typeof( T ).ToString() );
        }
    }
}
