using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace JLib
{
    public enum Enum_Local
    {
        Korean = 0,
        English,
        Max,
    }

    [System.Serializable]
    public class LocalizeDataList : ITable<LocalizeData>
    {
        public List<LocalizeData> List
        {
            get
            {
                return list;
            }

            set
            {
                list = value;
            }
        }
        public List<LocalizeData> list = new List<LocalizeData>();
    }

    [System.Serializable]
    public class LocalizeData
    {
        public string key;
        public List<string> list = new List<string>();
        public LocalizeData()
        {
            key = "";
            for( int i = 0 ; i < ( int )Enum_Local.Max ; i++ )
            {
                list.Add( "" );
            }
        }
    }
    //[Serializable]
    //public class LocalizeData
    //{
    //    public string key;
    //    public string local;
    //    public string value;
    //}

    ///// <summary>
    ///// 테이블을 통째로 읽을 때 쓰일 임시 클래스
    ///// </summary>
    //[Serializable]
    //public class LocalizeTableTemp : IDisposable
    //{
    //    public List<LocalizeData> data;

    //    public void Dispose()
    //    {
    //        data.Clear();
    //    }
    //}

    public class LocalizeTable : MonoSingle<LocalizeTable>
    {
        const string TABLE_PATH = "Tables/LocalizeTable";
        Dictionary<string, string> table = new Dictionary<string, string>();

        public void Awake()
        {
            string localTablePath = TABLE_PATH;
            TextAsset ta = JResources.Load<TextAsset>(localTablePath);
            string localJson = ta.text;

            LocalizeDataList tempTable = JsonUtility.FromJson<LocalizeDataList>( localJson );
            int index = MapLocalizationToIndex(Application.systemLanguage);
            for( int i = 0 ; i < tempTable.List.Count ; i++ )
            {
                string key = tempTable.List[i].key;
                string value = tempTable.List[i].list[index];
                LocalizeTable.AddLocalString( key , value );
            }

        }

        public int MapLocalizationToIndex( SystemLanguage lang )
        {
            Enum_Local loc = (Enum_Local) Enum.Parse(typeof(Enum_Local),lang.ToString());
            return ( int )loc;
        }
        public static string GetLocalString( string key )
        {
            string founded = "";
            if( Instance.table.TryGetValue( key , out founded ) )
            {
                return founded;
            }
            else
            {
                Debug.LogWarningFormat( "LocalizeTable => {0} is not founded" , key );
            }
            return "";
        }

        public static void AddLocalString( string key , string value )
        {
            Instance.table.Add( key , value );
        }
    }
}
