using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Kategorije.Queries
{
    public class GetKategorijaByIdQuery : IRequest<Kategorija?>
    {
        public int Id { get; set; }

        public GetKategorijaByIdQuery(int id)
        {
            Id = id;
        }
    }
}
