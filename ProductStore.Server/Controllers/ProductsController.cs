using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ProductStore.Application.DTOs;
using ProductStore.Application.Interfaces;
using ProductStore.Domain.AggregateModels.ProductAggregate.Exceptions;
using ProductStore.Server.Contracts;
using System.Text.Json;

namespace ProductStore.Server.Controllers
{
    public class ProductsController(IProductService productService, IValidator<ProductDto> validator, IOptions<JsonOptions> jsonOptions) : BaseApiController
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int categoryId, [FromQuery]int subCategoryId,[FromQuery] PaginationQuery paginationQuery)
        {
            var products = await _productService.GetAllProductsAsync(categoryId, subCategoryId, paginationQuery);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (ProductNotFoundException ex)
            {
                Logger.LogError(ex, "Exception Occured: {message}",ex.Message);
                return NotFound();
            }
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProduct(ProductRequest productRequest)
        {
            ProductDto productDto = JsonSerializer.Deserialize<ProductDto>(productRequest.Product, jsonOptions.Value.JsonSerializerOptions)!;
            var file = productRequest.File;
            ValidationResult result = await validator.ValidateAsync(productDto);

            if (!result.IsValid)
            {
                return BuildBadRequestResponse(result);
            }

            if (file != null && file.Length > 1 * 1024 * 1024) // 1MB size limit
            {
                return BadRequest("Image size should not exceed 1MB.");
            }

            await _productService.AddProductAsync(productDto, file?.OpenReadStream(), file?.FileName);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductRequest productRequest)
        {
            try
            {
                ProductDto productDto = JsonSerializer.Deserialize<ProductDto>(productRequest.Product, jsonOptions.Value.JsonSerializerOptions)!;
                if (id != productDto.Id)
                {
                    return BadRequest("Product ID mismatch");
                }
                ValidationResult result = await validator.ValidateAsync(productDto);

                if (!result.IsValid)
                {
                    return BuildBadRequestResponse(result);
                }

                var file = productRequest.File;

                if (file != null && file.Length > 1 * 1024 * 1024) // 1MB size limit
                {
                    return BadRequest("Image size should not exceed 1MB.");
                }

                await _productService.UpdateProductAsync(productDto, file?.OpenReadStream(), file?.FileName);
                return NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                Logger.LogError(ex, "Exception Occured: {message}", ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
