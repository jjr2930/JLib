using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using UnityEngine;
namespace JLib
{
    /// <summary>
    /// 글로벌로 이벤트 큐이다. 들어오는 이벤트를 큐에다 넣고 매 업데이트마다 그것을 순서대로 빼내어서 리스너(메소드)들을 실행한다.
    /// </summary>
    public class GlobalEventQueue : MonoSingle<GlobalEventQueue>
    {
        Queue<GlobalEventParameter> eventQueue = new Queue<GlobalEventParameter>();
        Dictionary<object, Action<object>> listeners = new Dictionary<object, Action<object>>();
        

        /// <summary>
        /// 이벤트 넣기
        /// </summary>
        /// <param name="param"></param>
        static void QueueEvent(GlobalEventParameter param)
        {
            Instance.eventQueue.Enqueue(param);
        }

        /// <summary>
        /// 리스너를 등록한다.
        /// </summary>
        /// <param name="eventName">지켜볼 이벤트</param>
        /// <param name="listener">이벤트가 발생하면 할 행위가 정의된 리스너</param>
        static void RegisterListener(object eventName, Action<object> listener)
        {
            if (Instance.listeners.ContainsKey(eventName))
            {
                Instance.listeners[eventName] += listener;
            }
            else
            {
                Instance.listeners.Add(eventName, listener);
            }
        }

        /// <summary>
        /// 리스너를 제거한다.
        /// </summary>
        /// <param name="eventName">제거할 리스너가 지켜보는 이벤트</param>
        /// <param name="listener">제거할 리스너</param>
        static void RemoveListener(object eventName, Action<object> listener)
        {
            Action<object> foundedListener= null;
            if(Instance.listeners.TryGetValue(eventName,out foundedListener))
            {
                foundedListener -= listener;
            }
        }

        /// <summary>
        /// 모든 이벤트들을 순서대로 빼내어서 등록된 리스너들을 실행한다.
        /// </summary>
        void Update()
        {
            for (; eventQueue.Count != 0;)
            {
                GlobalEventParameter e = eventQueue.Dequeue();
                Action<object> founded = null;
                if (listeners.TryGetValue(e.eventName, out founded))
                {
                    founded(e.value);
                }
            }
        }
    }
}
