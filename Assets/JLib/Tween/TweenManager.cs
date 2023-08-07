using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace JLib.Tween
{
    public class TweenManager : MonoSingle<TweenManager>
    {
        List<ITween> tweens = new List<ITween>();

        public static void AddTween( ITween  newTween )
        {
            Instance.tweens.Add(newTween);
        }

        public static void RemoveTween(ITween oldTween)
        {
            Instance.tweens.Remove(oldTween);
        }

        void Update()
        {
            foreach (var item in tweens)
            {
                if (item.Enabled)
                {
                    item.UpdateTween();
                }
            }
        }
    }
}
