using MediatR;
using SecondProject.Interfaces;

namespace SecondProject.CQRS.Command.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IUnitofWork _unitOfWork;

        public DeleteProductCommandHandler(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _unitOfWork._Productrepository.GetAsync(request.Id);

            if (existingProduct != null)
            {
                _unitOfWork._Productrepository.DeleteEntity(existingProduct);
                await _unitOfWork.SaveChangesAsync();
            }

            return Unit.Value;
        }

    }
}
