using ProductStore.Application.DTOs;

namespace ProductStore.Application.Interfaces
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(PaginationQuery paginationQuery);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task AddProductAsync(ProductDto productDto, Stream imageStream, string? imageName);
        Task UpdateProductAsync(ProductDto productDto, Stream imageStream, string? imageName);
        Task DeleteProductAsync(int id);
    }
}
