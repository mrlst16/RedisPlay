using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace RedisPlay.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class TimerAttributeBase : BeforeAfterTestAttribute
    {
        protected Stopwatch _stopwatch;

        protected TimerAttributeBase()
        {
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

        public override void After(MethodInfo methodUnderTest)
        {
            _stopwatch.Stop();

        }

        private void Write(MethodInfo methodInfo)
        {

        }

        protected abstract IStream WriteStream { get; }
    }
}
