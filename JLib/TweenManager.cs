using System.Collections.Generic;
using UnityEngine;
namespace JLib
{
    public class TweenManager : MonoSingle<TweenManager>
    {
        Dictionary<int,Tween> tweens = new Dictionary<int, Tween>();
        
        public static void AddTween(Tween tween)
        {
            Instance.tweens.Add(tween.gameObject.GetInstanceID(),tween);
        }

        void Awake()
        {
            GlobalEventQueue.RegisterListener(DefaultEvent.DoTween,ListenDoTween);
        }
        void Update()
        {
            for(int i = 0; i<tweens.Count; i++)
            {
                if(tweens[i].enabled)
                {
                    tweens[i].UpdateTween();
                }
            }
        }

        public void ListenDoTween( object parameter )
        {
            ListenDoTweenParameter param = parameter as ListenDoTweenParameter;
            if( null == param )
            {
                Debug.LogErrorFormat( "TweenManger.ListenDoTween=> parameter({0}) is not ListenDoTweenParameter" ,
                    parameter.ToString() );
                return;
            }

            Tween foundedTween = null;
            if( !tweens.TryGetValue( param.instnaceID , out foundedTween ) )
            {
                Debug.LogErrorFormat("TweenManger.ListenDoTween=> id: {0} is not founded",
                    param.instnaceID);
                return;
            }

            foundedTween.enabled = true;
        }
    }
}
