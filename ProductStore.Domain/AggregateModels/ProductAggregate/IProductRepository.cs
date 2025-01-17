﻿using ProductStore.Domain;
using ProductStore.Domain.AggregateModels.ProductAggregate;

namespace ProductStore.Infrastructure.Data.Repositories
{
    public interface IProductRepository
    {
        Task<PaginatedResult<Product>> GetAllAsync(int categoryId, int subCategoryId, int pageNumber, int pageSize);
        Task<int> GetTotalRecordsAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<int> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
    }
}
