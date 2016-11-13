using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace JLib
{
    public class BaseEventTriggerDataForUGUI
    {
        /// <summary>
        /// 어떤 트리거 타입인가.
        /// </summary>
        public EventTriggerType type;

        /// <summary>
        /// 트리거가 발동되면 할 행동은?
        /// </summary>
        public EventTrigger.TriggerEvent action;
    }
}
