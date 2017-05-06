using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{

    /// <summary>
    /// SmartObjet Data
    /// T1,T2 must be enum type
    /// </summary>
    /// <typeparam name="T1">smart object's type</typeparam>
    /// <typeparam name="T2">what do animation </typeparam>
    [Serializable]
    public class SmartObjectData<T1,T2> 
        where T1 : IConvertible
        where T2 : IConvertible
    {
        public T1 type;
        public List<T2> animations;
    }

    /// <summary>
    /// Smart Object's Interface
    /// T1,T2 must be enum type
    /// </summary>
    /// <typeparam name="T1">smart object's type</typeparam>
    /// <typeparam name="T2">what do animation </typeparam>
    public interface JISmartObject<T1,T2>
        where T1 : IConvertible
        where T2 : IConvertible
    {
        SmartObjectData<T1,T2>[] SmartObjectData { get; }

        Transform[] ActionPositions { get; }

        Vector3 RandomActionPosition { get;}

        void OnCollisionEnter( Collision other );
    }
}