using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
namespace JLib
{
    public class TweenManager : MonoSingle<TweenManager>
    {
        Dictionary<string,ITween> tweens = new Dictionary<string, ITween>();

        public static void AddTween<T>( Tween<T> tween )
        {
            Instance.tweens.Add( tween.GetTweenID() , tween );
        }

        void Awake()
        {
            GlobalEventQueue.RegisterListener( DefaultEvent.DoTween , ListenDoTween );
        }
        void Update()
        {
            ItorUpdate();

            //ForLinqUpdate();

            //ForeachUpdate();
        }


        private void ForeachUpdate()
        {
            foreach( var item in tweens )
            {
                if( item.Value.Enabled)
                {
                    item.Value.UpdateTween();
                }

            }
        }
        private void ForLinqUpdate()
        {
            for( int i = 0 ; i < tweens.Count ; i++ )
            {
                var tween = tweens.ElementAt(i);
                if( tween.Value.Enabled )
                {
                    tween.Value.UpdateTween();
                }
            }
        }

        private void ItorUpdate()
        {
            var enumerator = tweens.GetEnumerator();

            while( enumerator.MoveNext() )
            {
                var tween = enumerator.Current;
                if( tween.Value.Enabled)
                {
                    tween.Value.UpdateTween();
                }
            }
        }

        public void ListenDoTween( object parameter )
        {
            string param = parameter as string;
            if( string.IsNullOrEmpty( param ) )
            {
                Debug.LogErrorFormat( "TweenManger.ListenDoTween=> parameter({0}) is not ListenDoTweenParameter" ,
                    parameter.ToString() );
                return;
            }

            ITween foundedTween = null;
            if( !tweens.TryGetValue( param, out foundedTween ) )
            {
                Debug.LogErrorFormat( "TweenManger.ListenDoTween=> id: {0} is not founded" ,
                    param);
                return;
            }

            foundedTween.Enabled = true;
        }
    }
}
