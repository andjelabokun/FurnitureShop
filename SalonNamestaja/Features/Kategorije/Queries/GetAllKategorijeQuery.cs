using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Kategorije.Queries
{
    public class GetAllKategorijeQuery : IRequest<IEnumerable<Kategorija>>
    {
    }
}
