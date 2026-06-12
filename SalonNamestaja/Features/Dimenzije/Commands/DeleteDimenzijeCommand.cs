using MediatR;

namespace SalonNamestajaAPI.Features.Dimenzije.Commands
{
    public class DeleteDimenzijeCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteDimenzijeCommand(int id)
        {
            Id = id;
        }
    }
}