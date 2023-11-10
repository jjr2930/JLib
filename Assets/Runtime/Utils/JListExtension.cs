using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public static class JListExtension
    {

        public static T GetLast<T>(this List<T> list)
        {
            return list[list.Count - 1];
        }

        public static void ClearAndDeleteGameObject<T>( this List<T> list )
        {
            for (int i = 0; i < list.Count; i++)
            {
                Component com = list[i] as Component;
                GameObject.Destroy( com.gameObject );
            }

            list.Clear();
        }

        public static void RemoveLast<T>(this List<T> list)
        {
            list.RemoveAt( list.Count - 1 );
        }
    }
}
