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

        [HttpGet]
        public ActionResult<IEnumerable<Supplier>> GetAllSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }

        [HttpPost]
        public IActionResult AddSupplier([FromBody] Supplier supplier)
        {
            var result = _supplierService.AddSupplier(supplier);

            if (result.IsSuccess)
                return Ok(supplier);

            return BadRequest(new { message = result.ErrorMessage });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int supplierId)
        {
            try
            {
                _supplierService.DeleteSupplier(supplierId);
                return Ok(new { message = "Supplier deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
