using RedisPlay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPlay.Tests.Data
{
    public static class EpisodeMock
    {
        public static IDictionary<string, List<Episode>> CreatorToEpisodes
        {
            get => new Dictionary<string, List<Episode>>() { };
        }

        public static IDictionary<string, List<string>> CreatorToEpisodeKeys
        {
            get => new Dictionary<string, List<string>>()
            {
                { "creator1", new List<string>(){ "creator1_episode_one", "creator1_episode_two", "creator1_episode_three"} },
                { "creator2", new List<string>(){ "creator2_episode_one", "creator2_episode_two", "creator2_episode_three"} },
                { "creator3", new List<string>(){ "creator3_episode_one", "creator3_episode_two", "creator3_episode_three"} }
            };
        }
    }
}
