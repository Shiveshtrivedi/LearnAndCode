using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpPost("register")]
        public ActionResult RegisterInventory([FromBody] Product product)
        {
            try
            {
                var result = _inventoryService.RegisterInventory(product);
                if (result.IsSuccess)
                    return Ok("Inventory registered successfully.");
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public ActionResult UpdateInventory([FromQuery] int productId, [FromQuery] int quantity)
        {
            try
            {
                var result = _inventoryService.UpdateInventory(productId, quantity);
                if (result.IsSuccess)
                    return Ok("Inventory updated successfully.");
                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Inventory>> GetAllInventories()
        {
            return Ok(_inventoryService.GetAllInventories());
        }

        [HttpGet("lowstock")]
        public ActionResult<IEnumerable<Product>> GetLowStockItems()
        {
            var items = _inventoryService.GetLowStockItems();
            if (!items.Any())
                return Ok("All products have sufficient stock.");
            return Ok(items);
        }
    }
}
