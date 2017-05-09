
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public class PoolData
    {
        public GameObject obj;
        public bool IsInPool;

        public PoolData( GameObject obj , bool IsInPool )
        {
            this.obj = obj;
            this.IsInPool = IsInPool;
        }
    }
    public class BasePoolManager : MonoSingle<BasePoolManager>
    {
        Dictionary<string,List<PoolData>> unityObjectPool
            = new Dictionary<string, List<PoolData>>();

        public static GameObject GetObject( string resourcePath )
        {
            PoolData foundedData = null;
            List<PoolData> foundedList = null;
            if( !Instance.unityObjectPool.TryGetValue( resourcePath , out foundedList ) )
            {
                foundedList = new List<PoolData>();
                Instance.unityObjectPool.Add( resourcePath , foundedList );
            }

            //find at list
            for( int i = 0 ; i < foundedList.Count ; i++ )
            {
                if( foundedList[ i ].IsInPool )
                {
                    foundedData = foundedList[ i ];
                    foundedData.IsInPool = false;
                    return foundedData.obj;
                }
            }

            //not founded at List
            //so Instantiate new object
            //find path from ResourcesTable
            string[] splits = resourcePath.Split('.');
            string name = splits[splits.Length - 1 ];
            foundedData = Instance.SpawnObjectToPool( name , resourcePath);
            foundedData.IsInPool = false;
            return foundedData.obj;
        }

        public static T GetObject<T>( string resourceName ) where T : Component
        {
            GameObject loadedObj = GetObject(resourceName);
            T component = loadedObj.GetComponent<T>();
            if( null == component )
            {
                Debug.LogErrorFormat( "BasePoolManger.GetObject=>{0} is not have {1}" ,
                    resourceName , typeof( T ).ToString() );
                return null;
            }
            component.gameObject.SetActive( true );
            return component;
        }

        public static void ReturnObject( string resourceName , Object obj )
        {
            List<PoolData> foundedList = null;
            bool success = false;
            if( Instance.unityObjectPool.TryGetValue( resourceName , out foundedList ) )
            {
                for( int i = 0 ; i < foundedList.Count ; i++ )
                {
                    foundedList[ i ].obj.Equals( obj );
                    ( obj as GameObject ).SetActive( false );
                    foundedList[ i ].IsInPool = true;
                    success = true;
                }
            }

            if( !success )
            {
                Debug.LogErrorFormat( "BasePoolManger.ReturnObject=>{0},{1} can not be returned" ,
                    resourceName , obj.name );
            }
        }

        PoolData SpawnObjectToPool( string name , string path )
        {
            var obj = Instantiate(JResources.Load(path),Vector3.zero,Quaternion.identity) as GameObject;
            obj.SetActive( false );
            var list = Instance.GetPooldataList(name);
            PoolData newData = new PoolData(obj,true);
            list.Add( newData );
            return newData;
        }


        List<PoolData> GetPooldataList( string name )
        {
            List<PoolData> foundedList = null;
            if( !Instance.unityObjectPool.TryGetValue( name , out foundedList ) )
            {
                foundedList = new List<PoolData>();
                Instance.unityObjectPool.Add( name , foundedList );
            }

            return foundedList;
        }


    }
}
