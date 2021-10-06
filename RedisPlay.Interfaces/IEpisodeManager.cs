using RedisPlay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPlay.Interfaces
{
    public interface IEpisodeManager
    {
        Task Set(string key, Episode episode);
        Task Set(IDictionary<string, IList<string>> creatorEpisodeKeys);
        Task Set(IDictionary<string, IList<Episode>> creatorsToEpisodes);
        Task<Episode> GetById(int id);
        Task<List<string>> GetEpisodesByCreatorId(string creatorId);
    }
}
