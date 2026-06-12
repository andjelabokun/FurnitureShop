using MediatR;

namespace SalonNamestajaAPI.Features.Proizvodjaci.Commands
{
    public class DeleteProizvodjacCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteProizvodjacCommand(int id)
        {
            Id = id;
        }
    }
}
