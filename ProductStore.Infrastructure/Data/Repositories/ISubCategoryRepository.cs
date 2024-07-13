using ProductStore.Domain.Entities;

namespace ProductStore.Infrastructure.Data.Repositories
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetAllAsync(int categoryId);
        Task AddAsync(SubCategory subCategory);
    }
}
