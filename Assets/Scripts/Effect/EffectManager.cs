using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace JLib
{
    public class EffectManager : MonoSingle<EffectManager>
    {
        void Awake()
        {
            GlobalEventQueue.RegisterListener( DefaultEvent.ShowEffect , ListenShowEffect );
        }

        public void ListenShowEffect( object parameter )
        {
            EffectParameter p = parameter as  EffectParameter;
            if( null == p )
            {
                Debug.LogErrorFormat( "EffectManager.ListenShowEffect=> {0} is not EffectParamter" ,
                    parameter.ToString() );
            }

            //Load effect from pool
            BaseEffectExcutor effect = BasePoolManager.GetObject<BaseEffectExcutor>(p.effectName);
            effect.transform.parent = p.parent;
            effect.transform.position = p.position;
            effect.transform.rotation = p.rotation;
            //effect.transform.localScale = effect.transform.worldToLocalMatrix * Vector3.one;
            effect.transform.localScale = p.scale;
            effect.Play(p.effectName);
        }
    }
}
