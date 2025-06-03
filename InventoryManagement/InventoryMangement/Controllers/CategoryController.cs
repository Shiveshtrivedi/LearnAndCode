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

        [HttpGet("fetchAllCategory")]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var categories = _categoryService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("{categoryId}/fetchCategoryById")]
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

        [HttpPost("addCategory")]
        public IActionResult AddCategory([FromBody] Category category)
        {
            try
            {
                _categoryService.AddCategory(category);
                return Ok(new { message = "Category added successfully", category });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/updateCategory")]
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

        [HttpDelete("{id}/deleteCategory")]
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
