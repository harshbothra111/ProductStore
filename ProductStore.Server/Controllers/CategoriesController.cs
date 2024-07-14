using Microsoft.AspNetCore.Mvc;
using ProductStore.Application.Interfaces;

namespace ProductStore.Server.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await categoryService.GetAllCategoriesAsync());
        }
    }
}
