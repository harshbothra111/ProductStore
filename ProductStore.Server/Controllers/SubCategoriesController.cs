using Microsoft.AspNetCore.Mvc;
using ProductStore.Application.Interfaces;

namespace ProductStore.Server.Controllers
{
    [Route("api/Categories/{categoryId}/[controller]")]
    public class SubCategoriesController(ISubCategoryService subCategoryService) : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int categoryId)
        {
            return Ok(await subCategoryService.GetSubCategoriesAsync(categoryId));
        }
    }
}
