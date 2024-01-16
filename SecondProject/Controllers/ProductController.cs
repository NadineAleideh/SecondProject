using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondProject.CQRS.Command;
using SecondProject.CQRS.Query;
using SecondProject.Models;
using MediatR;
using SecondProject.CQRS.Query.GetAll;
using SecondProject.CQRS.Command.Delete;
using SecondProject.CQRS.Query.GetById;
using SecondProject.CQRS.Query.GetByName;

namespace SecondProject.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {

        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var query = new GetProductsQuery();
            var products = await _mediator.Send(query);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery { Id = id };
            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("byName/{name}")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var query = new GetProductByNameQuery { ProductName = name };
            var product = await _mediator.Send(query);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateProduct([FromBody] CreateProductCommand command)
        {
            var productId = await _mediator.Send(command);
            return Ok(productId);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            var product = await _mediator.Send(command);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }


    }
}
