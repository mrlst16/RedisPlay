using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisPlay.Tests.Data
{
    public static class ConnectionFactoryMock
    {

        private static ConnectionMultiplexer _connection;
        public static ConnectionMultiplexer Multiplexer
        {
            get => _connection ??= GetConnectionMultiplexer();
        }

        public static ConfigurationOptions FromAppSettings()
        {
            var configuration
                 = new ConfigurationBuilder()
                     .AddJsonFile("D:\\Repos\\RedisPlay\\appSettings.json")
                     .Build();

            return new ConfigurationOptions()
            {
                EndPoints = { { configuration.GetConnectionString("redis_cloud"), configuration.GetValue<int>("AppSettings:RedisPort") } },
                User = configuration.GetValue<string>("Credentials:Redis:User"),
                Password = configuration.GetValue<string>("Credentials:Redis:Password"),
            };
        }

        private static ConnectionMultiplexer GetConnectionMultiplexer()
        {
            var configurationOptions = FromAppSettings();
            var multiplexer = ConnectionMultiplexer.Connect(configurationOptions);
            return multiplexer;
        }
    }
}
