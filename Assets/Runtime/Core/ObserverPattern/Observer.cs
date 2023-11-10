using JLib.Core.ObserverPattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Core
{
    public class Observer : MonoBehaviour
    {
        public virtual void Notify(INoticeParameter paremter)
        {

        }
    }
}
