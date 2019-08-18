using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib.Sound
{
    /// <summary>
    /// 사운드 소스를 컨트롤하기 위한 리스너.
    /// 사용시에 이것을 상속받아서 사용하자.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public abstract class BaseAudioSourceListener : MonoBehaviour
    {
        [SerializeField]
        protected List<AudioClip> audioClips = new List<AudioClip>();

        List<AudioSource> audioSources = null;

        int currentIndex = 0;
        public void Awake()
        {
            audioSources = new List<AudioSource>( GetComponents<AudioSource>() );
            RegisterListener();
        }

        /// <summary>
        /// 등록된 클립들 중에 해당인덱스의 클립을 실행한다.
        /// </summary>
        /// <param name="index">원하는 클립의 인덱스</param>
        protected void PlayAudio( int index )
        {
            AudioClip foundedClip = audioClips[index];
            PlayAudio( foundedClip );
        }

        /// <summary>
        /// 파라미터에 있는 오디오클립을 실행하자.
        /// </summary>
        /// <param name="clip">실행하고 싶은 소리</param>
        protected void PlayAudio(AudioClip clip)
        {
            
            audioSources[currentIndex].clip = clip;
            audioSources[currentIndex].Play();
            currentIndex = ( currentIndex + 1 ) % audioSources.Count;
        }

        /// <summary>
        /// 리스너들을 등록한다.
        /// </summary>
        protected abstract void RegisterListener();


    }
}
