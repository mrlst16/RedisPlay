using Microsoft.Extensions.Configuration;
using RedisPlay.Interfaces;
using RedisPlay.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisPlay.Lib
{
    public abstract class EpisodeManagerBase : IEpisodeManager
    {
        private static ConnectionMultiplexer _connectionMultiplexer;


        protected async Task<T> UseRedisDatabaseAsync<T>(Func<IDatabase, Task<T>> func)
        {
            var multiplexer = await EpisodeManagerBase.GetConnection();
            var database = multiplexer.GetDatabase();
            var result = await func(database);
            await multiplexer.CloseAsync();
            return result;
        }

        protected async Task<bool> TryUseRedisDatabaseAsync(Func<IDatabase, Task<bool>> func)
        {
            try
            {
                return await UseRedisDatabaseAsync(func);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        protected string CreatorKey(string creatorId)
            => $"{creatorId}_keystore";

        public static async Task<ConnectionMultiplexer> GetConnection()
        {
            if (_connectionMultiplexer == null)
            {
                var configuration
                     = new ConfigurationBuilder()
                         .AddJsonFile("appSettings.json")
                         .Build();

                var configurationOptions = new ConfigurationOptions()
                {
                    EndPoints = { { configuration.GetConnectionString("redis_cloud"), configuration.GetValue<int>("AppSettings:RedisPort") } },
                    User = configuration.GetValue<string>("Credentials:Redis:User"),
                    Password = configuration.GetValue<string>("Credentials:Redis:Password"),
                };
                _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(configurationOptions);
            }
            return _connectionMultiplexer;
        }

        public abstract Task<Episode> GetById(string creatorId, int episodeId);

        public abstract Task<IEnumerable<Episode>> GetEpisodesByCreatorId(string creatorId);

        public abstract Task<bool> Set(IDictionary<string, IList<Episode>> creatorsToEpisodes);

        public abstract Task<bool> Update(Episode episode);
    }
}
