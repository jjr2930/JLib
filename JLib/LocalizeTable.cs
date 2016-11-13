using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
namespace JLib
{
    [Serializable]
    public class LocalizeData
    {
        public string key;
        public string local;
        public string value;
    }

    /// <summary>
    /// 테이블을 통째로 읽을 때 쓰일 임시 클래스
    /// </summary>
    [Serializable]
    public class LocalizeTableTemp : IDisposable
    {
        public List<LocalizeData> data;

        public void Dispose()
        {
            data.Clear();
        }
    }

    public class LocalizeTable : Singletone<LocalizeTable>
    {
        Dictionary<string, string> table = new Dictionary<string, string>();

        public static string GetLocalString(string key)
        {
            string founded = null;
            if(Instance.table.TryGetValue(key, out founded))
            {
                return founded;
            }
            else
            {
                Debug.LogErrorFormat("LocalizeTable => {0} is not founded", key);
            }
            return "";
        }

        public static void AddLocalString(string key, string value)
        {
            Instance.table.Add(key, value);
        }
    }
}
