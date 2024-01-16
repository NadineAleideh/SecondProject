using MediatR;
using SecondProject.Models;

namespace SecondProject.CQRS.Command
{
    
    public class CreateProductCommand: IRequest<Product>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        
    }
}
