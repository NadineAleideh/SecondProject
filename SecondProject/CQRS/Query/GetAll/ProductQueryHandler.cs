using MediatR;
using SecondProject.CQRS.Query.GetById;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Models;

namespace SecondProject.CQRS.Query.GetAll
{
    public class ProductQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IUnitofWork _unitOfWork;


        public ProductQueryHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _unitOfWork._Productrepository.GetAllAsync();
            return products;
        }
    }
}
