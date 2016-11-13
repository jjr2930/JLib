using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
namespace JLib
{
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
        void Start()
        {
            text = GetComponent<Text>();
            text.text = LocalizeTable.GetLocalString(key);
        }
    }
}
