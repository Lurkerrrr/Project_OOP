using CarParts.Models;
using CarParts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult GetAllSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }

        [HttpGet("{id}")]
        public IActionResult GetSupplierById(int id)
        {
            try
            {
                var supplier = _supplierService.GetSupplierById(id);
                return Ok(supplier);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddSupplier([FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdSupplier = _supplierService.AddSupplier(supplier);
                return CreatedAtAction(nameof(GetSupplierById), new { id = createdSupplier.Id }, createdSupplier);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSupplier(int id, [FromBody] Supplier supplier)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedSupplier = _supplierService.UpdateSupplier(id, supplier);
                if (updatedSupplier == null)
                    return NotFound($"Supplier with ID {id} not found.");

                return Ok(updatedSupplier);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                var success = _supplierService.DeleteSupplier(id);
                if (!success)
                    return NotFound($"Supplier with ID {id} not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }


    }
}
