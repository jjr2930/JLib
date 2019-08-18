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
        Queue<GlobalEventParameter> eventQueue = new Queue<GlobalEventParameter>();
        Dictionary<object, UnityAction<object>> listeners = new Dictionary<object, UnityAction<object>>();


        /// <summary>
        /// 이벤트 넣기
        /// </summary>
        /// <param name="eventName">이벤트 Enum</param>
        /// <param name="param">파라미터</param>
        public static void EnQueueEvent( Enum eventName , object param )
        {
            //Debug.LogFormat( "GlobalEventQueue.EnqueueEvent=> eventName : {0}, param : {1}" , eventName , param );
            var p = JLib.ParameterPool.GetParameter<GlobalEventParameter>();
            p.eventName = eventName;
            p.value = param;
            Instance.eventQueue.Enqueue( p );
        }

        #region use default parameters
        public static void EnQueueEvent( Enum eventName, int param)
        {
            Debug.LogFormat( "GlobalEventQueue.EnqueueEvent=> eventName : {0}, param : {1}" , eventName , param );
            var p = JLib.ParameterPool.GetParameter<IntParameter>();
            EnQueueEvent( eventName , p );
        }

        public static void EnQueueEvent( Enum eventName, bool param)
        {
            Debug.LogFormat( "GlobalEventQueue.EnqueueEvent=> eventName : {0}, param : {1}" , eventName , param );
            var p = JLib.ParameterPool.GetParameter<BoolParameter>();
            EnQueueEvent( eventName , p );
        }

        public static void EnQueueEvent( Enum eventName, float param)
        {
            Debug.LogFormat( "GlobalEventQueue.EnqueueEvent=> eventName : {0}, param : {1}" , eventName , param );
            var p = JLib.ParameterPool.GetParameter<FloatParameter>();
            EnQueueEvent( eventName , p );
        }

        public static void EnQueueEvent( Enum eventName , Vector3 param )
        {
            Debug.LogFormat( "GlobalEventQueue.EnqueueEvent=> eventName : {0}, param : {1}" , eventName , param );
            var p = JLib.ParameterPool.GetParameter<Vector3Parameter>();
            EnQueueEvent( eventName , p );
        }

        public static void EnQueueEvent( Enum eventName, Quaternion param)
        {
            Debug.LogFormat( "GlobalEventQueue.EnqueueEvent=> eventName : {0}, param : {1}" , eventName , param );
            var p = JLib.ParameterPool.GetParameter<QuaternionParameter>();
            EnQueueEvent( eventName , p );
        }
        #endregion






        /// <summary>
        /// 이벤트 넣기
        /// </summary>
        /// <param name="param"></param>
        public static void EnQueueEvent( GlobalEventParameter param )
        {
            Instance.eventQueue.Enqueue( param );
        }


        /// <summary>
        /// 리스너를 등록한다.
        /// </summary>
        /// <param name="eventName">지켜볼 이벤트</param>
        /// <param name="listener">이벤트가 발생하면 할 행위가 정의된 리스너</param>
        public static void RegisterListener( object eventName , UnityAction<object> listener )
        {
            //Debug.LogFormat( "GlobalEventQueue.RegisterListener=>name :{0}, listener :{1}" , eventName , listener );
            if( Instance.listeners.ContainsKey( eventName ) )
            {
                Instance.listeners[ eventName ] += listener;
            }
            else
            {

                Instance.listeners.Add( eventName , listener );
            }
        }

        /// <summary>
        /// 리스너를 제거한다.
        /// </summary>
        /// <param name="eventName">제거할 리스너가 지켜보는 이벤트</param>
        /// <param name="listener">제거할 리스너</param>
        public static void RemoveListener( object eventName , UnityAction<object> listener )
        {
            Debug.LogFormat( "GlobalEventQueue.RemoveListener=>name :{0}, listener :{1}" , eventName , listener );
            UnityAction<object> foundedAction = null;
            if( Instance.listeners.TryGetValue( eventName , out foundedAction ) )
            {
                foundedAction -= listener;
            }
        }

        /// <summary>
        /// 모든 이벤트들을 순서대로 빼내어서 등록된 리스너들을 실행한다.
        /// </summary>
        void Update()
        {
            for( ; eventQueue.Count != 0 ; )
            {
                GlobalEventParameter p = eventQueue.Dequeue();
                UnityAction<object> founded = null;
                if( listeners.TryGetValue( p.eventName , out founded ) )
                {
                    founded.Invoke( p.value );
                    ParameterPool.ReturnPool( p );
                    if(null != p.value )
                    {
                        ParameterPool.ReturnPool( p.value );
                        p.value = null;
                    }
                }
                else
                {
                    //UnityEngine.Debug.LogFormat( "{0}, {1} is not contain at listener" , p.eventName , p.value );
                }
            }
        }
    }
}
