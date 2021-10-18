using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace RedisPlay.Tests.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class TimerAttributeBase : BeforeAfterTestAttribute
    {
        protected Stopwatch _stopwatch;

        protected static object _lock = new object();

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
            Write(methodUnderTest);
        }

        protected void Write(MethodInfo methodInfo)
        {
            lock (_lock)
            {
                using (StreamWriter writer = new StreamWriter(OutputStream))
                {
                    try
                    {
                        var line = Append(methodInfo);
                        writer.WriteLine(line);
                    }
                    catch (Exception e)
                    {
                        //Fail silently for now
                    }
                }
            }
        }

        protected abstract string Append(MethodInfo methodInfo);

        protected abstract Stream OutputStream { get; }
    }
}
