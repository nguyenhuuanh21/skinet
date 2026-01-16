using skinet.Entities;

namespace skinet.Interfaces
{
    public interface ICartService
    {
        public Task<Cart?> GetCartAsync(string key);
        public Task<Cart?> SetCartAsync(Cart cart);
        public Task<bool?> DeleteCart(string key);
    }
}
