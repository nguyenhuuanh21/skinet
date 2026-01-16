using skinet.Entities;
using skinet.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace skinet.Services
{
    public class CartService(IConnectionMultiplexer redis) : ICartService
    {
        private readonly IDatabase _database = redis.GetDatabase();
        public async Task<bool?> DeleteCart(string key)
        {
            

                return await _database.KeyDeleteAsync(key);

        }

        public async Task<Cart?> GetCartAsync(string key)
        {

                var data = await _database.StringGetAsync(key);
                return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Cart>(data!);
            

        }
        public async Task<Cart?> SetCartAsync(Cart cart)
        {

            var updated = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if (!updated)
            {
                return null;
            }
            return await GetCartAsync(cart.Id);
        }
    }
}
