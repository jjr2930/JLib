using JLib.FSN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    public class BlackboardValueTemplate<T> : BlackboardValue , ISettable<T>   
    {
        [SerializeField] protected T value;
        
        public void SetValue(T value)
        {
            this.value = value;
        }

        public T GetValue()
        {
            return this.value;
        }
    }
}
