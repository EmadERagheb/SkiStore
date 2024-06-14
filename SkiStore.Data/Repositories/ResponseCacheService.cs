using SkiStore.Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace SkiStore.Data.Repositories
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDatabase _database;

        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task CashResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response is null) { return; }
            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializeReposnse = JsonSerializer.Serialize(response, options);
            await _database.StringSetAsync(cacheKey, serializeReposnse, timeToLive);
        }

        public async Task<string> GetCacheResponseAsync(string cacheKey)
        {
            var chachedResponse = await _database.StringGetAsync(cacheKey);
            if (chachedResponse.IsNullOrEmpty)
            {
                return null;
            }
            else
                return chachedResponse.ToString().Split("{\"value\":")[1].Split(",\"formatters\":")[0];
        }
    }
}
