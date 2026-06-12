using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Proizvodjaci.Queries
{
    public class GetAllProizvodjaciQuery : IRequest<IEnumerable<Proizvodjac>>
    {
    }
}
