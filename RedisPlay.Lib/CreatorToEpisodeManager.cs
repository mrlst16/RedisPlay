using Newtonsoft.Json;
using RedisPlay.Models;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisPlay.Lib
{
    public class CreatorToEpisodeManager : EpisodeManagerBase
    {

        public override async Task<Episode> GetById(string creatorId, int episodeId)
          => await UseRedisDatabaseAsync<Episode>(async (d) =>
          {
              Episode result = new();
              var creatorKey = CreatorKey(creatorId);
              var entry = await d.HashGetAsync(creatorKey, episodeId);
              result = JsonConvert.DeserializeObject<Episode>(entry.ToString());
              return result;
          });

        public override async Task<IEnumerable<Episode>> GetEpisodesByCreatorId(string creatorId)
            => await UseRedisDatabaseAsync<List<Episode>>(async (d) =>
            {
                List<Episode> result = new();
                var creatorKey = CreatorKey(creatorId);
                var hashes = await d.HashGetAllAsync(creatorKey);

                foreach (var hash in hashes)
                {
                    var item = JsonConvert.DeserializeObject<Episode>(hash.Value);
                    result.Add(item);
                }
                return result;
            });

        public override async Task<bool> Set(IDictionary<string, IList<Episode>> creatorsToEpisodes)
            => await TryUseRedisDatabaseAsync(async (d) =>
            {
                foreach (var kvp in creatorsToEpisodes)
                {
                    HashEntry[] hashEntries = new HashEntry[kvp.Value.Count];
                    var keystoreKey = CreatorKey(kvp.Key);
                    for (int i = 0; i < kvp.Value.Count; i++)
                    {
                        var episode = kvp.Value[i];
                        var json = JsonConvert.SerializeObject(episode);
                        HashEntry entry = new HashEntry(episode.Id, json); ;
                        hashEntries[i] = entry;
                    }
                    await d.HashSetAsync(keystoreKey, hashEntries);
                }
                return true;
            });

        public override async Task<bool> Update(Episode episode)
            => await TryUseRedisDatabaseAsync(async (d) =>
            {
                var creatorKey = CreatorKey(episode.CreatorId);
                var json = JsonConvert.SerializeObject(episode);
                await d.HashSetAsync(creatorKey, episode.Id.ToString(), json);
                return true;
            });
    }
}
