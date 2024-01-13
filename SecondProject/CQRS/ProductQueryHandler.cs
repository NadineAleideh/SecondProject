using SecondProject.CQRS.Query;
using SecondProject.Data;
using SecondProject.Models;

namespace SecondProject.CQRS
{
    public class ProductQueryHandler
    {
        private readonly AppDBContext _dbContext;

        public ProductQueryHandler(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> Handle(GetProductsQuery query)
        {
            return _dbContext.Products.ToList();
        }

        public Product Handle(GetProductByIdQuery query)
        {
            return _dbContext.Products.Find(query.Id);
        }
    }
}
