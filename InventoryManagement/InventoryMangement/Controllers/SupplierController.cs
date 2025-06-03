using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("getAllSupplier")]
        public ActionResult<IEnumerable<Supplier>> GetAllSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }

        [HttpPost("addSupplier")]
        public IActionResult AddSupplier([FromBody] Supplier supplier)
        {
            try
            {
                _supplierService.AddSupplier(supplier);

                return Ok(supplier);

            }
            catch (DuplicateSupplierException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }


        }

        [HttpDelete("{supplierId}/deleteSupplier")]
        public IActionResult DeleteSupplier(int supplierId)
        {
            try
            {
                _supplierService.DeleteSupplier(supplierId);
                return Ok(new { message = "Supplier deleted successfully." });
            }
            catch (SupplierNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
