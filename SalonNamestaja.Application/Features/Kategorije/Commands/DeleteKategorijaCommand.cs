using MediatR;

namespace SalonNamestaja.Application.Features.Kategorije.Commands
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
