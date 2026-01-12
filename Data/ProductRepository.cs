using Microsoft.EntityFrameworkCore;
using skinet.Entities;
using skinet.Interfaces;

namespace skinet.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context=context;
        }
        public void CreateProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
         
        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type,string? sort)
        {
            var query=_context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(brand))
            {
                query=query.Where(x=>x.Brand==brand);
            }
            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(x => x.Type == type);
            }
            query=sort switch
            {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDesc" => query.OrderByDescending(x => x.Price),
                _ => query.OrderBy(x => x.Name)
            };
            return await query.ToListAsync();
        }

        public  bool ProductExists(int id)
        {
            return  _context.Products.Any(p => p.Id == id);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
        }

        public async Task<IReadOnlyList<string>> GetBrandsAsync()
        {
            return await _context.Products.Select(x=>x.Brand).Distinct().ToListAsync();
        }

        public async Task<IReadOnlyList<string>> GetTypesAsync()
        {
            return await _context.Products.Select(x=>x.Type).Distinct().ToListAsync();
        }
    }
}
