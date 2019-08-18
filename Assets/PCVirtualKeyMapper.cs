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
        List<PCVirtualKeyMapData> keyMapList
            = new List<PCVirtualKeyMapData>();

        void Update()
        {
            
            for(int i =0; i<keyMapList.Count; i++)
            {
                if(Input.GetKeyDown(keyMapList[i].keyboardKey))
                {
                    VirtualKeyParameter param = ParameterPool.GetParameter<VirtualKeyParameter>();
                    param.key = keyMapList[ i ].virtualKey;
                    GlobalEventQueue.EnQueueEvent( VK_State.Down , param );
                }

                if(Input.GetKey(keyMapList[i].keyboardKey))
                {
                    VirtualKeyParameter param = ParameterPool.GetParameter<VirtualKeyParameter>();
                    param.key = keyMapList[ i ].virtualKey;
                    GlobalEventQueue.EnQueueEvent( VK_State.Press , param );
                }

                if(Input.GetKeyUp(keyMapList[i].keyboardKey))
                {
                    VirtualKeyParameter param = ParameterPool.GetParameter<VirtualKeyParameter>();
                    param.key = keyMapList[ i ].virtualKey;
                    GlobalEventQueue.EnQueueEvent( VK_State.Up, param );

                }
            }
        }
    }
}
