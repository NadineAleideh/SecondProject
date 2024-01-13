using SecondProject.CQRS.Command;
using SecondProject.Data;
using SecondProject.Models;

namespace SecondProject.CQRS
{
    public class ProductCommandHandler
    {
        private readonly AppDBContext _dbContext;

        public ProductCommandHandler(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle(CreateProductCommand command)
        {
            var newProduct = new Product
            {
                Name = command.Name,
                Price = command.Price

            };

            _dbContext.Products.Add(newProduct);
            _dbContext.SaveChanges();
        }

        public void Handle(UpdateProductCommand command)
        {
            var existingProduct = _dbContext.Products.Find(command.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = command.Name;
                existingProduct.Price = command.Price;

                _dbContext.SaveChanges();
            }
        }

        public void Handle(DeleteProductCommand command)
        {
            var productToDelete = _dbContext.Products.Find(command.Id);

            if (productToDelete != null)
            {
                _dbContext.Products.Remove(productToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
