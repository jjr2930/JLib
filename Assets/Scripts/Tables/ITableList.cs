using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JLib
{ 
    public interface ITable<T> where T : class, new()
    {
        List<T> List { get; set; }
    }
}
