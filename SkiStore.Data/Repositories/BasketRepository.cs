using SkiStore.Domain.Contracts;
using SkiStore.Domain.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace SkiStore.Data.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _db;
        public BasketRepository(IConnectionMultiplexer connection)
        {
            _db = connection.GetDatabase();
        }
        public async Task<CustomerBasket> GetBasketAsync(string id)
        {

            var data = await _db.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _db.KeyDeleteAsync(id);
        }


        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket customerBasket)
        {
        
            bool created = await _db.StringSetAsync(customerBasket.Id
                , JsonSerializer.Serialize(customerBasket)
                , TimeSpan.FromDays(30));
            return created ? await GetBasketAsync(customerBasket.Id) : null;
        }
    }
}
