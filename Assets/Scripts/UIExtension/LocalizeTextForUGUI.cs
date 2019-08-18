using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
namespace JLib
{
    [AddComponentMenu("LocalizeTextForUGUI")]
    [RequireComponent(typeof(Text))]
    public class LocalizeTextForUGUI: JMonoBehaviour
    {
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
                text.text = LocalizeTable.GetLocalString(key);
            }
        }

        [SerializeField]
        string key = "";

        Text text = null;
        void Awake()
        {
            text = GetComponent<Text>();
        }

        private void Start()
        {
            text.text = LocalizeTable.GetLocalString( key );
        }
    }
}
