using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Localization.Editor
{
    public static class Selection
    {
        static Stack<Object> selectionStack = new Stack<Object>();

        public static void PushSelection(Object obj)
        {
            selectionStack.Push(obj);
        }

        public static Object PopSelection()
        {
            return selectionStack.Pop();
        }

        public static bool IsEmpty()
        {
            return (selectionStack.Count == 0);
        }
        public static bool HasAny()
        {
            return selectionStack.Count > 0;
        }
    }
}
