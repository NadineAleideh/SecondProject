using MediatR;
using SecondProject.CQRS.Query.GetById;
using SecondProject.Interfaces;
using SecondProject.Models;

namespace SecondProject.CQRS.Query.GetByName
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, Product>
    {
        private readonly IUnitofWork _unitOfWork;


        public GetProductByNameQueryHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork._Productrepository.GetByNameAsync(request.ProductName);

            return product;
        }

    }
}
