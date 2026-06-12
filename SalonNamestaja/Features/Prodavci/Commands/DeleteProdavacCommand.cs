using MediatR;

namespace SalonNamestajaAPI.Features.Prodavci.Commands
{
    public class DeleteProdavacCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteProdavacCommand(int id)
        {
            Id = id;
        }
    }
}
