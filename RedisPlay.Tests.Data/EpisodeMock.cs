using RedisPlay.Models;
using System;
using System.Collections.Generic;

namespace RedisPlay.Tests.Data
{
    public static class EpisodeMock
    {
        public static IDictionary<string, IList<Episode>> CreatorToEpisodes
        {
            get => new Dictionary<string, IList<Episode>>()
            {
                { "creator1" , new List<Episode>(){
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/21"),
                        CreatorId = "creator1",
                        FilePath = "filepath",
                        Id = 1,
                        Name = "Casablanca",
                        RunningTime = TimeSpan.FromHours(4.99)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/22"),
                        CreatorId = "creator1",
                        FilePath = "filepath",
                        Id = 2,
                        Name = "Casablanca 2",
                        RunningTime = TimeSpan.FromHours(4.23)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/23"),
                        CreatorId = "creator1",
                        FilePath = "filepath",
                        Id = 3,
                        Name = "Casablanca 3",
                        RunningTime = TimeSpan.FromHours(3.85)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/24"),
                        CreatorId = "creator1",
                        FilePath = "filepath",
                        Id = 4,
                        Name = "Casablanca 4 Directors Cut",
                        RunningTime = TimeSpan.FromHours(8.12)
                    }
                }},
                { "creator2" , new List<Episode>(){
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/21"),
                        CreatorId = "creator2",
                        FilePath = "filepath",
                        Id = 5,
                        Name = "Homeward Bound",
                        RunningTime = TimeSpan.FromHours(4.99)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/22"),
                        CreatorId = "creator2",
                        FilePath = "filepath",
                        Id = 6,
                        Name = "Homeward Bound 2",
                        RunningTime = TimeSpan.FromHours(4.23)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/23"),
                        CreatorId = "creator2",
                        FilePath = "filepath",
                        Id = 7,
                        Name = "Homeward Bound 3",
                        RunningTime = TimeSpan.FromHours(3.85)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/24"),
                        CreatorId = "creator2",
                        FilePath = "filepath",
                        Id = 8,
                        Name = "Homeward Bound 4 Directors Cut",
                        RunningTime = TimeSpan.FromHours(8.12)
                    }
                }},{ "creator3" , new List<Episode>(){
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/21"),
                        CreatorId = "creator3",
                        FilePath = "filepath",
                        Id = 9,
                        Name = "Time and again",
                        RunningTime = TimeSpan.FromHours(4.99)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/22"),
                        CreatorId = "creator3",
                        FilePath = "filepath",
                        Id = 10,
                        Name = "Time and again 2",
                        RunningTime = TimeSpan.FromHours(4.23)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/23"),
                        CreatorId = "creator3",
                        FilePath = "filepath",
                        Id = 11,
                        Name = "Time and again 3",
                        RunningTime = TimeSpan.FromHours(3.85)
                    },
                    new Episode() {
                        AiredDate = DateTime.Parse("6/25/24"),
                        CreatorId = "creator3",
                        FilePath = "filepath",
                        Id = 12,
                        Name = "Time and again 4 Directors Cut",
                        RunningTime = TimeSpan.FromHours(8.12)
                    }
                }},
            };
        }

    }
}
