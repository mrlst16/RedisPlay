using RedisPlay.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisPlay.Lib
{
    public class CreatorToKeysEpisodeManager : EpisodeManagerBase
    {
        private HashEntry[] ConvertToHashEntries(Episode episode)
            => new HashEntry[] {
                    new HashEntry(nameof(episode.AiredDate), episode.AiredDate.Ticks),
                    new HashEntry(nameof(episode.FilePath), episode.FilePath),
                    new HashEntry(nameof(episode.Id), episode.Id),
                    new HashEntry(nameof(episode.Name), episode.Name),
                    new HashEntry(nameof(episode.CreatorId), episode.CreatorId),
                    new HashEntry(nameof(episode.RunningTime), episode.RunningTime.Ticks)
                };

        private Episode ConvertToEpisode(HashEntry[] hashEntries)
        {
            Episode result = new();

            foreach (var entry in hashEntries)
            {
                switch (entry.Name)
                {
                    case nameof(Episode.AiredDate):
                        var ticks = ((long)entry.Value);
                        result.AiredDate = new DateTime(ticks);
                        break;
                    case nameof(Episode.CreatorId):
                        result.CreatorId = entry.Value;
                        break;
                    case nameof(Episode.FilePath):
                        result.FilePath = entry.Value;
                        break;
                    case nameof(Episode.Id):
                        result.Id = int.TryParse(entry.Value, out int res) ? res : -1;
                        break;
                    case nameof(Episode.Name):
                        result.Name = entry.Value;
                        break;
                    case nameof(Episode.RunningTime):
                        var ticks2 = ((long)entry.Value);
                        result.RunningTime = new TimeSpan(ticks2);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private string EpisodeKey(string creatorId, int episodeId)
            => $"{creatorId}_{episodeId}";

        public override async Task<bool> Update(Episode episode)
            => await TryUseRedisDatabaseAsync(async (d) =>
            {
                var episodeKey = EpisodeKey(episode.CreatorId, episode.Id);
                var hash = ConvertToHashEntries(episode);
                await d.KeyDeleteAsync(episodeKey);
                await d.HashSetAsync(episodeKey, hash);
                return true;
            });

        public override async Task<bool> Set(IDictionary<string, IList<Episode>> creatorsToEpisodes)
            => await TryUseRedisDatabaseAsync(async (d) =>
            {
                List<HashEntry> episodeIdToCreatorKeyStore = new List<HashEntry>();

                foreach (var kvp in creatorsToEpisodes)
                {
                    List<HashEntry> keyEntries = new List<HashEntry>();
                    var keystoreKey = CreatorKey(kvp.Key);
                    foreach (var episode in kvp.Value)
                    {
                        var episodeKey = EpisodeKey(kvp.Key, episode.Id);
                        var episodeHash = ConvertToHashEntries(episode);
                        await d.HashSetAsync(episodeKey, episodeHash);
                        keyEntries.Add(new HashEntry(episode.Id.ToString(), episodeKey));
                        episodeIdToCreatorKeyStore.Add(new HashEntry(episode.Id, keystoreKey));
                    }
                    await d.HashSetAsync(keystoreKey, keyEntries.ToArray());
                }
                await d.HashSetAsync("episodesToKeystore", episodeIdToCreatorKeyStore.ToArray());
                return true;
            });

        public override async Task<Episode> GetById(string creatorId, int episodeId)
            => await UseRedisDatabaseAsync<Episode>(async (d) =>
                {
                    var keystoreKey = CreatorKey(creatorId);
                    var keyStoreHash = d.HashGetAll(keystoreKey);

                    for (int i = 0; i < keyStoreHash.Length; i++)
                    {
                        var hash = keyStoreHash[i];
                        if (string.Equals(hash.Name, episodeId.ToString(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            var episodeHash = d.HashGetAll(hash.Value.ToString());
                            return ConvertToEpisode(episodeHash);
                        }
                    }
                    return null;
                });

        public override async Task<IEnumerable<Episode>> GetEpisodesByCreatorId(string creatorId)
            => await UseRedisDatabaseAsync<List<Episode>>(async (d) =>
            {
                List<Episode> result = new();
                var creatorKey = CreatorKey(creatorId);
                var keys = d.HashGetAll(creatorKey);

                foreach (var k in keys)
                {
                    var hash = await d.HashGetAllAsync(k.Value.ToString());
                    var episode = ConvertToEpisode(hash);
                    result.Add(episode);
                }

                return result;
            });

    }
}