using MediatR;

namespace SalonNamestajaAPI.Features.Proizvodi.Commands
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
