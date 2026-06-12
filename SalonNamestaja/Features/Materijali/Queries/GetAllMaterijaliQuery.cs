using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestajaAPI.Features.Materijali.Queries
{
    public class GetAllMaterijaliQuery : IRequest<IEnumerable<Materijal>>
    {
    }
}
