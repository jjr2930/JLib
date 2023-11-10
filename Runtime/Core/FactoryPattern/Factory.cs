using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Core.FactoryMethodPattern
{    
    public interface IFactoryCreationParameter
    {
    }

    public abstract class Factory<T> : MonoBehaviour
        where T : IProduct
    {
        public abstract T GetOne(IFactoryCreationParameter parameter);
    }
}
