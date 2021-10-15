using RedisPlay.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RedisPlay.Tests
{
    public class CreatorToEpisodeManagerTests
    {
        private readonly CreatorToEpisodeManager _manager;

        public CreatorToEpisodeManagerTests()
        {
            _manager = new CreatorToEpisodeManager();
        }

        [Fact]
        public async Task Set()
        {

        }
    }
}
