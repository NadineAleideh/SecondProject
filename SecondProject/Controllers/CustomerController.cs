using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondProject.Interfaces;
using SecondProject.Models;

namespace SecondProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        public CustomerController(IUnitofWork unitofWork)
        {
            this._unitofWork = unitofWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Customers = await _unitofWork.Customerrepository.GetAllAsync();
            return Ok(Customers);
        }

        [HttpGet("Getbycode/{id}")]
        public async Task<IActionResult> Getbycode(int id)
        {
            var customer = await _unitofWork.Customerrepository.GetAsync(id);

            if (customer is null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Customer customer)
        {
            var customerToAdd = _unitofWork.Customerrepository.AddEntity(customer);
            await _unitofWork.SaveChangesAsync();
            //return Ok(customerToAdd);
            return CreatedAtAction("Getbycode", new { id = customer.Id }, customer);

        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(Customer customer, int id)
        {
            if (customer.Id != id)
            {
                return BadRequest();
            }

            var customerToUpdate = await _unitofWork.Customerrepository.UpdateEntity(customer, id);

            await _unitofWork.SaveChangesAsync();

            return Ok(customerToUpdate);
        }


        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _unitofWork.Customerrepository.DeleteEntity(id);

            await _unitofWork.SaveChangesAsync();
            return NoContent();
        }

    }
}
