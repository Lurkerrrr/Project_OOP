using CarParts.Models;
using CarParts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            return Ok(product);
        }

        
        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProduct = _productService.AddProduct(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isUpdated = _productService.UpdateProduct(id, product);
            if (!isUpdated)
                return NotFound($"Product with ID {id} not found.");

            return NoContent(); 
        }

        
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var isDeleted = _productService.DeleteProduct(id);
            if (!isDeleted)
                return NotFound($"Product with ID {id} not found.");

            return NoContent(); 
        }
    }
}
