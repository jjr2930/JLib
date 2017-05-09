using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JLib
{
    public class GlobalEventParameter
    {
        public long id { get; set; }
        public Enum eventName { get; set; }
        public object value { get; set; }
    }

}
