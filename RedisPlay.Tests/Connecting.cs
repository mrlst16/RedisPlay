using RedisPlay.Tests.Data;
using StackExchange.Redis;
using System;
using Xunit;

namespace RedisPlay.Tests
{
    public class Connecting
    {
        private readonly ConfigurationData _configurationData;

        public Connecting()
        {
            _configurationData = new ConfigurationData();
        }

        [Fact]
        public void Connect()
        {
            var configurationOptions = _configurationData.FromAppSettings();

            var multiplexer = ConnectionMultiplexer.Connect(configurationOptions);
            var db = multiplexer.GetDatabase();
            db.StringSet("Foo", "bar");
            var fooValue = db.StringGet("Foo");
            Assert.Equal("bar", fooValue);
        }
    }
}
