using ProductStore.Domain.AggregateModels.ProductAggregate;

namespace ProductStore.Application.Interfaces
{
    public interface ISubCategoryService
    {
        Task<IReadOnlyCollection<SubCategory>> GetSubCategoriesAsync(int categoryId);
    }
}
