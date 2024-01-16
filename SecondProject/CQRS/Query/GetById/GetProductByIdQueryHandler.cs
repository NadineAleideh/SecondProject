using MediatR;
using Microsoft.EntityFrameworkCore;
using SecondProject.Interfaces;
using SecondProject.Models;

namespace SecondProject.CQRS.Query.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IUnitofWork _unitOfWork;


        public GetProductByIdQueryHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork._Productrepository.GetAsync(request.Id);

            return product;
        }

    }
}
