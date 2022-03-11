using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisPrimaryDBDemo.Data;
using RedisPrimaryDBDemo.Models;

namespace RedisPrimaryDBDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo repo;

        public PlatformsController(IPlatformRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet("{id}",Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var platform = repo.GetPlatform(id);

            if (platform is not null)
            {
                return Ok(platform);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform(Platform platform)
        {
            repo.CreatePlatform(platform);
            
            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platform.Id} , platform );
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            return Ok(repo.GetAllPlatform());
        }
    }
}
