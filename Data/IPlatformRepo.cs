using RedisPrimaryDBDemo.Models;

namespace RedisPrimaryDBDemo.Data
{
    public interface IPlatformRepo
    {
        void CreatePlatform(Platform platform);
        Platform? GetPlatform(string id);
        IEnumerable<Platform?>? GetAllPlatform();
    }
}
