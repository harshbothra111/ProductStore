using ProductStore.Application.DTOs;
using ProductStore.Application.Interfaces;
using ProductStore.Domain.AggregateModels.ProductAggregate;
using ProductStore.Domain.AggregateModels.ProductAggregate.Exceptions;
using ProductStore.Infrastructure.Data.Repositories;

namespace ProductStore.Application.Services
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {
        private const int MaxPageSize = 5;
        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(int subCategoryId, PaginationQuery paginationQuery)
        {
            int pageSize = paginationQuery.PageSize;
            if (pageSize == 0) pageSize = MaxPageSize;
            if (pageSize > 5) pageSize = MaxPageSize;
            paginationQuery.PageSize = pageSize;
            var totalRecords = await productRepository.GetTotalRecordsAsync();
            int totalPages = (int)Math.Ceiling((double)totalRecords / paginationQuery.PageSize);
            if (paginationQuery.PageNumber < 0) paginationQuery.PageNumber = 0;
            if (paginationQuery.PageNumber >= totalPages) paginationQuery.PageNumber = totalPages - 1;
            var products = await productRepository.GetAllAsync(subCategoryId, paginationQuery.PageNumber, paginationQuery.PageSize);
            var productDtos = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                Code = p.Code,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                CategoryId = p.CategoryId,
                SubCategoryId = p.SubCategoryId
            });
            return new PaginatedResult<ProductDto>
            {
                Data = productDtos,
                CurrentPage = paginationQuery.PageNumber,
                PageSize = paginationQuery.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling((double)totalRecords / paginationQuery.PageSize)
            };
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id);
            return product is null
                ? throw new ProductNotFoundException($"Product not found for the given id: {id}")
                : new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = product.Quantity,
                Code = product.Code,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                SubCategoryId = product.SubCategoryId
            };
        }

        public async Task<ProductDto> AddProductAsync(ProductDto productDto, Stream? imageStream, string? imageName)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Quantity = productDto.Quantity,
                Code = productDto.Code,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageUrl = await SaveImageAsync(imageStream, imageName),
                CategoryId = productDto.CategoryId,
                SubCategoryId = productDto.SubCategoryId
            };
            productDto.Id = await productRepository.AddAsync(product);
            return productDto;
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto, Stream? imageStream, string? imageName)
        {
            var product = await productRepository.GetByIdAsync(productDto.Id) ?? throw new ProductNotFoundException($"Product not found for the given id: {productDto.Id}");
            product.Name = productDto.Name;
            product.Quantity = productDto.Quantity;
            product.Code = productDto.Code;
            product.Price = productDto.Price;
            product.Description = productDto.Description;
            string? imageUrl = await SaveImageAsync(imageStream, imageName);
            if (!string.IsNullOrEmpty(imageUrl))
            {
                product.ImageUrl = imageUrl;
            }
            await productRepository.UpdateAsync(product);
            return productDto;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await productRepository.GetByIdAsync(id) ?? throw new ProductNotFoundException($"Product not found for the given id: {id}");
            await productRepository.DeleteAsync(product);
        }

        private static async Task<string?> SaveImageAsync(Stream? imageStream, string? imageName)
        {
            if (imageStream is null || string.IsNullOrEmpty(imageName))
            {
                return null;
            }
            var filePath = Path.Combine("Upload/Images", Guid.NewGuid().ToString() + "_" + imageName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageStream.CopyToAsync(stream);
            }
            return filePath;
        }
    }
}
