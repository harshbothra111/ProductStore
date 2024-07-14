using ProductStore.Domain.AggregateModels.ProductAggregate;

namespace ProductStore.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IReadOnlyCollection<Category>> GetAllCategoriesAsync();
    }
}
