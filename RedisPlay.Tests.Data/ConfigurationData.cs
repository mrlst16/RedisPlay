using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;

namespace RedisPlay.Tests.Data
{
    public class ConfigurationData
    {
        private readonly IConfiguration _configuration;

        public ConfigurationData()
        {
            _configuration
                = new ConfigurationBuilder()
                    .AddJsonFile("appSettings.json")
                    .Build();
        }

        public ConfigurationOptions FromAppSettings()
        {
            return new ConfigurationOptions()
            {
                EndPoints = { { _configuration.GetConnectionString("redis_cloud"), _configuration.GetValue<int>("AppSettings:RedisPort") } },
                User = _configuration.GetValue<string>("Credentials:Redis:User"),
                Password = _configuration.GetValue<string>("Credentials:Redis:Password"),
            };
        }
    }
}
