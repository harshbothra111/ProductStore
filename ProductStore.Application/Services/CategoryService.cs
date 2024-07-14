using ProductStore.Application.Interfaces;
using ProductStore.Domain.AggregateModels.ProductAggregate;
using ProductStore.Infrastructure.Data.Repositories;

namespace ProductStore.Application.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        public async Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync();
        }
    }
}
