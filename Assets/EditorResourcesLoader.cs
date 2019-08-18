using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

namespace JLib
{
    public class EditorResourcesLoader : BaseResourcesLoader
    {
        public static EditorResourcesLoader Instance
        {
            get
            {
                if(null == instance)
                {
                    instance = new EditorResourcesLoader();
                }
                return instance;
            }
        }
        static EditorResourcesLoader instance = null;
        public override UnityEngine.Object OnLoad(string path)
        {
            return Resources.Load(path);
        }
    }
}
