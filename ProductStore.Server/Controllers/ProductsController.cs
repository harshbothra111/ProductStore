using Microsoft.AspNetCore.Mvc;
using ProductStore.Application.DTOs;
using ProductStore.Application.Interfaces;

namespace ProductStore.Server.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

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
        public async Task<IActionResult> AddProduct([FromForm] ProductDto productDto, [FromForm] IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 1 * 1024 * 1024) // 1MB size limit
            {
                return BadRequest("Image size should not exceed 1MB.");
            }

            await _productService.AddProductAsync(productDto, imageFile!.OpenReadStream(), imageFile.FileName);
            return CreatedAtAction(nameof(GetProductById), new { id = productDto.Id }, productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto, [FromForm] IFormFile imageFile)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Product ID mismatch");
            }

            if (imageFile != null && imageFile.Length > 1 * 1024 * 1024) // 1MB size limit
            {
                return BadRequest("Image size should not exceed 1MB.");
            }

            await _productService.UpdateProductAsync(productDto, imageFile!.OpenReadStream(), imageFile.FileName);
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
