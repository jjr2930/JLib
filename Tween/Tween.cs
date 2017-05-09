using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
namespace JLib
{
    public interface ITween
    {
        bool Enabled { get; set; }
        string GetTweenID();
        void UpdateTween();
    }

    public abstract class Tween<T> : JMonoBehaviour, ITween
    {
        [SerializeField]
        protected string tweenID;
        /// <summary>
        /// 트윈의 커브
        /// </summary>
        [SerializeField]
        protected AnimationCurve curve;

        /// <summary>
        /// 반복인지 핑퐁인지 한번만인지
        /// </summary>
        [SerializeField]
        protected TweenMode mode;

        /// <summary>
        /// 몇초간 진행할지
        /// </summary>
        [SerializeField]
        protected float duration = 0f;

        /// <summary>
        /// 몇 초의 딜레이후 작동할지
        /// </summary>
        [SerializeField]
        protected float delay = 0f;

        /// <summary>
        /// 트윈이 행해진후 실행할 함수
        /// </summary>
        [SerializeField]
        protected UnityEvent callback = null;

        /// <summary>
        /// 켜지면 바로 시작할지 첫 플래그용임
        /// </summary>
        [SerializeField]
        protected bool PlayWhenAwake = false;

        protected float startTime = 0f;
        protected float normalTime = 0f;
        protected float duringTime = 0f;
        protected float curveValue = 0f;
        protected bool isEnabledBefore= false;


        [SerializeField]
        protected T from;

        [SerializeField]
        protected T to;

        /// <summary>
        /// 이렇게 두는 이유는 Pingpong함수를 실행할 때 유저에게 보여주는 것은
        /// from, to로 보여주며, 실제로 돌아가는것은 realfrom,realto이고,
        /// 이 둘을 바꿔치기 하면 Pingpong과 Lerp의 논리구조를 단순화 시킬수 있기 때문이다.
        /// </summary>
        protected T realFrom;
        protected T realTo;

        public string GetTweenID()
        {
            return tweenID;
        }

        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                enabled = value;
            }
        }

        public void Set( T _from , T _to )
        {
            from = _from;
            to = _to;
        }

        void Awake()
        {
            if( string.IsNullOrEmpty( tweenID ) )
            {
                string[] splits = typeof(T).ToString().Split('.');
                string typeName = splits[splits.Length - 1];
                tweenID = string.Format( "{0}.{1}" , this.gameObject.name , typeName );
            }
            TweenManager.AddTween( this );
            this.enabled = PlayWhenAwake;
            
            OnAwake();
        }

        void OnEnable()
        {
            startTime = JTime.Time;
            duration = Mathf.Max( float.MinValue , duration );
            normalTime = 0f;
            realFrom = from;
            realTo = to;
            OnOnEnable();
            isEnabledBefore = true;

        }
        public void UpdateTween()
        {
            duringTime += JTime.DeltaTime;
            normalTime = duringTime / duration;
            curveValue = curve.Evaluate( normalTime );

            OnTweenUpdate();

            if( duringTime >= duration )
            {
                this.enabled = false;
                callback.Invoke();
            }

        }


        private void OnDisable()
        {
            if( !isEnabledBefore )
            {
                return;
            }
            switch( mode )
            {
                case TweenMode.Normal:
                    duringTime = 0;
                    break;

                case TweenMode.Loop:
                    LoopMethod();
                    enabled = true;
                    break;

                case TweenMode.Pingpong:
                    PingpongMethod();
                    break;
            }
        }

        public void Play()
        {
            if( !enabled )
            {
                enabled = true;
            }
            else
            {
                OnEnable();
            }
        }

        public void PlayFromBegin()
        {
            if( !enabled )
            {
                enabled = true;
            }
            else
            {
                OnEnable();
                duringTime = 0f;
            }
            
        }

        public void LoopMethod()
        {
            duringTime = 0f;
            startTime = Time.time;
        }

        public void PingpongMethod()
        {
            duringTime = 0f;
            T temp = realTo;
            realTo = realFrom;
            realFrom = temp;
        }

        abstract public T Lerp();
        protected abstract void OnOnEnable();
        protected abstract void OnTweenUpdate();
        protected virtual void OnAwake() { }
        protected virtual void OnOnDisable() { }
    }
}
