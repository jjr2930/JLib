using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace JLib.FSM
{
    public class StateMachineValueTemplate<T> : StateMachineValue, ISettable<T>
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
