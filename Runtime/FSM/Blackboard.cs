using JLib.FSN;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    [CreateAssetMenu(menuName ="FSM/Black board")]
    public class Blackboard : ScriptableObject
    {
        [SerializeField] List<BlackboardValue> values = new List<BlackboardValue>();

        Dictionary<int, BlackboardValue> valueByHash
            = new Dictionary<int, BlackboardValue>();

        public int Count 
        {
            get 
            { 
                return values.Count; 
            }            
        }

        public T GetValue<T>(string name)
        {
            return GetValue<T>(name.GetHashCode());
        }

        public T GetValue<T>(int hash)
        {
            if (false == valueByHash.ContainsKey(hash))
            {
                throw new KeyNotFoundException($"{hash} value is not exist");
            }

            var valueObject = valueByHash[hash] as ISettable<T>;
            if (null == valueObject)
            {
                throw new System.InvalidOperationException($"Hash({hash}) is not BlackboardValue{typeof(T)} type");
            }

            return valueObject.GetValue();
        }

        public void SetValue<T>(string name, T value)
        {
            SetValue(name.GetHashCode(), value);
        }

        public void SetValue<T>(int hash, T value)
        {
            if (false == valueByHash.ContainsKey(hash))
            {
                throw new KeyNotFoundException($"{hash} value is not exist");
            }

            var valueObject = valueByHash[hash] as ISettable<T>;
            if (null == valueObject)
            {
                throw new System.InvalidOperationException($"Hash({hash}) is not BlackboardValue{typeof(T)} type");
            }

            valueObject.SetValue(value);
        }

        public void Init()
        {
            valueByHash.Clear();
            foreach (var value in values)
            {
                valueByHash.Add(value.name.GetHashCode(), value);
            }
        }

        public BlackboardValue GetValue(int index)
        {
            return values[index];
        }

        public void AddValue(BlackboardValue newValue)
        {
            values.Add(newValue);
        }

        public void RemoveValue(BlackboardValue oldValue)
        {
            values.Remove(oldValue);
        }

        public void RemoveValue(int index)
        {
            values.RemoveAt(index);
        }
    }
}
