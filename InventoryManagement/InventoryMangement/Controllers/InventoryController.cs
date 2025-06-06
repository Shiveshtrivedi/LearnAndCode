using InventoryManagement.Models;
using InventoryMangement.Services.Interface;
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

        [HttpPut("updateInventory")]
        public ActionResult UpdateInventory([FromQuery] int productId, [FromQuery] int quantity)
        {
            try
            {
                _inventoryService.UpdateInventory(productId, quantity);
                
                return Ok("Inventory updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("fetchAllInvetory")]
        public ActionResult<IEnumerable<Inventory>> GetAllInventories()
        {
            try
            {
                var inventories = _inventoryService.GetAllInventories();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("lowstock")]
        public ActionResult<IEnumerable<Product>> GetLowStockItems()
        {
            try
            {
                var items = _inventoryService.GetLowStockItems();
                if (!items.Any())
                    return Ok("All products have sufficient stock.");
                return Ok(items);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
