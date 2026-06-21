using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.Kategorije.Queries
{
    public class GetKategorijaSaPodkategorijamaQuery : IRequest<Kategorija?>
    {
        public int Id { get; set; }

        public GetKategorijaSaPodkategorijamaQuery(int id)
        {
            Id = id;
        }
    }
}
