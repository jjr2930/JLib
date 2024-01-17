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
        [SerializeField, HideInInspector] protected T runtimeValue;

        public void SetValue(T value)
        {
            if (Application.isPlaying)
                runtimeValue = value;
            else
                this.value = value;
        }

        public T GetValue()
        {
            if (Application.isPlaying)
                return runtimeValue;
            else
                return this.value;
        }

        public override void CopyValueToRuntimeValue()
        {
            base.CopyValueToRuntimeValue();
            runtimeValue = value;
        }
    }
}
