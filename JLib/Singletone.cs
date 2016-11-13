using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JLib
{
    public class Singletone<T> where T : class, new()
    {
        static T _instance = null;
        public static T Instance
        {
            get
            {
                Initialize();
                return _instance;
            }
        }

        public static void Initialize()
        {
            if (null == _instance)
            {
                _instance = new T();
            }
        }
    }
}
