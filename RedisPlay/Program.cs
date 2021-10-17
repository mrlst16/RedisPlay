using Microsoft.Extensions.Configuration;

namespace RedisPlay
{
    public class Program
    {
        public IConfiguration Configuration
        {
            get => new ConfigurationBuilder()
                     .AddJsonFile("appSettings.json")
                     .Build();
        }

        static void Main(string[] args)
        {


        }
    }
}
