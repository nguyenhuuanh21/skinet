using skinet.Entities;
using System.Text.Json;

namespace skinet.Data
{
    public class StoreContextSeed(StoreContext context)
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.Products.Any())
            {
                var productsData= await File.ReadAllTextAsync("./Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products == null) return;
                context.Products.AddRange(products);
                await context.SaveChangesAsync();
            }
        }
    }
}
