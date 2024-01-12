using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib
{
    public static class TransformUtility
    {
        public static Transform FindByName(Transform parent, string name)
        {
            if (parent.name == name)
                return parent;

            int childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Transform child = parent.GetChild(i);
                var found = FindByName(child, name);
                if (null != found)
                {
                    return found;
                }
            }
         
            return null;
        }
    }
}
