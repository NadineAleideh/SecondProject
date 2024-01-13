using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Models;
using SecondProject.Models.Dtos;
using SecondProject.strategy;
using SecondProject.UoW;
using System;

namespace SecondProject.Controllers
{
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly IShippingContext _shippingContext;
        private readonly AppDBContext _dbContext;

        public CheckoutController(IShippingContext shippingContext, AppDBContext dbContext)
        {
            _shippingContext = shippingContext;
            _dbContext = dbContext;
        }

        [HttpGet("AllCheckouts")]
        public ActionResult<IEnumerable<Checkout>> GetAllCheckouts()
        {
            var checkouts = _dbContext.Checkouts.Include(c => c.shippingMethod).ToList();
            return Ok(checkouts);
        }

        [HttpGet("Checkout/{id}")]
        public ActionResult<Checkout> GetCheckoutById(int id)
        {
            var checkout = _dbContext.Checkouts.Include(c => c.shippingMethod).FirstOrDefault(c => c.Id == id);

            if (checkout == null)
            {
                return NotFound();
            }

            return Ok(checkout);
        }

        [HttpPost]
        public ActionResult<Checkout> CreateCheckout([FromBody] CheckoutPostDto checkoutDto)
        {
            if (checkoutDto == null || checkoutDto.ShippingMethodId == 0 || checkoutDto.OrderTotal <= 0)
            {
                return BadRequest("Invalid input");
            }

            var shippingMethod = _dbContext.ShippingMethods.FirstOrDefault(m => m.Id == checkoutDto.ShippingMethodId);

            if (shippingMethod == null)
            {
                return BadRequest("Invalid shipping method");
            }

            var newCheckout = new Checkout
            {
                OrderTotal = checkoutDto.OrderTotal,
                ShippingMethodId = checkoutDto.ShippingMethodId,
                shippingMethod = shippingMethod
            };

            _shippingContext.SetStrategy(GetShippingStrategy(shippingMethod));
            newCheckout.FinalTotal = _shippingContext.ExecuteStrategy(newCheckout.OrderTotal);

            _dbContext.Checkouts.Add(newCheckout);
            _dbContext.SaveChanges();

            return Ok(newCheckout);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCheckout(int id)
        {
            var checkout = _dbContext.Checkouts.FirstOrDefault(c => c.Id == id);

            if (checkout == null)
            {
                return NotFound();
            }

            _dbContext.Checkouts.Remove(checkout);
            _dbContext.SaveChanges();

            return NoContent();
        }

        private IShippingStrategy GetShippingStrategy(ShippingMethod shippingMethod)
        {
            switch (shippingMethod.Id)
            {
                case 1:
                    return new FreeShippingStrategy();
                case 2:
                    return new LocalShippingStrategy();
                case 3:
                    return new WorldwideShippingStrategy();
                default:
                    throw new NotSupportedException("Unsupported shipping method");
            }
        }
    }

}
