using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace JLib
{
    public class JUIManagerInitializer : MonoSingle<JUIManagerInitializer>
    {
        [SerializeField]
        List<GameObject> uimanagers = new List<GameObject>();

        private void Start()
        {
            List<JIUIManager> list = new List<JIUIManager>();
            for( int i = 0; i < uimanagers.Count; i++ )
            {
                var element = uimanagers[ i ].GetComponent<JIUIManager>();
                list.Add( element);
                element.Awake();
                uimanagers[ i ].SetActive( false );
            }
            DontDestroyOnLoad( gameObject );
        }
    }
}
