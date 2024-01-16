using MediatR;
using SecondProject.Models;

namespace SecondProject.CQRS.Query.GetByName
{
    public class GetProductByNameQuery : IRequest<Product>
    {
        public string ProductName { get; set; }
    }
}
