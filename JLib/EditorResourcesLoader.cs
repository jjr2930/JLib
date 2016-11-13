using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace JLib
{
    public class EditorResourcesLoader : BaseResourcesLoader
    {
        public override UnityEngine.Object OnLoad(string path)
        {
            return Resources.Load(path);
        }
    }
}
