using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLib
{
    public class TweenManager : MonoSingle<TweenManager>
    {
        List<Tween> tweens = new List<Tween>();
        public static void AddTween(Tween tween)
        {
            Instance.tweens.Add(tween);
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
    }
}
