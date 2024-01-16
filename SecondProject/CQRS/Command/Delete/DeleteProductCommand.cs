using MediatR;
using SecondProject.Models;

namespace SecondProject.CQRS.Command.Delete
{
    public class DeleteProductCommand: IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
