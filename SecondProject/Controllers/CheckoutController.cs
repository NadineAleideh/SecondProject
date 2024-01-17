using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [ApiController]
    [Route("api/checkout")]
    public class CheckoutController : ControllerBase
    {
        private readonly IShippingContext _shippingContext;
        private readonly IUnitofWork _unitOfWork;

        public CheckoutController(IUnitofWork unitOfWork, IShippingContext shippingContext)
        {
            _shippingContext = shippingContext;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Checkout>>> GetCheckouts()
        {
            var checkouts = await _unitOfWork._Checkoutrepository.GetAllWithShippingMethodAsync();
            return Ok(checkouts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Checkout>> GetCheckoutById(int id)
        {
            var checkout = await _unitOfWork._Checkoutrepository.GetWithShippingMethodAsync(id);

            if (checkout == null)
            {
                return NotFound();
            }

            return Ok(checkout);
        }

        [HttpPost]
        public async Task<ActionResult<Checkout>> CreateCheckout([FromBody] CheckoutPostDto checkoutDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newCheckout = new Checkout
            {
                OrderTotal = checkoutDto.OrderTotal,
                ShippingMethodId = checkoutDto.ShippingMethodId
            };

            // Fetch the selected shipping method using the repository
            var shippingMethod = await _unitOfWork._Checkoutrepository
                .GetShippingMethodByIdAsync(checkoutDto.ShippingMethodId);

            newCheckout.shippingMethod = shippingMethod;

            // Use the selected shipping strategy to calculate the final total
            _shippingContext.SetStrategy(GetShippingStrategy(shippingMethod.Id));
            newCheckout.FinalTotal = _shippingContext.ExecuteStrategy(newCheckout.OrderTotal);


            _unitOfWork._Checkoutrepository.AddEntity(newCheckout);
            await _unitOfWork.SaveChangesAsync();

            return Ok(newCheckout);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCheckout(int id)
        {
            var checkout = await _unitOfWork._Checkoutrepository.GetWithShippingMethodAsync(id);

            if (checkout == null)
            {
                return NotFound();
            }

            _unitOfWork._Checkoutrepository.DeleteEntity(id);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }


        private IShippingStrategy GetShippingStrategy(int shippingMethodId)
        {
            switch (shippingMethodId)
            {
                case 1:
                    return new FreeShippingStrategy();
                case 2:
                    return new LocalShippingStrategy();
                case 3:
                    return new WorldwideShippingStrategy();
                default:
                    throw new NotImplementedException("Shipping strategy not implemented for the selected method.");
            }
        }
    }


}
