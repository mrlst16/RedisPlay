using Microsoft.Extensions.Configuration;
using RedisPlay.Lib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RedisPlay.Tests.Attributes
{
    public class CsvToFileTimerAttribute : TimerAttributeBase
    {

        private readonly string _name;

        private static IDictionary<string, int> _methodCounts
            = new Dictionary<string, int>();
        private static IDictionary<string, int> MethodCounts
        {
            get
            {
                lock (_lock)
                {
                    return _methodCounts;
                }
            }
        }

        private static HashSet<string> _descriptions
            = new HashSet<string>();
        private static HashSet<string> Descriptions
        {
            get
            {
                lock (_lock)
                {
                    return _descriptions;
                }
            }
        }

        private static string _appSettingsFilePath = "C:\\RedisPlay\\appSettings.json";
        private static string _outputFilePath;

        static CsvToFileTimerAttribute()
        {
            var configuration
                     = new ConfigurationBuilder()
                         .AddJsonFile(_appSettingsFilePath)
                         .Build();
            _outputFilePath = configuration.GetValue<string>("AppSettings:TimerOutputFile");
        }

        public CsvToFileTimerAttribute() : base()
        {
        }

        public CsvToFileTimerAttribute(
            string name
            )
        {
            _name = name;
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            base.Before(methodUnderTest);
            MethodCounts.TryAdd(methodUnderTest.Name, 0);

            if (Descriptions.Count < 1)
            {
                var descriptionAttributes = methodUnderTest.DeclaringType.GetCustomAttributes<TheoryNameAttribute>();
                foreach (var descriptionAttribute in descriptionAttributes)
                    Descriptions.Add(descriptionAttribute.Name);
            }
        }

        protected override Stream OutputStream => new FileStream(_outputFilePath, FileMode.Append, FileAccess.Write);

        protected override string Append(MethodInfo methodInfo)
        {
            var descriptionIndex = MethodCounts.TryGetValue(methodInfo.Name, out int res) ? res : 0;
            var description = Descriptions.ElementAtOrDefault(descriptionIndex);
            MethodCounts[methodInfo.Name]++;

            return $"{methodInfo.DeclaringType.Name}, {_name ?? methodInfo.Name}, {description}, {DateTime.UtcNow}, {_stopwatch.ElapsedMilliseconds}";
        }
    }
}
