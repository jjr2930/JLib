using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLib
{
    public class ParameterPool : MonoSingle<ParameterPool>
    {
        List<object> sleepObject = new List<object>();
        List<object> awakeObject = new List<object>();

        public static T GetParameter<T>() where T: class, new()
        {
            return Instance.OnGetParameter<T>();
        }

        public static void ReturnPool(object obj)
        {
            Instance.OnReturnPool(obj);
        }

        public T OnGetParameter<T>() where T : class, new()
        {
            for( int i = 0 ; i < sleepObject.Count ; i++ )
            {
                T check = sleepObject[i] as T;
                if(null != check)
                {
                    sleepObject.RemoveAt(i);
                    awakeObject.Add(check);
                    return check;
                }
            }

            //이곳에 도달했다면 없는것임
            T newP = new T();
            awakeObject.Add(newP);
            return newP;
        }

        public void OnReturnPool(object obj)
        {
            if(awakeObject.Remove(obj))
            {
                sleepObject.Add(obj);
            }
        }
    }
}
