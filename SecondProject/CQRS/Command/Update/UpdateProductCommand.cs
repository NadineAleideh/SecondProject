using MediatR;
using SecondProject.Models;

namespace SecondProject.CQRS.Command
{
    public class UpdateProductCommand : IRequest<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

    }
}
