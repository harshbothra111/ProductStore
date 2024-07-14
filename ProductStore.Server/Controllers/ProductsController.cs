using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Application.DTOs;
using ProductStore.Application.Interfaces;
using System.Text.Json;

namespace ProductStore.Server.Controllers
{
    public class ProductsController(IProductService productService, IValidator<ProductDto> validator) : BaseApiController
    {
        private readonly IProductService _productService = productService;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationQuery paginationQuery)
        {
            var products = await _productService.GetAllProductsAsync(paginationQuery);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddProduct(string product,IFormFile? file)
        {
            ProductDto productDto = JsonSerializer.Deserialize<ProductDto>(product, _jsonSerializerOptions)!;

            ValidationResult result = await validator.ValidateAsync(productDto);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToArray();
                return BadRequest(errors);
            }

            if (file != null && file.Length > 1 * 1024 * 1024) // 1MB size limit
            {
                return BadRequest("Image size should not exceed 1MB.");
            }

            await _productService.AddProductAsync(productDto, file!.OpenReadStream(), file.FileName);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductDto productDto, IFormFile? file)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            if (file != null && file.Length > 1 * 1024 * 1024) // 1MB size limit
            {
                return BadRequest("Image size should not exceed 1MB.");
            }

            ValidationResult result = await validator.ValidateAsync(productDto);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }).ToArray();
                return BadRequest(errors);
            }

            await _productService.UpdateProductAsync(productDto, file!.OpenReadStream(), file.FileName);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
