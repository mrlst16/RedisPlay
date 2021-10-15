using RedisPlay.Tests.Data;
using Xunit;

namespace RedisPlay.Tests
{
    public class ConnectionFactoryTests
    {
        public ConnectionFactoryTests()
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
