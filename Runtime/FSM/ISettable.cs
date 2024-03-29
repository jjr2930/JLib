using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.FSM
{
    public interface ISettable<T>
    {
        T GetValue();
        void SetValue(T value);
    }
}
