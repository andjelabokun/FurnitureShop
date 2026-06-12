using MediatR;

namespace SalonNamestajaAPI.Features.Kupci.Commands
{
    public class DeleteKupacCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteKupacCommand(int id)
        {
            Id = id;
        }
    }
}
