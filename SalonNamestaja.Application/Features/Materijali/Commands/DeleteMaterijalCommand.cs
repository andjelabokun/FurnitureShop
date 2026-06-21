using MediatR;

namespace SalonNamestaja.Application.Features.Materijali.Commands
{
    public class DeleteMaterijalCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteMaterijalCommand(int id)
        {
            Id = id;
        }
    }
}
