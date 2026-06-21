using MediatR;
using SalonNamestaja.Domain;

namespace SalonNamestaja.Application.Features.Materijali.Queries
{
    public class GetAllMaterijaliQuery : IRequest<IEnumerable<Materijal>>
    {
    }
}
