using Microsoft.EntityFrameworkCore;
using ProductStore.Domain.AggregateModels.ProductAggregate;
using ProductStore.Infrastructure.Data.Contexts;

namespace ProductStore.Infrastructure.Data.Repositories
{
    internal sealed class CategoryRepository(ProductDbContext context) : ICategoryRepository
    {
        public async Task AddAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
