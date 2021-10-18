using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPlay.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TheoryNameAttribute : Attribute
    {
        public string Name { get; set; }
        public TheoryNameAttribute(
            string name
            )
        {
            Name = name;
        }
    }
}
