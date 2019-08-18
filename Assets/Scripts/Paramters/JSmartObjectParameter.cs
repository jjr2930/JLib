using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    /// <summary>
    /// parameter for smartobject
    /// </summary>
    /// <typeparam name="T1">smart object's type</typeparam>
    /// <typeparam name="T2">what do animation </typeparam>
    public class JSmartObjectParameter<T1,T2>
        where T1 : IConvertible
        where T2 : IConvertible
    {
        public Transform[] actionPostions;
        public SmartObjectData<T1,T2>[] data;
    }
}
