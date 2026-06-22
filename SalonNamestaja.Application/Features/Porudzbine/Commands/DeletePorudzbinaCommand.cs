using MediatR;

namespace SalonNamestaja.Application.Features.Porudzbine.Commands
{
    public class DeletePorudzbinaCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeletePorudzbinaCommand(int id)
        {
            Id = id;
        }
    }
}
