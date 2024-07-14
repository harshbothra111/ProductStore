using ProductStore.Domain.AggregateModels.ProductAggregate;

namespace ProductStore.Infrastructure.Data.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task AddAsync(Category category);
    }
}
