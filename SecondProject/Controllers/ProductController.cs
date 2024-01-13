using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecondProject.CQRS.Command;
using SecondProject.CQRS.Query;
using SecondProject.CQRS;
using SecondProject.Models;

namespace SecondProject.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly ProductCommandHandler _commandHandler;
        private readonly ProductQueryHandler _queryHandler;

        public ProductController(ProductCommandHandler commandHandler, ProductQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts()
        {
            var products = _queryHandler.Handle(new GetProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _queryHandler.Handle(new GetProductByIdQuery { Id = id });

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody] CreateProductCommand command)
        {
            _commandHandler.Handle(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            _commandHandler.Handle(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _commandHandler.Handle(new DeleteProductCommand { Id = id });
            return NoContent();
        }
    }
}
