using ProductStore.Application.Interfaces;
using ProductStore.Domain.AggregateModels.ProductAggregate;
using ProductStore.Infrastructure.Data.Repositories;

namespace ProductStore.Application.Services
{
    public class SubCategoryService(ISubCategoryRepository subCategoryRepository) : ISubCategoryService
    {
        public async Task<IReadOnlyCollection<SubCategory>> GetSubCategoriesAsync(int categoryId)
        {
            return await subCategoryRepository.GetAllAsync(categoryId);
        }
    }
}
