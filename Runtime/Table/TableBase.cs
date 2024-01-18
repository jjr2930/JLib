using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib
{
    public class TableBase<KeyT,SchemeT> : ScriptableObject
        where SchemeT : TableSchemeBase<KeyT>
    {
        //단지 직렬화를 위한 것일 뿐
        [SerializeField] List<SchemeT> data = new List<SchemeT>();
        
        //사용은 이것으로
        Dictionary<KeyT, SchemeT> datumByKey;

        public SchemeT this[KeyT key]
        {
            get
            {
                SchemeT found = null;
                if (false == datumByKey.TryGetValue(key, out found))
                {
                    Debug.LogError($"{name} there is not key : {key}");
                    return null;
                }

                return found;
            }
        }

        public int Count
        {
            get
            {
                return datumByKey.Count;
            }
        }

        public void OnEnable()
        {
            datumByKey = new Dictionary<KeyT, SchemeT>(data.Count);
            for (int i = 0; i < data.Count; ++i)
            {
                datumByKey.Add(data[i].key, data[i]);
            }
        }
    }
}
