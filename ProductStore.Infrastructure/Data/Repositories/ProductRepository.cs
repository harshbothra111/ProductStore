using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductStore.Domain.AggregateModels.ProductAggregate;
using ProductStore.Infrastructure.Data.Contexts;
using ProductStore.Infrastructure.Data.Exceptions;

namespace ProductStore.Infrastructure.Data.Repositories
{
    internal sealed class ProductRepository(ProductDbContext context, ILogger<ProductRepository> logger) : IProductRepository
    {
        public async Task<IEnumerable<Product>> GetAllAsync(int subCategoryId, int pageNumber, int pageSize)
        {
            if (subCategoryId == 0)
            {
                return await context.Products.Where(x => x.SubCategoryId == subCategoryId)
                .Include(p => p.Category)
                .Include(p => p.SubCategory)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
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

        public async Task<int> AddAsync(Product product)
        {
            try
            {
                context.Products.Add(product);
                await context.SaveChangesAsync();
                return product.Id;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to add Product");
                throw new DatabaseException("Unable to add Product");
            }
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unable to update Product for Id: {id}", product.Id);
                throw new DatabaseException($"Unable to upate Product for Id: {product.Id}");
            }
        }

        public async Task DeleteAsync(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
