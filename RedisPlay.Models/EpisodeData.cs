using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisPlay.Models
{
    public class EpisodeData
    {
        public string Name { get; set; }
        public string OwnerName { get; set; }
        public DateTime AiredDate { get; set; }
        public TimeSpan RunningTime { get; set; }
    }
}
