using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public abstract class Tween : JMonoBehaviour
    {
        [SerializeField]
        protected AnimationCurve curve;

        [SerializeField]
        protected float duration = 0f;

        [SerializeField]
        protected float delay = 0f;
        
        protected float startTime = 0f;
        protected float normalTime = 0f;
        protected float duringTime = 0f;
        protected float curveValue = 0f;
        void Awake()
        {
            TweenManager.AddTween(this);
            OnAwake();
        } 

        void OnEnable()
        {
            startTime = JTime.Time;
            duration = Mathf.Max(float.MinValue, duration);
            normalTime = 0f;
        }
        public void UpdateTween()
        {
            duringTime += JTime.DeltaTime;
            normalTime = duringTime / duration;
            curveValue = curve.Evaluate(normalTime);            
            OnTweenUpdate();

            if (duringTime >= duration)
            {
                this.enabled = false;
            }
        }

        protected abstract void OnTweenUpdate();
        protected abstract void OnOnEnable();
        protected virtual void OnAwake() { }
    }
}
