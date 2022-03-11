using RedisPrimaryDBDemo.Data;
using RedisPrimaryDBDemo.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace RedisPrimaryDBDemo.Data
{
    public class RedisPlatformRepo : IPlatformRepo
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase dbInstance;

        public RedisPlatformRepo(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
            dbInstance = _connectionMultiplexer.GetDatabase();
        }

        public void CreatePlatform(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException();
            }

            var serializedData = JsonSerializer.Serialize(platform);

            dbInstance.StringSet(platform.Id, serializedData);  
            dbInstance.SetAdd("PlatformSet", serializedData);  
        }

        public IEnumerable<Platform?>? GetAllPlatform()
        {
            var resultPlatform = dbInstance.SetMembers("PlatformSet");

            if(resultPlatform.Length > 0)
                return Array.ConvertAll(resultPlatform, value => JsonSerializer.Deserialize<Platform>(value)).ToList();

            return null;
        }

        public Platform? GetPlatform(string id)
        {
            var platform =  dbInstance.StringGet(id);

            if (!string.IsNullOrEmpty(platform))
            {
                return JsonSerializer.Deserialize<Platform>(platform);
            }

            return null;
        }
    }
}
