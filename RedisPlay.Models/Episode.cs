using System;

namespace RedisPlay.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public DateTime AiredDate { get; set; }
        public TimeSpan RunningTime { get; set; }
        public string FilePath { get; set; }
    }
}
