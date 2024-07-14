using ProductStore.Domain.AggregateModels.ProductAggregate;

namespace ProductStore.Infrastructure.Data.Repositories
{
    public interface ISubCategoryRepository
    {
        Task<IReadOnlyCollection<SubCategory>> GetAllAsync(int categoryId);
        Task AddAsync(SubCategory subCategory);
    }
}
