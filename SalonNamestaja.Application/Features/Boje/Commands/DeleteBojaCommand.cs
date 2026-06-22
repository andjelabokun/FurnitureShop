using MediatR;

namespace SalonNamestaja.Application.Features.Boje.Commands
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
