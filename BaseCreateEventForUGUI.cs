using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace JLib
{
    public abstract class BaseCreateEventForUGUI : JMonoBehaviour
    {

        public Enum id;
        public EventTriggerType type;


        public void CreateUIEvent()
        {
            GlobalEventQueue.EnQueueEvent(id, type);
        }
    }
}
