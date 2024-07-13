using Microsoft.EntityFrameworkCore;
using ProductStore.Domain.Entities;
using ProductStore.Infrastructure.Data.Contexts;

namespace ProductStore.Infrastructure.Data.Repositories
{
    public class SubCategoryRepository(ProductDbContext context) : ISubCategoryRepository
    {
        public async Task AddAsync(SubCategory category)
        {
            context.SubCategories.Add(category);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SubCategory>> GetAllAsync(int categoryId)
        {
            return await context.SubCategories.Where(x => x.CategoryId == categoryId).ToListAsync();
        }
    }
}
