using RedisPlay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RedisPlay.Interfaces
{
    public interface IEpisodeManager
    {
        Task<bool> Update(Episode episode);
        Task<bool> Set(IDictionary<string, IList<Episode>> creatorsToEpisodes);
        Task<Episode> GetById(string creatorId, int episodeId);
        Task<IEnumerable<Episode>> GetEpisodesByCreatorId(string creatorId);
    }
}
