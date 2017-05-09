using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;
namespace JLib
{
    public class DecalEffectExcutor : BaseEffectExcutor
    {
        public override void Play( string effectName )
        {
            //do nothing
            StartCoroutine( ReturnToPool( effectName ) );
        }

        IEnumerator ReturnToPool( string effetName )
        {
            yield return new WaitForSeconds( 10f );
            BasePoolManager.ReturnObject( effetName , this.gameObject );
        }
    }
}
