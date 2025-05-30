using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        public ActionResult<Category> GetCategoryById(int categoryId)
        {
            try
            {
                var category = _categoryService.GetCategoryById(categoryId);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddCategory([FromBody] Category category)
        {
            var result = _categoryService.AddCategory(category);

            if (result.IsSuccess)
                return Ok(category);

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int categoryId, [FromBody] Category updatedCategory)
        {
            try
            {
                updatedCategory.CategoryId = categoryId;
                _categoryService.UpdateCategory(updatedCategory);
                return Ok(new { message = "Category updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                _categoryService.DeleteCategory(categoryId);
                return Ok(new { message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
