using MediatR;
using SecondProject.Models;

namespace SecondProject.CQRS.Query.GetById
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }
}
