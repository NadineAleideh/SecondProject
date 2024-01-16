using MediatR;
using SecondProject.Interfaces;
using SecondProject.Models;

namespace SecondProject.CQRS.Command.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IUnitofWork _unitOfWork;

        public UpdateProductCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _unitOfWork._Productrepository.GetAsync(request.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = request.Name;
                existingProduct.Price = request.Price;
            }


            await _unitOfWork.SaveChangesAsync();

            return existingProduct;
        }


    }

}
