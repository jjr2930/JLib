using JLib.Core.FactoryMethodPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Core
{
    public interface IProduct 
    {
        public void SetMember(IFactoryCreationParameter creationParameter);
        public void Use();
    }
}
