using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Kupci.Queries
{
    public class GetAllKupciQuery : IRequest<IEnumerable<Kupac>>
    {
    }
}
