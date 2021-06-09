using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    class ValuesHolder : IValuesHolder
    {
        public List<string> Values { get; set; }
        public ValuesHolder() => Values = new List<string>();
        public List<string> Get() => Values;
        public void Add(string str) { Values.Add(str); }
    }
}
