using InventoryManagement.DTOs;
using InventoryManagement.Services;
using InventoryMangement.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Create(ProductCreateDto productDto)
        {
            var result = _productService.RegisterProduct(productDto);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok("Product created successfully.");
        }

        [HttpPut]
        public IActionResult Update(ProductUpdateDto productDto)
        {
            var result = _productService.UpdateProduct(productDto);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok("Product updated successfully.");
        }

        [HttpDelete("{productId}")]
        public IActionResult Delete(int productId)
        {
            var result = _productService.RemoveProduct(productId);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok("Product deleted successfully.");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public IActionResult GetById(int productId)
        {
            try
            {
                var product = _productService.GetProductById(productId);
                return Ok(product);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("add-multiple")]
        public IActionResult RegisterMultipleProducts([FromBody] List<ProductCreateDto> productDtoList)
        {
            var result = _productService.RegisterMultipleProducts(productDtoList);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("by-name/{productName}")]
        public IActionResult GetByName(string productName)
        {
            var product = _productService.GetProductByName(productName);
            return Ok(product);
        }
    }
}
