using MediatR;

namespace SalonNamestaja.Application.Features.Proizvodi.Commands
{
    public class DeleteProizvodCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteProizvodCommand(int id)
        {
            Id = id;
        }
    }
}
