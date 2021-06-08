using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public interface IValuesHolder
    {
        List<string> Values { get; set; }
        public void Add(string str);
        public List<string> Get() => Values;
    }
}
