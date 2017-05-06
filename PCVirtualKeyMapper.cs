using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace JLib
{
    [Serializable]
    public class PCVirtualKeyMapData
    {
        public KeyCode keyboardKey;
        public VK_Enum virtualKey;
    }
    public class PCVirtualKeyMapper : MonoSingle<PCVirtualKeyMapper>
    {
        [SerializeField]
        List<PCVirtualKeyMapData> mappingList
            = new List<PCVirtualKeyMapData>();

        void Update()
        {
            for(int i =0; i<mappingList.Count; i++)
            {
                if(Input.GetKeyDown(mappingList[i].keyboardKey))
                {
                    GlobalEventQueue.EnQueueEvent( VK_State.Down, mappingList[ i ].virtualKey );
                }

                if(Input.GetKey(mappingList[i].keyboardKey))
                {
                    GlobalEventQueue.EnQueueEvent(VK_State.Press, mappingList[i].virtualKey);
                }

                if(Input.GetKeyUp(mappingList[i].keyboardKey))
                {
                    GlobalEventQueue.EnQueueEvent( VK_State.Up, mappingList[ i ].virtualKey );
                }
            }
        }
    }
}
