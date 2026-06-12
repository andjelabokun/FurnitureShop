using MediatR;

namespace SalonNamestajaAPI.Features.Kategorije.Commands
{
    public class DeleteKategorijaCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteKategorijaCommand(int id)
        {
            Id = id;
        }
    }
}
