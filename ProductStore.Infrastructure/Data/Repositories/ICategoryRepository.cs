using ProductStore.Domain.Entities;

namespace ProductStore.Infrastructure.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
    }
}
