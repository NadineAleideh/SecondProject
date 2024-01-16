using MediatR;
using SecondProject.CQRS.Command.Delete;
using SecondProject.CQRS.Command.Update;
using SecondProject.Data;
using SecondProject.Interfaces;
using SecondProject.Models;

namespace SecondProject.CQRS.Command.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IUnitofWork _unitOfWork;


        public CreateProductCommandHandler(IUnitofWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }


        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Price = request.Price
            };

            _unitOfWork._Productrepository.AddEntity(newProduct);
            await _unitOfWork.SaveChangesAsync();

            return newProduct;
        }


    }
}
