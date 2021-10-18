using RedisPlay.Lib;
using RedisPlay.Tests.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using RedisPlay.Models;
using RedisPlay.Interfaces;
using RedisPlay.Tests.Attributes;
using System.ComponentModel;

namespace RedisPlay.Tests
{
    [TheoryName("CreatorToKeysEpisodeManager")]
    [TheoryName("CreatorToEpisodeManager")]
    public class EpisodeManagerTests
    {
        public static TheoryData<IEpisodeManager> EpisodeManager
            = new TheoryData<IEpisodeManager>()
            {
                new CreatorToKeysEpisodeManager(),
                new CreatorToEpisodeManager()
            };

        public EpisodeManagerTests()
        {
        }

        [Theory]
        [MemberData(nameof(EpisodeManager), MemberType = typeof(EpisodeManagerTests))]
        [CsvToFileTimer]
        public async Task Set_PerformanceTest(IEpisodeManager manager)
        {
            var dict = EpisodeMock.CreatorToEpisodes;
            var setResult = await manager.Set(dict);
            Assert.True(setResult);
        }

        [Theory]
        [MemberData(nameof(EpisodeManager), MemberType = typeof(EpisodeManagerTests))]
        [CsvToFileTimer]
        public async Task GetById_ResponseProperlyMapped(IEpisodeManager manager)
        {
            var dict = EpisodeMock.CreatorToEpisodes;
            var setResult = await manager.Set(dict);
            Assert.True(setResult);

            var getResult = await manager.GetById("creator1", 2);
            Assert.NotNull(getResult);
        }

        [Theory]
        [MemberData(nameof(EpisodeManager), MemberType = typeof(EpisodeManagerTests))]
        [CsvToFileTimer]
        public async Task GetEpisodesByCreatorId_ResponseProperSize(IEpisodeManager manager)
        {
            var dict = EpisodeMock.CreatorToEpisodes;
            var setResult = await manager.Set(dict);
            Assert.True(setResult);

            var getResult = await manager.GetEpisodesByCreatorId("creator1");
            Assert.True(getResult.Count() == 4);
        }

        [Theory]
        [MemberData(nameof(EpisodeManager), MemberType = typeof(EpisodeManagerTests))]
        [CsvToFileTimer]
        public async Task GetEpisodesId_ResponseProperlyMapped(IEpisodeManager manager)
        {
            var dict = EpisodeMock.CreatorToEpisodes;
            var setResult = await manager.Set(dict);
            Assert.True(setResult);

            var getResult = await manager.GetById("creator1", 1);
            Assert.Equal(1, getResult.Id);
            Assert.Equal("creator1", getResult.CreatorId);
            Assert.Equal("filepath", getResult.FilePath);
            Assert.Equal("Casablanca", getResult.Name);
            Assert.Equal<TimeSpan>(TimeSpan.FromHours(4.99), getResult.RunningTime);
            Assert.Equal(DateTime.Parse("6/25/21"), getResult.AiredDate);
        }

        [Theory]
        [MemberData(nameof(EpisodeManager), MemberType = typeof(EpisodeManagerTests))]
        [CsvToFileTimer]
        public async Task Update_ResponseProperlyMapped(IEpisodeManager manager)
        {
            var dict = EpisodeMock.CreatorToEpisodes;
            var setResult = await manager.Set(dict);
            Assert.True(setResult);

            var updateRequest = new Episode()
            {
                AiredDate = DateTime.Parse("6/25/21"),
                CreatorId = "creator1",
                FilePath = "filepath",
                Id = 1,
                Name = "Casablanca: The origin story",
                RunningTime = TimeSpan.FromHours(4.99)
            };

            var updateResult = await manager.Update(updateRequest);

            var getResult = await manager.GetById("creator1", 1);
            Assert.Equal(1, getResult.Id);
            Assert.Equal("creator1", getResult.CreatorId);
            Assert.Equal("filepath", getResult.FilePath);
            Assert.Equal("Casablanca: The origin story", getResult.Name);
            Assert.Equal<TimeSpan>(TimeSpan.FromHours(4.99), getResult.RunningTime);
            Assert.Equal(DateTime.Parse("6/25/21"), getResult.AiredDate);
        }
    }
}
