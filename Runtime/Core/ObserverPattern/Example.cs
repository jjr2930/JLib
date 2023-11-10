/*
 * 이것은 옵저버 패턴을 어떤식으로 구현해야 하는지에 대한 예제가 들어있는 파일이다.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Example
{
    public class SomePublisher : MonoBehaviour
    {
        int intNumber;
        string stringValue;

        public int IntNumber
        { 
            get
            {
                return IntNumber;
            }
            set
            {
                intNumber = value;
                OnIntValueChanged?.Invoke(value);
            }
        }
        public string StringValue
        {
            get
            {
                return stringValue;
            }
            set
            {
                stringValue = value;
                OnStringValueChanged?.Invoke(value);
            }
        }

        public event Action<int> OnIntValueChanged;
        public event Action<string> OnStringValueChanged;
    }

    public class ObserverA
    {
        public ObserverA()
        {
            SomePublisher somePublisher = UnityEngine.Object.FindObjectOfType<SomePublisher>();
            if (null == somePublisher)
                throw new NullReferenceException("somep publisher is not exist");

            somePublisher.OnIntValueChanged +=
                (changedValue) =>
                {
                    Debug.Log($"int value changed! : {changedValue}");
                };

            somePublisher.OnStringValueChanged +=
                (changedValue) =>
                {
                    Debug.Log($"string value changed! : {changedValue}");
                };
        }
    }
}