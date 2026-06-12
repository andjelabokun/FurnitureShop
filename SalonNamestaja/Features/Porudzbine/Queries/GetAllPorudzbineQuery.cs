using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Porudzbine.Queries
{
    public class GetAllPorudzbineQuery : IRequest<IEnumerable<Porudzbina>>
    {
    }
}
