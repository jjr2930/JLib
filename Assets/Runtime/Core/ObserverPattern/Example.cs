/*
 * 이것은 옵저버 패턴을 어떤식으로 구현해야 하는지에 대한 예제가 들어있는 파일이다.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Core.ObserverPattern
{
    public class SomeMemberChangedParemter : INoticeParameter
    {
        public int value;
    }

    public class CustomObserver : Observer
    {
        public override void Notify(INoticeParameter paremter)
        {
            base.Notify(paremter);
            Debug.Log("Custom observer notify called");
        }
    }

    public class CustomPublisher : Publisher
    {
        int someMember;
        public int SomeMember
        {
            get { return someMember; }
            set 
            {
                someMember = value;
                Notify(new SomeMemberChangedParemter()
                {
                    value = someMember
                });
            }
        }
    }

    public class Client 
    {
        public void SampleMethod(int someNumber)
        {
            var customPublisher = UnityEngine.Object.FindObjectOfType<CustomPublisher>();
            if(null == customPublisher)
            {
                throw new NullReferenceException("there is no custom publisher");
            }

            customPublisher.SomeMember = someNumber;
        }
    }
}
