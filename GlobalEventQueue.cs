using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
//using UnityEngine;
namespace JLib
{
    /// <summary>
    /// 글로벌로 이벤트 큐이다. 들어오는 이벤트를 큐에다 넣고 매 업데이트마다 그것을 순서대로 빼내어서 리스너(메소드)들을 실행한다.
    /// </summary>
    public class GlobalEventQueue : MonoSingle<GlobalEventQueue>
    {
        /// <summary>
        /// 글로벌 브로드캐스팅을 할 때 사용할 리스너 
        /// </summary>
        public const long GLOBAL_ID = long.MinValue;
        Queue<GlobalEventParameter> eventQueue = new Queue<GlobalEventParameter>();

        /// <summary>
        /// key : id
        /// value : (key : event name, value: listener)
        /// </summary>
        Dictionary<long, Dictionary<Enum, UnityAction<object>>> listeners
            = new Dictionary<long, Dictionary<Enum, UnityAction<object>>>();

        /// <summary>
        /// 이벤트 넣기
        /// </summary>
        /// <param name="id">해당 이벤트를 사용하는 리스너의 아이디 기본값은 globalID</param>
        /// <param name="eventName">이벤트 Enum</param>
        /// <param name="param">파라미터</param>
        public static void EnQueueEvent( Enum eventName, object param, long id = GLOBAL_ID )
        {
            Debug.LogFormat( "GlobalEventQueue.EnqueueEvent=> eventName : {0}, param : {1}" , eventName , param );
            var p = JLib.ParameterPool.GetParameter<GlobalEventParameter>();
            p.id = id;
            p.eventName = eventName;
            p.value = param;
            Instance.eventQueue.Enqueue( p );
        }

        /// <summary>
        /// 리스너를 등록한다.
        /// </summary>
        /// <param name="eventName">지켜볼 이벤트</param>
        /// <param name="listener">이벤트가 발생하면 할 행위가 정의된 리스너</param>
        /// <param name="id">이벤트를 사용할 아이디</param>
        public static void RegisterListener( Enum eventName , UnityAction<object> listener, long id = GLOBAL_ID )
        {
            Debug.LogFormat( "GlobalEventQueue.RegisterListener=>name :{0}, listener :{1}" , eventName , listener );
            if(!Instance.listeners.ContainsKey(id))
            {
                Instance.listeners.Add( id, new Dictionary<Enum, UnityAction<object>>() );
            }

            if(!Instance.listeners[id].ContainsKey(eventName))
            {
                Instance.listeners[ id ].Add( eventName, listener );
            }
            else
            {
                Instance.listeners[ id ][ eventName ] += listener;
            }
        }

        /// <summary>
        /// 리스너를 제거한다.
        /// </summary>
        /// <param name="eventName">제거할 리스너가 지켜보는 이벤트</param>
        /// <param name="listener">제거할 리스너</param>
        /// <param name="id">제거할 리스너가 사용하는 이이디</param>
        public static void RemoveListener( Enum eventName, UnityAction<object> listener, long id = GLOBAL_ID )
        {
            if( Instance.listeners.ContainsKey( id ) )
            {
                if(Instance.listeners[id].ContainsKey(eventName))
                {
                    if( null != Instance.listeners[ id ][ eventName ] )
                    {
                        Instance.listeners[ id ][ eventName ] -= listener;
                        Debug.LogFormat( "GlobalEventQueue.RemoveListener=>id: {0} name :{1}, listener :{2}", id, eventName, listener );
                        return;
                    }
                }
            }
            Debug.LogErrorFormat( "GlobalEventQueue.RemoveListener => not founded listener id:{0} name : {1}, listener:{2}", id, eventName, listener );
        }

        /// <summary>
        /// 모든 이벤트들을 순서대로 빼내어서 등록된 리스너들을 실행한다.
        /// </summary>
        void Update()
        {
            RunListener();
        }

        /// <summary>
        /// run all listeners
        /// </summary>
        private void RunListener()
        {
            for( ; eventQueue.Count != 0; )
            {
                GlobalEventParameter p = eventQueue.Dequeue();
                if(listeners.ContainsKey(p.id))
                {
                    if(listeners[p.id].ContainsKey(p.eventName))
                    {
                        if( null == listeners[ p.id ][ p.eventName ] )
                        {
                            listeners[ p.id ][ p.eventName ]( p.value );
                        }
                    }
                }
            }
        }
    }
}
