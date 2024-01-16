using MediatR;
using SecondProject.Models;

namespace SecondProject.CQRS.Query.GetAll
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
