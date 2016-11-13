using System;
using UnityEngine;
namespace JLib
{
    /// <summary>
    /// UnityEngine.Time클래스의 캐싱을 위한 클래스
    /// </summary>
    public class JTime : MonoSingle<JTime>
    {
        public static float Time
        {
            get
            {
                return Instance.time;
            }
        }
        public static float DeltaTime
        {
            get
            {
                return Instance.deltaTime;
            }
        }
        public static float TimeScale
        {
            get
            {
                return Instance.timeScale;
            }
            set
            {
                Instance.timeScale = value;
                UnityEngine.Time.timeScale = value;
            }
        }

        float time = 0f;
        float deltaTime = 0f;
        float timeScale = 0f;

        void Start()
        {
            time = UnityEngine.Time.time;
            deltaTime = UnityEngine.Time.deltaTime;
            timeScale = UnityEngine.Time.timeScale;
        }

        void Update()
        {
            time = UnityEngine.Time.time;
            deltaTime = UnityEngine.Time.deltaTime;
            timeScale = UnityEngine.Time.timeScale;
        }
    }
}
