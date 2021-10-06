using RedisPlay.Tests.Data;
using StackExchange.Redis;
using System;
using Xunit;

namespace RedisPlay.Tests
{
    public class Connecting
    {
        public Connecting()
        {
        }

        [Fact]
        public void Connect()
        {
            var multiplexer = ConnectionFactory.Multiplexer;

            var db = multiplexer.GetDatabase();
            db.StringSet("Foo", "bar");
            var fooValue = db.StringGet("Foo");
            Assert.Equal("bar", fooValue);
        }

    }
}
