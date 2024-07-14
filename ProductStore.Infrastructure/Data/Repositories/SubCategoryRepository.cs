using Microsoft.EntityFrameworkCore;
using ProductStore.Domain.AggregateModels.ProductAggregate;
using ProductStore.Infrastructure.Data.Contexts;

namespace ProductStore.Infrastructure.Data.Repositories
{
    internal sealed class SubCategoryRepository(ProductDbContext context) : ISubCategoryRepository
    {
        public async Task AddAsync(SubCategory category)
        {
            context.SubCategories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<SubCategory>> GetAllAsync(int categoryId)
        {
            return await context.SubCategories.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}
