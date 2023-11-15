using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JLib.FSM
{
    [CreateAssetMenu(menuName ="FSM/Black board")]
    public class Blackboard : ScriptableObject
    {
        [SerializeField] List<BlackboardValue> values = new List<BlackboardValue>();

        public int Count 
        {
            get 
            { 
                return values.Count; 
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
