using UnityEngine;
using System.Collections;

public static class TransfromExtension
{
    public static Transform FindChildUsingTraversal(this Transform t, string name)
    {
        for(int i =0; i<t.childCount; i++)
        {
            Transform currentChild = t.GetChild(i);
            if( name == currentChild.name )
            {
                return currentChild;
            }
            else
            {
                return currentChild.FindChildUsingTraversal( name );
            }
        }
        Debug.LogErrorFormat( "{0} does not have {1}" , t.name , name );
        return null;
    }
}
