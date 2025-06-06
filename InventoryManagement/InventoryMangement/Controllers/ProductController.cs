using InventoryManagement.DTOs;
using InventoryManagement.Exceptions;
using InventoryMangement.DTOs;
using InventoryMangement.Services.Interface;
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

        [HttpPost("addProduct")]
        public IActionResult Create(ProductCreateDto productDto)
        {
            try
            {
                string message = _productService.RegisterProduct(productDto);

                return Ok($"Product added successfully {message}");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updateProduct")]
        public IActionResult Update(ProductUpdateDto productDto)
        {
            var result = _productService.UpdateProduct(productDto);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok("Product updated successfully.");
        }

        [HttpDelete("{productId}/deleteProduct")]
        public IActionResult Delete(int productId)
        {
            try
            {
                _productService.RemoveProduct(productId);

                return Ok("Product deleted successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpGet("fetchAllProduct")]
        public IActionResult GetAll()
        {
            try
            {
                var products = _productService.GetAllProducts();

                return Ok(products);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{productId}/fetchProductById")]
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

        [HttpPost("addMultipleProduct")]
        public IActionResult RegisterMultipleProducts([FromBody] List<ProductCreateDto> productDtoList)
        {
            try
            {
                _productService.RegisterMultipleProducts(productDtoList);
                return Ok("All products registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to register multiple products: {ex.Message}");
            }
        }

        [HttpGet("{productName}/getProductByName")]
        public IActionResult GetByName(string productName)
        {
            try
            {
                var product = _productService.GetProductByName(productName);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
