using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLib
{
    [Serializable]
    public class TablePath
    {
        public string name;
        public string path;
    }

    [Serializable]
    public class TablePathTable
    {
        public List<TablePath> pathTable = new List<TablePath>();
    }
}
