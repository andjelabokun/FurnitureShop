using MediatR;

namespace SalonNamestajaAPI.Features.Boje.Commands
{
    public class DeleteBojaCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteBojaCommand(int id)
        {
            Id = id;
        }
    }
}
