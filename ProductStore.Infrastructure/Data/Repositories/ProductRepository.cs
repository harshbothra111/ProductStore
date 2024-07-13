using Microsoft.EntityFrameworkCore;
using ProductStore.Domain.Entities;
using ProductStore.Infrastructure.Data.Contexts;

namespace ProductStore.Infrastructure.Data.Repositories
{
    public class ProductRepository(ProductDbContext context) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalRecordsAsync()
        {
            return await context.Products.CountAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
