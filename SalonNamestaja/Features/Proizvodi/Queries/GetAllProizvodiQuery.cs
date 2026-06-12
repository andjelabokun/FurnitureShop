using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Proizvodi.Queries
{
    public class GetAllProizvodiQuery : IRequest<IEnumerable<Proizvod>>
    {
    }
}
