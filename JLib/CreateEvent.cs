using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace JLib
{
    [RequireComponent(typeof(EventTrigger))]
    public abstract class BaseCreateEventForUGUI : JMonoBehaviour
    {
        public List<BaseEventTriggerDataForUGUI> TriggersData
        {
            get
            {
                return triggersData;
            }
            set
            {
                triggersData = value;
            }
        }


        [SerializeField]
        protected EventTrigger trigger = null;
       
        protected List<BaseEventTriggerDataForUGUI> triggersData = new List<BaseEventTriggerDataForUGUI>();
        

        void Awake()
        {
            if(null == trigger)
            {
                trigger = this.GetComponent<EventTrigger>();
            }

            for(int i =0; i< triggersData.Count; i++)
            {
                CreateNewEntry(triggersData[i]);
            }
        }

        public void CreateNewEntry(BaseEventTriggerDataForUGUI triggerData)
        {
            EventTrigger.Entry newEntry = new EventTrigger.Entry();
            newEntry.eventID = triggerData.type;
            newEntry.callback = triggerData.action;

            if(null == trigger.triggers)
            {
                trigger.triggers = new List<EventTrigger.Entry>();
            }


            trigger.triggers.Add(newEntry);
        }
    }
}
