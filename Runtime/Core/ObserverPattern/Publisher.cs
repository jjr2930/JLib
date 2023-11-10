using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Core.ObserverPattern
{
    public interface INoticeParameter
    {

    }

    public class Publisher : MonoBehaviour
    {
        [SerializeField]
        protected List<Observer> subscribers = new List<Observer>();

        public void RegisterObserver(Observer observer)
        {
            if(subscribers.Contains(observer))
            {
                throw new InvalidOperationException($"{observer} is already exist");
            }

            subscribers.Add(observer);
            OnRegisterObserver();
        }

        public void RemoveObserver(Observer observer) 
        {
            if (false == subscribers.Contains(observer))
            {
                throw new InvalidOperationException($"{observer} is no exist");
            }

            subscribers.Remove(observer);
            OnRemoveObserver();
        }


        public void Notify(INoticeParameter parameter)
        {
            foreach (var subscriber in subscribers)
            {
                subscriber.Notify(parameter);
            }
            OnNotify();
        }

        protected virtual void OnRegisterObserver() { }
        protected virtual void OnRemoveObserver() { }
        protected virtual void OnNotify() { }
    }
}
