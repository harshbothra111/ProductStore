using ProductStore.Application.DTOs;
using ProductStore.Domain.AggregateModels.ProductAggregate;

namespace ProductStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(PaginationQuery paginationQuery);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> AddProductAsync(ProductDto productDto, Stream? imageStream, string? imageName);
        Task<ProductDto> UpdateProductAsync(ProductDto productDto, Stream? imageStream, string? imageName);
        Task DeleteProductAsync(int id);
    }
}
