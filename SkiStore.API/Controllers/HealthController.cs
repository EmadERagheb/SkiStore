using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using StackExchange.Redis;

namespace SkiStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IDatabase _db;
        public HealthController(IConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase();
        }
        [HttpGet]
        [Route("redis")]
        public async Task<IActionResult> CheckRedis()
        {
            try
            {
                // Set a test value in Redis
                await _db.StringSetAsync("health_check", "ok");

                // Get the test value from Redis

                var value = await _db.StringGetAsync("health_check");

                if (value == "ok")
                {
                    return Ok("Redis is working correctly.");
                }

                return StatusCode(500, "Redis is not returning the expected value.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Redis connection failed: {ex.Message}");
            }
        }

    }
}
